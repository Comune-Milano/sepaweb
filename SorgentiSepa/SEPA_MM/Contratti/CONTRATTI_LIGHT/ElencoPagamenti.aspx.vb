Imports System.IO

Partial Class Contratti_CONTRATTI_LIGHT_ElencoPagamenti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String
    Dim scriptblock As String
    Dim dt As New System.Data.DataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE_AU_LIGHT") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()
            LBLID.Value = Request.QueryString("id")
            lblCodUfficio.Value = Request.QueryString("UF")
            lblNumReg.Value = Request.QueryString("NR")
            Cerca()
            CERCA1()
        End If
    End Sub

    Private Function Cerca()
        Dim bTrovato As Boolean



        bTrovato = False
        sStringaSql = ""
        If LBLID.Value <> "-1" Then
            sStringaSQL1 = "SELECT to_char(to_date(DATA_RICEVUTA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_RICEVUTA,ID_CONTRATTO,ANNO,COD_TRIBUTO,to_char(to_date(DATA_CREAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_CREAZIONE,to_char(to_date(DATA_AE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI,FILE_SCARICATO,NOTE FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE ID_CONTRATTO=" & LBLID.Value
            sStringaSQL1 = sStringaSQL1 & " ORDER BY ANNO DESC"
        Else
            sStringaSQL1 = "SELECT to_char(to_date(DATA_RICEVUTA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_RICEVUTA,ID_CONTRATTO,ANNO,COD_TRIBUTO,to_char(to_date(DATA_CREAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_CREAZIONE,to_char(to_date(DATA_AE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI,FILE_SCARICATO,NOTE FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE ID_CONTRATTO=(select id from SISCOM_MI.rapporti_utenza where to_number(num_registrazione)=" & Val(lblNumReg.Value) & " and upper(cod_ufficio_reg)='" & UCase(lblCodUfficio.Value) & "')"
            sStringaSQL1 = sStringaSQL1 & " ORDER BY ANNO DESC"
        End If

        BindGrid()
    End Function

    Private Function Cerca1()
        Dim bTrovato As Boolean



        bTrovato = False
        sStringaSql = ""

        If LBLID.Value <> "-1" Then
            sStringaSQL2 = "SELECT pg_ae,to_char(to_date(DATA_REGISTRAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_AE,ID_CONTRATTO,DECODE(ANNO,'????',NULL) AS ANNO,to_char(to_date(VALIDA_FINO_AL,'yyyymmdd'),'dd/mm/yyyy') AS VALIDA_FINO_AL,COD_TRIBUTO,REGISTRO,SOSTITUTIVA,INTERESSI,SANZIONI,TOTALE," _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||NOME_FILE_REL||''','''','''');£>'||'Visuliazza REL'||'</a>','$','&'),'£','" & Chr(34) & "') as  NOME_FILE_REL, " _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||NOME_FILE_PDF||''','''','''');£>'||'Visuliazza PDF'||'</a>','$','&'),'£','" & Chr(34) & "') as  NOME_FILE_PDF,NOTE " _
                & " FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ID_CONTRATTO=" & LBLID.Value
            sStringaSQL2 = sStringaSQL2 & " ORDER BY DATA_REGISTRAZIONE DESC"
        Else
            sStringaSQL2 = "SELECT pg_ae,to_char(to_date(DATA_REGISTRAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_AE,ID_CONTRATTO,DECODE(ANNO,'????',NULL) AS ANNO,to_char(to_date(VALIDA_FINO_AL,'yyyymmdd'),'dd/mm/yyyy') AS VALIDA_FINO_AL,COD_TRIBUTO,REGISTRO,SOSTITUTIVA,INTERESSI,SANZIONI,TOTALE," _
               & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||NOME_FILE_REL||''','''','''');£>'||'Visuliazza REL'||'</a>','$','&'),'£','" & Chr(34) & "') as  NOME_FILE_REL, " _
               & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../../ALLEGATI/CONTRATTI/ELABORAZIONI/RICEVUTE/'||NOME_FILE_PDF||''','''','''');£>'||'Visuliazza PDF'||'</a>','$','&'),'£','" & Chr(34) & "') as  NOME_FILE_PDF,NOTE " _
               & " FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ID_CONTRATTO=(select id from SISCOM_MI.rapporti_utenza where to_number(num_registrazione)=" & Val(lblNumReg.Value) & " and upper(cod_ufficio_reg)='" & UCase(lblCodUfficio.Value) & "')"
        End If

        BindGrid1()
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
End Class
