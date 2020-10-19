
Partial Class ARPA_LOMBARDIA_Anomalie
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            HFGriglia.Value = RadGridAnomalie.ClientID.ToString.Replace("ctl00", "MasterOpen")
            Select Case Request.QueryString("T").ToString
                Case "1"
                    lblTitolo.Text = "Anomalia sui dati catastali (Foglio, Particella, Sub) delle UI"
                Case "2"
                    lblTitolo.Text = "Anomalia sulla categoria catastale delle UI"
                Case "3"
                    lblTitolo.Text = "Anomalia sulla consistenza (Numero Vani) delle UI"
                Case "4"
                    lblTitolo.Text = "Anomalia sulla Rendita delle UI"
                Case "5"
                    lblTitolo.Text = "Anomalia sugli indirizzi delle UI"
                Case "6"
                    lblTitolo.Text = "Anomalia sul piano delle UI"
                Case "7"
                    lblTitolo.Text = "Anomalia sulla superficie netta delle UI"
                Case "8"
                    lblTitolo.Text = "Anomalia sulla destinazione d'uso LR delle UI"
                Case "9"
                    lblTitolo.Text = "Anomalia sui dati catastali (Foglio, Particella, Sub non numerici) delle UI"
                Case "10"
                    lblTitolo.Text = "Anomalia sulla consistenza (Numero Vani) delle UI. Valori ammissibili: 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10"
                Case "11"
                    lblTitolo.Text = "Anomalia sugli indirizzi delle UI (Località)"
                Case "12"
                    lblTitolo.Text = "Anomalia sui dati catastali (Foglio, Particella, Sub non numerici interi) delle UI"
            End Select
            HFTipoGestione.Value = Request.QueryString("T")
        End If
    End Sub
    Protected Sub RadGridAnomalie_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridAnomalie.NeedDataSource
        Try
            Dim Query As String = ""
            Select Case HFTipoGestione.Value.ToString
                Case "1"
                    Query = "SELECT UNITA_IMMOBILIARI.ID, '<a href=" & Chr(34) & "javascript:ApriAnomalieUI(' || UNITA_IMMOBILIARI.ID || ');" & Chr(34) & ">' || UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE || '</a>' AS COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'Foglio e/o Particella e/o Subalterno mancante' AS MOTIVAZIONE_ERRORE, '' AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND (IDENTIFICATIVI_CATASTALI.FOGLIO IS NULL OR IDENTIFICATIVI_CATASTALI.NUMERO IS NULL OR IDENTIFICATIVI_CATASTALI.SUB IS NULL) " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "2"
                    Query = "SELECT UNITA_IMMOBILIARI.ID, '<a href=" & Chr(34) & "javascript:ApriAnomalieUI(' || UNITA_IMMOBILIARI.ID || ');" & Chr(34) & ">' || UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE || '</a>' AS COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'La categoria catastale non può essere vuota o uguale a 000' AS MOTIVAZIONE_ERRORE, COD_CATEGORIA_CATASTALE AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND (COD_CATEGORIA_CATASTALE IS NULL OR COD_CATEGORIA_CATASTALE = '000') " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "3"
                    Query = "SELECT UNITA_IMMOBILIARI.ID, '<a href=" & Chr(34) & "javascript:ApriAnomalieUI(' || UNITA_IMMOBILIARI.ID || ');" & Chr(34) & ">' || UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE || '</a>' AS COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'Il numero dei vani non può essere vuoto o uguale a 0' AS MOTIVAZIONE_ERRORE, NUM_VANI AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND (NUM_VANI IS NULL OR NVL(NUM_VANI, 0) = 0) " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "4"
                    Query = "SELECT UNITA_IMMOBILIARI.ID, '<a href=" & Chr(34) & "javascript:ApriAnomalieUI(' || UNITA_IMMOBILIARI.ID || ');" & Chr(34) & ">' || UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE || '</a>' AS COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'La rendita non può essere vuota o uguale a 0' AS MOTIVAZIONE_ERRORE, RENDITA AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND (RENDITA IS NULL OR NVL(RENDITA, 0) = 0) " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "5"
                    Query = "SELECT UNITA_IMMOBILIARI.ID, '<a href=" & Chr(34) & "javascript:ApriAnomalieUI(' || UNITA_IMMOBILIARI.ID || ');" & Chr(34) & ">' || UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE || '</a>' AS COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'L''indirizzo non può essere vuoto' AS MOTIVAZIONE_ERRORE, INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID(+) = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND UNITA_IMMOBILIARI.ID_INDIRIZZO IS NULL " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "6"
                    Query = "SELECT UNITA_IMMOBILIARI.ID, '<a href=" & Chr(34) & "javascript:ApriAnomalieUI(' || UNITA_IMMOBILIARI.ID || ');" & Chr(34) & ">' || UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE || '</a>' AS COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'Il piano non può essere vuoto' AS MOTIVAZIONE_ERRORE, '' AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID(+) = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND COD_TIPO_LIVELLO_PIANO IS NULL " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "7"
                    Query = "SELECT UNITA_IMMOBILIARI.ID, '<a href=" & Chr(34) & "javascript:ApriAnomalieUI(' || UNITA_IMMOBILIARI.ID || ');" & Chr(34) & ">' || UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE || '</a>' AS COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'La superficie netta nelle dimensioni delle UI non può essere vuota' AS MOTIVAZIONE_ERRORE, '' AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID(+) = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND NOT EXISTS (SELECT ID_UNITA_IMMOBILIARE FROM " & CType(Me.Master, Object).StringaSiscom & "DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA' AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID) " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "8"
                    Query = "SELECT UNITA_IMMOBILIARI.ID, '<a href=" & Chr(34) & "javascript:ApriAnomalieUI(' || UNITA_IMMOBILIARI.ID || ');" & Chr(34) & ">' || UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE || '</a>' AS COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'La destinazione d''uso RL non può essere vuota' AS MOTIVAZIONE_ERRORE, '' AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID(+) = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND ID_DESTINAZIONE_USO_RL IS NULL " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "9"
                    Query = "SELECT UNITA_IMMOBILIARI.ID, '<a href=" & Chr(34) & "javascript:ApriAnomalieUI(' || UNITA_IMMOBILIARI.ID || ');" & Chr(34) & ">' || UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE || '</a>' AS COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'Foglio, Particella e Subalterno devono essere dei dati numerici' AS MOTIVAZIONE_ERRORE, FOGLIO || '- ' || NUMERO || ' - ' || SUB AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND (" & CType(Me.Master, Object).StringaSiscom & "IS_NUMERIC(IDENTIFICATIVI_CATASTALI.FOGLIO) = 0 OR " & CType(Me.Master, Object).StringaSiscom & "IS_NUMERIC(IDENTIFICATIVI_CATASTALI.NUMERO) = 0 OR " & CType(Me.Master, Object).StringaSiscom & "IS_NUMERIC(IDENTIFICATIVI_CATASTALI.SUB) = 0) " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "10"
                    Query = "SELECT UNITA_IMMOBILIARI.ID, '<a href=" & Chr(34) & "javascript:ApriAnomalieUI(' || UNITA_IMMOBILIARI.ID || ');" & Chr(34) & ">' || UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE || '</a>' AS COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'Valori ammissibili: 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10' AS MOTIVAZIONE_ERRORE, NUM_VANI AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID(+) = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND NVL(NUM_VANI, 0) NOT IN (1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10) " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "11"
                    Query = "SELECT UNITA_IMMOBILIARI.ID, '<a href=" & Chr(34) & "javascript:ApriAnomalieUI(' || UNITA_IMMOBILIARI.ID || ');" & Chr(34) & ">' || UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE || '</a>' AS COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'La località dell''indirizzo non può essere vuota' AS MOTIVAZIONE_ERRORE, LOCALITA AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID(+) = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND UNITA_IMMOBILIARI.ID_INDIRIZZO IS NOT NULL AND LOCALITA IS NULL " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "12"
                    Query = "SELECT UNITA_IMMOBILIARI.ID, '<a href=" & Chr(34) & "javascript:ApriAnomalieUI(' || UNITA_IMMOBILIARI.ID || ');" & Chr(34) & ">' || UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE || '</a>' AS COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'Foglio, Particella e Subalterno devono essere dei dati numerici interi' AS MOTIVAZIONE_ERRORE, FOGLIO || '- ' || NUMERO || ' - ' || SUB AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND (FOGLIO LIKE '%.%' OR FOGLIO LIKE '%,%' OR NUMERO LIKE '%,%' OR NUMERO LIKE '%,%' OR SUB LIKE '%,%' OR SUB LIKE '%,%') " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
            End Select
            RadGridAnomalie.DataSource = par.getDataTableGrid(Query)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_Anomalie - RadGridAnomalie_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Try
            Dim Query As String = ""
            Select Case HFTipoGestione.Value.ToString
                Case "1"
                    Query = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'Foglio e/o Particella e/o Subalterno mancante' AS MOTIVAZIONE_ERRORE, '' AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND (IDENTIFICATIVI_CATASTALI.FOGLIO IS NULL OR IDENTIFICATIVI_CATASTALI.NUMERO IS NULL OR IDENTIFICATIVI_CATASTALI.SUB IS NULL) " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "2"
                    Query = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'La categoria catastale non può essere vuota o uguale a 000' AS MOTIVAZIONE_ERRORE, COD_CATEGORIA_CATASTALE AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND (COD_CATEGORIA_CATASTALE IS NULL OR COD_CATEGORIA_CATASTALE = '000') " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "3"
                    Query = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'Il numero dei vani non può essere vuoto o uguale a 0' AS MOTIVAZIONE_ERRORE, NUM_VANI AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND (NUM_VANI IS NULL OR NVL(NUM_VANI, 0) = 0) " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "4"
                    Query = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'La rendita non può essere vuota o uguale a 0' AS MOTIVAZIONE_ERRORE, RENDITA AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND (RENDITA IS NULL OR NVL(RENDITA, 0) = 0) " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "5"
                    Query = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'L''indirizzo non può essere vuoto' AS MOTIVAZIONE_ERRORE, INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID(+) = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND UNITA_IMMOBILIARI.ID_INDIRIZZO IS NULL " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "6"
                    Query = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'Il piano non può essere vuoto' AS MOTIVAZIONE_ERRORE, '' AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID(+) = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND COD_TIPO_LIVELLO_PIANO IS NULL " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "7"
                    Query = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                           & "'La superficie netta nelle dimensioni delle UI non può essere vuota' AS MOTIVAZIONE_ERRORE, '' AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID(+) = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND NOT EXISTS (SELECT ID_UNITA_IMMOBILIARE FROM " & CType(Me.Master, Object).StringaSiscom & "DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA' AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID) " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "8"
                    Query = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'La destinazione d''uso RL non può essere vuota' AS MOTIVAZIONE_ERRORE, '' AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID(+) = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND ID_DESTINAZIONE_USO_RL IS NULL " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "9"
                    Query = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'Foglio, Particella e Subalterno devono essere dei dati numerici' AS MOTIVAZIONE_ERRORE, FOGLIO || '- ' || NUMERO || ' - ' || SUB AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND (" & CType(Me.Master, Object).StringaSiscom & "IS_NUMERIC(IDENTIFICATIVI_CATASTALI.FOGLIO) = 0 OR " & CType(Me.Master, Object).StringaSiscom & "IS_NUMERIC(IDENTIFICATIVI_CATASTALI.NUMERO) = 0 OR " & CType(Me.Master, Object).StringaSiscom & "IS_NUMERIC(IDENTIFICATIVI_CATASTALI.SUB) = 0) " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "10"
                    Query = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'Valori ammissibili: 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10' AS MOTIVAZIONE_ERRORE, NUM_VANI AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID(+) = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND NVL(NUM_VANI, 0) NOT IN (1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10) " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "11"
                    Query = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'La località dell''indirizzo non può essere vuota' AS MOTIVAZIONE_ERRORE, LOCALITA AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID(+) = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND UNITA_IMMOBILIARI.ID_INDIRIZZO IS NOT NULL AND LOCALITA IS NULL " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
                Case "12"
                    Query = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS PARTICELLA, IDENTIFICATIVI_CATASTALI.SUB AS SUBALTERNO, " _
                          & "'Foglio, Particella e Subalterno devono essere dei dati numerici interi' AS MOTIVAZIONE_ERRORE, FOGLIO || '- ' || NUMERO || ' - ' || SUB AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI, " & CType(Me.Master, Object).StringaSiscom & "IDENTIFICATIVI_CATASTALI, " _
                          & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                          & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                          & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                          & "AND (FOGLIO LIKE '%.%' OR FOGLIO LIKE '%,%' OR NUMERO LIKE '%,%' OR NUMERO LIKE '%,%' OR SUB LIKE '%,%' OR SUB LIKE '%,%') " _
                          & "ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"
            End Select
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Query, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                Dim xls As New ExcelSiSol
                Dim nomeExcel As String = ""
                Dim nomeWorkSheet As String = ""
                Select Case HFTipoGestione.Value.ToString
                    Case "1"
                        nomeExcel = "ExportAnomalieCatastaliUI"
                        nomeWorkSheet = "AnomalieCatastaliUI"
                    Case "2"
                        nomeExcel = "ExportAnomalieCategoriaCatastaliUI"
                        nomeWorkSheet = "AnomalieCategoriaCatastaliUI"
                    Case "3"
                        nomeExcel = "ExportAnomalieConsistenzaUI"
                        nomeWorkSheet = "AnomalieConsistenzaUI"
                    Case "4"
                        nomeExcel = "ExportAnomalieRenditaUI"
                        nomeWorkSheet = "AnomalieRenditaUI"
                    Case "5"
                        nomeExcel = "ExportAnomalieIndirizziUI"
                        nomeWorkSheet = "AnomalieIndirizziUI"
                    Case "6"
                        nomeExcel = "ExportAnomaliePianiUI"
                        nomeWorkSheet = "AnomaliePianiUI"
                    Case "7"
                        nomeExcel = "ExportAnomalieSuperficiUI"
                        nomeWorkSheet = "AnomalieSuperficiUI"
                    Case "8"
                        nomeExcel = "ExportAnomalieDestUsoLRUI"
                        nomeWorkSheet = "AnomalieDestUsoLRUI"
                    Case "9"
                        nomeExcel = "ExportAnomalieCatastaliNumericiUI"
                        nomeWorkSheet = "AnomalieCatastaliNumericiUI"
                    Case "10"
                        nomeExcel = "ExportAnomalieConsistenzaNumUI"
                        nomeWorkSheet = "AnomalieConsistenzaNumUI"
                    Case "11"
                        nomeExcel = "ExportAnomalieIndirizziLocalitaUI"
                        nomeWorkSheet = "AnomalieIndirizziLocalitaUI"
                    Case "12"
                        nomeExcel = "ExportAnomalieCatastaliNumericiInteriUI"
                        nomeWorkSheet = "AnomalieCatastaliNumericiInteriUI"
                End Select
                Dim nomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, nomeExcel, nomeWorkSheet, dt)
                If System.IO.File.Exists(Server.MapPath("../FileTemp/" & nomeFile)) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                Else
                    RadNotificationNote.Text = "Errore durante l'Export. Riprovare!!"
                    RadNotificationNote.Show()
                End If
            Else
                RadNotificationNote.Text = par.Messaggio_NoExport
                RadNotificationNote.Show()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_Anomalie - btnExport_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
