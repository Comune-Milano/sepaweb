Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports xi = Telerik.Web.UI.ExportInfrastructure
Imports System.Web.UI
Imports System.Web
Imports Telerik.Web.UI.GridExcelBuilder
Imports System.Drawing

Partial Class ANAUT_SituazioneAgenda
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public Altezza As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Session.Item("OPERATORE") = "" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            Dim Str As String = ""

            'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            'Str = Str & "font:verdana; font-size:10px;'><br><img src='../Contratti/Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso...</br>"
            'Str = Str & "</div> <br />"

            'Response.Write(Str)
            'Response.Flush()
            If Not IsPostBack Then
                CaricaDatiAU()
                BindGrid()
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: AU - Situazione Agenda - Carica - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Sub BindGrid()
        'Try
        '    par.OracleConn.Open()
        '    par.SettaCommand(par)

        sStrSql = "select tab_filiali.NOME as struttura," _
                            & "convocazioni_au.id as id_convocazione,(select cod_contratto from siscom_mi.rapporti_utenza where id=id_contratto) as cod_contratto,(select cod_TIPOLOGIA_CONTR_LOC from siscom_mi.rapporti_utenza where id=id_contratto) as cod_TIPOLOGIA_CONTR_LOC," _
                            & "CONVOCAZIONI_AU.cognome,CONVOCAZIONI_AU.nome,to_char(to_date(CONVOCAZIONI_AU.DATA_APP,'yyyymmdd'),'dd/mm/yyyy') AS DATA_APPUNTAMENTO,CONVOCAZIONI_AU.ORE_APP AS ORE_APPUNTAMENTO," _
                            & "DECODE(CONVOCAZIONI_AU.ID_STATO,0,'VALIDA',1,'SOSPESA',2,'CHIUSA') AS STATO,TAB_MOTIVO_ANNULLO_APP.DESCRIZIONE AS MOTIVO_SOSP, " _
                            & "NVL((SELECT DECODE(ID_STATO,0,'DA COMPLETARE',1,'COMPLETA',2,'DA CANCELLARE') FROM UTENZA_DICHIARAZIONI,SISCOM_MI.RAPPORTI_UTENZA WHERE UTENZA_DICHIARAZIONI.ID_STATO<>2 AND UTENZA_DICHIARAZIONI.RAPPORTO=RAPPORTI_UTENZA.COD_CONTRATTO AND ID_BANDO=" & lIdAU & " AND RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO),'DA INSERIRE') AS STATO_SCHEDA_AU " _
                            & "from " _
                            & "siscom_mi.convocazioni_au, siscom_mi.tab_filiali, SISCOM_MI.TAB_MOTIVO_ANNULLO_APP " _
                            & "where CONVOCAZIONI_AU.ID_CONTRATTO IS NOT NULL AND " _
                            & "TAB_MOTIVO_ANNULLO_APP.ID(+)=CONVOCAZIONI_AU.ID_MOTIVO_ANNULLO AND " _
                            & "CONVOCAZIONI_AU.id_gruppo IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & lIdAU & ") AND TAB_FILIALI.ID=CONVOCAZIONI_AU.ID_FILIALE " _
                            & "ORDER BY STRUTTURA,DATA_APP ASC,REPLACE(ORE_APP,'.','') ASC,N_OPERATORE"
        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStrSql, par.OracleConn)
        'Dim ds As New Data.DataSet()
        'da.Fill(ds, "CONVOCAZIONI_AU")
        'dgvDocumenti.DataSource = ds
        'dgvDocumenti.DataBind()
        'par.OracleConn.Close()
        'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        'Catch ex As Exception
        '    par.OracleConn.Close()
        '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '    Session.Add("ERRORE", "Provenienza: AU - Situazione Agenda - BindGrid " & ex.Message)
        '    Response.Redirect("../Errore.aspx", True)
        'End Try
    End Sub


    Private Function CaricaDatiAU()
        Try
            Dim AnnoAU As String = ""

            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE STATO=1 ORDER BY ID DESC"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                lIdAU = myReader("id")
                AnnoAU = myReader("anno_au")
                Label2.Text = " Anno " & myReader("anno_au") & " Redditi " & myReader("anno_isee")
            End If
            myReader.Close()

            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza: AU - Situazione Agenda - Carica Dati AU - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function


    Public Property sStrSql() As String
        Get
            If Not (ViewState("par_sStrSql") Is Nothing) Then
                Return CStr(ViewState("par_sStrSql"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSql") = value
        End Set
    End Property

    Public Property lIdAU() As Long
        Get
            If Not (ViewState("par_lIdAU") Is Nothing) Then
                Return CLng(ViewState("par_lIdAU"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdAU") = value
        End Set
    End Property



    'Protected Sub dgvDocumenti_GroupsChanging(sender As Object, e As Telerik.Web.UI.GridGroupsChangingEventArgs) Handles dgvDocumenti.GroupsChanging
    '    Try
    '        BindGrid()
    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza: AU - Sit.Agenda - GroupsChanging - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub

    Protected Sub dgvDocumenti_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvDocumenti.NeedDataSource
        TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(sStrSql)
    End Sub

    'Protected Sub dgvDocumenti_SortCommand(sender As Object, e As Telerik.Web.UI.GridSortCommandEventArgs) Handles dgvDocumenti.SortCommand
    '    Try
    '        BindGrid()
    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza: AU - Sit.Agenda - SortCommand - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub

    'Protected Sub dgvDocumenti_PreRender(sender As Object, e As System.EventArgs) Handles dgvDocumenti.PreRender
    '    If AltezzaRadGrid.Value = 0 Then
    '        AltezzaRadGrid.Value = 500
    '    Else
    '        dgvDocumenti.Height = AltezzaRadGrid.Value
    '    End If
    'End Sub
End Class
