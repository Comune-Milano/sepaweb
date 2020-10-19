
Partial Class CENSIMENTO_StampaUI
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
        Dim codUnitaImmob As String = ""
        Dim codTipolopogia As String = ""
        Dim idedificio As String = ""
        Dim idscala As String = ""
        Dim idUnitaPrinc As String = ""
        Dim idpiano As String = ""
        Dim codDispon As String = ""
        Dim codstatocons As String = ""
        Dim idcatastale As String = "0"
        Dim codcensimento As String = ""
        Dim codLivPiano As String = ""


        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.unita_immobiliari where id =" & vId
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            Me.lblinterno.Text = par.IfNull(myReader1("INTERNO"), " - - ")
            codUnitaImmob = par.IfNull(myReader1("COD_UNITA_IMMOBILIARE"), "0")
            codTipolopogia = par.IfNull(myReader1("COD_TIPOLOGIA"), "0")
            idedificio = par.IfNull(myReader1("ID_EDIFICIO"), "0")
            idscala = par.IfNull(myReader1("ID_SCALA"), "0")
            idUnitaPrinc = par.IfNull(myReader1("id_unita_principale"), "0")
            idpiano = par.IfNull(myReader1("ID_PIANO"), "0")
            codDispon = par.IfNull(myReader1("COD_TIPO_DISPONIBILITA"), "0")
            codstatocons = par.IfNull(myReader1("COD_STATO_CONS_LG_392_78"), "0")
            idcatastale = par.IfNull(myReader1("ID_CATASTALE"), "0")
            codcensimento = par.IfNull(myReader1("COD_STATO_CENSIMENTO"), "0")
            codLivPiano = par.IfNull(myReader1("COD_TIPO_LIVELLO_PIANO"), "0")
            Me.LblCod.Text = par.IfEmpty(myReader1("COD_UNITA_IMMOBILIARE"), "0")
        End If
        myReader1.Close()



        par.cmd.CommandText = "SELECT DENOMINAZIONE FROM SISCOM_MI.EDIFICI where ID =" & idedificio
        myReader1 = par.cmd.ExecuteReader()

        If myReader1.Read Then
            lblcomplesso.Text = par.IfNull(myReader1("DENOMINAZIONE"), " - - ")
        End If
        myReader1.Close()


        par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI where COD = '" & codTipolopogia & "'"
        myReader1 = par.cmd.ExecuteReader()

        If myReader1.Read Then
            lbltipounita.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
        End If
        myReader1.Close()

        par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_DISPONIBILITA where COD ='" & codDispon & "'"
        myReader1 = par.cmd.ExecuteReader()

        If myReader1.Read Then
            lbldisponib.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
        End If
        myReader1.Close()

        par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.STATO_CONSERVATIVO_LG_392_78 where COD = '" & codstatocons & "'"
        myReader1 = par.cmd.ExecuteReader()

        If myReader1.Read Then
            lblstatoconserv.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
        End If
        myReader1.Close()

        par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.stato_censimento where COD = '" & codcensimento & "'"
        myReader1 = par.cmd.ExecuteReader()

        If myReader1.Read Then
            lblstatocensim.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
        End If
        myReader1.Close()

        par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.tipo_livello_piano where COD = '" & codLivPiano & "'"
        myReader1 = par.cmd.ExecuteReader()

        If myReader1.Read Then
            lbllivellopiano.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
        End If
        myReader1.Close()

        'Aggiornamento scale stampa prendeva la scala invece di ricavare dall'id scala la descrizione della scala associata
        par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI where ID =" & idscala
        myReader1 = par.cmd.ExecuteReader()

        If myReader1.Read Then
            lblscala.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
        End If
        myReader1.Close()

        If idcatastale <> "0" Then


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.identificativi_catastali where id = " & idcatastale
            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then
                Me.lblsezione.Text = par.IfNull(myReader1("SEZIONE"), " - - ")
                Me.lblfoglio.Text = par.IfNull(myReader1("FOGLIO"), " - - ")
                Me.lblnumero.Text = par.IfNull(myReader1("NUMERO"), " - - ")
                Me.lblsub.Text = par.IfNull(myReader1("SUB"), " - - ")
                Me.lblrendita.Text = par.IfNull(myReader1("RENDITA"), " - - ")
                Me.lblmq.Text = par.IfNull(myReader1("SUPERFICIE_MQ"), " - - ")
                Me.lblcubatura.Text = par.IfNull(myReader1("CUBATURA"), " - - ")
                Me.lblvani.Text = par.IfNull(myReader1("NUM_VANI"), " - - ")
                Me.lblsupcat.Text = par.IfNull(myReader1("SUPERFICIE_CATASTALE"), " - - ")
                Me.lblrenditastor.Text = par.IfNull(myReader1("RENDITA_STORICA"), " - - ")
                Me.lblreddomin.Text = par.IfNull(myReader1("REDDITO_DOMINICALE"), " - - ")
                Me.lblvalimpo.Text = par.IfNull(myReader1("VALORE_IMPONIBILE"), " - - ")
                Me.lblreddagrar.Text = par.IfNull(myReader1("REDDITO_AGRARIO"), " - - ")
                Me.lblvalbil.Text = par.IfNull(myReader1("VALORE_BILANCIO"), " - - ")
                Dim appoggiodate As String = par.IfNull(myReader1("data_acquisizione"), " - - ")
                If appoggiodate <> " - - " Then
                    lbldataacquisi.Text = par.FormattaData(appoggiodate)
                End If
                appoggiodate = par.IfNull(myReader1("data_fine_validita"), " - - ")
                If appoggiodate <> " - - " Then
                    lblfineval.Text = par.FormattaData(appoggiodate)
                End If
                Me.lblditta.Text = par.IfNull(myReader1("ditta"), " - - ")
                Me.lblpartita.Text = par.IfNull(myReader1("num_partita"), " - - ")
                Me.lblesenteici.Text = RitornaSiNoDaNum(par.IfNull(myReader1("esente_ici"), "2"))
                Me.lblimmobstorico.Text = RitornaSiNoDaNum(par.IfNull(myReader1("IMMOBILE_STORICO"), "2"))
                Me.lblpercposs.Text = par.IfNull(myReader1("perc_possesso"), " - - ")
                Me.lblinagibile.Text = RitornaSiNoDaNum(par.IfNull(myReader1("inagibile"), "2"))
                Me.lblcodcomu.Text = par.IfNull(myReader1("cod_comune"), " - - ")
                Me.lblmicrocens.Text = par.IfNull(myReader1("microzona_censuaria"), " - - ")
                Me.lblzonacens.Text = par.IfNull(myReader1("zona_censuaria"), " - - ")
                Me.lblnote.Text = par.IfNull(myReader1("note"), " - - ")

                par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_CATASTO WHERE COD = '" & par.IfNull(myReader1("COD_TIPOLOGIA_CATASTO"), " - - ") & "'"

                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader

                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    Me.lbltipocat.Text = par.IfNull(myReader2("DESCRIZIONE"), " - - ")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.CLASSE_CATASTALE WHERE COD = '" & par.IfNull(myReader1("COD_CLASSE_CATASTALE"), " - - ") & "'"
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    Me.lblclasse.Text = par.IfNull(myReader2("DESCRIZIONE"), " - - ")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.categoria_catastale WHERE COD = '" & par.IfNull(myReader1("COD_Categoria_cATASTALE"), " - - ") & "'"
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    Me.lblcategoria.Text = par.IfNull(myReader2("DESCRIZIONE"), " - - ")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.STATO_CATASTALE WHERE COD = '" & par.IfNull(myReader1("COD_STATO_CATASTALE"), " - - ") & "'"
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    Me.lblstatocatast.Text = par.IfNull(myReader2("DESCRIZIONE"), " - - ")
                End If
                myReader2.Close()
            End If


        End If

        myReader1.Close()
        par.cmd.CommandText = "SELECT tipologia_dimensioni.descrizione , to_char(DIMENSIONI.VALORE,'9999.99') as VALORE FROM SISCOM_MI.dimensioni, SISCOM_MI.tipologia_dimensioni WHERE dimensioni.cod_tipologia = tipologia_dimensioni.cod AND ID_UNITA_IMMOBILIARE = " & vId

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
