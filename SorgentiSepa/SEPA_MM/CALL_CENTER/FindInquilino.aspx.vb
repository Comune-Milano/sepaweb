
Partial Class CALL_CENTER_FindInquilino
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()
            If Request.QueryString("T") = "1" Then
                lblTitolo.Text = "Dettagli Individuazione Intestatario Contratto"
            Else
                lblTitolo.Text = "Dettagli Individuazione Chiamante"
            End If
            CaricaLista()

        End If

    End Sub

    Private Sub CaricaLista()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim s As String = ""

            Dim condizioneIntestatario As String = ""
            Dim cod_Contratto As String = ""
            If Request.QueryString("T") = "1" Then
                cod_Contratto = Request.QueryString("COD").ToUpper
                condizioneIntestatario = " AND COD_TIPOLOGIA_OCCUPANTE='INTE' "
            End If

            Dim cognome = Request.QueryString("COGNOME").ToUpper
            Dim nome = Request.QueryString("NOME").ToUpper
            Dim sCompara As String

            If Not String.IsNullOrEmpty(cod_Contratto) Then

                If InStr(cod_Contratto, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(cod_Contratto)
                Else
                    sCompara = " = "
                End If
                s = s & " AND RAPPORTI_UTENZA.COD_CONTRATTO " & sCompara & " '" & par.PulisciStrSql(cod_Contratto) & "' "
            End If

            If Not String.IsNullOrEmpty(cognome) Then

                If InStr(cognome, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(cognome)
                Else
                    sCompara = " = "
                End If
                s = s & " AND ANAGRAFICA.COGNOME " & sCompara & " '" & par.PulisciStrSql(cognome) & "' "
            End If

            If Not String.IsNullOrEmpty(nome) Then

                If InStr(nome, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(nome)
                Else
                    sCompara = " = "
                End If
                s = s & " AND ANAGRAFICA.NOME " & sCompara & " '" & par.PulisciStrSql(nome) & "' "
            End If



            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID AS ID_UNITA,SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AS ID_INTE,ANAGRAFICA.ID, SOGGETTI_CONTRATTUALI.ID_CONTRATTO, " _
                                & "CASE WHEN RAGIONE_SOCIALE IS NULL THEN COGNOME ||' '|| NOME ELSE RAGIONE_SOCIALE END AS NOMINATIVO,siscom_mi.getintestatari (unita_contrattuale.id_contratto) AS intestatario, " _
                                & "TO_CHAR(TO_DATE(SUBSTR(DATA_NASCITA,0,8),'yyyymmdd'),'dd/mm/yyyy')AS DATA_NASCITA, " _
                                & " INDIRIZZI.DESCRIZIONE AS INDIRIZZO ,INDIRIZZI.CIVICO,UNITA_IMMOBILIARI.INTERNO,SCALE_EDIFICI.DESCRIZIONE AS SCALA,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,RAPPORTI_UTENZA.COD_CONTRATTO AS CODICE_CONTRATTO,ID_ANAGRAFICA " _
                                & "FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA ,siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI " _
                                & "WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                & s _
                                & "AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                & "AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                                & "AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA " _
                                & "AND ID_INDIRIZZO = INDIRIZZI.ID " _
                                & "AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID (+) " _
                                & "AND UNITA_IMMOBILIARI.cod_tipologia = TIPOLOGIA_UNITA_IMMOBILIARI.cod " _
                                & condizioneIntestatario _
                                & "/*AND SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO) LIKE '%CORSO%'*/ order by NOMINATIVO ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If dt.Rows.Count >= 1 Then
                DataGridInte.DataSource = dt
                DataGridInte.DataBind()
            Else
                Session.Add("idui", -1)
                Response.Write("<script>alert('Nessuna corrispondenza trovata');self.close();</script>")

            End If


        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        If idSelected.Value > 0 And idAnagrafica.Value > 0 Then
            Session.Add("idui", idSelected.Value)
            Session.Add("idanagrafica", idAnagrafica.Value)
            Response.Write("<script>self.close();</script>")
        End If
    End Sub

    Protected Sub DataGridInte_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridInte.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''} Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & e.Item.Cells(4).Text.Replace("'", "\'") & "';document.getElementById('idSelected').value='" & e.Item.Cells(0).Text.Replace("'", "\'") & "';document.getElementById('idAnagrafica').value='" & e.Item.Cells(2).Text.Replace("'", "\'") & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''} Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & e.Item.Cells(4).Text.Replace("'", "\'") & "';document.getElementById('idSelected').value='" & e.Item.Cells(0).Text.Replace("'", "\'") & "';document.getElementById('idAnagrafica').value='" & e.Item.Cells(2).Text.Replace("'", "\'") & "';")

        End If
    End Sub
End Class
