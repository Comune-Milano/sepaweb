
Partial Class Contratti_Pagamenti_DettRiclassificate
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"

        Response.Write(Loading)
        Response.Flush()

        If Not IsNothing(Request.QueryString("CONN")) Then
            vIdConnessione.Value = Request.QueryString("CONN")
        End If

        If IsNothing(Session.Item("PGMANUALE" & vIdConnessione.Value)) Then
            Me.connData = New CM.datiConnessione(par, False, False)
        Else
            Me.connData = CType(HttpContext.Current.Session.Item("PGMANUALE" & vIdConnessione.Value), CM.datiConnessione)
            par.SettaCommand(par)
        End If

        If Not IsPostBack Then
            idContratto.Value = Request.QueryString("IDCONTR")
            idMor.Value = Request.QueryString("IDBOL")
            RiempiTabella()
            dataPagamento.Value = Request.QueryString("DATAPAG")
            tipoPagamento.Value = Request.QueryString("TIPOPAG")
            numAssegno.Value = Request.QueryString("ASS")
            notePagam.Value = Request.QueryString("NOTE")
            dataRegistr.Value = Request.QueryString("DATAREG")
            vIdConnessione.Value = Request.QueryString("CONN")
            idIncasso.Value = Request.QueryString("IDINC")
            inModifica.Value = Request.QueryString("MODIF")
            If Not IsNothing(Request.QueryString("IMPORTO")) Then
                ImportoIncasso.Value = CDec(Request.QueryString("IMPORTO").ToString.Replace(".", ""))
            End If
            If Not IsNothing(Request.QueryString("TOTPAGATO")) Then
                TotPagato.Value = CDec(Request.QueryString("TOTPAGATO").ToString.Replace(".", ""))
            End If
            If Not IsNothing(Session.Item("idIncassoDaMor")) Then
                idIncEseguito.Value = Session.Item("idIncassoDaMor")
            End If

            AggiustaResiduo()

            If Not String.IsNullOrEmpty(idIncasso.Value) Then
                If ControllaSeInBollVociPag() = "" Then
                    Response.Write("<script>alert('Nessuna bolletta messa in mora è coinvolta nell\'incasso selezionato!');self.close();</script>")
                Else
                    EvidenziaBollRicla()
                End If
            End If
        End If

    End Sub
    Private Sub AggiustaResiduo()

        txtPagResoconto.Text = Format(CDec(ImportoIncasso.Value), "##,##0.00")
        txtSommaSel.Text = Format(CDec(TotPagato.Value), "##,##0.00")
        Me.txtResResoconto.Text = Format(Math.Round(CDec(par.IfEmpty(ImportoIncasso.Value, 0)) - CDec(par.IfEmpty(TotPagato.Value, 0)), 2), "##,##0.00")

    End Sub

    Private Sub EvidenziaBollRicla()
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            End If

            Dim idBolletta As Integer = 0

            If Not String.IsNullOrEmpty(lstBollPag) Then
                'lstBollPag = "(" & Mid(lstBollPag, 1, Len(lstBollPag) - 1) & ")"

                par.cmd.CommandText = "SELECT distinct id_bolletta from siscom_mi.bol_bollette_voci where id in (" & Mid(lstBollPag, 1, Len(lstBollPag) - 1) & ")"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable()
                da.Fill(dt)
                For Each row In dt.Rows
                    For Each ri As DataGridItem In DgvBolRiclass.Items
                        If ri.Cells(par.IndDGC(DgvBolRiclass, "ID")).Text.Replace("&nbsp;", "") = row.item("id_bolletta") Then
                            For j As Integer = 0 To ri.Cells.Count - 1
                                ri.Cells(j).Font.Bold = True
                                ri.Cells(j).ForeColor = Drawing.ColorTranslator.FromHtml("#507CD1")
                            Next
                        End If
                    Next
                Next
            End If
            'connData.chiudi(False)

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: EvidenziaBollRicla - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Function ControllaSeInBollVociPag() As String

        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI WHERE ID_INCASSO_EXTRAMAV=" & idIncasso.Value & " AND ID_VOCE_BOLLETTA IN (SELECT BOL_BOLLETTE_VOCI.ID FROM SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=SISCOM_MI.BOL_BOLLETTE.ID AND ID_BOLLETTA_RIC=" & idMor.Value & ")"
            Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Do While reader.Read
                lstBollPag = lstBollPag & par.IfNull(reader("ID_VOCE_BOLLETTA"), 0) & ","
            Loop
            reader.Close()

            'connData.chiudi(False)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ControllaSeInBollVociPag - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try

        Return lstBollPag
    End Function

    Public Property lstBollPag() As String
        Get
            If Not (ViewState("par_lstBollPag") Is Nothing) Then
                Return CStr(ViewState("par_lstBollPag"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lstBollPag") = value
        End Set

    End Property

    Private Sub RiempiTabella()

        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            End If
            Dim importoTotale As Decimal = 0
            Dim importoPagato As Decimal = 0

            par.cmd.CommandText = "SELECT NUM_BOLLETTA, BOL_BOLLETTE.ID,BOL_BOLLETTE.N_RATA,TO_CHAR(TO_DATE(BOL_BOLLETTE.DATA_EMISSIONE,'yyyymmdd'),'dd/mm/yyyy') AS ""DATA_EMISSIONE"",(SELECT ACRONIMO FROM SISCOM_MI.TIPO_BOLLETTE,SISCOM_MI.BOL_BOLLETTE BOLBOLLETTE WHERE TIPO_BOLLETTE.ID(+)=BOLBOLLETTE.ID_TIPO AND BOL_BOLLETTE.ID=BOLBOLLETTE.ID) as TIPOBOLL,(SELECT ID FROM SISCOM_MI.TIPO_BOLLETTE WHERE TIPO_BOLLETTE.ID(+)=BOL_BOLLETTE.ID_TIPO) as ID_TIPOBOLL, TO_CHAR(TO_DATE(BOL_BOLLETTE.DATA_SCADENZA,'yyyymmdd'),'dd/mm/yyyy') AS ""DATA_SCADENZA"",TRIM(TO_CHAR(NVL(BOL_BOLLETTE.IMPORTO_PAGATO,0),'9G999G999G990D99'))AS IMPORTO_PAGATO,TRIM(TO_CHAR(NVL(BOL_BOLLETTE.IMPORTO_TOTALE,0),'9G999G999G990D99'))AS IMPORTO_TOTALE, (TO_CHAR(TO_DATE(BOL_BOLLETTE.riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' al '||TO_CHAR(TO_DATE(BOL_BOLLETTE.riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS ""RIFERIMENTO"",TRIM(TO_CHAR(NVL(NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_PAGATO,0),0),'9G999G999G990D99')) AS RESIDUO FROM SISCOM_MI.BOL_BOLLETTE WHERE ID=" & idMor.Value
            Dim daM As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtM As New Data.DataTable
            daM.Fill(dtM)
            DataGridMorosita.DataSource = dtM
            DataGridMorosita.DataBind()

            'Dim myReader As Data.OracleClient.OracleDataReader = par.cmd.ExecuteReader
            'If myReader.Read Then
            '    lblMorosita.Text = "Bolletta morosità €." & Format(Math.Abs(par.IfNull(myReader("IMPORTO_TOTALE"), 0)), "##,##0.00") & " pagata per €." & Format(Math.Abs(par.IfNull(myReader("IMPORTO_PAGATO"), 0)), "##,##0.00")
            'End If
            'myReader.Close()

            par.cmd.CommandText = "SELECT bol_bollette.ID, substr(bol_bollette.note,1,15)||'...' as note1,NVL(num_bolletta,'00000000000') AS num_bolletta, " _
                        & "TO_CHAR(TO_DATE(bol_bollette.riferimento_da,'yyyymmdd'),'dd/mm/yyyy') AS riferimento_da, " _
                        & "TO_CHAR(TO_DATE(bol_bollette.riferimento_a,'yyyymmdd'),'dd/mm/yyyy') AS riferimento_a, " _
                        & "TO_CHAR(TO_DATE(bol_bollette.data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS data_emissione, " _
                        & "trim(TO_CHAR((NVL(bol_bollette.importo_totale,0) - NVL(bol_bollette.QUOTA_SIND_B,0)),'9G999G999G999G990D99')) AS importo_totale1,  " _
                        & "trim(TO_CHAR((NVL(bol_bollette.importo_pagato,0)- NVL(bol_bollette.QUOTA_SIND_PAGATA_B,0)),'9G999G999G999G990D99')) AS importo_pagato1 ,(NVL(bol_bollette.importo_totale,0) - NVL(bol_bollette.QUOTA_SIND_B,0)-(NVL(bol_bollette.importo_pagato,0)- NVL(bol_bollette.QUOTA_SIND_PAGATA_B,0))) AS importo_residuo," _
                        & "TO_CHAR(TO_DATE(bol_bollette.data_scadenza,'yyyymmdd'),'dd/mm/yyyy')AS data_scadenza,TO_CHAR(TO_DATE(bol_bollette.data_pagamento,'yyyymmdd'),'dd/mm/yyyy')AS datapag,  " _
                        & "TIPO_BOLLETTE.ACRONIMO from siscom_mi.bol_bollette,SISCOM_MI.TIPO_BOLLETTE where id_contratto=" & idContratto.Value & " and fl_annullata=0 and id_tipo<> 22 and " _
                        & " BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID (+) AND NVL (id_rateizzazione, 0) = 0 AND id_bolletta_ric =" & idMor.Value & " order by bol_bollette.data_scadenza asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt0 As New Data.DataTable
            da.Fill(dt0)

            DgvBolRiclass.DataSource = dt0
            DgvBolRiclass.DataBind()
            lblNumBoll.Text = "Tot. bollette riclassificate: " & dt0.Rows.Count

            'connData.chiudi(False)

        Catch ex As Exception
            Response.Write("<script>alert('Errore nel caricamento delle bollette!');self.close();</script>")
        End Try

    End Sub

    Protected Sub DgvBolRiclass_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DgvBolRiclass.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmiaBol').value='Hai selezionato la bolletta n°: " & e.Item.Cells(1).Text & "';document.getElementById('idBolletta').value='" & e.Item.Cells(0).Text & "';document.getElementById('numBolletta').value='" & e.Item.Cells(1).Text & "';")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmiaBol').value='Hai selezionato la bolletta n°: " & e.Item.Cells(1).Text & "';document.getElementById('idBolletta').value='" & e.Item.Cells(0).Text & "';document.getElementById('numBolletta').value='" & e.Item.Cells(1).Text & "';")
        End If
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        RiempiTabella()
        EvidenziaBollRicla()
        idBolletta.Value = ""
        idIncEseguito.Value = Session.Item("idIncassoDaMor")
        If Not IsNothing(Session.Item("PagInMor")) Then
            If CDec(Session.Item("PagInMor")) <> CDec(oldPagInMor.Value) Then
                TotPagato.Value = CDec(par.IfEmpty(TotPagato.Value, 0)) + (CDec(Session.Item("PagInMor")) - CDec(oldPagInMor.Value))
                oldPagInMor.Value = Session.Item("PagInMor")
                AggiustaResiduo()
            End If
        End If
    End Sub

End Class
