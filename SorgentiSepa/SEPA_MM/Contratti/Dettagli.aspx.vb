
Partial Class Contratti_Dettagli
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String
        Dim numrata As String = ""

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>Reindirizzamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                indiceMorosita.Value = ""
                indiceFine.Value = ""

                par.cmd.CommandText = "select * from siscom_mi.bol_bollette where id=" & Request.QueryString("ID")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    indiceMorosita.Value = par.IfNull(myReader("id_morosita"), "")
                    indiceContratto.Value = par.IfNull(myReader("id_contratto"), "")
                    Select Case par.IfNull(myReader("id_tipo"), 0)
                        Case "3"
                            Label1.Text = "Bolletta di FINE CONTRATTO, il cui importo comprende le bollette scadute e non pagate:<br /><div id=" & Chr(34) & "Contenitore" & Chr(34) & " style=" & Chr(34) & "position: absolute; width: 100%; height: 100px; left: 15px; overflow: auto; top: 65px;" & Chr(34) & " >"
                            par.cmd.CommandText = "select * from siscom_mi.bol_bollette where id_bolletta_ric=" & Request.QueryString("ID") & " order by id desc"
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            Do While myReader1.Read

                                Select Case myReader1("n_rata")
                                    Case "999"
                                        numrata = "AU"
                                    Case "99"
                                        numrata = "MA"
                                    Case "99999"
                                        numrata = "CO"
                                    Case Else
                                        numrata = Format(myReader1("n_rata"), "00")
                                End Select

                                Label1.Text = Label1.Text & "<a href=" & Chr(34) & "AnteprimaBolletta.aspx?ID=" & myReader1("id") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & Format(myReader1("id"), "0000000000") & " Rata " & numrata & "/" & myReader1("anno") & " Emessa il " & par.FormattaData(myReader1("data_emissione")) & "</a> - <a href=" & Chr(34) & "../Contabilita/DettaglioBolletta.aspx?IDBOLL=" & myReader1("id") & "&IDANA=" & myReader1("COD_AFFITTUARIO") & "&IDCONT=" & myReader1("id_CONTRATTO") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">Dettagli</a><br />"
                            Loop
                            Label1.Text = Label1.Text & "</div>"
                            myReader1.Close()
                            indiceFine.Value = myReader("id")
                        Case "4"
                            indiceMorosita.Value = par.IfNull(myReader("id_morosita"), "")
                            indiceContratto.Value = par.IfNull(myReader("id_contratto"), "")
                            indiceFine.Value = ""
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Sub
                        Case "5"
                            Label1.Text = "Bolletta di Rateizzazione inclusa in un piano di rientro:<br /><br />"
                            Label1.Text = Label1.Text & "<a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "apriDettRateizz();" & Chr(34) & ">Visualizza Dettagli</a><br />"
                            indiceFine.Value = myReader("id")
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                            Exit Sub
                    End Select


                    If par.IfNull(myReader("id_bolletta_ric"), 0) <> 0 Then
                        Label1.Text = "Bolletta Riclassificata, il cui importo non pagato è stato incluso nella bolletta:<br /><div id=" & Chr(34) & "Contenitore" & Chr(34) & " style=" & Chr(34) & "position: absolute; width: 100%; left: 15px; overflow: auto; top: 65px;" & Chr(34) & " >"
                        par.cmd.CommandText = "select * from siscom_mi.bol_bollette where id=" & par.IfNull(myReader("id_bolletta_ric"), 0) & " order by id desc"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader1.Read
                            Select Case myReader1("n_rata")
                                Case "999"
                                    numrata = "AU"
                                Case "99"
                                    numrata = "MA"
                                Case "99999"
                                    numrata = "CO"
                                Case Else
                                    numrata = Format(myReader1("n_rata"), "00")
                            End Select

                            Label1.Text = Label1.Text & "<a href=" & Chr(34) & "AnteprimaBolletta.aspx?ID=" & myReader1("id") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & Format(myReader1("id"), "0000000000") & " Rata " & numrata & "/" & myReader1("anno") & " Emessa il " & par.FormattaData(myReader1("data_emissione")) & "</a> - <a href=" & Chr(34) & "../Contabilita/DettaglioBolletta.aspx?IDBOLL=" & myReader1("id") & "&IDANA=" & myReader1("COD_AFFITTUARIO") & "&IDCONT=" & myReader1("id_CONTRATTO") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">Dettagli</a><br />"
                        Loop
                        Label1.Text = Label1.Text & "</div>"
                        myReader1.Close()
                        indiceFine.Value = myReader("id")
                        myReader.Close()
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If

                    If par.IfNull(myReader("id_rateizzazione"), 0) <> 0 Then
                        Label1.Text = "Bolletta Riclassificata, il cui importo non pagato è stato incluso in un piano di rientro:<br /><div id=" & Chr(34) & "Contenitore" & Chr(34) & " style=" & Chr(34) & "position: absolute; width: 100%; height: 100px; left: 15px; overflow: auto; top: 65px;" & Chr(34) & " >"
                        par.cmd.CommandText = "select * from siscom_mi.BOL_RATEIZZAZIONI_DETT where id_rateizzazione=" & par.IfNull(myReader("id_rateizzazione"), 0) & " order by num_rata asc"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader1.Read

                            Label1.Text = Label1.Text & " Rata " & myReader1("num_rata") & " Da Emettere/Emessa il " & par.FormattaData(myReader1("data_emissione")) & " Importo: " & Format(par.IfNull(myReader1("importo_rata"), 0), "##,##0.00") & "<br />"
                        Loop
                        Label1.Text = Label1.Text & "</div>"
                        myReader1.Close()
                        indiceFine.Value = myReader("id")
                    End If

                    If par.IfNull(myReader("ID_TIPO"), 0) = 22 Then
                        Label1.Text = "Scrittura di storno negativa per la bolletta:<br /><div id=" & Chr(34) & "Contenitore" & Chr(34) & " style=" & Chr(34) & "position: absolute; width: 100%; left: 15px; overflow: auto;top:65px;" & Chr(34) & " >"
                        par.cmd.CommandText = "select * from siscom_mi.bol_bollette where id_bolletta_storno=" & Request.QueryString("ID") & " order by id desc"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader1.Read
                            Select Case myReader1("n_rata")
                                Case "999"
                                    numrata = "AU"
                                Case "99"
                                    numrata = "MA"
                                Case "99999"
                                    numrata = "CO"
                                Case Else
                                    numrata = Format(myReader1("n_rata"), "00")
                            End Select

                            Label1.Text = Label1.Text & "<a href=" & Chr(34) & "AnteprimaBolletta.aspx?ID=" & myReader1("id") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & Format(myReader1("id"), "0000000000") & " Rata " & numrata & "/" & myReader1("anno") & " Emessa il " & par.FormattaData(myReader1("data_emissione")) & "</a> - <a href=" & Chr(34) & "../Contabilita/DettaglioBolletta.aspx?IDBOLL=" & myReader1("id") & "&IDANA=" & myReader1("COD_AFFITTUARIO") & "&IDCONT=" & myReader1("id_CONTRATTO") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">Dettagli</a><br />"
                            'Label1.Text = Label1.Text & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('AnteprimaBolletta.aspx?ID=" & par.IfNull(myReader1("id"), "") & "','DettagliBoll','');" & Chr(34) & ">" & Format(myReader1("id"), "0000000000") & " Rata " & numrata & "/" & myReader1("anno") & " Emessa il " & par.FormattaData(myReader1("data_emissione")) & "</a>"
                        Loop
                        Label1.Text = Label1.Text & "</div>"
                        myReader1.Close()
                        indiceFine.Value = myReader("id")
                        myReader.Close()
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If


                    'If par.IfNull(myReader("id_morosita"), 0) <> 0 Then
                    '    indiceMorosita.Value = par.IfNull(myReader("id_morosita"), "")
                    '    indiceContratto.Value = par.IfNull(myReader("id_contratto"), "")
                    '    indiceFine.Value = ""
                    'Else
                    '    If par.IfNull(myReader("id_tipo"), 0) = 3 Then
                    '        Label1.Text = "Bolletta di FINE CONTRATTO, il cui importo comprende le bollette scadute e non pagate:<br /><div id=" & Chr(34) & "Contenitore" & Chr(34) & " style=" & Chr(34) & "position: absolute; width: 100%; height: 100px; left: 10px; overflow: auto; top: 60px;" & Chr(34) & " >"
                    '        par.cmd.CommandText = "select * from siscom_mi.bol_bollette where id_bolletta_ric=" & Request.QueryString("ID") & " order by id desc"
                    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    '        Do While myReader1.Read

                    '            Select Case myReader1("n_rata")
                    '                Case "999"
                    '                    numrata = "AU"
                    '                Case "99"
                    '                    numrata = "MA"
                    '                Case "99999"
                    '                    numrata = "CO"
                    '                Case Else
                    '                    numrata = Format(myReader1("n_rata"), "00")
                    '            End Select

                    '            Label1.Text = Label1.Text & "<a href=" & Chr(34) & "AnteprimaBolletta.aspx?ID=" & myReader1("id") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & Format(myReader1("id"), "0000000000") & " Rata " & numrata & "/" & myReader1("anno") & " Emessa il " & par.FormattaData(myReader1("data_emissione")) & "</a> - <a href=" & Chr(34) & "../Contabilita/DettaglioBolletta.aspx?IDBOLL=" & myReader1("id") & "&IDANA=" & myReader1("COD_AFFITTUARIO") & "&IDCONT=" & myReader1("id_CONTRATTO") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">Dettagli</a><br />"
                    '        Loop
                    '        Label1.Text = Label1.Text & "</div>"
                    '        myReader1.Close()
                    '        indiceFine.Value = myReader("id")
                    '    Else
                    '        If par.IfNull(myReader("id_bolletta_ric"), 0) <> 0 Then
                    '            Label1.Text = "Bolletta Riclassificata, il cui importo non pagato è stato incluso nella bolletta di FINE CONTRATTO:<br /><div id=" & Chr(34) & "Contenitore" & Chr(34) & " style=" & Chr(34) & "position: absolute; width: 100%; height: 100px; left: 10px; overflow: auto; top: 60px;" & Chr(34) & " >"
                    '            par.cmd.CommandText = "select * from siscom_mi.bol_bollette where id=" & par.IfNull(myReader("id_bolletta_ric"), 0) & " order by id desc"
                    '            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    '            Do While myReader1.Read
                    '                Select Case myReader1("n_rata")
                    '                    Case "999"
                    '                        numrata = "AU"
                    '                    Case "99"
                    '                        numrata = "MA"
                    '                    Case "99999"
                    '                        numrata = "CO"
                    '                    Case Else
                    '                        numrata = Format(myReader1("n_rata"), "00")
                    '                End Select

                    '                Label1.Text = Label1.Text & "<a href=" & Chr(34) & "AnteprimaBolletta.aspx?ID=" & myReader1("id") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & Format(myReader1("id"), "0000000000") & " Rata " & numrata & "/" & myReader1("anno") & " Emessa il " & par.FormattaData(myReader1("data_emissione")) & "</a> - <a href=" & Chr(34) & "../Contabilita/DettaglioBolletta.aspx?IDBOLL=" & myReader1("id") & "&IDANA=" & myReader1("COD_AFFITTUARIO") & "&IDCONT=" & myReader1("id_CONTRATTO") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">Dettagli</a><br />"
                    '            Loop
                    '            Label1.Text = Label1.Text & "</div>"
                    '            myReader1.Close()
                    '            indiceFine.Value = myReader("id")
                    '        Else
                    '            If par.IfNull(myReader("id_rateizzazione"), 0) <> 0 Then
                    '                Label1.Text = "Bolletta Riclassificata, il cui importo non pagato è stato incluso in un piano di rientro:<br /><div id=" & Chr(34) & "Contenitore" & Chr(34) & " style=" & Chr(34) & "position: absolute; width: 100%; height: 100px; left: 10px; overflow: auto; top: 60px;" & Chr(34) & " >"
                    '                par.cmd.CommandText = "select * from siscom_mi.bol_bollette where id=" & par.IfNull(myReader("id_bolletta_ric"), 0) & " order by id desc"
                    '                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    '                Do While myReader1.Read
                    '                    Select Case myReader1("n_rata")
                    '                        Case "999"
                    '                            numrata = "AU"
                    '                        Case "99"
                    '                            numrata = "MA"
                    '                        Case "99999"
                    '                            numrata = "CO"
                    '                        Case Else
                    '                            numrata = Format(myReader1("n_rata"), "00")
                    '                    End Select

                    '                    Label1.Text = Label1.Text & "<a href=" & Chr(34) & "AnteprimaBolletta.aspx?ID=" & myReader1("id") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & Format(myReader1("id"), "0000000000") & " Rata " & numrata & "/" & myReader1("anno") & " Emessa il " & par.FormattaData(myReader1("data_emissione")) & "</a> - <a href=" & Chr(34) & "../Contabilita/DettaglioBolletta.aspx?IDBOLL=" & myReader1("id") & "&IDANA=" & myReader1("COD_AFFITTUARIO") & "&IDCONT=" & myReader1("id_CONTRATTO") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">Dettagli</a><br />"
                    '                Loop
                    '                Label1.Text = Label1.Text & "</div>"
                    '                myReader1.Close()
                    '                indiceFine.Value = myReader("id")
                    '            Else

                    '                indiceMorosita.Value = ""
                    '                indiceFine.Value = ""
                    '            End If

                    '        End If
                    '    End If
                    'End If
                End If
                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try

        End If

    End Sub
End Class
