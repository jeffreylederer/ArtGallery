using System;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.ComponentModel;

namespace ArtGallery
{
    [ToolboxData( "<{0}:TpmsGridView runat=server></{0}:TpmsGridView>" )]
    public partial class TPMSGridView : System.Web.UI.WebControls.GridView
    {
        #region Private Fields
        private int rowIndex;
        private bool hasValidInsertTicket;
        private object dataKey;
        private int editIndexUsingDataKey = -1;
        #endregion

        #region Properties

        #region AddHeaderToFooter
        // PROPERTY:: AddButtonID
        [Category( "Behavior" )]
        [Description( "Adds Header to bottom of footer row" )]
        [DefaultValue( false )]
        public bool AddHeaderToFooter
        {
            get
            {
                if (ViewState["AddHeaderToFooter"] != null)
                    return (bool)ViewState["AddHeaderToFooter"];
                else
                    return false;
            }
            set
            {
                ViewState["AddHeaderToFooter"] = value;
            }
        }
        #endregion

        #region AddButtonID
        // PROPERTY:: AddButtonID
        [Category( "Behavior" )]
        [Description( "Button IDs that will cause the Gridview to show footer. Seperate by comma if more than one" )]
        [DefaultValue( "AddButton" )]
        public string AddButtonIDs
        {
            get
            {
                if (ViewState["addButtonIDs"] != null)
                    return ViewState["addButtonIDs"].ToString();
                else
                    return "";
            }
            set
            {
                ViewState["addButtonIDs"] = value;
            }
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

        
        #region private property LoadEmptyData
        [Browsable( false )]
        [ReadOnly( true )]
        private bool LoadEmptyData
        {
            get
            {
                using (Control ctrl = this.Parent.FindControl( this.DataSourceID ))
                {
                    if (ctrl is TPMSObjectDataSource)
                    {
                        using (TPMSObjectDataSource dataSource = (TPMSObjectDataSource)ctrl)
                        {
                            if (dataSource.HasDummyRow == "true")
                            {
                                ViewState["LoadEmptyData"] = "true";
                                return true;
                            }
                            else if (dataSource.HasDummyRow == "false")
                            {
                                ViewState["LoadEmptyData"] = "false";
                                return false;
                            }
                            else if (((dataSource.HasDummyRow == null) || (dataSource.HasDummyRow == "")) &&
                                        (ViewState["LoadEmptyData"] != null) &&
                                        (ViewState["LoadEmptyData"].ToString() == "true"))
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                    }
                    else
                        return false;
                }
            }
        }
        #endregion

        #region private Property AjaxEnabledPage
        /// <summary>
        /// Returns true if page uses Ajax Controls like ScriptManager and UpdatePanel
        /// False otherwise
        /// </summary>
        private bool AjaxEnabledPage
        {
            get { return (ScriptManager.GetCurrent( this.Page ) != null); }
        }
        #endregion

        #endregion

        #region Overridden Methods

   
        #region OnInit
        protected override void OnInit( EventArgs e )
        {
            this.RowEditing += new GridViewEditEventHandler( GridView_RowEditing );
            this.RowUpdating += new GridViewUpdateEventHandler( GridView_RowUpdating );
            this.RowUpdated += new GridViewUpdatedEventHandler( GridView_RowUpdated );
            this.RowCancelingEdit += new GridViewCancelEditEventHandler( GridView_RowCancelingEdit );
            this.RowCommand += new GridViewCommandEventHandler( GridView_RowCommand );
            this.RowCreated += new GridViewRowEventHandler( GridView_RowCreated );
            this.PreRender += new EventHandler( TPMSGridView_PreRender );
            List<Button> addButtons = getButtonControls();
            foreach (Button addButton in addButtons)
            {
                addButton.Click += new EventHandler( addButton_Click );
            }
            this.Attributes.Add( "bordercolor", System.Drawing.ColorTranslator.ToHtml( BorderColor ) );
            base.OnInit( e );
        }
        #endregion OnInit

        #endregion Overridden Methods

        #region GridView EventHandlers
        #region Prerender
        void TPMSGridView_PreRender( object sender, EventArgs e )
        {
            //Disable buttons if GridView is disabed
            if (!this.Enabled)
            {
                hideButtons();
            }
            else
            {
                if ((dataKey != null) && (this.EditIndex != -1))
                    this.EditIndex = editIndexUsingDataKey;

                //Set Focus to footer row if it is visible
                if ((this.FooterRow != null) && (this.FooterRow.Visible == true))
                {
                    //If Ajax is used on page then following 
                    //line of code will properly set focus to footer row 
                    if (AjaxEnabledPage)
                        ScriptManager.GetCurrent( this.Page ).SetFocus( FooterRow );
                    else
                        Page.SetFocus( FooterRow );

                    //Because Footer Row is visible gridview is in insert mode, so hiding AddButtons
                    hideButtons();

                    // Put header below datacontrols in footer row;
                    if (AddHeaderToFooter)
                    {
                        for (int i = 0; i < this.Columns.Count; i++)
                        {
                            string header = HeaderRow.Cells[i].Text;
                            if (header != null && header != string.Empty && header != "&nbsp;")
                            {
                                Label label = new Label();
                                label.Text = "<center>" + header + "</center>";
                                label.ID = "label00" + i.ToString();
                                label.BackColor = System.Drawing.Color.Black;
                                label.ForeColor = System.Drawing.Color.White;
                                label.Width = Unit.Percentage( 100.0 );
                                label.Font.Bold = true;
                                label.Font.Name = "verdana";
                                FooterRow.Cells[i].Controls.Add( label );
                            }
                        }
                    }
                }
                else if (this.EditIndex > -1)
                {
                    if (AjaxEnabledPage)
                        ScriptManager.GetCurrent( this.Page ).SetFocus( this.Rows[this.EditIndex] );
                    else
                        Page.SetFocus( this.Rows[this.EditIndex] );
                }
                else if ((this.rowIndex != -1) && (this.Rows.Count > 0))
                {
                    //Set Focus to the row that raised the post back event
                    if ((this.rowIndex < this.Rows.Count) &&
                             ((this.Rows[this.rowIndex].RowState == DataControlRowState.Edit) || (this.rowIndex > 0)))
                    {
                        if (AjaxEnabledPage)
                            ScriptManager.GetCurrent( this.Page ).SetFocus( this.Rows[this.rowIndex] );
                        else
                            Page.SetFocus( this.Rows[this.rowIndex] );
                    }
                }

                if (((this.FooterRow == null) || (this.FooterRow.Visible == false)) && (this.EditIndex < 0))
                {
                    showButtons();
                }
            }
            GetUpdatePanel.Update();
        }
        #endregion Prerender

        #region RowEditing
        /// <summary>
        /// Called after user click edit control on page
        /// Disable add button when editing a row
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">GridView Edit Event Arguments</param>
        protected virtual void GridView_RowEditing( object sender, GridViewEditEventArgs e )
        {
            //Remembering the DataKey of the row that is being changed to edit mode
            //so that if the order of items in grid changes correct row can be identifed
            dataKey = this.DataKeys[e.NewEditIndex][0];
            this.hideButtons();
            GetUpdatePanel.Update();
        }
        #endregion

        #region RowUpdating
        protected void GridView_RowUpdating( object sender, GridViewUpdateEventArgs e )
        {
            //Remembering the DataKey of the row that is being changed to edit mode
            //so that if the order of items in grid changes correct row can be identifed
            //and also if the record is deleted by another between EditClick and UpdateClick
            //GridView will not be in edit mode.
            dataKey = this.DataKeys[e.RowIndex][0];
        }
        #endregion

        #region RowUpdated
        /// <summary>
        /// Call after database has been updated,
        /// reenable add button when done editing row
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Grid View Updated Event</param>
        protected void GridView_RowUpdated( object sender, GridViewUpdatedEventArgs e )
        {
            if (e.AffectedRows == 0)
            {
                e.KeepInEditMode = true;
            }
            else
                this.showButtons();
            GetUpdatePanel.Update();
        }
        #endregion

        #region RowCancelingEdit
        /// <summary>
        /// Called when user select cancel button on editable row;
        /// reenable add button when done editing row
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">GridView Cancel Edit Event Arguments</param>
        protected void GridView_RowCancelingEdit( object sender, GridViewCancelEditEventArgs e )
        {
            this.showButtons();
            if (this.ShowFooter == true)
                this.ShowFooter = false;
            GetUpdatePanel.Update();
        }
        #endregion

        #region RowCommand
        /// <summary>
        /// Called when insert/update/edit/cancel link buttons are clicked on GridView
        /// This method calls the insert method on ObjectDataSource and 
        /// enables button control after insert operation is completed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">GridView Command Event Arguments</param>
        protected void GridView_RowCommand( object sender, GridViewCommandEventArgs e )
        {
            rowIndex = e.CommandSource is GridView ? -1 : ((GridViewRow)((Control)e.CommandSource).Parent.Parent).RowIndex;
            ObjectDataSource dataSource = (ObjectDataSource)this.Parent.FindControl( this.DataSourceID );
            if (dataSource != null)
            {
                if (e.CommandName == "Insert" && Page.IsValid)
                {
                    try
                    {
                        //Following block of code is chekced for Exceptions
                        //because Insert() will throw an exception if insert ito DB fails
                        dataSource.Insert();
                        showButtons();
                    }
                    catch (Exception ex)
                    {
                        //Object Data Source throws this exception on Primary Key, Unique Key and Foriegn Key Voilations
                        //No Action is required here.
                        //This exception ensures that gridview footer row is displayed and and typed in data is not lost.
                    }
                   
                }
            }
            GetUpdatePanel.Update();
        }
        #endregion RowCommand

        #region RowCreated
        protected void GridView_RowCreated( object sender, GridViewRowEventArgs e )
        {
            // if we have zero data rows (but a dummy row), hide the grid view row
            // and clear the controls off of that row so they don't cause binding errors
            if (e.Row.RowType == DataControlRowType.DataRow &&
                e.Row.RowIndex == 0 &&
                LoadEmptyData)
            {
                e.Row.Visible = false;
                e.Row.Controls.Clear();
            }

            //Following Logic records the correct row index of the 
            //item that was selected for editing
            if ((dataKey != null) &&
                (e.Row.RowType == DataControlRowType.DataRow) &&
                !((e.Row.RowIndex == 0) && LoadEmptyData) &&
                (this.DataKeys.Count > e.Row.RowIndex) &&
                (this.DataKeys[e.Row.RowIndex][0] != null) &&
                (this.DataKeys[e.Row.RowIndex][0].ToString() == dataKey.ToString()))
                editIndexUsingDataKey = e.Row.RowIndex;
            GetUpdatePanel.Update();
        }
        #endregion RowCreated

        #endregion

        #region AddButtonClick
        void addButton_Click( object sender, EventArgs e )
        {
            hideButtons();
            //Refreshing Grid content before changing to insert mode
            this.DataBind();
            this.FooterRow.Visible = true;
            GetUpdatePanel.Update();
        }
        #endregion AddButtonClick

        #region private Methods

        #region hideButtons
        private void hideButtons()
        {
            List<Button> buttonControls = this.getButtonControls();
            foreach (Button addButton in buttonControls)
            {
                addButton.Enabled = false;
            }
        }
        #endregion

        #region showButtons
        private void showButtons()
        {
            List<Button> buttonControls = this.getButtonControls();
            foreach (Button addButton in buttonControls)
            {
                addButton.Enabled = true;
            }
        }
        #endregion

        #region getButtonControls
        private List<Button> getButtonControls()
        {
            List<Button> buttonControls = new List<Button>();
            if ((AddButtonIDs != null) && (AddButtonIDs.Trim().Length > 0))
            {
                string[] addButtonIds = AddButtonIDs.Split( new char[1] { ',' } );
                foreach (string buttonID in addButtonIds)
                {
                    try
                    {
                        Button button = (Button)this.Parent.FindControl( buttonID );
                        buttonControls.Add( button );
                    }
                    catch (Exception e)
                    {
                        throw new Exception( "Error finding button control with ID: " + buttonID, e );
                    }

                }
            }
            return buttonControls;
        }
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

        #endregion private Methods
    }
}
