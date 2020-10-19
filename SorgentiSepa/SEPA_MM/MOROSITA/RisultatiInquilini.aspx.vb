'*** LISTA RISULTATO MOROSITA Proviene da : RicercaMorosita.aspx

Partial Class MOROSITA_RisultatiInquilini
    Inherits PageSetIdMode

    Dim par As New CM.Global

    Public sStringaSql As String

    Public sValoreStrutturaAler As String

    Public sValoreCognome As String
    Public sValoreNome As String

    Public sValoreStato As String

    Public sOrdinamento As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""Portale.aspx""</script>")
        End If

        If Not IsPostBack Then

            '*LBLID.Text = Request.QueryString("T")

            ' sValoreStrutturaAler = Request.QueryString("FI")    'FILIALE (STRUTTURA)

            sValoreCognome = Request.QueryString("CG")
            sValoreNome = Request.QueryString("NM")
            sValoreStato = Request.QueryString("TI")

            sOrdinamento = Request.QueryString("ORD")


            Cerca()
            BindGrid()

        End If

    End Sub


    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Private Sub Cerca()
        'Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String


        sStringaSql = ""

        If sValoreCognome <> "" Then
            sValore = Strings.UCase(sValoreCognome)
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            sStringaSql = "  and MOROSITA_LETTERE.ID_ANAGRAFICA in (select ID from SISCOM_MI.ANAGRAFICA " _
                                                                    & "  where ( COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' or RAGIONE_SOCIALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "')"
            If sValoreNome <> "" Then

                sValore = Strings.UCase(sValoreNome)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " and ANAGRAFICA.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            sStringaSql = sStringaSql & ")"

        ElseIf sValoreNome <> "" Then
            sValore = Strings.UCase(sValoreNome)
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If

            sStringaSql = sStringaSql & "  and MOROSITA_LETTERE.ID_ANAGRAFICA in (select ID from SISCOM_MI.ANAGRAFICA " _
                                                                    & "  where NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "') "
        End If


        If sValoreStato <> "" Then
            sStringaSql = sStringaSql & "  and MOROSITA_LETTERE.COD_STATO='" & par.PulisciStrSql(sValoreStato) & "' "
        End If


        sStringaSQL1 = "select distinct MOROSITA_LETTERE.ID_MOROSITA as ID,RAPPORTI_UTENZA.ID as ID_CONTRATTO," _
                         & "  RAPPORTI_UTENZA.COD_CONTRATTO," _
                         & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                            & "     then  trim(RAGIONE_SOCIALE) " _
                            & "     else  RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO, " _
                         & " RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC," _
                         & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                         & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE," _
                    & " INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO""," _
                    & " INDIRIZZI.CIVICO,(SELECT NOME FROM COMUNI_NAZIONI WHERE COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                    & " UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
          & " from  " _
                & " SISCOM_MI.MOROSITA_LETTERE," _
                & " SISCOM_MI.COMPLESSI_IMMOBILIARI," _
                & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE," _
                & " SISCOM_MI.RAPPORTI_UTENZA, " _
                & " SISCOM_MI.ANAGRAFICA," _
                & " SISCOM_MI.INDIRIZZI," _
                & " SISCOM_MI.EDIFICI," _
                & " SISCOM_MI.UNITA_CONTRATTUALE," _
                & " SISCOM_MI.UNITA_IMMOBILIARI," _
                & " SISCOM_MI.SOGGETTI_CONTRATTUALI " _
        & " where  " _
        & "       EDIFICI.ID_COMPLESSO                      =COMPLESSI_IMMOBILIARI.ID " _
        & "  and  UNITA_IMMOBILIARI.ID_EDIFICIO             =EDIFICI.ID" _
        & "  and  UNITA_IMMOBILIARI.ID_INDIRIZZO            =INDIRIZZI.ID (+) " _
        & "  and  UNITA_CONTRATTUALE.ID_UNITA               =UNITA_IMMOBILIARI.ID " _
        & "  and  UNITA_CONTRATTUALE.ID_CONTRATTO           =RAPPORTI_UTENZA.ID " _
        & "  and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO        =RAPPORTI_UTENZA.ID " _
        & "  and  RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  =TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
        & "  and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA       =ANAGRAFICA.ID" _
        & "  and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE'  " _
        & "  and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE     is null  " _
        & "  and RAPPORTI_UTENZA.ID=SISCOM_MI.MOROSITA_LETTERE.ID_CONTRATTO "

        sStringaSQL1 = sStringaSQL1 & sStringaSql & "  order by INTESTATARIO"


    End Sub

    Private Sub BindGrid()

        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds) ', "DOMANDE_BANDO,COMP_NUCLEO")

            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            Label1.Text = " " & ds.Tables(0).Rows.Count


            da.Dispose()
            ds.Dispose()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound


        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'inquilino: " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtid_contratto').value='" & e.Item.Cells(1).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'inquilino: " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtid_contratto').value='" & e.Item.Cells(1).Text & "'")

        End If

    End Sub


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        Else
            'Session.Add("ID", txtid.Text)

            sValoreStrutturaAler = Request.QueryString("FI")    'FILIALE (STRUTTURA)


            sValoreCognome = Request.QueryString("CG")
            sValoreNome = Request.QueryString("NM")
            sValoreStato = Request.QueryString("TI")

            sOrdinamento = Request.QueryString("ORD")



            Response.Write("<script>location.replace('MorositaInquilino.aspx?ID_MOR=" & Me.txtid.Text _
                                                                & "&CON=" & Me.txtid_contratto.Text _
                                                                & "&TI=" & sValoreStato _
                                                                & "&CG=" & par.VaroleDaPassare(sValoreCognome) _
                                                                & "&NM=" & par.VaroleDaPassare(sValoreNome) _
                                                                & "&PROV=RICERCA_INQUILINO" _
                                                                & "&ORD=" & sOrdinamento _
                                                    & "');</script>")


        End If

    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click

        Response.Write("<script>document.location.href=""RicercaInquilino.aspx""</script>")

    End Sub

   
End Class
