using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;

namespace ArtGallery
{
    [ToolboxData( "<{0}:TpmsObjectDataSource runat=server></{0}:TpmsObjectDataSource>" )]
    public partial class TPMSObjectDataSource : System.Web.UI.WebControls.ObjectDataSource
    {
        #region Private Members
        string hasDummyRow;
        #endregion

        #region Properties

        #region HasDummyRow
        [Category( "Behavior" )]
        [Description( "Read Only Property Indicating if a dummy row has been added to datasource" )]
        [DefaultValue( "" )]
        [Browsable( false )]
        [ReadOnly( true )]
        public string HasDummyRow
        {
            //This property is set when a dummy row is added to datasource
            //This is used by TPMSGridView
            get { return hasDummyRow; }
        }
        #endregion

        #region AddDummyRow
        [Category( "Behavior" )]
        [Description( "Set this property to add an empty item to list of items returned by Select Method" )]
        [DefaultValue( "true" )]
        [Browsable( true )]
        [ReadOnly( false )]
        public bool AddDummyRow
        {
            //This property is set when a dummy row should be added to return list of itmes by select method
            //This dummy row is necessary for TPMSGridView to dispay header with no rows when no data is returned by select method
            get
            {
                if (ViewState["AddDummyRow"] == null)
                    return true;
                else
                    return (bool)ViewState["AddDummyRow"];
            }
            set { ViewState["AddDummyRow"] = value; }
        }
        #endregion

        #region ErrorLabelID
        /// <summary>
        /// Gets or sets ID of the Label Control on page to display error messages
        /// </summary
        [Category( "Behavior" )]
        [Description( "Label ID of lable control to display data update errors" )]
        [DefaultValue( "ErrorLabel" )]
        [Bindable( false )]
        [Browsable( true )]
        [IDReferenceProperty( typeof( Label ) )]
        public string ErrorLabelID
        {
            get
            {
                if (ViewState["ErrorLabelID"] == null)
                    return "";
                else
                    return ViewState["ErrorLabelID"].ToString();
            }
            set
            {
                ViewState["ErrorLabelID"] = value;
            }
        }
        #endregion


        #region IsFormView
        /// <summary>
        /// Gets or sets ID of the Label Control on page to display error messages
        /// </summary
        [Category( "Behavior" )]
        [Description( "Set to true if part of FormView" )]
        [DefaultValue( false )]
        [Bindable( false )]
        [Browsable( true )]
        [IDReferenceProperty( typeof( Label ) )]
        public bool IsFormView
        {
            get
            {
                if (ViewState["IsFormView"] == null)
                    return false;
                else
                    return (bool) ViewState["IsFormView"];
            }
            set
            {
                ViewState["IsFormView"] = value;
            }
        }
        #endregion

        #region Unique Constraint Message
        /// <summary>
        /// Gets or sets ID of the Label Control on page to display error messages
        /// </summary
        [Category( "Behavior" )]
        [Description( "Message for unique constraint violation" )]
        [DefaultValue( "*** Conflicts with a unique index constraint ***" )]
        [Bindable( false )]
        [Browsable( true )]
        [IDReferenceProperty( typeof( System.String ) )]
        public string UniqueConstaintMessage
        {
            get
            {
                if (ViewState["UniqueConstraint"] == null)
                    return "";
                else
                    return ViewState["UniqueConstraint"].ToString();
            }
            set
            {
                ViewState["UniqueConstraint"] = value;
            }
        }
        #endregion


        #region PkErrorMessage
        /// <summary>
        /// Gets or sets Error Message to display when a Primary Key 
        /// Violation Exception is thrown from the database
        /// </summary
        [Category( "Behavior" )]
        [Description( "Error Message to display for primary key constraint violation" )]
        [DefaultValue( "" )]
        [Bindable( false )]
        [Browsable( true )]
        public string PkErrorMessage
        {
            get
            {
                if (ViewState["PkErrorMessage"] == null)
                    return "";
                else
                    return ViewState["PkErrorMessage"].ToString();
            }
            set
            {
                ViewState["PkErrorMessage"] = value;
            }
        }
        #endregion

        #region FkErrorMessage
        /// <summary>
        /// Gets or sets Error Message to display when a Foriegn Key 
        /// Violation Exception is thrown from the database
        /// </summary>
        [Category( "Behavior" )]
        [Description( "Error Message to display for foriegn key constraint violation" )]
        [DefaultValue( "" )]
        [Bindable( false )]
        public string FkErrorMessage
        {
            get
            {
                if (ViewState["FkErrorMessage"] == null)
                    return "";
                else
                    return ViewState["FkErrorMessage"].ToString();
            }
            set
            {
                ViewState["FkErrorMessage"] = value;
            }
        }
        #endregion

        #region ConcurrencyViolationMessage
        /// <summary>
        /// Gets or sets Error Message to display when a Concurrency Violation 
        /// Violation Exception is thrown from the database
        /// </summary>
        [Category( "Behavior" )]
        [Description( "Error Message to display for concurrency violation" )]
        [DefaultValue( "" )]
        [Bindable( false )]
        public string ConcurrencyViolationMessage
        {
            get
            {
                if (ViewState["CVMessage"] == null)
                    return "";
                else
                    return ViewState["CVMessage"].ToString();
            }
            set
            {
                ViewState["CVMessage"] = value;
            }
        }
        #endregion

        #endregion

        #region OnInit
        protected override void OnInit( EventArgs e )
        {
            this.Selected += new System.Web.UI.WebControls.ObjectDataSourceStatusEventHandler( ObjectDataSource_Selected );
            this.Deleted += new ObjectDataSourceStatusEventHandler( ObjectDataSource_Updated );
            this.Updated += new ObjectDataSourceStatusEventHandler( ObjectDataSource_Updated );
            this.Inserted += new ObjectDataSourceStatusEventHandler( TPMSObjectDataSource_Inserted );
            base.OnInit( e );
        }


        #endregion OnInit

        #region code for empty datagrid
        protected void ObjectDataSource_Selected( object sender, ObjectDataSourceStatusEventArgs e )
        {
            DataTable dataTable = null;
            if (e.Exception == null)
            {
                // get the DataTable from the ODS select mothod
                if (e.ReturnValue is DataTable)
                    dataTable = (DataTable)e.ReturnValue;
                else if (e.ReturnValue is DataSet)
                {
                    dataTable = ((DataSet)e.ReturnValue).Tables[0];
                }
                else if (!IsFormView)
                    throw new Exception( "Return Type of the Select Method should be a DataTable or DataSet" );
                else
                    return;

                // if rows=0 then add a dummy (null) row and set the LoadDataEmpty flag.
                if ((dataTable != null) && (dataTable.Rows.Count == 0) && AddDummyRow)
                {
                    //Following line of code ensures DataTable Constratins are not enforced for the dummy row.
                    dataTable.BeginLoadData();

                    dataTable.Rows.Add( dataTable.NewRow() );
                    hasDummyRow = "true";
                }
                else
                    hasDummyRow = "false";
            }
        }
        #endregion

        #region Deleted
        ///// <summary>
        ///// This is called after objectdatasource deletes a row in the db
        ///// </summary>
        ///// <param name="sender">Sender</param>
        ///// <param name="e">ObjectDataSource Deleted Method Event Arguments</param>
        //protected void ObjectDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        //{
        //    Label ELabel;
        //    if ((e.ReturnValue == null) || ((int)e.ReturnValue < 1))
        //    {
        //        ELabel = (Label)Parent.FindControl(errorLabelID);

        //        if ((e.Exception != null) && e.Exception.InnerException is SqlException)
        //        {
        //            SqlException sqlException = (SqlException)e.Exception.InnerException;
        //            if (sqlException.Number == 547)
        //            {
        //                if (ELabel != null)
        //                {
        //                    ELabel.Text = "*** Unable to delete record, Delete conflicted with Foriegn Key reference constraint ***";
        //                    e.ExceptionHandled = true;
        //                }
        //            }
        //        }

        //            ELabel.Text = "*** Unable to update record, data may have been updated by another user since page load ***";
        //    }

        //    if (e.ReturnValue != null)
        //        e.AffectedRows = (int)e.ReturnValue;
        //}
        #endregion

        #region Inserted
        /// <summary>
        /// This is called after an insert operation is performed on ObjectDataSource
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">ObjectDataSource Inserted Method Event Arguments</param>
        void TPMSObjectDataSource_Inserted( object sender, ObjectDataSourceStatusEventArgs e )
        {
            Label ELabel;
            if ((e.Exception != null) && e.Exception.InnerException is SqlException)
            {
                ELabel = (Label)Parent.FindControl( ErrorLabelID );

                SqlException sqlException = (SqlException)e.Exception.InnerException;
                if (sqlException.Number == 2627)
                {
                    if ((PkErrorMessage == null) || (PkErrorMessage == ""))
                        ELabel.Text = "*** Unable to insert record, insert conflicted with Primary Key constraint ***";
                    else
                        ELabel.Text = PkErrorMessage;

                    e.ExceptionHandled = true;
                    throw new ApplicationException();
                }
                else if (sqlException.Number == 547)
                {
                    if ((FkErrorMessage == null) || (FkErrorMessage == ""))
                        ELabel.Text = "*** Unable to insert record, insert conflicted with Foriegn Key reference constraint ***";
                    else
                        ELabel.Text = FkErrorMessage;

                    e.ExceptionHandled = true;
                    throw new ApplicationException();
                }
                else if (sqlException.Number == 2601)
                {
                    ELabel.Text = UniqueConstaintMessage == "" ? "*** Conflicts with a unique index constraint ***" : UniqueConstaintMessage;
                    e.ExceptionHandled = true;
                    throw new ApplicationException();
                }
                else if (sqlException.Number == 50000)
                {
                    ELabel.Text = sqlException.Message;
                    e.ExceptionHandled = true;
                    throw new ApplicationException();
                }
            }
        }
        #endregion

        #region Updated/Deleted
        /// <summary>
        /// This is called after objectdatasource updates a row in the db
        /// </summary>
        /// <param name="sender">ObjectDataSource</param>
        /// <param name="e">ObjectDataSource Deleted Method Event Arguments</param>
        void ObjectDataSource_Updated( object sender, ObjectDataSourceStatusEventArgs e )
        {
            Label ELabel = (Label)Parent.FindControl( ErrorLabelID );;
            if ((e.ReturnValue == null) || ((int)e.ReturnValue < 1))
            {
                if ((e.Exception != null) && e.Exception.InnerException is SqlException)
                {
                    SqlException sqlException = (SqlException)e.Exception.InnerException;
                    if (sqlException.Number == 547)
                    {
                        if ((FkErrorMessage == null) || (FkErrorMessage == ""))
                            ELabel.Text = "*** Unable to update record, Update conflicted with Foriegn Key reference constraint ***";
                        else
                            ELabel.Text = FkErrorMessage;
                        e.AffectedRows = 0;
                        e.ExceptionHandled = true;
                    }
                    else if (sqlException.Number == 2627)
                    {
                        if ((PkErrorMessage == null) || (PkErrorMessage == ""))
                            ELabel.Text = "*** Unable to update record, Update conflicted with Primary Key reference constraint ***";
                        else
                            ELabel.Text = PkErrorMessage;
                        e.AffectedRows = 0;
                        e.ExceptionHandled = true;
                    }
                    else if (sqlException.Number == 50000)
                    {
                        ELabel.Text = sqlException.Message;
                        e.AffectedRows = 0;
                        e.ExceptionHandled = true;
                    }
                    else if (sqlException.Number == 2601)
                    {
                        ELabel.Text = UniqueConstaintMessage == "" ? "*** Conflicts with a unique index constraint ***" : UniqueConstaintMessage;
                        e.AffectedRows = 0;
                        e.ExceptionHandled = true;
                    }
                    else
                    {
                        ELabel.Text = e.Exception.InnerException == null ? e.Exception.Message : e.Exception.InnerException.Message;
                        e.AffectedRows = 0;
                        e.ExceptionHandled = true;
                    }
                }
                else if ((e.ReturnValue != null) && ((int)e.ReturnValue == 0))
                {
                    if ((ConcurrencyViolationMessage == null) || (ConcurrencyViolationMessage == ""))
                        ELabel.Text = "*** Concurrency violation, the record you attempted to update has been modified by another since this page was loaded. Your save was cancelled to allow you to review the other user’s changes and determine which changes you wish to make. Then, save again. ***";
                    else
                        ELabel.Text = ConcurrencyViolationMessage;
                    e.AffectedRows = 0;
                    e.ExceptionHandled = true;
                }
                else if (e.Exception != null)
                {
                    ELabel.Text = e.Exception.InnerException == null ? e.Exception.Message : e.Exception.InnerException.Message;
                    e.AffectedRows = 0;
                    e.ExceptionHandled = true;
                }
            }
            else
            {
                e.AffectedRows = (int)e.ReturnValue;
                ELabel.Text = "Update successfull";
            }
        }
        #endregion


    }
}