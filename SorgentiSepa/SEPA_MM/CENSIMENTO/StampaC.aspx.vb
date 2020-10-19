
Partial Class CENSIMENTO_Stampa
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim codtipoComp As String
    Dim codliv_poss As String
    Dim idIndirizzo As String
    Dim CodProvenienza As String
    Dim codTipoUbicaz As String
    Dim codcomune As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            vIdComplesso = Request.QueryString("ID")

            Me.Riempicampi()

        End If
    End Sub
    Public Property vIdComplesso() As Long
        Get
            If Not (ViewState("par_lIdDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_lIdDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDichiarazione") = value
        End Set

    End Property
    Private Sub Riempicampi()
        Try


            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim IdCommissariato As String = ""
            Dim idFiliale As String = ""

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COMPLESSI_IMMOBILIARI where id =" & vIdComplesso
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.lblcodcomplesso.Text = par.IfNull(myReader1("COD_COMPLESSO"), " - - ")
                Me.lbllotto.Text = par.IfNull(myReader1("LOTTO"), " - - ")
                Me.lblcodgimi.Text = par.IfNull(myReader1("COD_COMPLESSO_GIMI"), " - - ")
                Me.lbldencomp.Text = par.IfNull(myReader1("DENOMINAZIONE"), " - - ")
                Me.codtipoComp = par.IfNull(myReader1("COD_TIPO_COMPLESSO"), " - - ")
                Me.codliv_poss = par.IfNull(myReader1("COD_LIVELLO_POSSESSO"), " - - ")
                Me.CodProvenienza = par.IfNull(myReader1("COD_TIPOLOGIA_PROVENIENZA"), " - - ")
                Me.codTipoUbicaz = par.IfNull(myReader1("COD_TIPO_UBICAZIONE_LG_392_78"), " - - ")
                Me.idIndirizzo = par.IfNull(myReader1("ID_INDIRIZZO_RIFERIMENTO"), " - - ")
                IdCommissariato = par.IfNull(myReader1("ID_COMMISSARIATO"), " - - ")
                idFiliale = par.IfNull(myReader1("ID_FILIALE"), " - - ")
            End If
            myReader1.Close()

            If IdCommissariato <> "" Then
                par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TAB_COMMISSARIATI WHERE ID = " & IdCommissariato
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.lblCommissariato.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
                End If
                MYREADER1.Close 
            End If

            If idFiliale <> "" Then
                par.cmd.CommandText = "SELECT TAB_FILIALI.NOME, (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP||' '||COMUNI_NAZIONI.NOME) AS INDIRIZZO FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI WHERE TAB_FILIALI.ID_INDIRIZZO = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE = COMUNI_NAZIONI.COD  AND TAB_FILIALI.ID = " & idFiliale
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.lblFiliale.Text = par.IfNull(myReader1("NOME"), " - - ") & "- -" & par.IfNull(myReader1("INDIRIZZO"), " - - ")
                End If
                myReader1.Close()
            End If

            par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_COMPLESSO_IMMOBILIARE where COD = '" & codtipoComp & "'"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.lbltipocom.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
            End If
            myReader1.Close()

            par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.LIVELLO_POSSESSO WHERE COD = '" & codliv_poss & "'"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.lbllivposs.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
            End If
            myReader1.Close()

            par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_PROVENIENZA WHERE COD = '" & CodProvenienza & "'"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.lblprovenienza.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
            End If
            myReader1.Close()

            par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_UBICAZIONE_LG_392_78 WHERE COD = '" & codTipoUbicaz & "'"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.lblcodubicaz.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
            End If
            myReader1.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI WHERE ID = " & idIndirizzo
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.lblvia.Text = par.IfNull(myReader1("DESCRIZIONE"), " - - ")
                Me.lblcivico.Text = par.IfNull(myReader1("CIVICO"), " - - ")
                Me.lblcap.Text = par.IfNull(myReader1("CAP"), " - - ")
                Me.lbllocalita.Text = par.IfNull(myReader1("LOCALITA"), " - - ")

                Me.codcomune = par.IfNull(myReader1("COD_COMUNE"), " ")
            End If
            myReader1.Close()

            par.cmd.CommandText = "SELECT comu_descr FROM sepa.comuni WHERE comu_cod = '" & codcomune & "'"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.lblcomune.Text = par.IfNull(myReader1("comu_descr"), " - - ")
            End If
            myReader1.Close()


            par.cmd.CommandText = "select tabelle_millesimali.descrizione, tipologia_millesimale.descrizione as tipologia, tabelle_millesimali.descrizione_tabella from SISCOM_MI.tabelle_millesimali, SISCOM_MI.tipologia_millesimale where cod_tipologia=tipologia_millesimale.cod and id_complesso = " & vIdComplesso & ""
            myReader1 = par.cmd.ExecuteReader()
            'Response.Write("<p><font face='Arial'>MILLESIMALI</font></p>")

            Label14.Text = "<table width='100%'><tr><td width='30%'>DESCRIZIONE</td> <td width='30%'>TIPO</td><td width='30%'>DETTAGLI</td></tr>"

            Do While myReader1.Read()
                Label14.Text = Label14.Text & ("<tr>")
                Label14.Text = Label14.Text & ("<td width='30%'>" & par.IfNull(myReader1("descrizione"), "") & "</td>")
                Label14.Text = Label14.Text & ("<td width='30%'>" & par.IfNull(myReader1("tipologia"), "") & "</td>")
                Label14.Text = Label14.Text & ("<td width='30%'>" & par.IfNull(myReader1("descrizione_tabella"), "") & "</td>")
                Label14.Text = Label14.Text & ("</tr>")
            Loop

            myReader1.Close()

            Label14.Text = Label14.Text & ("</table>")

            myReader1.Close()

            par.cmd.CommandText = " SELECT  DISTINCT utenze.id, utenze.cod_tipologia, ANAGRAFICA_FORNITORI.DESCRIZIONE AS FORNITORE, utenze.contatore, UTENZE.CONTRATTO,UTENZE.DESCRIZIONE  FROM SISCOM_MI.ANAGRAFICA_FORNITORI,SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.UTENZE_TABELLE_MILLESIMALI, SISCOM_MI.UTENZE WHERE UTENZE_TABELLE_MILLESIMALI.ID_TABELLA_MILLESIMALE = TABELLE_MILLESIMALI.ID AND ANAGRAFICA_FORNITORI.ID= UTENZE.ID_FORNITORE   AND UTENZE.ID = UTENZE_TABELLE_MILLESIMALI.ID_UTENZA AND TABELLE_MILLESIMALI.ID_COMPLESSO = " & vIdComplesso & ""

            myReader1 = par.cmd.ExecuteReader()
            'Response.Write("<p><font face='Arial'>UTENZE MILLESIMALI</font></p>")
            Label16.Text = "<table width='100%'><tr><td width='30%'>COD.TIPOLOGIA</td> <td width='30%'>TIPO</td><td width='30%'>DETTAGLI</td></tr>"

            Do While myReader1.Read()
                Label16.Text = Label16.Text & ("<tr>")
                Label16.Text = Label16.Text & ("<td width='30%'>" & par.IfNull(myReader1("descrizione"), "") & "</td>")
                Label16.Text = Label16.Text & ("<td width='30%'>" & par.IfNull(myReader1("tipologia"), "") & "</td>")
                Label16.Text = Label16.Text & ("<td width='30%'>" & par.IfNull(myReader1("descrizione_tabella"), "") & "</td>")
                Label16.Text = Label16.Text & ("</tr>")
            Loop

            myReader1.Close()
            Label14.Text = Label14.Text & ("</table>")
            '*********************CHIUSURA CONNESSIONE**********************

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception

        End Try

    End Sub
End Class
