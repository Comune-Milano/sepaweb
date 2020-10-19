
Partial Class CambioIntestazione
    Inherits System.Web.UI.Page
    Public connData As CM.datiConnessione = Nothing
    Dim par As New CM.Global

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)

        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"

        Response.Write(Loading)
        Response.Flush()
    End Sub
    Protected Sub btnCercaPG_Click(sender As Object, e As ImageClickEventArgs) Handles btnCercaPG.Click
        Try
            connData.apri(False)
            lblErr.Text = ""
            If txtPG.Text <> "" Then
                par.cmd.CommandText = "select domande_bando.*,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME from domande_bando,COMP_NUCLEO where DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND pg like '%" & Replace(par.PulisciStrSql(txtPG.Text), "*", "") & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then

                    btnProcedi.Visible = True
                    idDomanda.Value = myReader("id")
                    par.cmd.CommandText = "select bandi_graduatoria_def.*,DOMANDE_BANDO.ISBARC_R AS ""OP"",comp_nucleo.cognome,comp_nucleo.nome from bandi_graduatoria_def,comp_nucleo," _
                        & " domande_bando where bandi_graduatoria_def.id_domanda=domande_bando.id And domande_bando.id_dichiarazione=comp_nucleo.id_dichiarazione And " _
                        & " domande_bando.progr_componente=comp_nucleo.progr And bandi_graduatoria_def.id_domanda=" & myReader("id")
                    Dim myrec2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myrec2.Read Then
                        'txtPosizGrad.Text = myrec2("posizione")
                        lblPosizGrad.Visible = True
                        txtPosizGrad.Visible = True
                        lblInfoDomanda.Text = "- Nominativo: " & myrec2("cognome") & " " & myrec2("nome") & "<br /> - Posizione Attuale della domanda: " & myrec2("posizione") & "<br /> - Isbarc/r in Grad.: " & par.Tronca(myrec2("isbarc_r")) & "<br /> - Isbarc/r OPERATIVO: " & par.Tronca(myrec2("OP")) & ""
                    Else
                        lblPosizGrad.Visible = True
                        txtPosizGrad.Visible = True
                        lblInfoDomanda.Text = "- Nominativo: " & myReader("cognome") & " " & myReader("nome") & "<br /> - Posizione Attuale della domanda: NESSUNA" & "<br /> - Isbarc/r in Grad.: 0" & "<br /> - Isbarc/r OPERATIVO:" & par.Tronca(myReader("ISBARC_R")) & ""
                    End If
                    myrec2.Close()
                Else
                    btnProcedi.Visible = False
                    lblErr.Text = "Domanda non trovata!"
                End If
                myReader.Close()
            Else
                Response.Write("<script>alert('Inserire il numero della domanda!');</script>")
            End If
            connData.chiudi(False)

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: btnCerca_Click - " & ex.Message)
            lblErr.Text = ex.Message

        End Try
    End Sub

    Private Sub ScriviEventoBANDI(ByVal Id As Long, ByVal Funzione As Integer, ByVal Stato As String, ByVal TipoOperazione As Integer, Optional MOTIVAZIONE As String = "")

        Dim Stringa As String = ""
        Dim codice As String = ""
        Dim StringaSQL As String = ""

        par.cmd.CommandText = "SELECT COD FROM TAB_EVENTI WHERE FUNZIONE=" & Funzione
        codice = par.IfNull(par.cmd.ExecuteScalar, "0")

        If codice <> "0" Then
            If TipoOperazione = 0 Then
                If Funzione = 3 Then
                    par.cmd.CommandText = "select data_ora from eventi_BANDI where id_DOMANDA=" & Id & " and cod_evento='F01'"
                    Dim myrec2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myrec2.Read Then
                        If Mid(par.IfNull(myrec2(0), "         "), 1, 8) = Format(Now, "yyyyMMdd") Then
                            myrec2.Close()
                            Exit Sub
                        End If
                    End If
                    myrec2.Close()
                End If
                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE) " _
                      & "VALUES (" & Id & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & Stato _
                      & "','" & codice & "','" & par.PulisciStrSql(MOTIVAZIONE) & "')"
                par.cmd.ExecuteNonQuery()
            End If
        End If
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As ImageClickEventArgs) Handles btnProcedi.Click
        If confermaCambioPos.Value = "1" Then

            Try
                connData.apri(True)
                par.cmd.CommandText = "select * from bandi where stato=1"
                Dim data_inizio As String = par.IfNull(par.cmd.ExecuteScalar, 0)


                par.cmd.CommandText = "select domande_bando.*,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME from domande_bando,COMP_NUCLEO where DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND DOMANDE_BANDO.id=" & idDomanda.Value
                Dim myrec As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myrec.Read Then
                    If myrec("id_stato") = "8" Or myrec("id_stato") = "2" Or myrec("id_stato") = "9" Then
                        par.cmd.CommandText = "select id from bandi where stato=2 order by id desc"
                        Dim myrec1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If myrec1.Read Then
                            If myrec("id_bando") <= myrec1("id") Then
                                par.cmd.CommandText = "select bandi_graduatoria_def.*,DOMANDE_BANDO.ISBARC_R AS ""OP"",comp_nucleo.cognome,comp_nucleo.nome from bandi_graduatoria_def,comp_nucleo,domande_bando where bandi_graduatoria_def.id_domanda=domande_bando.id and domande_bando.id_dichiarazione=comp_nucleo.id_dichiarazione and domande_bando.progr_componente=comp_nucleo.progr and bandi_graduatoria_def.id_domanda=" & myrec("id")
                                Dim myrec2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                If myrec2.Read Then
                                    If myrec2("isbarc_r") <> myrec("isbarc_r") Then

                                        'InputBox("Posizione Attuale della domanda: " & myrec2("posizione") & vbCrLf & "Isbarc/r in Grad.:" & Tronca(myrec2("isbarc_r")) & vbCrLf & "Isbarc/r OPERATIVO:" & Tronca(myrec2("OP")) & vbCrLf & "Nominativo: " & myrec2("cognome") & " " & myrec2("nome") & vbCrLf & "Inserire la nuova posizione", "Cambio Posizione Graduatoria")
                                        If Val(txtPosizGrad.Text) > 0 Then

                                            par.cmd.CommandText = "update bandi_graduatoria_def set ISBARC_R=" & par.VirgoleInPunti(myrec("ISBARC_R")) & ",ISBAR=" & par.VirgoleInPunti(myrec("ISBAR")) & ",ISE=" & par.VirgoleInPunti(myrec("ISE_ERP")) & ",ISEE=" & par.VirgoleInPunti(myrec("REDDITO_ISEE")) & ",DISAGIO_A=" & par.VirgoleInPunti(Format(myrec("DISAGIO_A"), "0.000000000000000")) & ",DISAGIO_E=" & par.VirgoleInPunti(Format(myrec("DISAGIO_E"), "0.000000000000000")) & ",DISAGIO_F=" & par.VirgoleInPunti(Format(myrec("DISAGIO_F"), "0.000000000000000")) & ",posizione=" & txtPosizGrad.Text & " where id_domanda=" & myrec("id")
                                            par.cmd.ExecuteNonQuery()

                                            par.cmd.CommandText = "update bandi_graduatoria Set ISBARC_R=" & par.VirgoleInPunti(myrec("ISBARC_R")) & ",ISBAR=" & par.VirgoleInPunti(myrec("ISBAR")) & ",ISE=" & par.VirgoleInPunti(myrec("ISE_ERP")) & ",ISEE=" & par.VirgoleInPunti(myrec("REDDITO_ISEE")) & ",DISAGIO_A=" & par.VirgoleInPunti(Format(myrec("DISAGIO_A"), "0.000000000000000")) & ",DISAGIO_E=" & par.VirgoleInPunti(Format(myrec("DISAGIO_E"), "0.000000000000000")) & ",DISAGIO_F=" & par.VirgoleInPunti(Format(myrec("DISAGIO_F"), "0.000000000000000")) & " where id_domanda=" & myrec("id")
                                            par.cmd.ExecuteNonQuery()

                                            If myrec("id_stato") = "9" Then
                                                par.cmd.CommandText = "update domande_bando set id_stato=8 where id=" & myrec("id")
                                                par.cmd.ExecuteNonQuery()
                                            End If

                                            ScriviEventoBANDI(myrec("id"), 161, "8", 0, "Da " & myrec2("posizione") & " a " & txtPosizGrad.Text)
                                            Response.Write("<script>alert('Operazione effettuata');</script>")

                                        Else
                                            Response.Write("<script>alert('Attenzione, posizione non valida! Inserire un valore numerico.');</script>")

                                        End If

                                    Else
                                        Response.Write("<script>alert('Attenzione, non è possibile cambiare la posizione perchè l\'ISBARC/R non è cambiato!');</script>")

                                    End If
                                Else
                                    '                    If MsgBox("Attenzione, Questa domanda non risulta essere mai stata in graduatoria. Proseguire ugualmente? Sarà inserita in graduatoria con i parametri correnti!", vbExclamation + vbYesNo) = vbYes Then
                                    '                        Set myrec3 = MyOpenRecordSet("select * from eventi_bandi where id_domanda=" & myrec("id") & " and data_ora>'" & data_inizio & "000000' and cod_evento='F53'")
                                    '                        If myrec3.EOF = False Then
                                    '                            nuovapos = InputBox("Posizione Attuale della domanda: NESSUNA" & vbCrLf & "Isbarc/r in Grad.: 0" & vbCrLf & "Isbarc/r OPERATIVO:" & Tronca(myrec("ISBARC_R")) & vbCrLf & "Nominativo: " & myrec("cognome") & " " & myrec("nome") & vbCrLf & "Inserire la nuova posizione", "Cambio Posizione Graduatoria")
                                    '                            If Val(nuovapos) > 0 Then
                                    '                                Set MYREC33 = MyOpenRecordSet("select * from bandi_graduatoria where id_domanda=" & myrec("id"))
                                    '                                If MYREC33.EOF = True Then
                                    '                                    MyExecuteSql ("insert into bandi_graduatoria (id,id_domanda,id_bando,TIPO,PROSSIMA_GRAD) values (SEQ_BANDI_GRADUATORIA.NEXTVAL," & myrec("id") & "," & myrec("id_bando") & ",1,0)")
                                    '                                End If
                                    '                                MYREC33.Close
                                    '                                Set MYREC33 = MyOpenRecordSet("select * from bandi_graduatoria_def where id_domanda=" & myrec("id"))
                                    '                                If MYREC33.EOF = True Then
                                    '                                    MyExecuteSql ("insert into bandi_graduatoria_def (id,id_domanda,id_bando,TIPO) values (SEQ_BANDI_GRADUATORIA_def.NEXTVAL," & myrec("id") & "," & myrec("id_bando") & ",1)")
                                    '                                End If
                                    '                                MYREC33.Close
                                    '                                MyExecuteSql ("update bandi_graduatoria_def set ISBARC_R=" & VirgoleInPunti(myrec("ISBARC_R")) & ",ISBAR=" & VirgoleInPunti(myrec("ISBAR")) & ",ISE=" & VirgoleInPunti(myrec("ISE_ERP")) & ",ISEE=" & VirgoleInPunti(myrec("REDDITO_ISEE")) & ",DISAGIO_A=" & VirgoleInPunti(Format(myrec("DISAGIO_A"), "0.000000000000000")) & ",DISAGIO_E=" & VirgoleInPunti(Format(myrec("DISAGIO_E"), "0.000000000000000")) & ",DISAGIO_F=" & VirgoleInPunti(Format(myrec("DISAGIO_F"), "0.000000000000000")) & ",posizione=" & nuovapos & " where id_domanda=" & myrec("id"))
                                    '                                MyExecuteSql ("update bandi_graduatoria set ISBARC_R=" & VirgoleInPunti(myrec("ISBARC_R")) & ",ISBAR=" & VirgoleInPunti(myrec("ISBAR")) & ",ISE=" & VirgoleInPunti(myrec("ISE_ERP")) & ",ISEE=" & VirgoleInPunti(myrec("REDDITO_ISEE")) & ",DISAGIO_A=" & VirgoleInPunti(Format(myrec("DISAGIO_A"), "0.000000000000000")) & ",DISAGIO_E=" & VirgoleInPunti(Format(myrec("DISAGIO_E"), "0.000000000000000")) & ",DISAGIO_F=" & VirgoleInPunti(Format(myrec("DISAGIO_F"), "0.000000000000000")) & " where id_domanda=" & myrec("id"))
                                    '                                MsgBox "Operazione Effettuata!", vbInformation
                                    '                                
                                    '                                ScriviEventoBANDI myrec("id"), 161, "8", 0, "Da " & myrec2.Fields("posizione") & " a " & nuovapos
                                    '                              Else
                                    '                                MsgBox "Attenzione, posizione non valida! Inserire un valore numerico.", vbExclamation
                                    '                            End If
                                    '                          Else
                                    '                            MsgBox "Attenzione, non è possibile cambiare la posizione perchè non risultano Ricorsi del cittadino inseriti!", vbExclamation
                                    '                        End If
                                    '                        myrec3.Close
                                    '                    
                                    '                    End If

                                    'nuovapos = InputBox("Posizione Attuale della domanda: NESSUNA" & vbCrLf & "Isbarc/r in Grad.: 0" & vbCrLf & "Isbarc/r OPERATIVO:" & Tronca(myrec("ISBARC_R")) & vbCrLf & "Nominativo: " & myrec("cognome") & " " & myrec("nome") & vbCrLf & "Inserire la nuova posizione", "Cambio Posizione Graduatoria")
                                    If txtPosizGrad.Text > 0 Then
                                        par.cmd.CommandText = "select * from bandi_graduatoria where id_domanda=" & myrec("id")
                                        Dim MYREC33 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                        If Not MYREC33.Read Then
                                            par.cmd.CommandText = "insert into bandi_graduatoria (id,id_domanda,id_bando,TIPO,PROSSIMA_GRAD) values (SEQ_BANDI_GRADUATORIA.NEXTVAL," & myrec("id") & "," & myrec("id_bando") & ",1,0)"
                                            par.cmd.ExecuteNonQuery()
                                        End If
                                        MYREC33.Close()
                                        par.cmd.CommandText = "select * from bandi_graduatoria_def where id_domanda=" & myrec("id")
                                        MYREC33 = par.cmd.ExecuteReader
                                        If Not MYREC33.Read Then
                                            par.cmd.CommandText = "insert into bandi_graduatoria_def (id,id_domanda,id_bando,TIPO) values (SEQ_BANDI_GRADUATORIA_def.NEXTVAL," & myrec("id") & "," & myrec("id_bando") & ",1)"
                                            par.cmd.ExecuteNonQuery()
                                        End If
                                        MYREC33.Close()
                                        par.cmd.CommandText = "update bandi_graduatoria_def set ISBARC_R=" & par.VirgoleInPunti(myrec("ISBARC_R")) & ",ISBAR=" & par.VirgoleInPunti(myrec("ISBAR")) & ",ISE=" & par.VirgoleInPunti(myrec("ISE_ERP")) & ",ISEE=" & par.VirgoleInPunti(myrec("REDDITO_ISEE")) & ",DISAGIO_A=" & par.VirgoleInPunti(Format(myrec("DISAGIO_A"), "0.000000000000000")) & ",DISAGIO_E=" & par.VirgoleInPunti(Format(myrec("DISAGIO_E"), "0.000000000000000")) & ",DISAGIO_F=" & par.VirgoleInPunti(Format(myrec("DISAGIO_F"), "0.000000000000000")) & ",posizione=" & txtPosizGrad.Text & " where id_domanda=" & myrec("id")
                                        par.cmd.ExecuteNonQuery()
                                        par.cmd.CommandText = "update bandi_graduatoria set ISBARC_R=" & par.VirgoleInPunti(myrec("ISBARC_R")) & ",ISBAR=" & par.VirgoleInPunti(myrec("ISBAR")) & ",ISE=" & par.VirgoleInPunti(myrec("ISE_ERP")) & ",ISEE=" & par.VirgoleInPunti(myrec("REDDITO_ISEE")) & ",DISAGIO_A=" & par.VirgoleInPunti(Format(myrec("DISAGIO_A"), "0.000000000000000")) & ",DISAGIO_E=" & par.VirgoleInPunti(Format(myrec("DISAGIO_E"), "0.000000000000000")) & ",DISAGIO_F=" & par.VirgoleInPunti(Format(myrec("DISAGIO_F"), "0.000000000000000")) & " where id_domanda=" & myrec("id")
                                        par.cmd.ExecuteNonQuery()

                                        If myrec("id_stato") = "9" Then
                                            par.cmd.CommandText = "update domande_bando set id_stato=8 where id=" & myrec("id")
                                            par.cmd.ExecuteNonQuery()
                                        End If

                                        ScriviEventoBANDI(myrec("id"), 161, "8", 0, "Da NESSUNA a " & txtPosizGrad.Text)
                                        Response.Write("<script>alert('Operazione effettuata');</script>")
                                    Else
                                        Response.Write("<script>alert('Attenzione, posizione non valida! Inserire un valore numerico.');</script>")
                                    End If
                                End If
                                myrec2.Close()
                            Else
                                Response.Write("<script>alert('Attenzione, non è possibile cambiare la posizione di una domanda di bando corrente!');</script>")
                            End If
                        End If
                        myrec1.Close()
                    Else
                        Response.Write("<script>alert('Attenzione, stato della domanda non corretto per il cambio di posizione in graduatoria!');</script>")
                    End If
                End If
                myrec.Close()

                connData.chiudi(True)
            Catch ex As Exception
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    connData.chiudi(False)
                End If
                Session.Add("ERRORE", "Provenienza: btnCerca_Click - " & ex.Message)
                lblErr.Text = ex.Message
            End Try
        End If
    End Sub
End Class
