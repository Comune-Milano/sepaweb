
Partial Class AMMSEPA_RipristinaAnnullo
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Function Eventi_Gestione(ByVal operatore As String, ByVal cod_evento As String, ByVal motivazione As String) As Integer
        Dim data As String = Format(Now, "yyyyMMddHHmmss")
        Try
            PAR.cmd.CommandText = "INSERT INTO EVENTI_GESTIONE (ID_OPERATORE, COD_EVENTO, DATA_ORA, MOTIVAZIONE) VALUES" _
                            & " (" & operatore & ",'" & cod_evento & "'," & data & ",'" & motivazione & "')"
            PAR.cmd.ExecuteNonQuery()
        Catch ex As Exception
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
        Return 0
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select * from OPERATORI where id=" & Session.Item("ID_OPERATORE")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.DeCripta(myReader("PW")) = txtpw.Text Then
                    Label2.Visible = False
                    txtpw0.Visible = True
                    Button2.Visible = True
                    Label1.Visible = False
                    txtpw.Visible = False
                    Button1.Visible = False
                    Label3.Visible = True
                    lblErrore.Visible = False
                Else
                    lblErrore.Visible = True
                    lblErrore.Text = "Password non valida!"
                End If
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select * from siscom_mi.bol_bollette where id=" & par.IfEmpty(txtpw0.Text, "0")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("fl_annullata"), "0") = "1" Or par.IfNull(myReader("fl_annullata"), "0") = "2" Then
                    par.cmd.CommandText = "update siscom_mi.bol_bollette set fl_annullata='0' where id=" & myReader("id")
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & "VALUES (" & myReader("id_contratto") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                        & "'F58','num. " & Format(myReader("id"), "0000000000") & "')"
                    par.cmd.ExecuteNonQuery()

                    Try
                        Dim operatore As String = Session.Item("ID_OPERATORE")
                        Eventi_Gestione(operatore, "F55", "RIPRISTINO BOLLETTA " & txtpw0.Text)
                    Catch ex As Exception
                        lblErrore.Text = ex.Message
                        lblErrore.Visible = True
                    End Try

                    Label3.Visible = True
                    Label3.Text = "La bolletta è stata ripristinata!"
                    txtpw0.Text = ""
                Else
                    Label3.Visible = True
                    Label3.Text = "La bolletta è già valida!"
                End If
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
End Class
