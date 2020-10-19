
Partial Class SPESE_REVERSIBILI_PageModal
    Inherits PageSetMasterIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.ID = "MasterPage"
        If Not IsPostBack Then
            CaricaOperatore()
        End If
    End Sub
    Public Sub NascondiMenu()
    End Sub
    Private Sub CaricaOperatore()
        connData = New CM.datiConnessione(par, False, False)
        Try
            lblOperatore.Text = par.IfNull(Session.Item("NOME_OPERATORE").ToString, "- - -")
            If Not IsNothing(Session.Item("ID_STRUTTURA")) Then
                connData.apri(False)
                par.cmd.CommandText = "SELECT NOME, TIPOLOGIA_STRUTTURA_ALER.DESCRIZIONE " _
                                    & "FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.TIPOLOGIA_STRUTTURA_ALER " _
                                    & "WHERE TIPOLOGIA_STRUTTURA_ALER.ID(+) = TAB_FILIALI.ID_TIPO_ST AND TAB_FILIALI.ID = " & par.insDbValue(Session.Item("ID_STRUTTURA").ToString, True)
                Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If MyReader.Read Then
                    lblFiliale.Text = par.IfEmpty(MyReader("NOME"), "- - -").ToString.ToUpper & "<br>" & par.IfEmpty(MyReader("DESCRIZIONE"), "").ToString.ToUpper
                Else
                    lblFiliale.Text = "- - -<br>- - -"
                End If
                MyReader.Close()
                connData.chiudi(False)
            Else
                lblFiliale.Text = "- - -<br>- - -"
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Spese Reversibili - Modal_Master - CaricaOperatore - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        Finally
            connData.PulisciPool()
        End Try
    End Sub
End Class

