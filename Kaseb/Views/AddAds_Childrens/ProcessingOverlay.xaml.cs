using KasebCore.Models.Element;
using System.ComponentModel;

namespace Kaseb.Views.AddAds_Childrens
{
    public partial class ProcessingOverlay : Grid, INotifyPropertyChanged
    {
        public ProcessingOverlay()
        {
            InitializeComponent();
        }

        ProcessingOverlayModel Model = new ProcessingOverlayModel();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Show(string Message)
        {
            MessageLabel.IsVisible = true;
            StatementLabel.IsVisible = true;
            this.Statement = "وضعیت:";
            this.Message = Message;
            MessageSubtitleLabel.IsVisible = false;
            this.MessageSubtitle = string.Empty;
        }
        public void Show(string Message, bool StatetmentShow)
        {
            if (StatetmentShow)
            {

                StatementLabel.IsVisible = true;
                this.Statement = "وضعیت:";
            }
            else
            {

                this.Statement = string.Empty;
                StatementLabel.IsVisible = false;
            }

            MessageLabel.IsVisible = true;
            MessageSubtitleLabel.IsVisible = false;
            this.Message = Message;
            this.MessageSubtitle = string.Empty;
        }
        public void Show(string Message, string MessageSubtitle)
        {
            MessageSubtitleLabel.IsVisible = true;
            MessageLabel.IsVisible = true;
            StatementLabel.IsVisible = true;
            this.Statement = "وضعیت:";
            this.Message = Message;
            this.MessageSubtitle = MessageSubtitle;
        }
        public void Show(string Message, string MessageSubtitle, bool StatetmentShow)
        {
            if (StatetmentShow)
            {

                StatementLabel.IsVisible = true;
                this.Statement = "وضعیت:";
            }
            else
            {

                this.Statement = string.Empty;
                StatementLabel.IsVisible = false;
            }
            MessageSubtitleLabel.IsVisible = true;
            MessageLabel.IsVisible = true;
            this.Message = Message;
            this.MessageSubtitle = MessageSubtitle;
        }
        public void Show(string Message, string MessageSubtitle, string Statement)
        {
            StatementLabel.IsVisible = true;
            MessageSubtitleLabel.IsVisible = true;
            MessageLabel.IsVisible = true;
            this.Statement = Statement;
            this.Message = Message;
            this.MessageSubtitle = MessageSubtitle;
        }

        public string Statement
        {
            get
            {
                StatementLabel.Text = Model.Statement;
                return Model.Statement;
            }
            set
            {
                StatementLabel.Text = value;
                Model.Statement = value;
                OnPropertyChanged(nameof(Statement));
                OnPropertyChanged(nameof(Model));
            }
        }
        public string Message
        {
            get
            {
                MessageLabel.Text = Model.Message;
                return Model.Message;
            }
            set
            {
                MessageLabel.Text = value;
                Model.Message = value;
                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(Model));
            }
        }
        public string MessageSubtitle
        {
            get
            {
                MessageSubtitleLabel.Text = Model.MessageSubtitle;
                return Model.MessageSubtitle;
            }
            set
            {
                MessageSubtitleLabel.Text = value;
                Model.MessageSubtitle = value;
                OnPropertyChanged(nameof(MessageSubtitle));
                OnPropertyChanged(nameof(Model));
            }
        }
        public string Icon
        {
            get
            {
                return Model.Icon;
            }
            set
            {
                Model.Icon = value;
                OnPropertyChanged(nameof(Icon));
                OnPropertyChanged(nameof(Model));
            }
        }

    }

    public class ProcessingOverlayModel()
    {

        public string Statement { get; set; } = "وضعیت:";
        public string Message { get; set; } = string.Empty;
        public string MessageSubtitle { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}