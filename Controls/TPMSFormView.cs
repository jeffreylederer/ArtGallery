using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ArtGallery
{
    /// <summary>
    /// Summary description for TPMSFormView
    /// </summary>
    [ToolboxData( "<{0}:TpmsFormView runat=\"server\" InsertOrUpdateCheckField=\"\" ></{0}:TpmsFormView>" )]
    public class TPMSFormView : FormView
    {
        #region Private Fields
        private string insertOrUpdateCheckField;
        private Label errorLabel;
        #endregion

        #region Properties
        #region PROPERTY:: InsertOrUpdateCheckField
        [Category( "Behavior" )]
        [Description( "Name of the field in the business object that will be used to set the form view in edit or insert mode" )]
        [DefaultValue( "None" )]
        [Bindable( false )]
        [Browsable( true )]
        public string InsertOrUpdateCheckField
        {
            get { return insertOrUpdateCheckField; }
            set { insertOrUpdateCheckField = value; }
        }
        #endregion

        #region UpdatePanelID
        // PROPERTY:: UpdatePanelID
        [Category( "Behavior" )]
        [Description( "Name of UpdatePanel that contains the gridview" )]
        [DefaultValue( "AddButton" )]
        public string UpdatePanelID
        {
            get
            {
                if (ViewState["UpdatePanelID"] != null)
                    return ViewState["UpdatePanelID"].ToString();
                else
                    return "";
            }
            set
            {
                ViewState["UpdatePanelID"] = value;
            }
        }
        #endregion

        #region Private Property ErrorLabel
        private Label ErrorLabel
        {
            get
            {
                if (errorLabel == null)
                {
                    TPMSObjectDataSource ODS = (TPMSObjectDataSource)this.Parent.FindControl( this.DataSourceID );
                    string errorLabelName = ODS.ErrorLabelID;
                    errorLabel = (Label)this.Parent.FindControl( errorLabelName );
                }
                return errorLabel;
            }
        }
        #endregion

        #endregion

        #region Overridden Methods
        #region OnInit
        protected override void OnInit( EventArgs e )
        {
            this.ItemInserted += new FormViewInsertedEventHandler( TPMSFormView_ItemInserted );
            this.ItemUpdated += new FormViewUpdatedEventHandler( TPMSFormView_ItemUpdated );
            this.ItemDeleted += new FormViewDeletedEventHandler( TPMSFormView_ItemDeleted );
            this.DataBound += new EventHandler( TPMSFormView_DataBound );
            base.OnInit( e );
        }
        #endregion

        
        #endregion

        #region EventHandlers

        
        
        #region TPMSFormView_ItemUpdated
        void TPMSFormView_ItemUpdated( object sender, FormViewUpdatedEventArgs e )
        {
            StringBuilder errorMessage = new StringBuilder();
            Exception ex = e.Exception;
            if (ex != null)
            {
                if (!(ex is ApplicationException))
                {
                   
                    while (ex != null);
                    ErrorLabel.Text = errorMessage.ToString();
                }

                e.ExceptionHandled = true;
                e.KeepInEditMode = true;
            }
            else if (e.AffectedRows == 0)
            {
                e.KeepInEditMode = true;
                //Set Error Message only if no ErrorMessage was assigned by DataSource Control
                if (ErrorLabel.Text.Trim() == "")
                {

                    ErrorLabel.Text = "Concurrency Error";
                }
            }
            GetUpdatePanel.Update();
            
        }
        #endregion

        #region TPMSFormView_ItemInserted
        void TPMSFormView_ItemInserted( object sender, FormViewInsertedEventArgs e )
        {
            StringBuilder errorMessage = new StringBuilder();
            Exception ex = e.Exception;
            if (ex != null)
            {
                if (!(ex is ApplicationException))
                {
                    while (ex != null);
                    ErrorLabel.Text = errorMessage.ToString();
                }

                e.ExceptionHandled = true;
                e.KeepInInsertMode = true;
            }
            GetUpdatePanel.Update();
         }
        #endregion

        #region TPMSFormView_ItemDeleted
        void TPMSFormView_ItemDeleted( object sender, FormViewDeletedEventArgs e )
        {
            StringBuilder errorMessage = new StringBuilder();
            Exception ex = e.Exception;
            if (ex != null)
            {
                if (!(ex is ApplicationException))
                {
                    while (ex != null);
                    ErrorLabel.Text = errorMessage.ToString();
                }
                else if (e.AffectedRows == 0)
                {
                    //Set Error Message only if no ErrorMessage was assigned by DataSource Control
                    if (ErrorLabel.Text.Trim() == "")
                    {
                        ErrorLabel.Text = "Concurrency Error on deleting";
                    }
                }
                GetUpdatePanel.Update();
            }
         }
        #endregion

        #region TPMSFormView_DataBound
        void TPMSFormView_DataBound( object sender, EventArgs e )
        {

            if (insertOrUpdateCheckField == "None" || this.DataKey[insertOrUpdateCheckField] is DateTime)
            {
                if (this.CurrentMode != FormViewMode.Edit)
                    this.ChangeMode( FormViewMode.Edit );
            }
            else
            {
                if (this.CurrentMode != FormViewMode.Insert)
                    this.ChangeMode( FormViewMode.Insert );
            }
        }
        #endregion

        #endregion

        #region GetUpdatePanel
        private UpdatePanel GetUpdatePanel
        {
            get
            {
                return (UpdatePanel)this.Parent.FindControl( UpdatePanelID );
            }

        }
        #endregion
    }
}
