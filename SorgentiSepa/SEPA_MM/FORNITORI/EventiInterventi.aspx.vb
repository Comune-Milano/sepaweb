Imports Telerik.Web.UI


Partial Class FORNITORI_EventiInterventi
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Session.Item("OPERATORE") = "" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            If Session.Item("MOD_FORNITORI") <> "1" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            If Session.Item("MOD_FORNITORI_RDO") <> "1" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                CaricaEventi()
            End If

        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza: Fornitori - Eventi - Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Function CaricaEventi()
        Try
            sStrSql = "select OPERATORI.OPERATORE,TO_CHAR (TO_DATE (substr(data_ora,1,8), 'yyyymmdd'),'dd/mm/yyyy') AS DATA_EVENTO,MOTIVAZIONE,TAB_EVENTI.DESCRIZIONE AS EVENTO from OPERATORI,SISCOM_MI.TAB_EVENTI,siscom_mi.EVENTI_SEGNALAZIONI_FO WHERE OPERATORI.ID=EVENTI_SEGNALAZIONI_FO.ID_OPERATORE AND TAB_EVENTI.COD=EVENTI_SEGNALAZIONI_FO.COD_EVENTO AND ID_SEGNALAZIONE_FO=" & Request.QueryString("D") & " ORDER BY DATA_ORA DESC "


        Catch ex As Exception

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

    Protected Sub dgvEventi_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvEventi.NeedDataSource
        Try
            If sStrSql <> "" Then
                dgvEventi.DataSource = par.getDataTableGrid(sStrSql)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - Eventi - NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
