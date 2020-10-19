
Partial Class CENSIMENTO_StampaE
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
        Dim appoggioDate As String

        If par.OracleConn.State = Data.ConnectionState.Open Then
            Response.Write("IMPOSSIBILE VISUALIZZARE")
            Exit Sub
        Else
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        Dim vIdIndirizzo As String = ""
        Dim vTipoEdif As String = ""
        Dim vCodUtilizzo As String = ""
        Dim vCodTipoCostr As String = ""
        Dim vCodLivPossesso As String = ""
        Dim vCodImpRiscald As String = ""
        Dim vcodComune As String = ""


        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.EDIFICI where id =" & vId
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        myReader1 = par.cmd.ExecuteReader()

        If myReader1.Read Then
            vIdIndirizzo = par.IfNull(myReader1("ID_INDIRIZZO_PRINCIPALE"), " - - ")
            vTipoEdif = par.IfNull(myReader1("COD_TIPOLOGIA_EDIFICIO"), " - - ")
            vCodUtilizzo = par.IfNull(myReader1("COD_UTILIZZO_PRINCIPALE"), " - - ")
            vCodTipoCostr = par.IfNull(myReader1("COD_TIPOLOGIA_COSTRUTTIVA"), " - - ")
            vCodLivPossesso = par.IfNull(myReader1("COD_LIVELLO_POSSESSO"), " - - ")
            vCodImpRiscald = par.IfNull(myReader1("COD_TIPOLOGIA_IMP_RISCALD"), " - - ")
            'vcodComune = par.IfNull(myReader1("COD_COMUNE"), " - - ")
            Me.lblAscensori.Text = par.IfNull(myReader1("NUM_ASCENSORI"), " - - ")
            Me.lbldenedificio.Text = par.IfNull(myReader1("DENOMINAZIONE"), " - - ")
            Me.lblentroter.Text = par.IfNull(myReader1("NUM_PIANI_ENTRO"), " - - ")
            Me.lblpianfuo.Text = par.IfNull(myReader1("NUM_PIANI_FUORI"), " - - ")
            Me.lblscale.Text = par.IfNull(myReader1("NUM_SCALE"), " - - ")
            Me.lblgimi.Text = par.IfNull(myReader1("COD_EDIFICIO_GIMI"), " - - ")
            appoggioDate = par.IfNull(myReader1("DATA_COSTRUZIONE"), "dd/mm/YYYY")
            If appoggioDate <> "dd/mm/YYYY" Then
                Me.lbldatacostr.Text = par.FormattaData(appoggioDate)
            Else
                Me.lbldatacostr.Text = "- - - -"
            End If

            appoggioDate = par.IfNull(myReader1("DATA_RISTRUTTURAZIONE"), "dd/mm/YYYY")
            If appoggioDate <> "dd/mm/YYYY" Then
                Me.lbldataristrutt.Text = par.FormattaData(appoggioDate)
            Else
                Me.lbldataristrutt.Text = "- - - -"
            End If
            Me.lblsezione.Text = par.IfNull(myReader1("SEZIONE"), " - - ")
            Me.lblfoglio.Text = par.IfNull(myReader1("FOGLIO"), " - - ")
            Me.lblnumero.Text = par.IfNull(myReader1("NUMERO"), " - - ")
            Me.lblcodcomun.Text = par.IfNull(myReader1("COD_COMUNE"), " - - ")
            Me.lblcondominio.Text = RitornaSiNoDaNum(par.IfNull(myReader1("CONDOMINIO"), "2"))
            Me.TxtSintesi.Text = par.IfNull(myReader1("SINTESI_EDIFICIO"), "- -")
            Me.lblpianoterra.Text = RitornaSiNoDaNum(par.IfNull(myReader1("PIANO_TERRA"), "2"))
            Me.lblseminterrato.Text = RitornaSiNoDaNum(par.IfNull(myReader1("SEMINTERRATO"), "2"))
            Me.lblsottOTETTO.Text = RitornaSiNoDaNum(par.IfNull(myReader1("SOTTOTETTO"), "2"))
            Me.lblattico.Text = RitornaSiNoDaNum(par.IfNull(myReader1("ATTICO"), "2"))
            Me.lblsuperatt.Text = RitornaSiNoDaNum(par.IfNull(myReader1("SUPERATTICO"), "2"))

            If par.IfNull(myReader1("NUM_MEZZANINI"), " - - ") = 1 Then
                Me.lblmezzani.Text = "SI"
            Else
                Me.lblmezzani.Text = "NO"
            End If


            Me.LblCod.Text = par.IfNull(myReader1("COD_EDIFICIO"), " - - ")
        End If
        myReader1.Close()
        par.cmd.CommandText = "SELECT COMPLESSI_IMMOBILIARI.DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.EDIFICI where EDIFICI.id =" & vId & " AND EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID"
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            Me.lblcomplesso.Text = par.IfNull(myReader1("DENOMINAZIONE"), " - - ")

        End If

        myReader1.Close()
        par.cmd.CommandText = ""
        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI where id =" & vIdIndirizzo
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            Me.lblvia.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
            Me.lblcivico.Text = par.IfNull(myReader1("CIVICO"), " - - ")
            Me.lblcap.Text = par.IfNull(myReader1("CAP"), " - - ")
            Me.lbllocalita.Text = par.IfNull(myReader1("LOCALITA"), " - - ")

            vcodComune = par.IfNull(myReader1("COD_COMUNE"), " - - ")
        End If
        myReader1.Close()

        par.cmd.CommandText = "SELECT comu_descr FROM sepa.comuni WHERE comu_cod = '" & vcodComune & "'"
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            Me.lblcomune.Text = par.IfNull(myReader1("comu_descr"), " - - ")
        End If
        myReader1.Close()

        par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_EDIFICIO WHERE cod = '" & vTipoEdif & "'"
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            Me.lbltipoedif.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
        End If
        myReader1.Close()

        par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.UTILIZZO_PRINCIPALE_EDIFICIO WHERE cod = " & vCodUtilizzo
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            Me.lblutilprinc.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
        End If
        myReader1.Close()

        par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_COSTRUTTIVA WHERE cod = " & vCodTipoCostr
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            Me.lbltipocostr.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
        End If

        myReader1.Close()
        par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.LIVELLO_POSSESSO WHERE cod = '" & vCodLivPossesso & "'"
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            Me.lbllivposs.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
        End If
        myReader1.Close()

        'myReader1.Close()
        'par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.LIVELLO_POSSESSO WHERE cod = '" & vCodLivPossesso & "'"
        'myReader1 = par.cmd.ExecuteReader()
        'If myReader1.Read Then
        '    Me.lbllivposs.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
        'End If
        'myReader1.Close()
        'myReader1.Close()
        par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_IMP_RISCALDAMENTO WHERE cod = '" & vCodImpRiscald & "'"
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            Me.lblimpriscald.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
        End If
        myReader1.Close()

        par.cmd.CommandText = "SELECT tipologia_dimensioni.descrizione , to_char(DIMENSIONI.VALORE,'9999.99') as VALORE FROM SISCOM_MI.dimensioni, SISCOM_MI.tipologia_dimensioni WHERE dimensioni.cod_tipologia = tipologia_dimensioni.cod AND id_edificio = " & vId

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
    Private Function RitornaSiNoDaNum(ByVal valoredapassare As String) As String

        Dim a As String = "- -"
        If valoredapassare = 1 Then
            a = "SI"
        ElseIf valoredapassare = 0 Then
            a = "NO"
       
        End If

        Return a

    End Function

End Class
