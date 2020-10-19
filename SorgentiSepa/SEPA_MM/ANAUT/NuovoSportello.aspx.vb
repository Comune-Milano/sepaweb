
Partial Class ANAUT_NuovoSportello
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
    End Sub

    Private Function CaricaDati()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If Request.QueryString("T") <> "1" Then
                par.cmd.CommandText = "SELECT INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,INDIRIZZI.CAP,INDIRIZZI.LOCALITA,INDIRIZZI.COD_COMUNE,TAB_FILIALI.N_TELEFONO,TAB_FILIALI.NOME,TAB_FILIALI.N_FAX,TAB_FILIALI.N_TELEFONO_VERDE,tab_filiali.id as idf FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.TAB_FILIALI,UTENZA_FILIALI WHERE TAB_FILIALI.ID=UTENZA_FILIALI.ID_STRUTTURA AND INDIRIZZI.ID=TAB_FILIALI.ID_INDIRIZZO AND UTENZA_FILIALI.ID=" & Request.QueryString("IDF")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    idf.Value = par.IfNull(myReader("IDF"), "")
                    Label1.Text = " - " & par.IfNull(myReader("NOME"), "")
                    txtDescrizione.Text = par.IfNull(myReader("NOME"), "")
                    txtIndirizzo.Text = par.IfNull(myReader("DESCRIZIONE"), "")
                    txtCivico.Text = par.IfNull(myReader("CIVICO"), "")
                    txtCap.Text = par.IfNull(myReader("CAP"), "")
                    txtComune.Text = par.IfNull(myReader("LOCALITA"), "")
                    txtTelefono.Text = par.IfNull(myReader("N_TELEFONO"), "")
                    txtFAX.Text = par.IfNull(myReader("N_FAX"), "")
                    txtVerde.Text = par.IfNull(myReader("N_TELEFONO_VERDE"), "")
                End If
                myReader.Close()
            Else
                par.cmd.CommandText = "SELECT COMUNI_NAZIONI.NOME AS LOCALITA,UTENZA_FILIALI.ID_STRUTTURA AS IDF,UTENZA_SPORTELLI.* FROM COMUNI_NAZIONI,UTENZA_SPORTELLI,UTENZA_FILIALI WHERE COMUNI_NAZIONI.ID=UTENZA_SPORTELLI.ID_COMUNE AND UTENZA_FILIALI.ID=UTENZA_SPORTELLI.ID_FILIALE  AND UTENZA_SPORTELLI.ID=" & Request.QueryString("IDS")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    idf.Value = par.IfNull(myReader("IDF"), "")
                    Label1.Text = " - " & par.IfNull(myReader("DESCRIZIONE"), "")
                    txtDescrizione.Text = par.IfNull(myReader("DESCRIZIONE"), "")
                    txtIndirizzo.Text = par.IfNull(myReader("INDIRIZZO"), "")
                    txtCivico.Text = par.IfNull(myReader("CIVICO"), "")
                    txtCap.Text = par.IfNull(myReader("CAP"), "")
                    txtComune.Text = par.IfNull(myReader("LOCALITA"), "")
                    txtTelefono.Text = par.IfNull(myReader("N_TELEFONO"), "")
                    txtFAX.Text = par.IfNull(myReader("N_FAX"), "")
                    txtVerde.Text = par.IfNull(myReader("N_VERDE"), "")

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

                    cmbDurata.SelectedIndex = -1
                    cmbDurata.Items.FindByText(par.IfNull(myReader("DURATA"), "20")).Selected = True

                    If par.IfNull(myReader("GL_1"), "0") = "1" Then
                        Ch1.Checked = True
                    Else
                        Ch1.Checked = False
                    End If
                    If par.IfNull(myReader("GL_1_P"), "0") = "1" Then
                        Ch1_P.Checked = True
                    Else
                        Ch1_P.Checked = False
                    End If

                    'MARTEDI
                    If par.IfNull(myReader("GL_2"), "0") = "1" Then
                        Ch2.Checked = True
                    Else
                        Ch2.Checked = False
                    End If
                    If par.IfNull(myReader("GL_2_P"), "0") = "1" Then
                        Ch2_P.Checked = True
                    Else
                        Ch2_P.Checked = False
                    End If

                    'MERCOLEDI
                    If par.IfNull(myReader("GL_3"), "0") = "1" Then
                        Ch3.Checked = True
                    Else
                        Ch3.Checked = False
                    End If
                    If par.IfNull(myReader("GL_3_P"), "0") = "1" Then
                        Ch3_P.Checked = True
                    Else
                        Ch3_P.Checked = False
                    End If

                    'GIOVEDI
                    If par.IfNull(myReader("GL_4"), "0") = "1" Then
                        Ch4.Checked = True
                    Else
                        Ch4.Checked = False
                    End If
                    If par.IfNull(myReader("GL_4_P"), "0") = "1" Then
                        Ch4_P.Checked = True
                    Else
                        Ch4_P.Checked = False
                    End If

                    'VENERDI
                    If par.IfNull(myReader("GL_5"), "0") = "1" Then
                        Ch5.Checked = True
                    Else
                        Ch5.Checked = False
                    End If
                    If par.IfNull(myReader("GL_5_P"), "0") = "1" Then
                        Ch5_P.Checked = True
                    Else
                        Ch5_P.Checked = False
                    End If

                    'SABATO
                    If par.IfNull(myReader("GL_6"), "0") = "1" Then
                        Ch6.Checked = True
                    Else
                        Ch6.Checked = False
                    End If
                    If par.IfNull(myReader("GL_6_P"), "0") = "1" Then
                        Ch6_P.Checked = True
                    Else
                        Ch6_P.Checked = False
                    End If

                    'DOMENICA
                    If par.IfNull(myReader("GL_7"), "0") = "1" Then
                        Ch7.Checked = True
                    Else
                        Ch7.Checked = False
                    End If
                    If par.IfNull(myReader("GL_7_P"), "0") = "1" Then
                        Ch7_P.Checked = True
                    Else
                        Ch7_P.Checked = False
                    End If
                End If
                myReader.Close()

                Ch8.Enabled = False
            End If

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

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click

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
            If CInt(cmbInizioP.SelectedItem.Value & cmbInizioP1.SelectedItem.Value) < CInt(cmbFineM.SelectedItem.Value & cmbFineM1.SelectedItem.Value) Then
                MessJQuery("Orario inizio pomeriggio e fine mattina non valido!", 0, "Attenzione")
                Exit Sub
            End If
        End If

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            Dim Trovato As Boolean = False
            Dim id_comune As Long = 0

            par.cmd.CommandText = "SELECT * FROM comuni_nazioni WHERE upper(nome)='" & UCase(txtComune.Text) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                id_comune = par.IfNull(myReader("id"), 0)
                Trovato = True
            Else
                Trovato = False
            End If
            myReader.Close()

            If Trovato = True Then
                If Request.QueryString("T") <> "1" Then
                    par.cmd.CommandText = "INSERT INTO UTENZA_SPORTELLI (ID,ID_FILIALE,DESCRIZIONE,INDIRIZZO,CIVICO,CAP,ID_COMUNE,N_TELEFONO,N_FAX,N_VERDE,ORA_INIZIO_M,ORA_FINE_M,ORA_INIZIO_P," _
                                        & "ORA_FINE_P,GL_1,GL_2,GL_3,GL_4,GL_5,GL_6,GL_7,DURATA,GL_1_P,GL_2_P,GL_3_P,GL_4_P,GL_5_P,GL_6_P,GL_7_P) VALUES " _
                                        & "(seq_utenza_sportelli.NEXTVAL, " & Request.QueryString("IDF") & ",'" & par.PulisciStrSql(txtDescrizione.Text) & "','" & par.PulisciStrSql(txtIndirizzo.Text) & "','" _
                                        & par.PulisciStrSql(txtCivico.Text) & "','" & par.PulisciStrSql(txtCap.Text) & "', " & id_comune & ",'" & par.PulisciStrSql(txtTelefono.Text) & "','" & par.PulisciStrSql(txtFAX.Text) _
                                        & "','" & par.PulisciStrSql(txtVerde.Text) & "', '" & cmbInizioM.SelectedItem.Value & cmbInizioM1.SelectedItem.Value _
                                        & "','" & cmbFineM.SelectedItem.Value & cmbFineM1.SelectedItem.Value & "','" & cmbInizioP.SelectedItem.Value & cmbInizioP1.SelectedItem.Value _
                                        & "','" & cmbFineP.SelectedItem.Value & cmbFineP1.SelectedItem.Value & "'," & Valore01(Ch1.Checked) & "," & Valore01(Ch2.Checked) & "," & Valore01(Ch3.Checked) _
                                        & "," & Valore01(Ch4.Checked) & "," & Valore01(Ch5.Checked) & "," & Valore01(Ch6.Checked) & "," & Valore01(Ch7.Checked) & "," & cmbDurata.SelectedItem.Value & "," _
                                        & Valore01(Ch1_P.Checked) & "," & Valore01(Ch2_P.Checked) & "," & Valore01(Ch3_P.Checked) _
                                        & "," & Valore01(Ch4_P.Checked) & "," & Valore01(Ch5_P.Checked) & "," & Valore01(Ch6_P.Checked) & "," & Valore01(Ch7_P.Checked) & ")"
                    par.cmd.ExecuteNonQuery()
                    If Ch8.Checked = True Then
                        Dim indice As Long = 0
                        par.cmd.CommandText = "SELECT seq_utenza_sportelli.CURRVAL FROM DUAL"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            indice = myReader1(0)
                        End If
                        myReader1.Close()
                        par.cmd.CommandText = "select DISTINCT ID_UI from SISCOM_MI.FILIALI_UI,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=FILIALI_UI.ID_UI AND UNITA_IMMOBILIARI.COD_TIPOLOGIA='AL' AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA<>'VEND' AND FILIALI_UI.INIZIO_VALIDITA<='" & Format(Now, "yyyyMMdd") & "' AND FILIALI_UI.FINE_VALIDITA>='" & Format(Now, "yyyyMMdd") & "' AND FILIALI_UI.ID_FILIALE=" & idf.Value & " AND FILIALI_UI.ID_UI NOT IN (SELECT UTENZA_SPORTELLI_PATRIMONIO.ID_UNITA FROM UTENZA_SPORTELLI_PATRIMONIO,UTENZA_SPORTELLI WHERE UTENZA_SPORTELLI.FL_ELIMINATO=0 AND UTENZA_SPORTELLI_PATRIMONIO.ID_SPORTELLO=UTENZA_SPORTELLI.ID AND UTENZA_SPORTELLI_PATRIMONIO.ID_AU=" & Request.QueryString("AU") & ")"
                        myReader1 = par.cmd.ExecuteReader()
                        Do While myReader1.Read
                            par.cmd.CommandText = "Insert into UTENZA_SPORTELLI_PATRIMONIO (ID, ID_SPORTELLO, ID_COMPLESSO, ID_AU,ID_UNITA) Values (SEQ_UTENZA_SPO_PATR.nextval, " & indice & ", NULL, " & Request.QueryString("AU") & "," & par.IfNull(myReader1("id_UI"), "NULL") & ")"
                            par.cmd.ExecuteNonQuery()
                        Loop
                        myReader1.Close()
                    End If
                Else
                    'UPDATE
                    par.cmd.CommandText = "UPDATE UTENZA_SPORTELLI SET DESCRIZIONE='" & par.PulisciStrSql(txtDescrizione.Text) & "',INDIRIZZO='" & par.PulisciStrSql(txtIndirizzo.Text) _
                                        & "',CIVICO='" & par.PulisciStrSql(txtCivico.Text) & "',CAP='" & par.PulisciStrSql(txtCap.Text) & "',ID_COMUNE=" & id_comune & ",N_TELEFONO='" _
                                        & par.PulisciStrSql(txtTelefono.Text) & "',N_FAX='" & par.PulisciStrSql(txtFAX.Text) & "',N_VERDE='" & par.PulisciStrSql(txtVerde.Text) _
                                        & "',ORA_INIZIO_M='" & cmbInizioM.SelectedItem.Value & cmbInizioM1.SelectedItem.Value & "' ,ORA_FINE_M='" & cmbFineM.SelectedItem.Value & cmbFineM1.SelectedItem.Value _
                                        & "',ORA_INIZIO_P='" & cmbInizioP.SelectedItem.Value & cmbInizioP1.SelectedItem.Value & "'," _
                                        & "ORA_FINE_P='" & cmbFineP.SelectedItem.Value & cmbFineP1.SelectedItem.Value & "',GL_1=" & Valore01(Ch1.Checked) & ",GL_2=" & Valore01(Ch2.Checked) _
                                        & ",GL_3=" & Valore01(Ch3.Checked) & ",GL_4=" & Valore01(Ch4.Checked) & ",GL_5=" & Valore01(Ch5.Checked) & ",GL_6=" & Valore01(Ch6.Checked) _
                                        & ",GL_7=" & Valore01(Ch7.Checked) & ",DURATA=" & cmbDurata.SelectedItem.Value & ",GL_1_P=" & Valore01(Ch1_P.Checked) & ",GL_2_P=" & Valore01(Ch2_P.Checked) _
                                        & ",GL_3_P=" & Valore01(Ch3_P.Checked) & ",GL_4_P=" & Valore01(Ch4_P.Checked) & ",GL_5_P=" & Valore01(Ch5_P.Checked) & ",GL_6_P=" & Valore01(Ch6_P.Checked) _
                                        & ",GL_7_P=" & Valore01(Ch7_P.Checked) & " WHERE ID=" & Request.QueryString("IDS")
                    par.cmd.ExecuteNonQuery()
                End If

            Else
                MessJQuery("Il Comune inserito non è stato trovato, impossibile procedere!", 0, "Attenzione")
                par.myTrans.Rollback()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
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
End Class
