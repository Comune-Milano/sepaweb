Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class Contratti_Morosita_CreaLettere
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If


        Dim IndiceMorosita As String = ""


        If Not IsPostBack Then
            Try
                IndiceMorosita = Request.QueryString("IDMOR")
                idm.Value = IndiceMorosita
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "SELECT COND_MOROSITA_LETTERE.* FROM SISCOM_MI.COND_MOROSITA_LETTERE WHERE BOLLETTINO IS NULL and id_morosita = " & IndiceMorosita
                Dim myReader1234 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1234.HasRows = True Then
                    myReader1234.Close()

                    par.cmd.CommandText = "SELECT SISCOM_MI.CONDOMINI.* FROM SISCOM_MI.CONDOMINI,SISCOM_MI.COND_MOROSITA WHERE CONDOMINI.ID=COND_MOROSITA.ID_CONDOMINIO AND COND_MOROSITA.id = " & IndiceMorosita
                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        cmbTitoloD.Items.FindByText(par.IfNull(myReaderA("TITOLO_DIRIGENTE"), "Dott.")).Selected = True
                        cmbTitoloR.Items.FindByText(par.IfNull(myReaderA("TITOLO_RESPONSABILE"), "Dott.")).Selected = True
                        cmbTitoloT.Items.FindByText(par.IfNull(myReaderA("TITOLO_TRATTATA"), "Dott.")).Selected = True

                        txtCognomeD.Text = par.IfNull(myReaderA("COGNOME_DIRIGENTE"), "")
                        txtCognomeR.Text = par.IfNull(myReaderA("COGNOME_RESPONSABILE"), "")
                        txtCognomeT.Text = par.IfNull(myReaderA("COGNOME_TRATTATA"), "")

                        txtNomeD.Text = par.IfNull(myReaderA("NOME_DIRIGENTE"), "")
                        txtNomeR.Text = par.IfNull(myReaderA("NOME_RESPONSABILE"), "")
                        txtNomeT.Text = par.IfNull(myReaderA("NOME_TRATTATA"), "")

                        txtTelefonoR.Text = par.IfNull(myReaderA("TELEFONO_RESPONSABILE"), "")
                        txtTelefonoT.Text = par.IfNull(myReaderA("TELEFONO_TRATTATA"), "")

                        cond.Value = par.IfNull(myReaderA("ID"), "-1")
                    End If
                    myReaderA.Close()

                Else
                    myReader1234.Close()

                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    Response.Redirect("CreaLettere2.aspx?IDMOR=" & idm.Value & "&IDCOND=" & cond.Value)

                End If

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()




            Catch ex As Exception
                Response.Write(ex.Message)
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        End If
    End Sub

    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try
            Label15.Visible = False
            'If Trim(txtCognomeD.Text) <> "" And Trim(txtCognomeR.Text) <> "" And Trim(txtCognomeT.Text) <> "" And Trim(txtNomeD.Text) <> "" And Trim(txtNomeR.Text) <> "" And Trim(txtNomeT.Text) <> "" And Trim(txtTelefonoR.Text) <> "" And Trim(txtTelefonoT.Text) <> "" Then

            par.OracleConn.Open()
            par.SettaCommand(par)



            If Trim(txtCognomeD.Text) <> "" And Trim(txtCognomeR.Text) <> "" And Trim(txtNomeD.Text) <> "" And Trim(txtNomeR.Text) <> "" And Trim(txtTelefonoR.Text) <> "" Then
                par.cmd.CommandText = "update siscom_mi.condomini set " _
                                    & "TITOLO_DIRIGENTE='" & cmbTitoloD.SelectedItem.Text _
                                    & "',TITOLO_RESPONSABILE='" & cmbTitoloR.SelectedItem.Text _
                                    & "',TITOLO_TRATTATA='" & cmbTitoloT.SelectedItem.Text _
                                    & "',COGNOME_DIRIGENTE='" & par.PulisciStrSql(txtCognomeD.Text) _
                                    & "',COGNOME_RESPONSABILE='" & par.PulisciStrSql(txtCognomeR.Text) _
                                    & "',COGNOME_TRATTATA='" & par.PulisciStrSql(txtCognomeT.Text) _
                                    & "',NOME_DIRIGENTE='" & par.PulisciStrSql(txtNomeD.Text) _
                                    & "',NOME_RESPONSABILE='" & par.PulisciStrSql(txtNomeR.Text) _
                                    & "',NOME_TRATTATA='" & par.PulisciStrSql(txtNomeT.Text) _
                                    & "',TELEFONO_RESPONSABILE='" & par.PulisciStrSql(txtTelefonoR.Text) _
                                    & "',TELEFONO_TRATTATA='" & par.PulisciStrSql(txtTelefonoT.Text) & "' WHERE ID=" & cond.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Response.Redirect("CreaLettere2.aspx?IDMOR=" & idm.Value & "&IDCOND=" & cond.Value, False)

            Else
                Label15.Text = "Valorizzare tutti i campi!"
                Label15.Visible = True

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If




        Catch ex As Exception
            Response.Write(ex.Message)
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
End Class
