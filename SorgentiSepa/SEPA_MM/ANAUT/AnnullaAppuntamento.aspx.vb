
Partial Class ANAUT_AnnullaAppuntamento
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim strScript As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If


        Response.Expires = 0
        If IsPostBack = False Then

            IDA.Value = Request.QueryString("IDA")
            idc.Value = Request.QueryString("IDC")


            If Request.QueryString("T") = "1" Then
                Label1.Visible = False
                cmbLista.Visible = False
                Label2.Visible = False
                TextBox1.Visible = False
                ImageButton1.Visible = False
                Ripristina()
            Else
                Label1.Text = "Indicare il motivo per cui si vuole sospendere questo appuntamento"
                Try
                    DatiAppuntamento()
                    par.RiempiDList(Me, par.OracleConn, "cmbLista", "SELECT * FROM siscom_mi.tab_Motivo_Annullo_App WHERE ID_AU=(SELECT MAX(ID) FROM UTENZA_BANDI) order by descrizione asc", "DESCRIZIONE", "ID")
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                Catch ex As Exception
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Label1.Text = ex.Message
                End Try
            End If

            
        End If
    End Sub

    Private Function DatiAppuntamento()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT  * FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID=" & IDA.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label3.Text = par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & " - " & par.FormattaData(Mid(par.IfNull(myReader("inizio"), ""), 1, 8)) & " ore " & Mid(par.IfNull(myReader("inizio"), ""), 9, 2) & "." & Mid(par.IfNull(myReader("inizio"), ""), 11, 2)
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label3.Text = ex.Message

        End Try
    End Function

    Private Function Ripristina()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            Label1.Visible = True

            par.cmd.CommandText = "SELECT  CONVOCAZIONI_AU.*,RAPPORTI_UTENZA.COD_CONTRATTO FROM SISCOM_MI.CONVOCAZIONI_AU,SISCOM_MI.RAPPORTI_UTENZA WHERE RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO AND CONVOCAZIONI_AU.ID=" & Request.QueryString("IC")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID_FILIALE=" & myReader("ID_FILIALE") & " AND N_OPERATORE='" & myReader("N_OPERATORE") & "' AND INIZIO='" & myReader("DATA_APP") & Mid(myReader("ORE_APP"), 1, 2) & Mid(myReader("ORE_APP"), 4, 2) & "'"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    If par.IfNull(myReader1("id_contratto"), "0") <> "0" Then
                        If myReader1("inizio") > Format(Now, "yyyyMMddHHmm") Then

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & myReader("ID") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO RIPRISTINATO')"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_APPUNTAMENTI_EVENTI (ID_APPUNTAMENTO,DATA_ORA,ID_OPERATORE,NOTE) VALUES (" & myReader1("ID") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO RIPRISTINATO - " & par.PulisciStrSql(par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "")) & "')"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET ID_STATO=1,ID_CONVOCAZIONE=" & myReader("ID") & ",COD_CONTRATTO='" & myReader("COD_CONTRATTO") & "',COGNOME='" & par.PulisciStrSql(myReader("COGNOME")) & "',NOME='" & par.PulisciStrSql(myReader("NOME")) & "',ID_CONTRATTO=" & par.PulisciStrSql(myReader("ID_CONTRATTO")) & " WHERE ID=" & myReader1("ID")
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "UPDATE SISCOM_MI.CONVOCAZIONI_AU SET ID_STATO=0,ID_MOTIVO_ANNULLO=NULL WHERE ID=" & myReader("ID")
                            par.cmd.ExecuteNonQuery()

                            strScript = "<script language='javascript'>alert('Operazione effettuata con successo.');self.close();" _
                            & "</script>"
                            Response.Write(strScript)

                        Else
                            Label1.Text = "Non è possibile ripristinare questo appuntamento! Data già superata."
                        End If
                    Else
                        Label1.Text = "Non è possibile ripristinare questo appuntamento! E' stato già fissato un appuntamento in questa data. Utilizzare la funzione FISSA ALTRO APPUNTAMENTO"
                    End If
                Else
                    Label1.Text = "Non è possibile ripristinare questo appuntamento!"
                End If

                

            End If
            myReader.Close()



            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

           

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            Dim NOTE As String = ""

            Label4.Visible = False

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            If TextBox1.Text <> "" And Len(TextBox1.Text) >= 5 Then
                par.cmd.CommandText = "SELECT  * FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID=" & IDA.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    If TextBox1.Text <> "" Then
                        NOTE = " - " & par.PulisciStrSql(TextBox1.Text)
                    End If

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & myReader("ID_CONVOCAZIONE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO SOSPESO PER " & par.PulisciStrSql(cmbLista.SelectedItem.Text) & " " & NOTE & "')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_APPUNTAMENTI_EVENTI (ID_APPUNTAMENTO,DATA_ORA,ID_OPERATORE,NOTE) VALUES (" & IDA.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO SOSPESO - " & par.PulisciStrSql(par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "")) & " PER " & par.PulisciStrSql(cmbLista.SelectedItem.Text) & " " & NOTE & "')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET ID_STATO=0,ID_CONVOCAZIONE=NULL,COD_CONTRATTO='',COGNOME='',NOME='',ID_CONTRATTO=NULL WHERE ID=" & IDA.Value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.CONVOCAZIONI_AU SET ID_STATO=1,ID_MOTIVO_ANNULLO=" & cmbLista.SelectedItem.Value & " WHERE ID=" & myReader("ID_CONVOCAZIONE")
                    par.cmd.ExecuteNonQuery()

                End If
                myReader.Close()
                strScript = "<script language='javascript'>alert('Operazione effettuata con successo.');self.close();" _
           & "</script>"
                Response.Write(strScript)
            Else
                strScript = "<script language='javascript'>alert('Inserire almeno 5 caratteri nel campo note!');" _
                           & "</script>"
                Response.Write(strScript)

            End If

            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

           

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub
End Class
