
Partial Class CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_ConvAssestStruttura
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Session.Item("OPERATORE") = "" Or Session.Item("BP_GENERALE") <> 1 Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Try

            '##### CARICAMENTO PAGINA #####
            Dim Str As String
            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"
            Response.Write(Str)

            If Not IsPostBack Then
                Response.Flush()
                IdVoce.Value = Request.QueryString("IDVOCE")
                IdAssestamento.Value = Request.QueryString("IDASS")
                CaricaImpStruttra()
                If Request.QueryString("SL") = 1 Then
                    frmSoloLettura()
                    Exit Sub
                End If
                AddJavascriptFunction()
            End If
            If Session.Item("MOD_ASS_CONV_ALER") <> 1 Then
                frmSoloLettura()
                btnConfirm.Visible = False
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub CaricaImpStruttra()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader

            If IdVoce.Value > 0 Then
                par.cmd.CommandText = "SELECT CODICE, DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE ID = " & IdVoce.Value
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    Me.lblVoceBp.Text = par.IfNull(lettore("codice"), "n.d.") & " - " & par.IfNull(lettore("descrizione"), "")
                End If
                lettore.Close()
            End If

            par.cmd.CommandText = "SELECT ID_STRUTTURA,NOME AS STRUTTURA,trim(to_char(nvl(importo,0),'9G999G999G990D99')) AS assestamento,trim(to_char(nvl(importo_approvato,nvl(importo,0)),'9G999G999G990D99')) as IMPORTO_APPR, COMPLETO " _
                                & "FROM siscom_mi.pf_assestamento_voci, siscom_mi.tab_filiali " _
                                & "WHERE id_voce = " & IdVoce.Value & " AND id_assestamento = " & IdAssestamento.Value & " AND tab_filiali.ID = id_struttura " _
                                & "ORDER BY id_struttura ASC "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable()
            da.Fill(dt)

            Me.DgvApprAssest.DataSource = dt
            Me.DgvApprAssest.DataBind()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            LokIncompleti()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "CaricaImpStruttra - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub
    Private Sub LokIncompleti()
        Try

            Dim msgIncompleti As Boolean = False
            For Each di As DataGridItem In DgvApprAssest.Items
                If di.Cells(TrovaIndiceColonna(DgvApprAssest, "COMPLETO")).Text = 0 Then

                    DirectCast(di.Cells(0).FindControl("txtApprovato"), TextBox).ReadOnly = True
                    DirectCast(di.Cells(0).FindControl("txtApprovato"), TextBox).BorderColor = Drawing.Color.Transparent
                    DirectCast(di.Cells(0).FindControl("txtApprovato"), TextBox).BackColor = Drawing.Color.Transparent

                    di.Cells(TrovaIndiceColonna(DgvApprAssest, "STRUTTURA")).ForeColor = Drawing.Color.Black
                    di.Cells(TrovaIndiceColonna(DgvApprAssest, "STRUTTURA")).BackColor = Drawing.Color.Gainsboro
                    msgIncompleti = True
                Else
                    DirectCast(di.Cells(0).FindControl("txtApprovato"), TextBox).ReadOnly = False
                    'DirectCast(di.Cells(0).FindControl("txtApprovato"), TextBox).Text = di.Cells(TrovaIndiceColonna(DgvApprAssest, "ASSESTAMENTO")).Text

                    DirectCast(di.Cells(0).FindControl("txtApprovato"), TextBox).BackColor = Drawing.Color.LightBlue
                    DirectCast(di.Cells(0).FindControl("txtApprovato"), TextBox).ForeColor = Drawing.Color.Black

                End If

            Next
            If msgIncompleti = True Then
                Response.Write("<script>alert('Attenzione!Le Strutture evidenziate non hanno completato l\'assestamento!');</script>")

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "LokIncompleti - " & ex.Message

        End Try
    End Sub
    Function TrovaIndiceColonna(ByVal dgv As DataGrid, ByVal nameCol As String) As Integer
        TrovaIndiceColonna = -1
        Dim Indice As Integer = 0
        Try
            For Each c As DataGridColumn In dgv.Columns
                If String.Equals(nameCol, DirectCast(c, System.Web.UI.WebControls.BoundColumn).DataField, StringComparison.InvariantCultureIgnoreCase) Then
                    TrovaIndiceColonna = Indice
                    Exit For
                End If
                Indice = Indice + 1
            Next

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "TrovaIndiceColonna - " & ex.Message
        End Try

        Return TrovaIndiceColonna

    End Function

    Protected Sub btnConfirm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConfirm.Click
        Update()
    End Sub
    Private Sub Update()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            Dim Importo As Decimal = 0
            For Each item As DataGridItem In DgvApprAssest.Items
                If DirectCast(item.Cells(0).FindControl("txtApprovato"), TextBox).ReadOnly = False Then
                    Importo = CDec(par.IfEmpty(DirectCast(item.Cells(0).FindControl("txtApprovato"), TextBox).Text.Replace(".", ""), "0,00"))
                    '##### modifica per importi negativi
                    'If Importo >= 0 Then 
                        par.cmd.CommandText = "UPDATE SISCOM_MI.PF_ASSESTAMENTO_VOCI " _
                                            & "SET IMPORTO_APPROVATO = " & par.VirgoleInPunti(Importo) _
                                            & " WHERE ID_VOCE = " & IdVoce.Value _
                                            & " AND ID_STRUTTURA = " & item.Cells(TrovaIndiceColonna(DgvApprAssest, "ID_STRUTTURA")).Text _
                                            & " AND ID_ASSESTAMENTO=" & IdAssestamento.Value

                        par.cmd.ExecuteNonQuery()
                    WriteEvent("F02", IdVoce.Value, Importo, item.Cells(TrovaIndiceColonna(DgvApprAssest, "ID_STRUTTURA")).Text, "APPROVAZIONE GESTORE DELL'IMPORTO DI ASSESTAMENTO")
                    'End If
                End If
            Next
            par.myTrans.Commit()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
            CaricaImpStruttra()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "Update - " & ex.Message
            par.myTrans.Rollback()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub

    Private Sub AddJavascriptFunction()
        Try
            Dim i As Integer = 0
            For Each di As DataGridItem In DgvApprAssest.Items

                DirectCast(di.Cells(0).FindControl("txtApprovato"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);  ")
                DirectCast(di.Cells(0).FindControl("txtApprovato"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "AddJavascriptFunction - " & ex.Message
        End Try

    End Sub
    Private Sub frmSoloLettura()
        Try
            Me.btnConfirm.Visible = False
            For Each di As DataGridItem In DgvApprAssest.Items
                DirectCast(di.Cells(0).FindControl("txtApprovato"), TextBox).ReadOnly = True
                DirectCast(di.Cells(0).FindControl("txtApprovato"), TextBox).BorderColor = Drawing.Color.Transparent
                DirectCast(di.Cells(0).FindControl("txtApprovato"), TextBox).BackColor = Drawing.Color.Transparent
            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "frmSoloLettura - " & ex.Message
        End Try
    End Sub
    Protected Sub WriteEvent(ByVal CodEvento As String, ByVal idVoce As Integer, ByVal Importo As Decimal, ByVal IdStMod As String, Optional ByVal Motivazione As String = "")
        Dim ConnOpenNow As Boolean = False
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_ASSESTAMENTO (ID_ASSESTAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_VOCE,ID_STRUTTURA,IMPORTO) VALUES " _
                                & "(" & IdAssestamento.Value & ", " & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                & "'" & CodEvento & "','" & par.PulisciStrSql(Motivazione) & "'," & idVoce & "," & IdStMod & "," & par.VirgoleInPunti(Importo) & " )"
            par.cmd.ExecuteNonQuery()


            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "WriteEvent - " & ex.Message
            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        End Try
    End Sub

End Class
