Imports System.IO


Partial Class Contratti_ElencoPagamenti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String
    Dim scriptblock As String
    Dim dt As New System.Data.DataTable


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            LBLID.Value = Request.QueryString("id")
            lblCodUfficio.Value = Request.QueryString("UF")
            lblNumReg.Value = Request.QueryString("NR")
            DatiContratto()
            Cerca()
            Cerca1()
            Cerca2()
            CERCA3()
        End If
    End Sub

    Private Function DatiContratto()
        If LBLID.Value <> "-1" Then
            Try

                Label3.Text = ""
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "select cod_contratto,siscom_mi.getintestatari(id) as intestatario From siscom_mi.rapporti_utenza where id=" & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Label3.Text = "Codice Contratto: " & par.IfNull(myReader("cod_contratto"), "") & " intestato a: " & Replace(par.IfNull(myReader("intestatario"), ""), ";", "")
                End If
                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Label3.Text = ex.Message
            End Try
        Else
            Label3.Text = ""
        End If

    End Function

    Private Function Cerca()
        Dim bTrovato As Boolean

        bTrovato = False
        sStringaSql = ""
        If LBLID.Value <> "-1" Then
            sStringaSQL1 = "SELECT (case when length(data_creazione)=8 then to_char(to_date(SUBSTR(DATA_CREAZIONE,1,8),'yyyymmdd'),'dd/mm/yyyy') else to_char(to_date(SUBSTR(DATA_CREAZIONE,1,8),'yyyymmdd'),'dd/mm/yyyy')||' - '||SUBSTR(DATA_CREAZIONE,9,2)||':'||SUBSTR(DATA_CREAZIONE,11,2)  end) AS DATA_GENERAZIONE,NVL(DATA_CREAZIONE,'19000101') AS DT1, RAPPORTI_UTENZA.COD_CONTRATTO,SISCOM_MI.GETINTESTATARI(RAPPORTI_UTENZA.ID) AS INTESTATARIO, to_char(to_date(DATA_RICEVUTA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_RICEVUTA,ID_CONTRATTO,ANNO,COD_TRIBUTO,to_char(to_date(DATA_AE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI,FILE_SCARICATO,RAPPORTI_UTENZA_IMPOSTE.NOTE,TAB_FASI_REGISTRAZIONE.DESCRIZIONE AS STATO,TAB_FASI_REGISTRAZIONE.id as ID_STATO_REGISTRAZIONE FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE,SISCOM_MI.TAB_FASI_REGISTRAZIONE,SISCOM_MI.RAPPORTI_UTENZA WHERE RAPPORTI_UTENZA.ID=RAPPORTI_UTENZA_IMPOSTE.ID_CONTRATTO AND TAB_FASI_REGISTRAZIONE.ID=RAPPORTI_UTENZA_IMPOSTE.ID_FASE_REGISTRAZIONE AND ID_CONTRATTO=" & LBLID.Value
            sStringaSQL1 = sStringaSQL1 & " ORDER BY DT1 DESC"
        Else
            sStringaSQL1 = "SELECT (case when length(data_creazione)=8 then to_char(to_date(SUBSTR(DATA_CREAZIONE,1,8),'yyyymmdd'),'dd/mm/yyyy') else to_char(to_date(SUBSTR(DATA_CREAZIONE,1,8),'yyyymmdd'),'dd/mm/yyyy')||' - '||SUBSTR(DATA_CREAZIONE,9,2)||':'||SUBSTR(DATA_CREAZIONE,11,2)  end) AS DATA_GENERAZIONE,NVL(DATA_CREAZIONE,'19000101') AS DT1, RAPPORTI_UTENZA.COD_CONTRATTO,SISCOM_MI.GETINTESTATARI(RAPPORTI_UTENZA.ID) AS INTESTATARIO,to_char(to_date(DATA_RICEVUTA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_RICEVUTA,ID_CONTRATTO,ANNO,COD_TRIBUTO,to_char(to_date(DATA_AE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI,FILE_SCARICATO,RAPPORTI_UTENZA_IMPOSTE.NOTE,TAB_FASI_REGISTRAZIONE.DESCRIZIONE AS STATO,TAB_FASI_REGISTRAZIONE.id as ID_STATO_REGISTRAZIONE FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE,SISCOM_MI.TAB_FASI_REGISTRAZIONE,SISCOM_MI.RAPPORTI_UTENZA WHERE RAPPORTI_UTENZA.ID=RAPPORTI_UTENZA_IMPOSTE.ID_CONTRATTO AND TAB_FASI_REGISTRAZIONE.ID=RAPPORTI_UTENZA_IMPOSTE.ID_FASE_REGISTRAZIONE AND  ID_CONTRATTO=(select id from SISCOM_MI.rapporti_utenza where to_number(num_registrazione)=" & Val(lblNumReg.Value) & " and upper(RAPPORTI_UTENZA_IMPOSTE.cod_ufficio_reg)='" & UCase(lblCodUfficio.Value) & "')"
            sStringaSQL1 = sStringaSQL1 & " ORDER BY DT1 DESC"
        End If

        BindGrid()
    End Function

    Private Function Cerca2()
        Dim bTrovato As Boolean

        bTrovato = False
        sStringaSql = ""

        If LBLID.Value <> "-1" Then
            sStringaSQL2 = "SELECT (CASE WHEN RICEVUTA IS NULL THEN '' ELSE replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||RICEVUTA||''','''','''');£>'||'Visuliazza'||'</a>','$','&'),'£','') END) AS RICEVUTA,(CASE WHEN RICEVUTA IS NULL THEN '' ELSE replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||QUIETANZA||''','''','''');£>'||'Visuliazza'||'</a>','$','&'),'£','') END) AS QUIETANZA,TRIM(TO_CHAR(IMPORTO,'9G999G999G999G999G990D99')) AS IMPORTO,RAPPORTI_UTENZA_AVVISI_LIQ.ID,TAB_COD_TRIBUTO.CODICE,TRIM(TO_CHAR(SANZIONI,'9G999G999G999G999G990D99')) as SANZIONI,TRIM(TO_CHAR(INTERESSI,'9G999G999G999G999G990D99')) AS INTERESSI,TRIM(TO_CHAR(SPESE_NOTIFICA,'9G999G999G999G999G990D99')) AS SPESE_NOTIFICA,to_char(to_date(DATA_PG,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PG,to_char(to_date(DATA_PAG,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PAG,NOTE,IMPORTO+SANZIONI+INTERESSI+SPESE_NOTIFICA as totale FROM SISCOM_MI.RAPPORTI_UTENZA_AVVISI_LIQ,SISCOM_MI.TAB_COD_TRIBUTO WHERE TAB_COD_TRIBUTO.ID=RAPPORTI_UTENZA_AVVISI_LIQ.IMPOSTA AND ID_CONTRATTO=" & LBLID.Value
            sStringaSQL2 = sStringaSQL2 & " ORDER BY  RAPPORTI_UTENZA_AVVISI_LIQ.ID DESC"
        Else
            sStringaSQL2 = "SELECT (CASE WHEN RICEVUTA IS NULL THEN '' ELSE replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||RICEVUTA||''','''','''');£>'||'Visuliazza'||'</a>','$','&'),'£','') END) AS RICEVUTA,(CASE WHEN RICEVUTA IS NULL THEN '' ELSE replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||QUIETANZA||''','''','''');£>'||'Visuliazza'||'</a>','$','&'),'£','') END) AS QUIETANZA,TRIM(TO_CHAR(IMPORTO,'9G999G999G999G999G990D99')) AS IMPORTO,RAPPORTI_UTENZA_AVVISI_LIQ.ID,TAB_COD_TRIBUTO.CODICE,TRIM(TO_CHAR(SANZIONI,'9G999G999G999G999G990D99')) as SANZIONI,TRIM(TO_CHAR(INTERESSI,'9G999G999G999G999G990D99')) AS INTERESSI,TRIM(TO_CHAR(SPESE_NOTIFICA,'9G999G999G999G999G990D99')) AS SPESE_NOTIFICA,to_char(to_date(DATA_PG,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PG,to_char(to_date(DATA_PAG,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PAG,NOTE,IMPORTO+SANZIONI+INTERESSI+SPESE_NOTIFICA as totale FROM SISCOM_MI.RAPPORTI_UTENZA_AVVISI_LIQ,SISCOM_MI.TAB_COD_TRIBUTO WHERE TAB_COD_TRIBUTO.ID=RAPPORTI_UTENZA_AVVISI_LIQ.IMPOSTA AND ID_CONTRATTO=(select id from SISCOM_MI.rapporti_utenza where to_number(num_registrazione)=" & Val(lblNumReg.Value) & " and upper(cod_ufficio_reg)='" & UCase(lblCodUfficio.Value) & "')"
            sStringaSQL2 = sStringaSQL2 & " ORDER BY RAPPORTI_UTENZA_AVVISI_LIQ.ID DESC"


        End If

        BindGrid2()

    End Function
    Private Function Cerca1()
        Dim bTrovato As Boolean

        bTrovato = False
        sStringaSql = ""

        If LBLID.Value <> "-1" Then
            sStringaSQL2 = "SELECT (case when DATA_INSERIMENTO IS NULL then '' else to_char(to_date(SUBSTR(DATA_INSERIMENTO,1,8),'yyyymmdd'),'dd/mm/yyyy')||' - '||SUBSTR(DATA_INSERIMENTO,9,2)||':'||SUBSTR(DATA_INSERIMENTO,11,2)  end) AS DATA_INSERIMENTO,pg_ae,to_char(to_date(DATA_REGISTRAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_AE,ID_CONTRATTO,NVL(ANNO,'') AS ANNO,substr(VALIDA_FINO_AL,1,4) AS VALIDA_FINO_AL,COD_TRIBUTO,REGISTRO,SOSTITUTIVA,INTERESSI,SANZIONI,TOTALE," _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''DownloadREL.aspx?V='||NOME_FILE_REL||''',''REL'','''');£>'||'Visuliazza REL'||'</a>','$','&'),'£','" & Chr(34) & "') as  NOME_FILE_REL, " _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||NOME_FILE_PDF||''','''','''');£>'||'Visuliazza PDF'||'</a>','$','&'),'£','" & Chr(34) & "') as  NOME_FILE_PDF,NOTE " _
                & " FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ID_CONTRATTO=" & LBLID.Value
            sStringaSQL2 = sStringaSQL2 & " ORDER BY DATA_INSERIMENTO DESC"
        Else
            sStringaSQL2 = "SELECT (case when DATA_INSERIMENTO IS NULL then '' else to_char(to_date(SUBSTR(DATA_INSERIMENTO,1,8),'yyyymmdd'),'dd/mm/yyyy')||' - '||SUBSTR(DATA_INSERIMENTO,9,2)||':'||SUBSTR(DATA_INSERIMENTO,11,2)  end) AS DATA_INSERIMENTO,pg_ae,to_char(to_date(DATA_REGISTRAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_AE,ID_CONTRATTO,NVL(ANNO,'') AS ANNO,substr(VALIDA_FINO_AL,1,4) AS VALIDA_FINO_AL,COD_TRIBUTO,REGISTRO,SOSTITUTIVA,INTERESSI,SANZIONI,TOTALE," _
               & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''DownloadREL.aspx?V='||NOME_FILE_REL||''',''REL'','''');£>'||'Visuliazza REL'||'</a>','$','&'),'£','" & Chr(34) & "') as  NOME_FILE_REL, " _
               & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||NOME_FILE_PDF||''','''','''');£>'||'Visuliazza PDF'||'</a>','$','&'),'£','" & Chr(34) & "') as  NOME_FILE_PDF,NOTE " _
               & " FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ID_CONTRATTO=(select id from SISCOM_MI.rapporti_utenza where to_number(num_registrazione)=" & Val(lblNumReg.Value) & " and upper(cod_ufficio_reg)='" & UCase(lblCodUfficio.Value) & "')"
        End If
        'If LBLID.Value <> "-1" Then
        '    sStringaSQL2 = "SELECT (case when DATA_INSERIMENTO IS NULL then '' else to_char(to_date(SUBSTR(DATA_INSERIMENTO,1,8),'yyyymmdd'),'dd/mm/yyyy')||' - '||SUBSTR(DATA_INSERIMENTO,9,2)||':'||SUBSTR(DATA_INSERIMENTO,11,2)  end) AS DATA_INSERIMENTO,pg_ae,to_char(to_date(DATA_REGISTRAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_AE,ID_CONTRATTO,NVL(ANNO,'') AS ANNO,substr(VALIDA_FINO_AL,1,4) AS VALIDA_FINO_AL,COD_TRIBUTO,REGISTRO,SOSTITUTIVA,INTERESSI,SANZIONI,TOTALE," _
        '        & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||NOME_FILE_REL||''','''','''');£>'||'Visuliazza REL'||'</a>','$','&'),'£','" & Chr(34) & "') as  NOME_FILE_REL, " _
        '        & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||NOME_FILE_PDF||''','''','''');£>'||'Visuliazza PDF'||'</a>','$','&'),'£','" & Chr(34) & "') as  NOME_FILE_PDF,NOTE " _
        '        & " FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ID_CONTRATTO=" & LBLID.Value
        '    sStringaSQL2 = sStringaSQL2 & " ORDER BY DATA_INSERIMENTO DESC"
        'Else
        '    sStringaSQL2 = "SELECT (case when DATA_INSERIMENTO IS NULL then '' else to_char(to_date(SUBSTR(DATA_INSERIMENTO,1,8),'yyyymmdd'),'dd/mm/yyyy')||' - '||SUBSTR(DATA_INSERIMENTO,9,2)||':'||SUBSTR(DATA_INSERIMENTO,11,2)  end) AS DATA_INSERIMENTO,pg_ae,to_char(to_date(DATA_REGISTRAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_AE,ID_CONTRATTO,NVL(ANNO,'') AS ANNO,substr(VALIDA_FINO_AL,1,4) AS VALIDA_FINO_AL,COD_TRIBUTO,REGISTRO,SOSTITUTIVA,INTERESSI,SANZIONI,TOTALE," _
        '       & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||NOME_FILE_REL||''','''','''');£>'||'Visuliazza REL'||'</a>','$','&'),'£','" & Chr(34) & "') as  NOME_FILE_REL, " _
        '       & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||NOME_FILE_PDF||''','''','''');£>'||'Visuliazza PDF'||'</a>','$','&'),'£','" & Chr(34) & "') as  NOME_FILE_PDF,NOTE " _
        '       & " FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ID_CONTRATTO=(select id from SISCOM_MI.rapporti_utenza where to_number(num_registrazione)=" & Val(lblNumReg.Value) & " and upper(cod_ufficio_reg)='" & UCase(lblCodUfficio.Value) & "')"
        'End If

        BindGrid1()
    End Function

    Private Function Cerca3()
        Dim bTrovato As Boolean

        bTrovato = False
        sStringaSql = ""

        If LBLID.Value <> "-1" Then
            sStringaSQL3 = "SELECT DATA_INSERIMENTO AS DT1,(case when length(DATA_INSERIMENTO)=8 then to_char(to_date(SUBSTR(DATA_INSERIMENTO,1,8),'yyyymmdd'),'dd/mm/yyyy') else to_char(to_date(SUBSTR(DATA_INSERIMENTO,1,8),'yyyymmdd'),'dd/mm/yyyy')||' - '||SUBSTR(DATA_INSERIMENTO,9,2)||':'||SUBSTR(DATA_INSERIMENTO,11,2)  end) AS DATA_INSERIMENTO, replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||NOME_FILE_REL||''','''','''');£>'||'Visuliazza REL'||'</a>','$','&'),'£','" & Chr(34) & "') as  NOME_FILE_REL, " _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||NOME_FILE_PDF||''','''','''');£>'||'Visuliazza PDF'||'</a>','$','&'),'£','" & Chr(34) & "') as  NOME_FILE_PDF,NOTE " _
                & " FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE_SC WHERE ID_CONTRATTO=" & LBLID.Value
            sStringaSQL3 = sStringaSQL3 & " ORDER BY DT1 DESC"
        Else
            sStringaSQL3 = "SELECT DATA_INSERIMENTO AS DT1,(case when length(DATA_INSERIMENTO)=8 then to_char(to_date(SUBSTR(DATA_INSERIMENTO,1,8),'yyyymmdd'),'dd/mm/yyyy') else to_char(to_date(SUBSTR(DATA_INSERIMENTO,1,8),'yyyymmdd'),'dd/mm/yyyy')||' - '||SUBSTR(DATA_INSERIMENTO,9,2)||':'||SUBSTR(DATA_INSERIMENTO,11,2)  end) AS DATA_INSERIMENTO, replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||NOME_FILE_REL||''','''','''');£>'||'Visuliazza REL'||'</a>','$','&'),'£','" & Chr(34) & "') as  NOME_FILE_REL, " _
               & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||NOME_FILE_PDF||''','''','''');£>'||'Visuliazza PDF'||'</a>','$','&'),'£','" & Chr(34) & "') as  NOME_FILE_PDF,NOTE " _
               & " FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE_SC WHERE ID_CONTRATTO=(select id from SISCOM_MI.rapporti_utenza where to_number(num_registrazione)=" & Val(lblNumReg.Value) & " and upper(cod_ufficio_reg)='" & UCase(lblCodUfficio.Value) & "')"
            sStringaSQL3 = sStringaSQL3 & " ORDER BY DT1 DESC"
        End If

        BindGrid3()
    End Function

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

    Public Property sStringaSQL2() As String
        Get
            If Not (ViewState("par_sStringaSQL2") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL2"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL2") = value
        End Set

    End Property

    Public Property sStringaSQL3() As String
        Get
            If Not (ViewState("par_sStringaSQL3") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL3"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL3") = value
        End Set

    End Property

    Private Sub BindGrid()

        Try
            par.OracleConn.Open()
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "RAPPORTI_UTENZA_IMPOSTE,RAPPORTI_UTENZA_IMPOSTE")
            da.Fill(dt)
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            ' HttpContext.Current.Session.Add("AA1", dt)


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label1.Text = ex.Message
        End Try
      

    End Sub

    Private Sub BindGrid3()
        Try
            par.OracleConn.Open()
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL3, par.OracleConn)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "RAPPORTI_UTENZA_IMPOSTE,RAPPORTI_UTENZA_RICEVUTE_SC")
            da.Fill(dt)
            DataGridScarti.DataSource = ds
            DataGridScarti.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label1.Text = ex.Message
        End Try


    End Sub

    Private Sub BindGrid1()
        Try
            par.OracleConn.Open()
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "RAPPORTI_UTENZA_IMPOSTE,RAPPORTI_UTENZA_RICEVUTE")
            da.Fill(dt)
            DataGrid2.DataSource = ds
            DataGrid2.DataBind()
            ' HttpContext.Current.Session.Add("AA1", dt)

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label1.Text = ex.Message
        End Try
       

    End Sub

    Private Sub BindGrid2()
        Try
            par.OracleConn.Open()
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "RAPPORTI_UTENZA_AVVISI_LIQ")
            da.Fill(dt)
            DataGrid3.DataSource = ds
            DataGrid3.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label1.Text = ex.Message
        End Try


    End Sub

    Protected Sub DataGrid1_EditCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        Dim scriptblock As String = ""
        If e.Item.Cells(par.IndDGC(DataGrid1, "ID_STATO_REGISTRAZIONE")).Text = "2" Then
            scriptblock = "<script language='javascript' type='text/javascript'>alert('Impossibile annullare lo scarico. Ricevuta caricata.');</script>"

            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript56")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript56", scriptblock)
            End If
        Else
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "popupWindow=window.open('AnnullaInvioImposte.aspx?ID=" & par.Cripta(e.Item.Cells(par.IndDGC(DataGrid1, "FILE_SCARICATO")).Text) & "&IDRU=" & LBLID.Value & "','AnnulloImposte');" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
            End If
        End If
    End Sub
End Class
