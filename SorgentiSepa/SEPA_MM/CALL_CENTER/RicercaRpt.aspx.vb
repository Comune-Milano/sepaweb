
Partial Class CALL_CENTER_RicercaRpt
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then


            RiempiOperatori()
            CarStrutture()
            CarStato()
            CarTipo()
            'txtInizio.Attributes.Add("onfocus", "javascript:selectText(this);")
            txtInizio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'txtFine.Attributes.Add("onfocus", "javascript:selectText(this);")
            txtFine.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtApertoDa.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');")

            If Request.QueryString("tipo") = 0 Or Request.QueryString("tipo") = 2 Then
                Me.lblStato.Visible = False
                Me.lblApertoDa.Visible = False
                Me.cmbstato.Visible = False
                txtApertoDa.Visible = False
            End If
        End If
    End Sub

    Private Sub RiempiOperatori()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        cmbOpSegnalante.Items.Add(New ListItem("- - -", "-1"))

        par.cmd.CommandText = "select distinct operatori.id, operatori.operatore as nome from operatori, SISCOM_MI.SEGNALAZIONI WHERE OPERATORI.ID = SEGNALAZIONI.ID_OPERATORE_INS order by operatori.operatore asc"

        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        While lettore.Read
            Me.cmbOpSegnalante.Items.Add(New ListItem(par.IfNull(lettore("nome"), " "), par.IfNull(lettore("id"), " ")))
        End While
        lettore.Close()
        '*********************CHIUSURA CONNESSIONE**********************
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub
    Private Sub CarStrutture()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.caricaComboBox("SELECT * FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME", cmbStruttura, "ID", "NOME")
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CarStato()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            cmbstato.Items.Add(New ListItem("- - -", "-1"))
            par.cmd.CommandText = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE ID >=0"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore.Read
                cmbstato.Items.Add(New ListItem(par.IfNull(lettore("DESCRIZIONE"), ""), par.IfNull(lettore("id"), "-1")))
            End While
            lettore.Close()




            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub
    Private Sub CarTipo()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPOLOGIE_GUASTI", cmbTipo, "ID", "DESCRIZIONE")

            'cmbTipo.Items.Add(New ListItem("- - -", "-1"))
            'par.cmd.CommandText = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPOLOGIE_GUASTI "
            'Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'While lettore.Read
            '    cmbTipo.Items.Add(New ListItem(par.IfNull(lettore("DESCRIZIONE"), ""), par.IfNull(lettore("id"), "-1")))
            '    Me.cmbTipo.SelectedValue = par.IfNull(lettore("id"), "-1")
            'End While
            'lettore.Close()


            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try


    End Sub
    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click

        Select Case Request.QueryString("tipo")
            Case 0
                Response.Write("<script>window.open('RptStato.aspx?DAL=" & par.AggiustaData(Me.txtInizio.Text) & "&AL=" & par.AggiustaData(Me.txtFine.Text) & "&TIPO=" & Me.cmbTipo.SelectedValue.ToString & "&STR=" & cmbStruttura.SelectedItem.Value & "&OP=" & cmbOpSegnalante.SelectedItem.Value & "','rpt','');</script>")

            Case 1
                Response.Write("<script>window.open('RptSitInt.aspx?DAL=" & par.AggiustaData(Me.txtInizio.Text) & "&AL=" & par.AggiustaData(Me.txtFine.Text) & "&TIPO=" & Me.cmbTipo.SelectedValue.ToString & "&STR=" & cmbStruttura.SelectedItem.Value & "&OP=" & cmbOpSegnalante.SelectedItem.Value & "&ST=" & Me.cmbstato.SelectedValue & "&DAY=" & Me.txtApertoDa.Text & "','rpt','');</script>")

            Case 2
                Response.Write("<script>window.open('RptTempo.aspx?DAL=" & par.AggiustaData(Me.txtInizio.Text) & "&AL=" & par.AggiustaData(Me.txtFine.Text) & "&TIPO=" & Me.cmbTipo.SelectedValue.ToString & "&STR=" & cmbStruttura.SelectedItem.Value & "&OP=" & cmbOpSegnalante.SelectedItem.Value & "','rpt','');</script>")

        End Select


    End Sub
End Class
