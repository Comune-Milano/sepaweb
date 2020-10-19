
Partial Class ANAUT_SimulazioneConv1
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public percentuale As Long = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
         Dim Str As String = ""

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Contratti/Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso...Fase 1 di 4</br><div class=" & Chr(34) & "style1" & Chr(34) & " align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
        Str = Str & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            CaricaDati()
        End If
    End Sub

    Function DataDiPasqua(ByVal iAnno As Integer) As String
        Dim a As Integer
        Dim b As Integer
        Dim c As Integer
        Dim d As Integer
        Dim e As Integer
        Dim f As Integer
        Dim g As Integer
        Dim h As Integer
        Dim k As Integer
        Dim m As Integer
        Dim sTmp As String

        a = iAnno Mod 19
        b = iAnno Mod 4
        c = iAnno Mod 7
        h = 19 * a + 24
        d = h Mod 30
        k = 5 + 2 * b + 4 * c + 6 * d
        e = k Mod 7
        m = d + e

        If m <= 9 Then
            g = 31 - (9 - m)
            m = 3
        Else
            f = m - 9
            If f = 26 Then
                g = 19
                m = 4
            Else
                If f = 25 Then
                    If d <> 28 Then
                        g = f
                        m = 4
                    Else
                        g = 18
                        m = 4
                    End If
                Else
                    g = f
                    m = 4
                End If
            End If
        End If

        sTmp = Format(g, "00") + "/" + Format(m, "00") + "/" + Mid(Str(iAnno), 2)

        DataDiPasqua = (sTmp)
    End Function


    Private Sub CaricaDati()
        Try
            Dim i As Long = 0
            Dim DataPasqua As String = par.AggiustaData(par.DataDiPasqua(Year(Now)))
            Dim DataPartenza As String = Request.QueryString("DA")
            Dim DataArrivo As String = Request.QueryString("A")
            Dim J As Date = CDate(par.FormattaData(DataPartenza))

            Dim BUONO As Boolean = True
            Dim BUONO_POMERIGGIO As Boolean = True
            Dim MOTIVO As String = ""
            Dim MOTIVO_POMERIGGIO As String = ""
            Dim AGGIUNTA As String = ""
            Dim AGGIUNTA_POMERIGGIO As String = ""
            Dim NumeroOperatori As Integer = 0
            Dim IndiceProcedimento As Long = 0
            Dim NumeroUpdate As Long = 0
            Dim NumeroOperatoriX As Integer = 0

            Dim Contatore As Long = 0
            Dim NUMERORIGHE As Long = 0


            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE id=" & Request.QueryString("ID")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                IndiceBando = myReader("ID")
                Label1.Text = par.IfNull(myReader("DESCRIZIONE"), "")
                'Label2.Attributes.Add("onclick", "javascript:window.open('BandoAU.aspx?ID=" & IndiceBando & "&L=" & par.Cripta("LETTURA") & "', 'Anagrafe', 'status:no;dialogWidth:600px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');")
            End If
            myReader.Close()

            Dim ID_GRUPPO As Long = 0

            par.cmd.CommandText = "Insert into SISCOM_MI.CONVOCAZIONI_AU_GRUPPI (ID, DESCRIZIONE, ID_AU,INIZIO,FINE) Values (SISCOM_MI.SEQ_CONVOCAZIONI_GRUPPI.NEXTVAL, 'SIMULAZIONE DEL " & Format(Now, "dd/MM/yyyy HH:mm") & "', " & IndiceBando & ",'" & DataPartenza & "','" & DataArrivo & "')"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_CONVOCAZIONI_GRUPPI.CURRVAL FROM DUAL"
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                ID_GRUPPO = par.IfNull(myReader2(0), 0)
            End If
            myReader2.Close()

            IndiceProcedimento = ID_GRUPPO


            par.cmd.CommandText = "SELECT rownum,UTENZA_SPORTELLI.*,utenza_filiali.ID_STRUTTURA FROM UTENZA_SPORTELLI,UTENZA_FILIALI WHERE UTENZA_FILIALI.ID=UTENZA_SPORTELLI.ID_FILIALE AND UTENZA_SPORTELLI.FL_ELIMINATO=0 AND UTENZA_SPORTELLI.ID IN (SELECT DISTINCT ID_SPORTELLO FROM UTENZA_LISTE_CDETT WHERE " & par.DeCripta(Request.QueryString("S")) & ") ORDER BY DESCRIZIONE ASC"
            myReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                J = CDate(par.FormattaData(DataPartenza))

                If NUMERORIGHE = 0 Then
                    NUMERORIGHE = DateDiff(DateInterval.Day, CDate(par.FormattaData(DataPartenza)), CDate(par.FormattaData(DataArrivo)))
                End If

                For i = 0 To 9000

                    Dim tsOrarioPartenza As New TimeSpan(Mid(Replace(myReader("ora_inizio_m"), "----", "2300"), 1, 2), Mid(Replace(myReader("ora_inizio_m"), "----", "2300"), 3, 2), 0)
                    Dim tsOrarioFine As New TimeSpan(Mid(Replace(myReader("ora_fine_m"), "----", "1800"), 1, 2), Mid(Replace(myReader("ora_fine_m"), "----", "1800"), 3, 2), 0)
                    Dim tsOraFineAppuntamento As TimeSpan = tsOrarioPartenza + TimeSpan.FromMinutes(myReader("durata"))
                    MOTIVO = ""
                    BUONO = True
                    BUONO_POMERIGGIO = True
                    MOTIVO_POMERIGGIO = ""
                    Select Case J.DayOfWeek
                        Case 1
                            If myReader("GL_1") = "0" Then
                                BUONO = False
                                MOTIVO = "LUNEDI M. - SPORTELLO CHIUSO"
                            End If
                            AGGIUNTA = " AND GL_1='1' "

                            If myReader("GL_1_P") = "0" Then
                                BUONO_POMERIGGIO = False
                                MOTIVO_POMERIGGIO = "LUNEDI P. - SPORTELLO CHIUSO"
                            End If
                            AGGIUNTA_POMERIGGIO = " AND GL_1_P='1' "
                        Case 2
                            If myReader("GL_2") = "0" Then
                                BUONO = False
                                MOTIVO = "MARTEDI - SPORTELLO CHIUSO"
                            End If
                            AGGIUNTA = " AND GL_2='1' "

                            If myReader("GL_2_P") = "0" Then
                                BUONO_POMERIGGIO = False
                                MOTIVO_POMERIGGIO = "MARTEDI P. - SPORTELLO CHIUSO"
                            End If
                            AGGIUNTA_POMERIGGIO = " AND GL_2_P='1' "
                        Case 3
                            If myReader("GL_3") = "0" Then
                                BUONO = False
                                MOTIVO = "MERCOLEDI - SPORTELLO CHIUSO"
                            End If
                            AGGIUNTA = " AND GL_3='1' "

                            If myReader("GL_3_P") = "0" Then
                                BUONO_POMERIGGIO = False
                                MOTIVO_POMERIGGIO = "MERCOLEDI P. - SPORTELLO CHIUSO"
                            End If
                            AGGIUNTA_POMERIGGIO = " AND GL_3_P='1' "

                        Case 4
                            If myReader("GL_4") = "0" Then
                                BUONO = False
                                MOTIVO = "GIOVEDI - SPORTELLO CHIUSO"
                            End If
                            AGGIUNTA = " AND GL_4='1' "

                            If myReader("GL_4_P") = "0" Then
                                BUONO_POMERIGGIO = False
                                MOTIVO_POMERIGGIO = "GIOVEDI P. - SPORTELLO CHIUSO"
                            End If
                            AGGIUNTA_POMERIGGIO = " AND GL_4_P='1' "
                        Case 5
                            If myReader("GL_5") = "0" Then
                                BUONO = False
                                MOTIVO = "VENERDI - SPORTELLO CHIUSO"
                            End If
                            AGGIUNTA = " AND GL_5='1' "

                            If myReader("GL_5_P") = "0" Then
                                BUONO_POMERIGGIO = False
                                MOTIVO_POMERIGGIO = "VENERDI P. - SPORTELLO CHIUSO"
                            End If
                            AGGIUNTA_POMERIGGIO = " AND GL_5_P='1' "

                        Case 6
                            If myReader("GL_6") = "0" Then
                                BUONO = False
                                MOTIVO = "SABATO - SPORTELLO CHIUSO"
                            End If
                            AGGIUNTA = " AND GL_6='1' "

                            If myReader("GL_6_P") = "0" Then
                                BUONO_POMERIGGIO = False
                                MOTIVO_POMERIGGIO = "SABATO P. - SPORTELLO CHIUSO"
                            End If
                            AGGIUNTA_POMERIGGIO = " AND GL_6_P='1' "

                        Case 0
                            If myReader("GL_7") = "0" Then
                                BUONO = False
                                MOTIVO = "DOMENICA - SPORTELLO CHIUSO"
                            End If
                            AGGIUNTA = " AND GL_7='1' "

                            If myReader("GL_7_P") = "0" Then
                                BUONO_POMERIGGIO = False
                                MOTIVO_POMERIGGIO = "DOMENICA P. - SPORTELLO CHIUSO"
                            End If
                            AGGIUNTA_POMERIGGIO = " AND GL_7_P='1' "

                    End Select

                    Do While tsOrarioPartenza <= tsOrarioFine
                        If BUONO = True Then

                            'orario di mattina
                            NumeroOperatori = 0
                            par.cmd.CommandText = "SELECT count(id) FROM UTENZA_OPERATORI WHERE FL_ELIMINATO=0 AND id_sportello=" & myReader("ID") & " AND PERIODO_DAL<='" & par.AggiustaData(J) & "' AND PERIODO_AL>='" & par.AggiustaData(J) & "' AND TO_NUMBER(REPLACE(ORA_INIZIO_M,'----','0'))<=" & tsOrarioPartenza.Hours & Format(tsOrarioPartenza.Minutes, "00") & " AND TO_NUMBER(REPLACE(ORA_FINE_M,'----','0'))>=" & tsOrarioPartenza.Hours & Format(tsOrarioPartenza.Minutes, "00") & AGGIUNTA
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                NumeroOperatori = par.IfNull(myReader1(0), 0)
                            End If
                            myReader1.Close()

                            NumeroOperatoriX = 0
                            par.cmd.CommandText = "select count(id) from siscom_mi.AGENDA_APPUNTAMENTI where id_contratto is not null and  nvl(n_operatore,'0')<>'0' and id_sportello=" & myReader("ID") & " and INIZIO='" & par.AggiustaData(J) & Format(tsOrarioPartenza.Hours, "00") & Format(tsOrarioPartenza.Minutes, "00") & "'"
                            myReader1 = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                NumeroOperatoriX = par.IfNull(myReader1(0), 0)
                            End If
                            myReader1.Close()
                            NumeroOperatori = NumeroOperatori - NumeroOperatoriX
                            'If NumeroOperatoriX = 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_PARAMETRI_OP (ID_PROCEDIMENTO,ID_FILIALE,ID_AU,GIORNO,INIZIO,FINE,N_OPERATORI,ID_SPORTELLO) values (" & IndiceProcedimento & "," & myReader("ID_STRUTTURA") & "," & IndiceBando & ",'" & par.AggiustaData(J) & "','" & tsOrarioPartenza.Hours & Format(tsOrarioPartenza.Minutes, "00") & "','" & tsOraFineAppuntamento.Hours & Format(tsOraFineAppuntamento.Minutes, "00") & "'," & NumeroOperatori & "," & myReader("ID") & ")"
                            par.cmd.ExecuteNonQuery()
                            'End If

                        Else
                            'orario di mattina
                            NumeroOperatori = 0
                            par.cmd.CommandText = "SELECT count(id) FROM UTENZA_OPERATORI WHERE FL_ELIMINATO=0 AND id_sportello=" & myReader("ID") & " AND PERIODO_DAL<='" & par.AggiustaData(J) & "' AND PERIODO_AL>='" & par.AggiustaData(J) & "' AND TO_NUMBER(REPLACE(ORA_INIZIO_M,'----','0'))<=" & tsOrarioPartenza.Hours & Format(tsOrarioPartenza.Minutes, "00") & " AND TO_NUMBER(REPLACE(ORA_FINE_M,'----','0'))>=" & tsOrarioPartenza.Hours & Format(tsOrarioPartenza.Minutes, "00") & AGGIUNTA

                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                NumeroOperatori = par.IfNull(myReader1(0), 0)
                            End If
                            myReader1.Close()

                            NumeroOperatoriX = 0
                            par.cmd.CommandText = "select count(id) from siscom_mi.AGENDA_APPUNTAMENTI where id_contratto is not null and  nvl(n_operatore,'0')<>'0' and id_sportello=" & myReader("ID") & " and INIZIO='" & par.AggiustaData(J) & Format(tsOrarioPartenza.Hours, "00") & Format(tsOrarioPartenza.Minutes, "00") & "'"
                            myReader1 = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                NumeroOperatoriX = par.IfNull(myReader1(0), 0)
                            End If
                            myReader1.Close()
                            NumeroOperatori = NumeroOperatori - NumeroOperatoriX

                            'If NumeroOperatoriX = 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_PARAMETRI_OP (ID_PROCEDIMENTO,ID_FILIALE,ID_AU,GIORNO,INIZIO,FINE,N_OPERATORI,ID_SPORTELLO,NOTE) values (" & IndiceProcedimento & "," & myReader("ID_STRUTTURA") & "," & IndiceBando & ",'" & par.AggiustaData(J) & "','" & tsOrarioPartenza.Hours & Format(tsOrarioPartenza.Minutes, "00") & "','" & tsOraFineAppuntamento.Hours & Format(tsOraFineAppuntamento.Minutes, "00") & "'," & NumeroOperatori & "," & myReader("ID") & ",'" & MOTIVO & "')"
                            par.cmd.ExecuteNonQuery()
                            'End If
                        End If

                        tsOrarioPartenza = tsOraFineAppuntamento
                        tsOraFineAppuntamento = tsOrarioPartenza + TimeSpan.FromMinutes(myReader("durata"))
                    Loop


                    Dim tsOrarioPartenzaP As New TimeSpan(Mid(Replace(myReader("ora_inizio_p"), "----", "2300"), 1, 2), Mid(Replace(myReader("ora_inizio_p"), "----", "2300"), 3, 2), 0)
                    Dim tsOrarioFineP As New TimeSpan(Mid(Replace(myReader("ora_fine_p"), "----", "1800"), 1, 2), Mid(Replace(myReader("ora_fine_p"), "----", "1800"), 3, 2), 0)
                    Dim tsOraFineAppuntamentoP As TimeSpan = tsOrarioPartenzaP + TimeSpan.FromMinutes(myReader("durata"))

                    Do While tsOrarioPartenzaP <= tsOrarioFineP
                        If BUONO_POMERIGGIO = True Then
                            'orario di pomeriggio

                            NumeroOperatori = 0
                            par.cmd.CommandText = "SELECT count(id) FROM UTENZA_OPERATORI WHERE FL_ELIMINATO=0 AND id_sportello=" & myReader("ID") & " AND PERIODO_DAL<='" & par.AggiustaData(J) & "' AND PERIODO_AL>='" & par.AggiustaData(J) & "' AND TO_NUMBER(REPLACE(ORA_INIZIO_P,'----','0'))<=" & tsOrarioPartenzaP.Hours & Format(tsOrarioPartenzaP.Minutes, "00") & " AND TO_NUMBER(REPLACE(ORA_FINE_P,'----','0'))>=" & tsOrarioPartenzaP.Hours & Format(tsOrarioPartenzaP.Minutes, "00") & AGGIUNTA_POMERIGGIO
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                NumeroOperatori = par.IfNull(myReader1(0), 0)
                            End If
                            myReader1.Close()

                            NumeroOperatoriX = 0
                            par.cmd.CommandText = "select count(id) from siscom_mi.AGENDA_APPUNTAMENTI where id_contratto is not null and nvl(n_operatore,'0')<>'0' and id_sportello=" & myReader("ID") & " and INIZIO='" & par.AggiustaData(J) & Format(tsOrarioPartenzaP.Hours, "00") & Format(tsOrarioPartenzaP.Minutes, "00") & "'"
                            myReader1 = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                NumeroOperatoriX = par.IfNull(myReader1(0), 0)
                            End If
                            myReader1.Close()
                            NumeroOperatori = NumeroOperatori - NumeroOperatoriX

                            'If NumeroOperatoriX = 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_PARAMETRI_OP (ID_PROCEDIMENTO,ID_FILIALE,ID_AU,GIORNO,INIZIO,FINE,N_OPERATORI,ID_SPORTELLO) values (" & IndiceProcedimento & "," & myReader("ID_STRUTTURA") & "," & IndiceBando & ",'" & par.AggiustaData(J) & "','" & tsOrarioPartenzaP.Hours & Format(tsOrarioPartenzaP.Minutes, "00") & "','" & tsOraFineAppuntamentoP.Hours & Format(tsOraFineAppuntamentoP.Minutes, "00") & "'," & NumeroOperatori & "," & myReader("ID") & ")"
                            par.cmd.ExecuteNonQuery()
                            'End If


                        Else
                            NumeroOperatori = 0
                            par.cmd.CommandText = "SELECT count(id) FROM UTENZA_OPERATORI WHERE FL_ELIMINATO=0 AND id_sportello=" & myReader("ID") & " AND PERIODO_DAL<='" & par.AggiustaData(J) & "' AND PERIODO_AL>='" & par.AggiustaData(J) & "' AND TO_NUMBER(REPLACE(ORA_INIZIO_P,'----','0'))<=" & tsOrarioPartenzaP.Hours & Format(tsOrarioPartenzaP.Minutes, "00") & " AND TO_NUMBER(REPLACE(ORA_FINE_P,'----','0'))>=" & tsOrarioPartenzaP.Hours & Format(tsOrarioPartenzaP.Minutes, "00") & AGGIUNTA_POMERIGGIO
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                NumeroOperatori = par.IfNull(myReader1(0), 0)
                            End If
                            myReader1.Close()

                            NumeroOperatoriX = 0
                            par.cmd.CommandText = "select count(id) from siscom_mi.AGENDA_APPUNTAMENTI where id_contratto is not null and nvl(n_operatore,'0')<>'0' and id_sportello=" & myReader("ID") & " and INIZIO='" & par.AggiustaData(J) & Format(tsOrarioPartenzaP.Hours, "00") & Format(tsOrarioPartenzaP.Minutes, "00") & "'"
                            myReader1 = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                NumeroOperatoriX = par.IfNull(myReader1(0), 0)
                            End If
                            myReader1.Close()
                            NumeroOperatori = NumeroOperatori - NumeroOperatoriX

                            'If NumeroOperatoriX = 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_PARAMETRI_OP (ID_PROCEDIMENTO,ID_FILIALE,ID_AU,GIORNO,INIZIO,FINE,N_OPERATORI,ID_SPORTELLO,NOTE) values (" & IndiceProcedimento & "," & myReader("ID_STRUTTURA") & "," & IndiceBando & ",'" & par.AggiustaData(J) & "','" & tsOrarioPartenzaP.Hours & Format(tsOrarioPartenzaP.Minutes, "00") & "','" & tsOraFineAppuntamentoP.Hours & Format(tsOraFineAppuntamentoP.Minutes, "00") & "'," & NumeroOperatori & "," & myReader("ID") & ",'" & MOTIVO & "')"
                            par.cmd.ExecuteNonQuery()
                            'End If
                        End If
                        tsOrarioPartenzaP = tsOraFineAppuntamentoP
                        tsOraFineAppuntamentoP = tsOrarioPartenzaP + TimeSpan.FromMinutes(myReader("durata"))
                    Loop
                    J = DateAdd("d", 1, J)

                    Contatore = Contatore + 1
                    percentuale = (Contatore * 100) / NUMERORIGHE
                    Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                    Response.Flush()

                    If par.AggiustaData(J) > DataArrivo Then

                        'ZERO OPERATORI I GIORNI DI PASQUA E PASQUETTA
                        par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_PARAMETRI_OP SET NOTE='CHIUSURA FEST. NAZIONALE' WHERE ID_PROCEDIMENTO=" & IndiceProcedimento & " AND (GIORNO='" & DataPasqua & "' OR GIORNO='" & Format(DateAdd("d", 1, CDate(par.FormattaData(DataPasqua))), "yyyyMMdd") & "') AND ID_AU=" & IndiceBando
                        par.cmd.ExecuteNonQuery()

                        'ZERO OPERATORI I GIORNI DI CHIUSURA NAZIONALE
                        par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_PARAMETRI_OP SET NOTE='CHIUSURA FEST. NAZIONALE' WHERE ID_PROCEDIMENTO=" & IndiceProcedimento & " AND GIORNO IN (SELECT TO_CHAR(SYSDATE,'YYYY')||GIORNO FROM UTENZA_FESTIVITA_ST) AND ID_AU=" & IndiceBando
                        par.cmd.ExecuteNonQuery()

                        'ZERO OPERATORI I GIORNI DI CHIUSURA FILIALE
                        par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_PARAMETRI_OP SET NOTE='CHIUSURA FILIALE' WHERE ID_PROCEDIMENTO=" & IndiceProcedimento & " AND GIORNO IN (SELECT GIORNO FROM UTENZA_CHIUSURE_PR WHERE ID_FILIALE=" & myReader("ID_FILIALE") & ") AND ID_AU=" & IndiceBando
                        par.cmd.ExecuteNonQuery()

                        'ZERO OPERATORI I GIORNI DI CHIUSURA SPORTELLO
                        par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_PARAMETRI_OP SET NOTE='CHIUSURA SPORTELLO' WHERE ID_PROCEDIMENTO=" & IndiceProcedimento & " AND GIORNO IN (SELECT GIORNO FROM UTENZA_CHIUSURE_PR WHERE ID_SPORTELLO=" & myReader("ID") & ") AND ID_AU=" & IndiceBando
                        par.cmd.ExecuteNonQuery()

                        Exit For
                    End If
                Next


            Loop
            myReader.Close()


            Dim Str As String = ""
            Contatore = 0
            NUMERORIGHE = 0


            Str = "<script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">document.getElementById('dvvvPre').style.visibility = 'hidden';</script><div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='../Contratti/Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso...Fase 2 di 4</br><div class=" & Chr(34) & "style1" & Chr(34) & " align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA1" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra1" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
            Str = Str & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra1() {document.getElementById('barra1').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra1()" & Chr(34) & ", 100);</script>"

            Response.Write(Str)
            Response.Flush()

            par.cmd.CommandText = "SELECT ROWNUM,UTENZA_SPORTELLI.* FROM UTENZA_SPORTELLI WHERE FL_ELIMINATO=0 AND ID IN (SELECT DISTINCT ID_SPORTELLO FROM UTENZA_LISTE_CDETT WHERE " & par.DeCripta(Request.QueryString("S")) & ") ORDER BY DESCRIZIONE ASC"
            myReader = par.cmd.ExecuteReader()
            Do While myReader.Read

                Contatore = 0
                NUMERORIGHE = 0
                Response.Write("<script>tempo=0;</script>")
                Response.Flush()

                par.cmd.CommandText = "SELECT ROWNUM,UTENZA_LISTE_CDETT.* FROM UTENZA_LISTE_CDETT where " & par.DeCripta(Request.QueryString("S")) & " and  id_sportello=" & myReader("ID") & " ORDER BY ROWNUM DESC" '& " and id_lista in (select id from UTENZA_LISTE_CONV where id_au=" & IndiceBando & ") and id_lista_conv IN (SELECT ID FROM UTENZA_LISTE WHERE LETTERA_CREATA=0)"
                myReader2 = par.cmd.ExecuteReader()
                Do While myReader2.Read
                    If NUMERORIGHE = 0 Then
                        NUMERORIGHE = par.IfNull(myReader2("ROWNUM"), 0)
                    End If

                    par.cmd.CommandText = "Insert into SISCOM_MI.CONVOCAZIONI_AU (ID, ID_CONTRATTO, ID_GRUPPO, DATA_APP, ORE_APP, ID_FILIALE, N_OPERATORE, ORE_FINE_APP, COGNOME, NOME, ID_STATO, ID_MOTIVO_ANNULLO, CARICO_AUSI, DATA_AUSI, ID_SPORTELLO) Values (SISCOM_MI.SEQ_CONVOCAZIONI_AU.NEXTVAL, " & myReader2("ID_CONTRATTO") & "," & ID_GRUPPO & ", '', '', " & myReader2("ID_TAB_FILIALI") & ", '', '', '" & par.PulisciStrSql(par.IfNull(myReader2("INTESTATARIO"), "")) & "', '', 0, NULL, 0, NULL, " & myReader("ID") & ")"
                    par.cmd.ExecuteNonQuery()

                    Contatore = Contatore + 1
                    percentuale = (Contatore * 100) / NUMERORIGHE
                    Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                    Response.Flush()
                Loop
                myReader2.Close()
            Loop
            myReader.Close()

            Dim N_OPERATORI As Integer = 0
            Dim K As Integer = 0

            Contatore = 0
            NUMERORIGHE = 0

            Str = "<script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">document.getElementById('dvvvPre').style.visibility = 'hidden';</script><div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='../Contratti/Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso...Fase 3 di 4</br><div class=" & Chr(34) & "style1" & Chr(34) & " align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA2" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra2" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
            Str = Str & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra2() {document.getElementById('barra2').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra2()" & Chr(34) & ", 100);</script>"

            Response.Write(Str)
            Response.Flush()

            par.cmd.CommandText = "SELECT * FROM UTENZA_SPORTELLI WHERE FL_ELIMINATO=0 AND ID IN (SELECT DISTINCT ID_SPORTELLO FROM UTENZA_LISTE_CDETT WHERE " & par.DeCripta(Request.QueryString("S")) & ") ORDER BY DESCRIZIONE ASC"
            myReader = par.cmd.ExecuteReader()
            Do While myReader.Read

                Response.Write("<script>tempo=" & Format(0, "0") & ";</script>")
                Response.Flush()

                par.cmd.CommandText = "SELECT COUNT(*) FROM UTENZA_FILIALI,SISCOM_MI.AGENDA_PARAMETRI_OP WHERE UTENZA_FILIALI.ID_STRUTTURA=AGENDA_PARAMETRI_OP.ID_FILIALE AND ID_PROCEDIMENTO=" & IndiceProcedimento & " AND ID_AU=" & IndiceBando & " AND ID_SPORTELLO=" & myReader("ID")
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    NUMERORIGHE = par.IfNull(myReader2(0), 0)
                End If
                myReader2.Close()
                par.cmd.CommandText = "SELECT AGENDA_PARAMETRI_OP.*,UTENZA_FILIALI.ID_STRUTTURA FROM UTENZA_FILIALI,SISCOM_MI.AGENDA_PARAMETRI_OP WHERE UTENZA_FILIALI.ID_STRUTTURA=AGENDA_PARAMETRI_OP.ID_FILIALE AND ID_PROCEDIMENTO=" & IndiceProcedimento & " AND ID_AU=" & IndiceBando & " AND ID_SPORTELLO=" & myReader("ID") & " ORDER BY GIORNO ASC,TO_NUMBER(INIZIO) ASC"
                myReader2 = par.cmd.ExecuteReader()
                Do While myReader2.Read
                    N_OPERATORI = myReader2("N_OPERATORI")
                    K = 1
                    If par.IfNull(myReader2("NOTE"), "") = "" Then
                        par.cmd.CommandText = "select count(id) from siscom_mi.AGENDA_APPUNTAMENTI where id_contratto is not null and nvl(n_operatore,'0')<>'0' and id_sportello=" & myReader("ID") & " and INIZIO='" & myReader2("GIORNO") & Format(Mid(myReader2("INIZIO"), 1, Len(myReader2("INIZIO")) - 2), "00") & Format(Mid(myReader2("INIZIO"), Len(myReader2("INIZIO")) - 1, 2), "00") & "'"
                        Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader4.Read Then
                            N_OPERATORI = N_OPERATORI - par.IfNull(myReader4(0), 0)
                        End If
                        myReader4.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_GRUPPO=" & IndiceProcedimento & " AND ID_SPORTELLO=" & myReader("ID") & " AND DATA_APP IS NULL ORDER BY COGNOME ASC"
                        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader3.Read
                            If K <= N_OPERATORI Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.CONVOCAZIONI_AU SET n_operatore=" & K & ",DATA_APP='" & myReader2("GIORNO") & "',ORE_APP='" & Mid(myReader2("INIZIO"), 1, Len(myReader2("INIZIO")) - 2) & "." & Mid(myReader2("INIZIO"), Len(myReader2("INIZIO")) - 1, 2) & "',ORE_FINE_APP='" & Mid(myReader2("FINE"), 1, Len(myReader2("FINE")) - 2) & "." & Mid(myReader2("FINE"), Len(myReader2("FINE")) - 1, 2) & "' WHERE ID=" & myReader3("ID")
                                NumeroUpdate = par.cmd.ExecuteNonQuery()
                                If NumeroUpdate = 0 Then
                                    par.cmd.CommandText = "Insert into SISCOM_MI.CONVOCAZIONI_AU (ID, ID_CONTRATTO, ID_GRUPPO, DATA_APP, ORE_APP, ID_FILIALE, N_OPERATORE, ORE_FINE_APP, COGNOME, NOME, ID_STATO, ID_MOTIVO_ANNULLO, CARICO_AUSI, DATA_AUSI, ID_SPORTELLO) Values (SISCOM_MI.SEQ_CONVOCAZIONI_AU.NEXTVAL, null," & ID_GRUPPO & ", '" & myReader2("GIORNO") & "', '" & Mid(myReader2("INIZIO"), 1, Len(myReader2("INIZIO")) - 2) & "." & Mid(myReader2("INIZIO"), Len(myReader2("INIZIO")) - 1, 2) & "', " & myReader2("ID_STRUTTURA") & ", '" & K & "', '" & Mid(myReader2("FINE"), 1, Len(myReader2("FINE")) - 2) & "." & Mid(myReader2("FINE"), Len(myReader2("FINE")) - 1, 2) & "', '', '', 0, NULL, 0, NULL, " & myReader("ID") & ")"
                                    par.cmd.ExecuteNonQuery()
                                End If
                                K = K + 1
                            Else
                                If K = 0 Then K = -1
                                Exit Do
                            End If
                        Loop
                        If K <> -1 Then
                            If myReader3.HasRows = True Then
                                If K = 1 Then K = 0
                                For i = K To N_OPERATORI
                                    par.cmd.CommandText = "Insert into SISCOM_MI.CONVOCAZIONI_AU (ID, ID_CONTRATTO, ID_GRUPPO, DATA_APP, ORE_APP, ID_FILIALE, N_OPERATORE, ORE_FINE_APP, COGNOME, NOME, ID_STATO, ID_MOTIVO_ANNULLO, CARICO_AUSI, DATA_AUSI, ID_SPORTELLO) Values (SISCOM_MI.SEQ_CONVOCAZIONI_AU.NEXTVAL, null," & ID_GRUPPO & ", '" & myReader2("GIORNO") & "', '" & Mid(myReader2("INIZIO"), 1, Len(myReader2("INIZIO")) - 2) & "." & Mid(myReader2("INIZIO"), Len(myReader2("INIZIO")) - 1, 2) & "', " & myReader2("ID_filiale") & ", '" & i & "', '" & Mid(myReader2("FINE"), 1, Len(myReader2("FINE")) - 2) & "." & Mid(myReader2("FINE"), Len(myReader2("FINE")) - 1, 2) & "', '', '', 0, NULL, 0, NULL, " & myReader("ID") & ")"
                                    par.cmd.ExecuteNonQuery()
                                Next
                            End If

                            If myReader3.HasRows = False Then
                                For i = K To N_OPERATORI
                                    par.cmd.CommandText = "Insert into SISCOM_MI.CONVOCAZIONI_AU (ID, ID_CONTRATTO, ID_GRUPPO, DATA_APP, ORE_APP, ID_FILIALE, N_OPERATORE, ORE_FINE_APP, COGNOME, NOME, ID_STATO, ID_MOTIVO_ANNULLO, CARICO_AUSI, DATA_AUSI, ID_SPORTELLO) Values (SISCOM_MI.SEQ_CONVOCAZIONI_AU.NEXTVAL, null," & ID_GRUPPO & ", '" & myReader2("GIORNO") & "', '" & Mid(myReader2("INIZIO"), 1, Len(myReader2("INIZIO")) - 2) & "." & Mid(myReader2("INIZIO"), Len(myReader2("INIZIO")) - 1, 2) & "', " & myReader2("ID_filiale") & ", '" & i & "', '" & Mid(myReader2("FINE"), 1, Len(myReader2("FINE")) - 2) & "." & Mid(myReader2("FINE"), Len(myReader2("FINE")) - 1, 2) & "', '', '', 0, NULL, 0, NULL, " & myReader("ID") & ")"
                                    par.cmd.ExecuteNonQuery()
                                Next
                                If N_OPERATORI = 0 Then
                                    par.cmd.CommandText = "Insert into SISCOM_MI.CONVOCAZIONI_AU (ID, ID_CONTRATTO, ID_GRUPPO, DATA_APP, ORE_APP, ID_FILIALE, N_OPERATORE, ORE_FINE_APP, COGNOME, NOME, ID_STATO, ID_MOTIVO_ANNULLO, CARICO_AUSI, DATA_AUSI, ID_SPORTELLO) Values (SISCOM_MI.SEQ_CONVOCAZIONI_AU.NEXTVAL, null," & ID_GRUPPO & ", '" & myReader2("GIORNO") & "', '" & Mid(myReader2("INIZIO"), 1, Len(myReader2("INIZIO")) - 2) & "." & Mid(myReader2("INIZIO"), Len(myReader2("INIZIO")) - 1, 2) & "', " & myReader2("ID_filiale") & ", '0', '" & Mid(myReader2("FINE"), 1, Len(myReader2("FINE")) - 2) & "." & Mid(myReader2("FINE"), Len(myReader2("FINE")) - 1, 2) & "', '', '', 0, NULL, 0, NULL, " & myReader("ID") & ")"
                                    par.cmd.ExecuteNonQuery()
                                End If
                            End If
                        End If
                        myReader3.Close()
                    Else
                        par.cmd.CommandText = "select count(id) from siscom_mi.AGENDA_APPUNTAMENTI where id_contratto is not null and nvl(n_operatore,'0')<>'0' and id_sportello=" & myReader("ID") & " and INIZIO='" & myReader2("GIORNO") & Format(Mid(myReader2("INIZIO"), 1, Len(myReader2("INIZIO")) - 2), "00") & Format(Mid(myReader2("INIZIO"), Len(myReader2("INIZIO")) - 1, 2), "00") & "'"
                        Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader4.Read Then
                            N_OPERATORI = N_OPERATORI - par.IfNull(myReader4(0), 0)
                        End If
                        myReader4.Close()

                        For i = K To N_OPERATORI
                            par.cmd.CommandText = "Insert into SISCOM_MI.CONVOCAZIONI_AU (ID, ID_CONTRATTO, ID_GRUPPO, DATA_APP, ORE_APP, ID_FILIALE, N_OPERATORE, ORE_FINE_APP, COGNOME, NOME, ID_STATO, ID_MOTIVO_ANNULLO, CARICO_AUSI, DATA_AUSI, ID_SPORTELLO) Values (SISCOM_MI.SEQ_CONVOCAZIONI_AU.NEXTVAL, null," & ID_GRUPPO & ", '" & myReader2("GIORNO") & "', '" & Mid(myReader2("INIZIO"), 1, Len(myReader2("INIZIO")) - 2) & "." & Mid(myReader2("INIZIO"), Len(myReader2("INIZIO")) - 1, 2) & "', " & myReader2("ID_filiale") & ", '" & i & "', '" & Mid(myReader2("FINE"), 1, Len(myReader2("FINE")) - 2) & "." & Mid(myReader2("FINE"), Len(myReader2("FINE")) - 1, 2) & "', '', '', 0, NULL, 0, NULL, " & myReader("ID") & ")"
                            par.cmd.ExecuteNonQuery()
                        Next
                        If N_OPERATORI = 0 Then
                            par.cmd.CommandText = "Insert into SISCOM_MI.CONVOCAZIONI_AU (ID, ID_CONTRATTO, ID_GRUPPO, DATA_APP, ORE_APP, ID_FILIALE, N_OPERATORE, ORE_FINE_APP, COGNOME, NOME, ID_STATO, ID_MOTIVO_ANNULLO, CARICO_AUSI, DATA_AUSI, ID_SPORTELLO) Values (SISCOM_MI.SEQ_CONVOCAZIONI_AU.NEXTVAL, null," & ID_GRUPPO & ", '" & myReader2("GIORNO") & "', '" & Mid(myReader2("INIZIO"), 1, Len(myReader2("INIZIO")) - 2) & "." & Mid(myReader2("INIZIO"), Len(myReader2("INIZIO")) - 1, 2) & "', " & myReader2("ID_filiale") & ", '0', '" & Mid(myReader2("FINE"), 1, Len(myReader2("FINE")) - 2) & "." & Mid(myReader2("FINE"), Len(myReader2("FINE")) - 1, 2) & "', '', '', 0, NULL, 0, NULL, " & myReader("ID") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    End If

                    Contatore = Contatore + 1
                    percentuale = (Contatore * 100) / NUMERORIGHE
                    Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                    Response.Flush()
                Loop
                myReader2.Close()
            Loop
            myReader.Close()

            Contatore = 0
            NUMERORIGHE = 0


            Response.Write("<script>tempo=" & Format(0, "0") & ";</script>")
            Str = "<script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">document.getElementById('dvvvPre').style.visibility = 'hidden';</script><div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='../Contratti/Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso...Fase 4 d 4</br><div class=" & Chr(34) & "style1" & Chr(34) & " align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra3" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
            Str = Str & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra3() {document.getElementById('barra3').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra3()" & Chr(34) & ", 100);</script>"
            Response.Write(Str)
            Response.Flush()

            par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET N_OPERATORE=0 WHERE ID_CONVOCAZIONE IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_GRUPPO=" & IndiceProcedimento & ") and (SUBSTR(INIZIO,1,8)='" & DataPasqua & "' OR SUBSTR(INIZIO,1,8)='" & Format(DateAdd("d", 1, CDate(par.FormattaData(DataPasqua))), "yyyyMMdd") & "') AND ID_AU=" & IndiceBando
            par.cmd.ExecuteNonQuery()

            'ZERO OPERATORI I GIORNI DI CHIUSURA NAZIONALE
            par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET N_OPERATORE=0 WHERE ID_CONVOCAZIONE IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_GRUPPO=" & IndiceProcedimento & ") and  SUBSTR(INIZIO,1,8) IN (SELECT TO_CHAR(SYSDATE,'YYYY')||GIORNO FROM UTENZA_FESTIVITA_ST) AND ID_AU=" & IndiceBando
            par.cmd.ExecuteNonQuery()

            'ZERO OPERATORI I GIORNI DI CHIUSURA FILIALE
            par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET N_OPERATORE=0 WHERE ID_CONVOCAZIONE IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_GRUPPO=" & IndiceProcedimento & ") and  SUBSTR(INIZIO,1,8) IN (SELECT UTENZA_CHIUSURE_PR.GIORNO FROM UTENZA_CHIUSURE_PR,UTENZA_FILIALI WHERE UTENZA_CHIUSURE_PR.ID_FILIALE=UTENZA_FILIALI.ID AND UTENZA_FILIALI.ID_STRUTTURA=AGENDA_APPUNTAMENTI.ID_FILIALE) AND ID_AU=" & IndiceBando
            par.cmd.ExecuteNonQuery()

            'ZERO OPERATORI I GIORNI DI CHIUSURA SPORTELLO
            par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET N_OPERATORE=0 WHERE ID_CONVOCAZIONE IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_GRUPPO=" & IndiceProcedimento & ") and  SUBSTR(INIZIO,1,8) IN (SELECT GIORNO FROM UTENZA_CHIUSURE_PR WHERE ID_SPORTELLO=AGENDA_APPUNTAMENTI.ID_SPORTELLO) AND ID_AU=" & IndiceBando
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_GRUPPO=" & IndiceProcedimento & " AND DATA_APP IS NULL"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If myReader(0) > 0 Then
                    Response.Write("<script>alert('Attenzione...I contratti da convocare sono superiori alla disponibilità data da periodo e numero di operatori! " & myReader(0) & " contratti non saranno convocati!');</script>")
                End If
            End If
            myReader.Close()

            Response.Write("<script>tempo=" & Format(100, "0") & ";</script>")
            Response.Flush()

            par.myTrans.Commit()
            'par.myTrans.Rollback()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write("<script>alert('Operazione Effettuata');location.href='ElencoSimulazioniConv.aspx';</script>")
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:SimulazioneConv - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Public Property IndiceBando() As Long
        Get
            If Not (ViewState("par_IndiceBando") Is Nothing) Then
                Return CLng(ViewState("par_IndiceBando"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IndiceBando") = value
        End Set
    End Property

    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click

    End Sub
End Class
