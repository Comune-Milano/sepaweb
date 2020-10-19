
Partial Class ANAUT_Stampe_ElencoStampe
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Dim MiaSHTML As String
            Dim MIOCOLORE As String
            Dim i As Integer
            Dim ElencoFile() as string

            Dim j As Integer

            MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='90%'>" & vbCrLf
            MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='33%'><font face='Arial' size='2'>Tipo stampa</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='33%'><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

            i = 0
            MIOCOLORE = "#CCFFFF"
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/LOCATARI/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Request.QueryString("IDDIC") & "*.zip")
                If InStr(foundFile, "20_") = 0 Then
                    ReDim Preserve ElencoFile(i)
                    ElencoFile(i) = foundFile
                    i = i + 1
                End If
            Next

            Dim kk As Long
            Dim jj As Long
            Dim scambia

            For kk = 0 To i - 2
                For jj = kk + 1 To i - 1
                    If CLng(Mid(RicavaFile(ElencoFile(kk)), InStr(RicavaFile(ElencoFile(kk)), "-") + 1, 14)) < CLng(Mid(RicavaFile(ElencoFile(jj)), InStr(RicavaFile(ElencoFile(jj)), "-") + 1, 14)) Then
                        scambia = ElencoFile(kk)
                        ElencoFile(kk) = ElencoFile(jj)
                        ElencoFile(jj) = scambia
                    End If
                Next
            Next


            If i > 0 Then
                For j = 0 To i - 1
                    MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                    Select Case Mid(RicavaFile(ElencoFile(j)), 1, 2)
                        Case "10"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Sopralluogo</a></font></td>" & vbCrLf
                        Case "00"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Richiesta Revisione Canone</a></font></td>" & vbCrLf
                        Case "01"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Richiesta Documentazione Mancante</a></font></td>" & vbCrLf
                        Case "02"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Avvio Procedimento</a></font></td>" & vbCrLf
                        Case "03"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Autocertificazione</a></font></td>" & vbCrLf
                        Case "04"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Comunicaz. Esito Negativo</a></font></td>" & vbCrLf
                        Case "05"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Comunicaz. Esito Negativo Riesame</a></font></td>" & vbCrLf
                        Case "06"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Stato Famiglia Ospite</a></font></td>" & vbCrLf
                        Case "07"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Conviv. More Uxorio</a></font></td>" & vbCrLf
                        Case "08"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Conviv. per Assistenza</a></font></td>" & vbCrLf
                        Case "09"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Comunicaz. Esito Positivo</a></font></td>" & vbCrLf

                        Case "S1"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Domanda di subentro nell'intestazione</a></font></td>" & vbCrLf
                        Case "S2"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Dichiaraz. sost. perm. requisiti</a></font></td>" & vbCrLf
                        Case "S3"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Sopralluogo</a></font></td>" & vbCrLf
                        Case "S4"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Com. Sopralluogo</a></font></td>" & vbCrLf
                        Case "S5"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Comunicaz. Esito Positivo Commiss.</a></font></td>" & vbCrLf
                        Case "S6"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Comunicaz. Esito Positivo Dir. Crediti</a></font></td>" & vbCrLf
                        Case "S7"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Comunicazione FF.OO. al Commissario di Governo </a></font></td>" & vbCrLf
                        Case "S8"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Comunicaz. Esito Positivo del Riesame</a></font></td>" & vbCrLf

                        Case "V1"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Esito Negativo Senza Osservaz.</a></font></td>" & vbCrLf
                        Case "V2"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Modulo Richiesta</a></font></td>" & vbCrLf

                        Case "O1"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Richiesta Ospitalità Generica</a></font></td>" & vbCrLf
                        Case "O2"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Richiesta Ospitalità Badanti</a></font></td>" & vbCrLf
                        Case "O3"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Richiesta Ospitalità Scolast.</a></font></td>" & vbCrLf
                        Case "O5"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Esito Positivo per badanti</a></font></td>" & vbCrLf
                        Case "O6"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Esito Positivo per autorizz.scolastica</a></font></td>" & vbCrLf
                        Case "O7"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Esito Positivo Riesame per badanti</a></font></td>" & vbCrLf
                        Case "O8"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Esito Positivo Riesame per autorizz.scolastica</a></font></td>" & vbCrLf
                        Case "O9"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Esito Negativo Con Osservaz.</a></font></td>" & vbCrLf
                        Case "C1"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Richiesta Cambio Consensuale</a></font></td>" & vbCrLf


                            'Nuovi Documenti per Ampliamento 28/02/2012
                        Case "A0"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Ricezione Richiesta</a></font></td>" & vbCrLf
                        Case "A1"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Presa d'atto per rientro</a></font></td>" & vbCrLf

                        Case "A2"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Comunicaz.Esito Positivo Riesame</a></font></td>" & vbCrLf

                        Case "A3"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Comunicaz.Esito Positivo Riesame Rientro</a></font></td>" & vbCrLf

                        Case "A4"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Permanenza Requisiti ERP (titolare)</a></font></td>" & vbCrLf

                        Case "A5"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Permanenza Requisiti ERP (ospite)</a></font></td>" & vbCrLf

                        Case "R1"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Rapporto Sintetico</a></font></td>" & vbCrLf

                        Case "CR"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Canone a regime</a></font></td>" & vbCrLf

                        Case "SC"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Esito Positivo Condomini</a></font></td>" & vbCrLf

                        Case "MD"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Com. Accettazione del debito</a></font></td>" & vbCrLf

                        Case "Ne"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Comunicaz.Esito Negativo</a></font></td>" & vbCrLf

                        Case "Po"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/LOCATARI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Comunicaz.Esito Positivo</a></font></td>" & vbCrLf

                    End Select

                    MiaSHTML = MiaSHTML & "<td width='250px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & My.Computer.FileSystem.GetFileInfo(ElencoFile(j)).CreationTime & "</font></td>" & vbCrLf

                    MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                    If MIOCOLORE = "#CCFFFF" Then
                        MIOCOLORE = "#FFFFCC"
                    Else
                        MIOCOLORE = "#CCFFFF"
                    End If
                    If j = 10 Then Exit For
                Next j
            End If
            MiaSHTML = MiaSHTML & "</table>" & vbCrLf
            Label3.Text = MiaSHTML

        End If
    End Sub

    Private Function RicavaFile(ByVal sFile) As String
        Dim N

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function
End Class
