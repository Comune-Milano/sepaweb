
Partial Class CENSIMENTO_StampaUC
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            vid = Request.QueryString("ID")
            Me.Riempicampi()
        End If
    End Sub
    Private Property vId() As Long
        Get
            If Not (ViewState("par_idEdificio") Is Nothing) Then
                Return CLng(ViewState("par_idEdificio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idEdificio") = value
        End Set

    End Property
    Private Sub Riempicampi()
        If par.OracleConn.State = Data.ConnectionState.Open Then
            Response.Write("IMPOSSIBILE VISUALIZZARE")
            Exit Sub
        Else
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_COMUNI where id =" & vId
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        myReader1 = par.cmd.ExecuteReader()
        Dim coddisponibilita As String = ""
        Dim codtipologia As String = ""
        Dim idcomplesso As String = ""
        Dim idedificio As String = ""

        If myReader1.Read Then
            Me.lbllocalizzazione.Text = par.IfNull(myReader1("LOCALIZZAZIONE"), " - - ")
            Me.LBLNUMPIANIASC.Text = par.IfNull(myReader1("NUM_PIANI_ASCENSORE"), " - - ")
            Me.lblnumpianiSC.Text = par.IfNull(myReader1("NUM_PIANI_SCALE"), " - - ")
            coddisponibilita = par.IfNull(myReader1("COD_DISPONIBILITA"), " - - ")
            codtipologia = par.IfNull(myReader1("COD_TIPOLOGIA"), " - - ")
            idcomplesso = par.IfNull(myReader1("ID_COMPLESSO"), " - - ")
            idedificio = par.IfNull(myReader1("ID_EDIFICIO"), " - - ")
            Me.LblCod.Text = par.IfNull(myReader1("COD_UNITA_COMUNE"), " - - ")
        End If
        myReader1.Close()
        If idedificio <> " - - " Then
            par.cmd.CommandText = "SELECT denominazione FROM SISCOM_MI.edifici where id =" & idedificio
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                lbledificio.Text = myReader1("denominazione")
            End If
            myReader1.Close()

        End If

        If idcomplesso <> " - - " Then
            par.cmd.CommandText = "SELECT DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI where ID = " & idcomplesso
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                lblcomplesso.Text = myReader1("denominazione")
            End If
            myReader1.Close()
        End If

        par.cmd.CommandText = "SELECT descrizione FROM SISCOM_MI.TIPO_UNITA_COMUNE where COD = '" & codtipologia & "'"
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            lbltipoUnita.Text = myReader1("descrizione")
        End If
        myReader1.Close()

        par.cmd.CommandText = "SELECT descrizione FROM SISCOM_MI.tipo_disponibilita where cod = '" & coddisponibilita & "'"
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            lbldisponib.Text = myReader1("descrizione")
        End If
        myReader1.Close()
        par.cmd.CommandText = "SELECT tipologia_dimensioni.descrizione , to_char(DIMENSIONI.VALORE,'9999.99') as VALORE FROM SISCOM_MI.dimensioni, SISCOM_MI.tipologia_dimensioni WHERE dimensioni.cod_tipologia = tipologia_dimensioni.cod AND ID_UNITA_COMUNE = " & vId

        myReader1 = par.cmd.ExecuteReader()
        'Response.Write("<p><font face='Arial'>UTENZE MILLESIMALI</font></p>")
        lblDimensioni.Text = "<table width='100%'><tr><td width='30%'>DESCRIZIONE</td> <td width='30%'>VALORE</td></tr>"

        Do While myReader1.Read()
            lblDimensioni.Text = lblDimensioni.Text & ("<tr>")
            lblDimensioni.Text = lblDimensioni.Text & ("<td width='30%'>" & par.IfNull(myReader1("descrizione"), "") & "</td>")
            lblDimensioni.Text = lblDimensioni.Text & ("<td width='30%'>" & par.IfNull(myReader1("valore"), "") & "</td>")
            lblDimensioni.Text = lblDimensioni.Text & ("</tr>")
        Loop

        myReader1.Close()
        lblDimensioni.Text = lblDimensioni.Text & ("</table>")


        '*********************CHIUSURA CONNESSIONE**********************

        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub
End Class
