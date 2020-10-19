
Partial Class ANAUT_NuovoOperatoreAU
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If Not IsPostBack Then
            CaricaDati()
        End If
        txtAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtdal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

    End Sub

    Private Function CaricaDati()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT * FROM UTENZA_SPORTELLI WHERE ID=" & Request.QueryString("IDS")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label1.Text = " DI " & par.IfNull(myReader("DESCRIZIONE"), "")
                Label2.Text = "Orario Sportello/Sede: Dalle " & Mid(par.IfNull(myReader("ORA_INIZIO_M"), ""), 1, 2) & ":" & Mid(par.IfNull(myReader("ORA_INIZIO_M"), ""), 3, 2) & " alle " & Mid(par.IfNull(myReader("ORA_FINE_M"), ""), 1, 2) & ":" & Mid(par.IfNull(myReader("ORA_FINE_M"), ""), 3, 2)
                Label4.Text = "Orario Sportello/Sede: Dalle " & Mid(par.IfNull(myReader("ORA_INIZIO_P"), ""), 1, 2) & ":" & Mid(par.IfNull(myReader("ORA_INIZIO_P"), ""), 3, 2) & " alle " & Mid(par.IfNull(myReader("ORA_FINE_P"), ""), 1, 2) & ":" & Mid(par.IfNull(myReader("ORA_FINE_P"), ""), 3, 2)
                Label6.Text = "Lo sportello/Sede lavora nelle giornate di:</ br>"
                If par.IfNull(myReader("gl_1"), "") = "1" Then
                    Label6.Text = Label6.Text & "Lunedì M."
                    Ch1.Enabled = True
                Else
                    Ch1.Enabled = False
                    Ch1.Checked = False
                End If
                If par.IfNull(myReader("gl_1_p"), "") = "1" Then
                    Label6.Text = Label6.Text & "/Lunedì P."
                    Ch1_P.Enabled = True
                Else
                    Ch1_P.Enabled = False
                    Ch1_P.Checked = False
                End If
                If par.IfNull(myReader("gl_2"), "") = "1" Then
                    Label6.Text = Label6.Text & "/Martedì M."
                    Ch2.Enabled = True
                Else
                    Ch2.Enabled = False
                    Ch2.Checked = False
                End If
                If par.IfNull(myReader("gl_2_p"), "") = "1" Then
                    Label6.Text = Label6.Text & "/Martedì P."
                    Ch2_P.Enabled = True
                Else
                    Ch2_P.Enabled = False
                    Ch2_P.Checked = False
                End If
                If par.IfNull(myReader("gl_3"), "") = "1" Then
                    Label6.Text = Label6.Text & "/Mercoledì M."
                    Ch3.Enabled = True
                Else
                    Ch3.Enabled = False
                    Ch3.Checked = False
                End If
                If par.IfNull(myReader("gl_3_p"), "") = "1" Then
                    Label6.Text = Label6.Text & "/Mercoledì P."
                    Ch3_P.Enabled = True
                Else
                    Ch3_P.Enabled = False
                    Ch3_P.Checked = False
                End If
                If par.IfNull(myReader("gl_4"), "") = "1" Then
                    Label6.Text = Label6.Text & "/Giovedì M."
                    Ch4.Enabled = True
                Else
                    Ch4.Enabled = False
                    Ch4.Checked = False
                End If
                If par.IfNull(myReader("gl_4_p"), "") = "1" Then
                    Label6.Text = Label6.Text & "/Giovedì P."
                    Ch4_P.Enabled = True
                Else
                    Ch4_P.Enabled = False
                    Ch4_P.Checked = False
                End If

                If par.IfNull(myReader("gl_5"), "") = "1" Then
                    Label6.Text = Label6.Text & "/Venerdì M."
                    Ch5.Enabled = True
                Else
                    Ch5.Enabled = False
                    Ch5.Checked = False
                End If

                If par.IfNull(myReader("gl_5_p"), "") = "1" Then
                    Label6.Text = Label6.Text & "/Venerdì P."
                    Ch5_P.Enabled = True
                Else
                    Ch5_P.Enabled = False
                    Ch5_P.Checked = False
                End If

                If par.IfNull(myReader("gl_6"), "") = "1" Then
                    Label6.Text = Label6.Text & "/Sabato M."
                    Ch6.Enabled = True
                Else
                    Ch6.Enabled = False
                    Ch6.Checked = False
                End If

                If par.IfNull(myReader("gl_6_p"), "") = "1" Then
                    Label6.Text = Label6.Text & "/Sabato P."
                    Ch6_P.Enabled = True
                Else
                    Ch6_P.Enabled = False
                    Ch6_P.Checked = False
                End If

                If par.IfNull(myReader("gl_7"), "") = "1" Then
                    Label6.Text = Label6.Text & "/Domenica M."
                    Ch7.Enabled = True
                Else
                    Ch7.Enabled = False
                    Ch7.Checked = False
                End If
                If par.IfNull(myReader("gl_7_p"), "") = "1" Then
                    Label6.Text = Label6.Text & "/Domenica P."
                    Ch7_P.Enabled = True
                Else
                    Ch7_P.Enabled = False
                    Ch7_P.Checked = False
                End If

                cmbInizioM.SelectedIndex = -1
                cmbInizioM.Items.FindByValue(Mid(par.IfNull(myReader("ORA_INIZIO_M"), "0800"), 1, 2)).Selected = True
                cmbInizioM1.SelectedIndex = -1
                cmbInizioM1.Items.FindByValue(Mid(par.IfNull(myReader("ORA_INIZIO_M"), "0800"), 3, 2)).Selected = True

                cmbFineM.SelectedIndex = -1
                cmbFineM.Items.FindByValue(Mid(par.IfNull(myReader("ORA_FINE_M"), "1300"), 1, 2)).Selected = True
                cmbFineM1.SelectedIndex = -1
                cmbFineM1.Items.FindByValue(Mid(par.IfNull(myReader("ORA_FINE_M"), "1300"), 3, 2)).Selected = True

                cmbInizioP.SelectedIndex = -1
                cmbInizioP.Items.FindByValue(Mid(par.IfNull(myReader("ORA_INIZIO_P"), "1400"), 1, 2)).Selected = True
                cmbInizioP1.SelectedIndex = -1
                cmbInizioP1.Items.FindByValue(Mid(par.IfNull(myReader("ORA_INIZIO_P"), "1400"), 3, 2)).Selected = True

                cmbFineP.SelectedIndex = -1
                cmbFineP.Items.FindByValue(Mid(par.IfNull(myReader("ORA_FINE_P"), "1800"), 1, 2)).Selected = True
                cmbFineP1.SelectedIndex = -1
                cmbFineP1.Items.FindByValue(Mid(par.IfNull(myReader("ORA_FINE_P"), "1800"), 3, 2)).Selected = True

                If Request.QueryString("T") = "1" Then
                    cmbOperatori.Enabled = False
                    par.cmd.CommandText = "SELECT * FROM UTENZA_OPERATORI WHERE ID=" & Request.QueryString("IDO")
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        If par.IfNull(myReader1("GL_1"), "0") = "1" Then
                            Ch1.Checked = True
                        Else
                            Ch1.Checked = False
                        End If
                        If par.IfNull(myReader1("GL_1_P"), "0") = "1" Then
                            Ch1_P.Checked = True
                        Else
                            Ch1_P.Checked = False
                        End If

                        'MARTEDI
                        If par.IfNull(myReader1("GL_2"), "0") = "1" Then
                            Ch2.Checked = True
                        Else
                            Ch2.Checked = False
                        End If
                        If par.IfNull(myReader1("GL_2_P"), "0") = "1" Then
                            Ch2_P.Checked = True
                        Else
                            Ch2_P.Checked = False
                        End If

                        'MERCOLEDI
                        If par.IfNull(myReader1("GL_3"), "0") = "1" Then
                            Ch3.Checked = True
                        Else
                            Ch3.Checked = False
                        End If
                        If par.IfNull(myReader1("GL_3_P"), "0") = "1" Then
                            Ch3_P.Checked = True
                        Else
                            Ch3_P.Checked = False
                        End If

                        'GIOVEDI
                        If par.IfNull(myReader1("GL_4"), "0") = "1" Then
                            Ch4.Checked = True
                        Else
                            Ch4.Checked = False
                        End If
                        If par.IfNull(myReader1("GL_4_P"), "0") = "1" Then
                            Ch4_P.Checked = True
                        Else
                            Ch4_P.Checked = False
                        End If

                        'VENERDI
                        If par.IfNull(myReader1("GL_5"), "0") = "1" Then
                            Ch5.Checked = True
                        Else
                            Ch5.Checked = False
                        End If
                        If par.IfNull(myReader1("GL_5_P"), "0") = "1" Then
                            Ch5_P.Checked = True
                        Else
                            Ch5_P.Checked = False
                        End If

                        'SABATO
                        If par.IfNull(myReader1("GL_6"), "0") = "1" Then
                            Ch6.Checked = True
                        Else
                            Ch6.Checked = False
                        End If
                        If par.IfNull(myReader1("GL_6_P"), "0") = "1" Then
                            Ch6_P.Checked = True
                        Else
                            Ch6_P.Checked = False
                        End If

                        'DOMENICA
                        If par.IfNull(myReader1("GL_7"), "0") = "1" Then
                            Ch7.Checked = True
                        Else
                            Ch7.Checked = False
                        End If
                        If par.IfNull(myReader1("GL_7_P"), "0") = "1" Then
                            Ch7_P.Checked = True
                        Else
                            Ch7_P.Checked = False
                        End If

                        txtdal.Text = par.FormattaData(myReader1("PERIODO_DAL"))
                        txtAl.Text = par.FormattaData(myReader1("PERIODO_AL"))

                        cmbInizioM.Text = Mid(myReader1("ORA_INIZIO_M"), 1, 2)
                        cmbInizioM1.Text = Mid(myReader1("ORA_INIZIO_M"), 3, 2)
                        cmbFineM.Text = Mid(myReader1("ORA_FINE_M"), 1, 2)
                        cmbFineM1.Text = Mid(myReader1("ORA_FINE_M"), 3, 2)

                        cmbInizioP.Text = Mid(myReader1("ORA_INIZIO_P"), 1, 2)
                        cmbInizioP1.Text = Mid(myReader1("ORA_INIZIO_P"), 3, 2)
                        cmbFineP.Text = Mid(myReader1("ORA_FINE_P"), 1, 2)
                        cmbFineP1.Text = Mid(myReader1("ORA_FINE_P"), 3, 2)

                    End If
                    myReader1.Close()
                End If

            End If
            myReader.Close()

            par.cmd.CommandText = "select utenza_bandi.data_inizio,utenza_bandi.data_fine from utenza_bandi,utenza_filiali,utenza_sportelli where utenza_bandi.id=utenza_filiali.id_bando and utenza_filiali.id=utenza_sportelli.id_filiale and utenza_sportelli.id=" & Request.QueryString("IDS")
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label7.Text = "Inizio AU: " & par.FormattaData(myReader("data_inizio")) & " - Fine AU: " & par.FormattaData(myReader("data_fine"))
                Label8.Text = myReader("data_inizio")
                Label9.Text = myReader("data_fine")
            End If
            myReader.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Function

    Private Sub MessJQuery(ByVal Messaggio As String, ByVal Tipo As Integer, Optional ByVal Titolo As String = "Messaggio")
        Try
            Dim sc As String = ""
            If Tipo = 0 Then
                sc = ScriptErrori(Messaggio, Titolo)
            Else
                sc = ScriptChiudi(Messaggio, Titolo)
            End If
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "ScriptMsg", sc, True)
        Catch ex As Exception
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Sub

    Private Function ScriptErrori(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
            sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function ScriptChiudi(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
            sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');self.close();}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click

        If txtdal.Text = "" Or txtAl.Text = "" Then
            MessJQuery("Periodo di validità non valido!", 0, "Attenzione")
            Exit Sub
        End If

        If par.AggiustaData(txtdal.Text) < Label8.Text Then
            MessJQuery("Data Inizio validità non valida, deve essere uguale o successiva alla data di inizio AU!", 0, "Attenzione")
            Exit Sub
        End If

        If par.AggiustaData(txtdal.Text) > Label9.Text Then
            MessJQuery("Data Inizio validità non valida, deve essere precedente alla data di fine AU!", 0, "Attenzione")
            Exit Sub
        End If

        If par.AggiustaData(txtAl.Text) > Label9.Text Then
            MessJQuery("Data Fine validità non valida, deve essere uguale o precedente alla data di fine AU!", 0, "Attenzione")
            Exit Sub
        End If

        If par.AggiustaData(txtAl.Text) < Label8.Text Then
            MessJQuery("Data Fine validità non valida, deve essere uguale o successiva alla data di inizio AU!", 0, "Attenzione")
            Exit Sub
        End If



        If cmbInizioM.SelectedItem.Value = "--" And cmbInizioM1.SelectedItem.Value <> "--" Then
            MessJQuery("Orario inizio mattino non valido!", 0, "Attenzione")
            Exit Sub
        End If
        If cmbInizioM.SelectedItem.Value <> "--" And cmbInizioM1.SelectedItem.Value = "--" Then
            MessJQuery("Orario inizio mattino non valido!", 0, "Attenzione")
            Exit Sub
        End If
        If cmbInizioP.SelectedItem.Value = "--" And cmbInizioP1.SelectedItem.Value <> "--" Then
            MessJQuery("Orario inizio pomeriggio non valido!", 0, "Attenzione")
            Exit Sub
        End If
        If cmbInizioP.SelectedItem.Value <> "--" And cmbInizioP1.SelectedItem.Value = "--" Then
            MessJQuery("Orario inizio pomeriggio non valido!", 0, "Attenzione")
            Exit Sub
        End If

        If cmbFineM.SelectedItem.Value = "--" And cmbFineM1.SelectedItem.Value <> "--" Then
            MessJQuery("Orario fine mattino non valido!", 0, "Attenzione")
            Exit Sub
        End If
        If cmbFineM.SelectedItem.Value <> "--" And cmbFineM1.SelectedItem.Value = "--" Then
            MessJQuery("Orario fine mattino non valido!", 0, "Attenzione")
            Exit Sub
        End If
        If cmbFineP.SelectedItem.Value = "--" And cmbFineP1.SelectedItem.Value <> "--" Then
            MessJQuery("Orario fine pomeriggio non valido!", 0, "Attenzione")
            Exit Sub
        End If
        If cmbFineP.SelectedItem.Value <> "--" And cmbFineP1.SelectedItem.Value = "--" Then
            MessJQuery("Orario fine pomeriggio non valido!", 0, "Attenzione")
            Exit Sub
        End If

        If (cmbInizioM.SelectedItem.Value = "--" And cmbInizioM1.SelectedItem.Value = "--") And (cmbFineM.SelectedItem.Value <> "--" Or cmbFineM1.SelectedItem.Value <> "--") Then
            MessJQuery("Orario inizio e fine mattino non valido!", 0, "Attenzione")
            Exit Sub
        End If

        If (cmbInizioM.SelectedItem.Value <> "--" Or cmbInizioM1.SelectedItem.Value <> "--") And (cmbFineM.SelectedItem.Value = "--" And cmbFineM1.SelectedItem.Value = "--") Then
            MessJQuery("Orario inizio e fine mattino non valido!", 0, "Attenzione")
            Exit Sub
        End If

        If (cmbInizioP.SelectedItem.Value = "--" And cmbInizioP1.SelectedItem.Value = "--") And (cmbFineP.SelectedItem.Value <> "--" Or cmbFineP1.SelectedItem.Value <> "--") Then
            MessJQuery("Orario inizio e fine pomeriggio non valido!", 0, "Attenzione")
            Exit Sub
        End If

        If (cmbInizioP.SelectedItem.Value <> "--" Or cmbInizioP1.SelectedItem.Value <> "--") And (cmbFineP.SelectedItem.Value = "--" And cmbFineP1.SelectedItem.Value = "--") Then
            MessJQuery("Orario inizio e fine pomeriggio non valido!", 0, "Attenzione")
            Exit Sub
        End If



        If cmbInizioM.SelectedItem.Value <> "--" Then
            If CInt(cmbInizioM.SelectedItem.Value & cmbInizioM1.SelectedItem.Value) >= CInt(cmbFineM.SelectedItem.Value & cmbFineM1.SelectedItem.Value) Then
                MessJQuery("Orario inizio e fine mattina non valido!", 0, "Attenzione")
                Exit Sub
            End If
        End If

        If cmbInizioP.SelectedItem.Value <> "--" Then
            If CInt(cmbInizioP.SelectedItem.Value & cmbInizioP1.SelectedItem.Value) >= CInt(cmbFineP.SelectedItem.Value & cmbFineP1.SelectedItem.Value) Then
                MessJQuery("Orario inizio e fine pomeriggio non valido!", 0, "Attenzione")
                Exit Sub
            End If
        End If

        If cmbInizioP.SelectedItem.Value <> "--" Then
            If cmbFineM.SelectedItem.Value <> "--" Then
                If CInt(cmbInizioP.SelectedItem.Value & cmbInizioP1.SelectedItem.Value) < CInt(cmbFineM.SelectedItem.Value & cmbFineM1.SelectedItem.Value) Then
                    MessJQuery("Orario inizio pomeriggio e fine mattina non valido!", 0, "Attenzione")
                    Exit Sub
                End If
            End If
        End If

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            Dim i As Integer = 1

            If Request.QueryString("T") <> "1" Then
                For i = 1 To cmbOperatori.SelectedItem.Value
                    par.cmd.CommandText = "Insert into UTENZA_OPERATORI (ID, ID_SPORTELLO, DESCRIZIONE, PERIODO_DAL, PERIODO_AL, ORA_INIZIO_M, ORA_FINE_M, ORA_INIZIO_P, ORA_FINE_P, GL_1, GL_2, GL_3, GL_4, GL_5, GL_6, GL_7,GL_1_P, GL_2_P, GL_3_P, GL_4_P, GL_5_P, GL_6_P, GL_7_P) " _
                    & " Values " _
                    & "(seq_utenza_operatori.nextval, " & Request.QueryString("IDS") & ", '','" & par.AggiustaData(txtdal.Text) & "','" & par.AggiustaData(txtAl.Text) & "', '" _
                    & cmbInizioM.SelectedItem.Value & cmbInizioM1.SelectedItem.Value & "', '" & cmbFineM.SelectedItem.Value & cmbFineM1.SelectedItem.Value & "','" _
                    & cmbInizioP.SelectedItem.Value & cmbInizioP1.SelectedItem.Value & "','" _
                    & cmbFineP.SelectedItem.Value & cmbFineP1.SelectedItem.Value & "', " _
                    & Valore01(Ch1.Checked) & "," & Valore01(Ch2.Checked) & "," & Valore01(Ch3.Checked) _
                    & "," & Valore01(Ch4.Checked) & "," & Valore01(Ch5.Checked) & "," & Valore01(Ch6.Checked) & "," & Valore01(Ch7.Checked) & "," _
                    & Valore01(Ch1_P.Checked) & "," & Valore01(Ch2_P.Checked) & "," & Valore01(Ch3_P.Checked) _
                    & "," & Valore01(Ch4_P.Checked) & "," & Valore01(Ch5_P.Checked) & "," & Valore01(Ch6_P.Checked) & "," & Valore01(Ch7_P.Checked) & ")"

                    par.cmd.ExecuteNonQuery()
                Next
            Else
                par.cmd.CommandText = "UPDATE UTENZA_OPERATORI SET PERIODO_DAL='" & par.AggiustaData(txtdal.Text) & "', PERIODO_AL='" & par.AggiustaData(txtAl.Text) & "', ORA_INIZIO_M='" & cmbInizioM.SelectedItem.Value & cmbInizioM1.SelectedItem.Value _
                    & "', ORA_FINE_M='" & cmbFineM.SelectedItem.Value & cmbFineM1.SelectedItem.Value & "', ORA_INIZIO_P='" & cmbInizioP.SelectedItem.Value & cmbInizioP1.SelectedItem.Value _
                    & "', ORA_FINE_P='" & cmbFineP.SelectedItem.Value & cmbFineP1.SelectedItem.Value & "', GL_1=" & Valore01(Ch1.Checked) & ", GL_2=" & Valore01(Ch2.Checked) & ", GL_3=" & Valore01(Ch3.Checked) _
                    & ", GL_4=" & Valore01(Ch4.Checked) & ", GL_5=" & Valore01(Ch5.Checked) & ", GL_6=" & Valore01(Ch6.Checked) & ", GL_7=" & Valore01(Ch7.Checked) _
                    & ",GL_1_P=" & Valore01(Ch1_P.Checked) & ", GL_2_P=" & Valore01(Ch2_P.Checked) & ", GL_3_P=" & Valore01(Ch3_P.Checked) & ", GL_4_P=" & Valore01(Ch4_P.Checked) _
                    & ", GL_5_P=" & Valore01(Ch5_P.Checked) & ", GL_6_P=" & Valore01(Ch6_P.Checked) & ", GL_7_P=" & Valore01(Ch7_P.Checked) & " WHERE ID= " & Request.QueryString("IDO")
                par.cmd.ExecuteNonQuery()
            End If


            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            MessJQuery("Operazione Effettuata!", 1, "Avviso")
        Catch ex As Exception
            par.myTrans.Rollback()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try


    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function
End Class
