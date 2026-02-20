
<?php
/**
 * Plugin Name: Kaseb Ads API (Public Users)
 * Description: پلاگین ساخت آگهی با REST API — هر کاربر لاگین شده میتونه آگهی اضافه کنه.
 * Version: 1.5
 * Author: Moradstudio
 */

if ( ! defined( 'ABSPATH' ) ) { exit; }

// Register CPT 'ad'
function kaseb_register_ad_post_type() {
    $labels = array(
        'name' => 'آگهی‌ها',
        'singular_name' => 'آگهی',
        'menu_name' => 'آگهی‌ها'
    );
    $args = array(
        'labels' => $labels,
        'public' => true,
        'has_archive' => true,
        'supports' => array('title','editor','author','thumbnail','custom-fields'),
        'show_in_rest' => true,
    );
    register_post_type('ad', $args);
}
add_action('init', 'kaseb_register_ad_post_type');

// Add CORS headers
add_filter('rest_pre_serve_request', function($value) {
    header('Access-Control-Allow-Origin: *');
    header('Access-Control-Allow-Methods: GET, POST, OPTIONS');
    header('Access-Control-Allow-Credentials: true');
    header('Access-Control-Allow-Headers: Authorization, Content-Type, X-WP-Nonce');
    return $value;
});

// REST routes
add_action('rest_api_init', function() {
    register_rest_route('kaseb/v1','/create-ad', array(
        'methods'=>'POST',
        'callback'=>'kaseb_create_ad',
        'permission_callback'=> function($request){
            return is_user_logged_in(); // اجازه به همه کاربران لاگین شده
        },
    ));

    register_rest_route('kaseb/v1','/ads', array(
        'methods'=>'GET',
        'callback'=>'kaseb_get_ads',
        'permission_callback'=>'__return_true',
    ));
});

function kaseb_create_ad(WP_REST_Request $request) {
    $params = $request->get_json_params();
    if (empty($params)) {
        $params = $request->get_params();
    }

    $title   = isset($params['title']) ? sanitize_text_field($params['title']) : '';
    $content = isset($params['content']) ? sanitize_textarea_field($params['content']) : '';
    $price   = isset($params['price']) ? sanitize_text_field($params['price']) : '';
    $city    = isset($params['city']) ? sanitize_text_field($params['city']) : '';
    $phone   = isset($params['phone']) ? sanitize_text_field($params['phone']) : '';
    $image   = isset($params['image']) ? esc_url_raw($params['image']) : '';

    // فیلدهای اضافه
    $weigh = isset($params['ValueOfWeighKG']) ? sanitize_text_field($params['ValueOfWeighKG']) : '';
    $tag1  = isset($params['ValueOfTag1']) ? sanitize_text_field($params['ValueOfTag1']) : '';
    $tag2  = isset($params['ValueOfTag2']) ? sanitize_text_field($params['ValueOfTag2']) : '';

    // تبدیل IsUrgent به بولین واقعی
    $urgent = isset($params['IsUrgent']) ? filter_var($params['IsUrgent'], FILTER_VALIDATE_BOOLEAN) : false;

    if (empty($title) || empty($content)) {
        return new WP_Error('missing_fields','عنوان و متن آگهی الزامی است', array('status'=>400));
    }

    $author = get_current_user_id();
    if (empty($author)) { $author = 1; }

    $postarr = array(
        'post_title'=>$title,
        'post_content'=>$content,
        'post_status'=>'publish',
        'post_type'=>'ad',
        'post_author'=>$author
    );

    $post_id = wp_insert_post($postarr, true);
    if (is_wp_error($post_id)) {
        return new WP_Error('db_error','ثبت آگهی شکست خورد', array('status'=>500));
    }

    // ذخیره متاها
    if (!empty($price)) update_post_meta($post_id, 'price', $price);
    if (!empty($city)) update_post_meta($post_id, 'city', $city);
    if (!empty($phone)) update_post_meta($post_id, 'phone', $phone);
    if (!empty($weigh)) update_post_meta($post_id, 'ValueOfWeighKG', $weigh);
    if (!empty($tag1)) update_post_meta($post_id, 'ValueOfTag1', $tag1);
    if (!empty($tag2)) update_post_meta($post_id, 'ValueOfTag2', $tag2);
    update_post_meta($post_id, 'IsUrgent', $urgent ? 1 : 0);

    // ذخیره تصویر شاخص
    if (!empty($image)) {
        require_once( ABSPATH . 'wp-admin/includes/image.php' );
        require_once( ABSPATH . 'wp-admin/includes/file.php' );
        require_once( ABSPATH . 'wp-admin/includes/media.php' );
        $attach_id = media_sideload_image($image, $post_id, null, 'id');
        if (!is_wp_error($attach_id)) {
            set_post_thumbnail($post_id, $attach_id);
        }
    }

    return rest_ensure_response(array(
        'status'=>'success',
        'id'=>$post_id,
        'title'=>$title,
        'content'=>$content,
        'price'=>$price,
        'city'=>$city,
        'phone'=>$phone,
        'ValueOfWeighKG'=>$weigh,
        'ValueOfTag1'=>$tag1,
        'ValueOfTag2'=>$tag2,
        'IsUrgent'=>$urgent,
    ));
}

function kaseb_get_ads(WP_REST_Request $request) {
    $per_page = (int)$request->get_param('per_page') ?: 10;
    $after_id = (int)$request->get_param('after_id'); // برای cursor-based

    $args = array(
        'post_type'=>'ad',
        'post_status'=>'publish',
        'posts_per_page'=>$per_page,
        'orderby'=>'ID',
        'order'=>'DESC',
    );

    if ($after_id > 0) {
        // ادامه لیست از بعد از این ID
        $args['date_query'] = array(
            array(
                'column' => 'post_date',
                'before' => get_post_field('post_date', $after_id)
            )
        );
    }

    $q = new WP_Query($args);
    $data = array();

    if ($q->have_posts()) {
        foreach ($q->posts as $ad) {
            $thumb_id = get_post_thumbnail_id($ad->ID);
            $thumb = $thumb_id ? wp_get_attachment_url($thumb_id) : '';

            $data[] = array(
                'id' => $ad->ID,
                'title' => $ad->post_title,
                'content' => wp_trim_words($ad->post_content, 40, '...'),
                'price' => get_post_meta($ad->ID, 'price', true),
                'city' => get_post_meta($ad->ID, 'city', true),
                'phone' => get_post_meta($ad->ID, 'phone', true),
                'ValueOfWeighKG'=> get_post_meta($ad->ID, 'ValueOfWeighKG', true),
                'ValueOfTag1' => get_post_meta($ad->ID, 'ValueOfTag1', true),
                'ValueOfTag2' => get_post_meta($ad->ID, 'ValueOfTag2', true),
                'IsUrgent' => get_post_meta($ad->ID, 'IsUrgent', true) == 1 ? true : false,
                'image' => $thumb,
                'author'=> get_the_author_meta('display_name', $ad->post_author),
                'date' => $ad->post_date,
            );
        }
    }

    return rest_ensure_response($data);
}