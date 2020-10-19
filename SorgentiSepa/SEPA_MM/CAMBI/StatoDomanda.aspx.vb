
Partial Class CAMBI_StatoDomanda
    Inherits PageSetIdMode
    Dim ID_DOMANDA As Long
    Dim PAR As New CM.Global
    Dim sStringaSQL1 As String
    Dim posizione As String = ""
    Dim I As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Label8.Visible = False
            ID_DOMANDA = Request.QueryString("ID")
            sStringaSQL1 = "SELECT DOMANDE_BANDO.*,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,COMP_NUCLEO.id_dichiarazione,TAB_STATI.DESCRIZIONE,TAB_STATI.COD FROM TAB_STATI,DOMANDE_BANDO,COMP_NUCLEO WHERE DOMANDE_BANDO.ID=" & ID_DOMANDA & " AND DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND DOMANDE_BANDO.ID_STATO=TAB_STATI.COD"
            PAR.OracleConn.Open()
            Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSQL1, PAR.OracleConn)
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()

            If myReader.Read() Then
                lblpg.Text = PAR.IfNull(myReader("PG"), "")
                lblNominativo.Text = PAR.IfNull(myReader("COGNOME"), "") & " " & PAR.IfNull(myReader("NOME"), "")
                lblStato.Text = PAR.IfNull(myReader("DESCRIZIONE"), "")
                LBLiSBARCOP.Text = PAR.IfNull(myReader("isbarc_r"), "0")

                Dim cmd2 As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("select isbarc_r,posizione from bandi_graduatoria_def where id_domanda=" & ID_DOMANDA, PAR.OracleConn)
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = cmd2.ExecuteReader()
                If myReader2.Read Then
                    LBLISBARCGR.Text = PAR.IfNull(myReader2("isbarc_r"), "0")
                    posizione = PAR.IfNull(myReader2("posizione"), "")
                End If
                myReader2.Close()
                cmd2.Dispose()

                I = 1
                Dim cmd3 As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("select cognome,nome,cod_fiscale from comp_nucleo where id_dichiarazione=" & PAR.IfNull(myReader("id_dichiarazione"), -1) & " order by progr asc", PAR.OracleConn)
                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = cmd3.ExecuteReader()
                While myReader3.Read
                    lblNucleo.Text = lblNucleo.Text & I & ") " & PAR.IfNull(myReader3("cognome"), "") & " " & PAR.IfNull(myReader3("nome"), "") & " (" & PAR.IfNull(myReader3("cod_fiscale"), "") & ")</BR>"
                    I = I + 1
                End While
                myReader3.Close()
                cmd3.Dispose()


                Select Case PAR.IfNull(myReader("COD"), "")
                    Case "2"
                        Dim cmd1 As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("select descrizione from t_tipo_pratiche where cod=" & PAR.IfNull(myReader("id_tipo_contenzioso"), -1), PAR.OracleConn)
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = cmd1.ExecuteReader()
                        If myReader1.Read Then
                            lblMotivo.Text = "La domanda è in Istruttoria a seguito di " & PAR.IfNull(myReader1("descrizione"), "")
                        End If
                        myReader1.Close()
                        cmd1.Dispose()
                    Case "3"
                        Dim cmd1 As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("select descrizione from t_tipo_pratiche where cod=" & PAR.IfNull(myReader("id_tipo_contenzioso"), -1), PAR.OracleConn)
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = cmd1.ExecuteReader()
                        If myReader1.Read Then
                            lblMotivo.Text = "La domanda è in Commissione a seguito di " & PAR.IfNull(myReader1("descrizione"), "")
                        End If
                        myReader1.Close()
                    Case "4"
                        If PAR.IfNull(myReader("periodo_res"), "") = "4" Then
                            lblMotivo.Text = "Risiede e lavora da meno di 5 anni " & vbCrLf
                        End If

                        If PAR.IfNull(myReader("requisito6"), "1") = "0" Then
                            lblMotivo.Text = "Limite Patrimoniale superato / ISEE superiore al limite ERP " & vbCrLf
                        End If

                        If PAR.IfNull(myReader("requisito1"), "1") = "0" Then
                            lblMotivo.Text = "Mancanza della cittadinanza o del permesso di soggiorno " & vbCrLf
                        End If

                        If PAR.IfNull(myReader("requisito2"), "1") = "0" Then
                            lblMotivo.Text = "Mancanza della residenza anagrafica o attività lavorativa nel comune " & vbCrLf
                        End If

                        If PAR.IfNull(myReader("requisito3"), "1") = "0" Then
                            lblMotivo.Text = "Predecente Assegnazione in proprietà " & vbCrLf
                        End If

                        If PAR.IfNull(myReader("requisito4"), "1") = "0" Then
                            lblMotivo.Text = "Decadenza " & vbCrLf
                        End If

                        If PAR.IfNull(myReader("requisito5"), "1") = "0" Then
                            lblMotivo.Text = "Cessione " & vbCrLf
                        End If

                        If PAR.IfNull(myReader("requisito7"), "1") = "0" Then
                            lblMotivo.Text = "Proprietà o Godimento di alloggio adeguato " & vbCrLf
                        End If

                        If PAR.IfNull(myReader("requisito8"), "1") = "0" Then
                            lblMotivo.Text = "Morosità da alloggio ERP negli ultimi 5 anni " & vbCrLf
                        End If

                        If PAR.IfNull(myReader("requisito9"), "1") = "0" Then
                            lblMotivo.Text = "Occupazione abusiva negli ultimi 5 anni " & vbCrLf
                        End If

                        Dim cmd1 As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("select T_TIPO_ESCLUSIONE_domande.SIGLA from T_TIPO_ESCLUSIONE_domande,DOMANDE_ESCLUSIONI where DOMANDE_ESCLUSIONI.ID_TIPO_ESCLUSIONE=T_TIPO_ESCLUSIONE_domande.COD AND  DOMANDE_ESCLUSIONI.ID_DOMANDA=" & ID_DOMANDA, PAR.OracleConn)
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = cmd1.ExecuteReader()
                        While myReader1.Read
                            lblMotivo.Text = lblMotivo.Text & " " & PAR.IfNull(myReader1("SIGLA"), "")
                            Label8.Visible = True
                        End While
                        myReader1.Close()
                        cmd1.Dispose()

                    Case "8"
                        'Dim cmd1 As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("select posizione from bandi_graduatoria_def where id_domanda=" & ID_DOMANDA, PAR.OracleConn)
                        'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = cmd1.ExecuteReader()
                        'If myReader1.Read Then
                        lblMotivo.Text = "La domanda è in graduatoria di bando alla posizione n. " & posizione
                        'If PAR.IfNull(myReader("FL_INIZIO_REQ"), "0") = "1" Then
                        '    lblMotivo.Text = lblMotivo.Text & vbCrLf & " Attualmente in fase di verifica requisiti!"
                        'End If
                        'End If
                        'myReader1.Close()
                        'cmd1.Dispose()
                    Case "9"
                        lblMotivo.Text = "La domanda attualmente è in assegnazione"
                    Case "10"
                        lblMotivo.Text = "Contratto N° " & PAR.IfNull(myReader("contratto_num"), "") & " in data " & PAR.FormattaData(PAR.IfNull(myReader("contratto_data"), "")) & " Alloggio Assegnato " & PAR.IfNull(myReader("NUM_alloggio"), "")
                End Select
                If PAR.IfNull(myReader("FL_INIZIO_REQ"), "0") = "1" Then
                    lblMotivo.Text = lblMotivo.Text & vbCrLf & " Attualmente in fase di verifica requisiti!"
                End If

            End If
            myReader.Close()
            cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End If
    End Sub
End Class
