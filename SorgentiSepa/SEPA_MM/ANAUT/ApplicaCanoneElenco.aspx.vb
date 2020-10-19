
Partial Class ANAUT_ApplicaCanoneElenco
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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
            Cerca()
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

    Private Function Cerca()
        Try
            Dim IDAU As Long

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            sStringaSQL1 = " SELECT UTENZA_BANDI.ID_TIPO_PROVENIENZA,UTENZA_BANDI.DESCRIZIONE AS ANAGRAFE,CANONI_EC_APPLICAZIONI_AU_FILE.ID,'Applicazione Canoni del '||TO_CHAR(TO_DATE(SUBSTR(CANONI_EC_APPLICAZIONI_AU_FILE.DATA_APPLICAZIONE,1,8),'yyyymmdd'),'dd/mm/yyyy')||' '||SUBSTR(CANONI_EC_APPLICAZIONI_AU_FILE.DATA_APPLICAZIONE,9,2)||':'||SUBSTR(CANONI_EC_APPLICAZIONI_AU_FILE.DATA_APPLICAZIONE,11,2) AS NOME_APPLICAZIONE,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''ApplicazioneCanoniDettagli.aspx?T='||ID_TIPO_PROVENIENZA||'&ID='||CANONI_EC_APPLICAZIONI_AU_FILE.id||''','''','''');£>'||'Visualizza Elenco'||'</a>','$','&'),'£','" & Chr(34) & "') as  DETTAGLI FROM UTENZA_BANDI,SISCOM_MI.CANONI_EC_APPLICAZIONI_AU_FILE WHERE CANONI_EC_APPLICAZIONI_AU_FILE.tipo=0 and UTENZA_BANDI.ID=CANONI_EC_APPLICAZIONI_AU_FILE.ID_AU order BY DATA_APPLICAZIONE desc"
            BindGrid()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Function

    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()

        da.Fill(ds, "CANONI_EC_APPLICAZIONI_AU_FILE")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub
End Class
