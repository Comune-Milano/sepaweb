
Partial Class NEW_CENSIMENTO_Tab_CPI
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            TxtDataRilascio.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            TxtDataScadenza.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            ' ''********************************************************************************************
            TxtDataRilascio.Attributes.Add("onfocus", "javascript:selectText(this);")
            TxtDataScadenza.Attributes.Add("onfocus", "javascript:selectText(this);")
            '*********************************************************************************************
            TxtDataRilascio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            TxtDataScadenza.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Carica()
            Cerca()

        End If
    End Sub
    Private Sub Carica()
        Try

            If Session("SLE") = "1" Then
                par.OracleConn.Open()
                par.SettaCommand(par)

            Else
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

            End If


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

            par.cmd.CommandText = "SELECT * FROM siscom_mi.TAB_ATTIVITA_CPI ORDER BY ID ASC"
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                Attivita.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()

            If Session("PED2_SOLOLETTURA") = "1" Then
                FrmSolaLettura()
            End If
            If Session("SLE") = "1" Then
                FrmSolaLettura()
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub

    Private Sub FrmSolaLettura()
        Try
            Dim CTRL As Control = Nothing

            For Each CTRL In Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Enabled = False
                End If
            Next

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub
    Private Sub Cerca()
        Try

            par.cmd.CommandText = "Select * from siscom_mi.CPI_EDIFICI where ID_EDIFICIO = " & CType(Me.Page, Object).vId
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While myReader1.Read
                Me.Attivita.Items.FindByValue(myReader1.Item("ID_ATTIVITA_CPI")).Selected = True
            End While
            myReader1.Close()

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub
End Class
