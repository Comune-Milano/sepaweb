
Partial Class Contratti_SpeseUnita
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:100px; left:100px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        If Not IsPostBack Then
            Response.Flush()
            Try
                Dim tabella As String = ""
                Dim MCRISCALD As Double = 0
                Dim SUP_NETTA As Double = 0
                Dim VOCE(3) As Double
                Dim IMPORTO(3) As Double
                Dim I As Integer = 0


                Label3.Text = "Anno " & Year(Now)

                idunita.Value = Request.QueryString("ID")
                T.Value = Request.QueryString("T")
                CONTRATTO.Value = Request.QueryString("IDC")

                If Request.QueryString("IDC") = "" Or Request.QueryString("IDC") = "-1" Then
                    imgSalva.Visible = False
                End If

                'par.OracleConn.Open()
                'par.SettaCommand(par)

                par.OracleConn = CType(HttpContext.Current.Session.Item(T.Value), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & T.Value), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "select * from SISCOM_MI.dimensioni where cod_tipologia='SUP_NETTA' AND ID_UNITA_IMMOBILIARE=" & idunita.Value
                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader3.Read Then
                    SUP_NETTA = par.IfNull(myReader3("VALORE"), 0)
                End If
                myReader3.Close()

                par.cmd.CommandText = "SELECT edifici.cod_tipologia_imp_riscald,edifici.condominio,edifici.gest_risc_dir,EDIFICI.ID AS IDEDIFICIO,unita_immobiliari.* FROM siscom_mi.edifici,SISCOM_MI.unita_immobiliari WHERE unita_immobiliari.id_edificio=edifici.id AND unita_immobiliari.ID=" & Request.QueryString("ID")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    If par.IfNull(myReader("condominio"), "") = "0" Then

                        Label1.Text = "Sup. Netta: " & SUP_NETTA

                        If SUP_NETTA = 0 Then
                            Ch300.Checked = False
                            Ch300.Enabled = False

                            Ch301.Checked = False
                            Ch301.Enabled = False

                            Ch303.Checked = False
                            Ch303.Enabled = False

                            Ch302.Checked = False
                            Ch302.Enabled = False

                            Image1.Visible = True
                        End If


                        If SUP_NETTA = 0 Then
                            'non salva
                        End If

                        Dim coeff_asc As Integer = 0
                        Dim COEFF_SPESE As Double = 0

                        'par.cmd.CommandText = "select * from SISCOM_MI.IMPIANTI where ID_EDIFICIO=" & par.IfNull(myReader("ID_EDIFICIO"), "NULL") & " AND COD_TIPOLOGIA='SO'"
                        par.cmd.CommandText = "SELECT IMPIANTI.* FROM siscom_mi.i_sollevamento,SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE i_sollevamento.tipologia='1' and i_sollevamento.id=impianti.id and UNITA_IMMOBILIARI.ID=" & idunita.Value & " AND IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI_SCALE.ID_SCALA=UNITA_IMMOBILIARI.ID_SCALA AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO"
                        myReader3 = par.cmd.ExecuteReader()
                        If myReader3.HasRows = True Then
                            coeff_asc = 1
                        End If
                        myReader3.Close()

                        If coeff_asc = 1 Then
                            If par.IfNull(myReader("cod_tipo_livello_piano"), "00") <> "01" Then
                                coeff_asc = 1
                            Else
                                coeff_asc = 0
                            End If
                        End If

                        If coeff_asc = 1 Then
                            par.cmd.CommandText = "select * from SISCOM_MI.tariffe_oneri WHERE id_voce=300"
                            myReader3 = par.cmd.ExecuteReader()
                            If myReader3.Read Then
                                lblDett300.Text = SUP_NETTA & " (Sup. Netta in mq) * " & par.IfNull(myReader3("TARIFFA"), 0)
                                If par.IfNull(myReader("cod_tipo_livello_piano"), "00") = "42" Then
                                    lblDett300.Text = lblDett300.Text & "-P. MEZZANINO!"
                                End If
                                lblImp300.Text = Format(SUP_NETTA * par.IfNull(myReader3("TARIFFA"), 0), "0.00")
                                Ch300.Checked = True
                            End If
                            myReader3.Close()
                        Else
                            lblDett300.Text = "P.Terra o senza Asc."
                            lblImp300.Text = "0,00"
                            Ch300.Checked = False
                            Ch300.Enabled = False
                        End If


                        If par.IfNull(myReader("cod_tipologia_imp_riscald"), "CENT") = "CENT" Then
                            par.cmd.CommandText = "select * from SISCOM_MI.tariffe_oneri WHERE id_voce=302"
                            myReader3 = par.cmd.ExecuteReader()
                            If myReader3.Read Then
                                lblDett302.Text = SUP_NETTA & " (Sup. Netta in mq) * " & par.IfNull(myReader3("TARIFFA"), 0)
                                lblImp302.Text = SUP_NETTA * par.IfNull(myReader3("TARIFFA"), 0)
                                Ch302.Checked = True
                            End If
                            myReader3.Close()
                        Else
                            lblImp302.Text = "0,00"
                            lblDett302.Text = "Imp.Autonomo"
                            Ch302.Checked = False
                            Ch302.Enabled = False
                        End If


                        Ch301.Checked = False
                        Ch301.Enabled = False
                        lblDett301.Text = "Comprese nelle sp. Generali"
                        lblImp301.Text = "0,00"



                        par.cmd.CommandText = "select * from SISCOM_MI.tariffe_oneri WHERE id_voce=303"
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            lblDett303.Text = SUP_NETTA & " (Sup. Netta in mq) * " & par.IfNull(myReader2("TARIFFA"), 0)
                            lblImp303.Text = SUP_NETTA * par.IfNull(myReader2("TARIFFA"), 0)
                            Ch303.Checked = True
                        End If
                        myReader2.Close()


                    Else
                        If par.IfNull(myReader("gest_risc_dir"), "") = "0" Then


                            Label1.Text = "Sup. Netta: " & SUP_NETTA

                            Ch300.Checked = False
                            Ch300.Enabled = False

                            Ch301.Checked = False
                            Ch301.Enabled = False

                            Ch303.Checked = False
                            Ch303.Enabled = False
                            Image1.Visible = True


                            If SUP_NETTA = 0 Then
                                Ch302.Checked = False
                                Ch302.Enabled = False

                                Image1.Visible = True
                            End If

                            If par.IfNull(myReader("cod_tipologia_imp_riscald"), "CENT") = "CENT" Then
                                par.cmd.CommandText = "select * from SISCOM_MI.tariffe_oneri WHERE id_voce=302"
                                myReader3 = par.cmd.ExecuteReader()
                                If myReader3.Read Then
                                    lblDett302.Text = SUP_NETTA & " (Sup. Netta in mq) * " & par.IfNull(myReader3("TARIFFA"), 0)
                                    lblImp302.Text = SUP_NETTA * par.IfNull(myReader3("TARIFFA"), 0)
                                    Ch302.Checked = True
                                End If
                                myReader3.Close()
                            Else
                                lblImp302.Text = "0,00"
                                lblDett302.Text = "Imp.Autonomo"
                                Ch302.Checked = False
                                Ch302.Enabled = False
                            End If

                            'par.cmd.CommandText = "select * from SISCOM_MI.tariffe_oneri WHERE id_voce=302"
                            'myReader3 = par.cmd.ExecuteReader()
                            'If myReader3.Read Then
                            '    lblDett302.Text = MCRISCALD & " (Vol. Riscaldato) * " & par.IfNull(myReader3("TARIFFA"), 0)
                            '    lblImp302.Text = MCRISCALD * par.IfNull(myReader3("TARIFFA"), 0)
                            'End If
                            'myReader3.Close()



                            Response.Write("<script>alert('Attenzione, unità immobiliare in condominio, MA con gestione INDIRETTA del riscaldamento, è possibile calcolare solo le spese di riscaldamento. Se questa informazione non è corretta, contattare l\'amministratore del sistema!');</script>")
                            Ch300.Enabled = False
                            Ch301.Enabled = False
                            Ch303.Enabled = False

                        Else
                            Response.Write("<script>alert('Attenzione, unità immobiliare in condominio. Non è possibile calcolare le spese. Se questa informazione non è corretta, contattare l\'amministratore del sistema!');</script>")
                            Ch300.Enabled = False
                            Ch301.Enabled = False
                            Ch302.Enabled = False
                            Ch303.Enabled = False
                            imgSalva.Visible = False
                        End If


                    End If

                End If
                myReader.Close()


                ' par.cmd.Dispose()
                ' par.OracleConn.Close()
                ' Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                'par.OracleConn.Close()
                'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Label2.Visible = True
                Label2.Text = ex.Message
                'Session.Add("ERRORE", "Provenienza:Calcolo Spese Unita" & " - " & ex.Message)
                'Response.Write("<script>top.location.href='../Errore.aspx';</script>")

            End Try
        End If

    End Sub


    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click
        Try
            Dim Anno As String = ""
            Dim ESERCIZIOF As Long = 0
            Dim ESERCIZIOF_1 As Long = 0

            LABEL2.VISIBLE = False
            par.OracleConn = CType(HttpContext.Current.Session.Item(T.VALUE), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & T.VALUE), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans





            par.cmd.CommandText = "SELECT cod_tipologia_contr_loc,FL_STAMPATO,DATA_DECORRENZA,NRO_RATE FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & CONTRATTO.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("FL_STAMPATO"), "0") <> "1" Or par.IfNull(myReader("cod_tipologia_contr_loc"), "") = "NONE" Then
                    par.cmd.CommandText = "DELETE FROM siscom_mi.BOL_SCHEMA WHERE (ID_VOCE=300 OR ID_VOCE=301 OR ID_VOCE=302 OR ID_VOCE=303) AND ID_CONTRATTO=" & CONTRATTO.Value
                    par.cmd.ExecuteNonQuery()

                    If par.IfNull(myReader("DATA_DECORRENZA"), "") <> "" Then
                        Anno = Mid(myReader("DATA_DECORRENZA"), 1, 4)

                        If Anno < Year(Now) Then
                            Anno = Year(Now)
                        End If

                        If Month(Now) = 12 Then
                            Anno = Year(Now) + 1
                        End If

                        If Anno > Year(Now) Then
                            Anno = Year(Now) + 1
                        End If

                        ESERCIZIOF = par.RicavaEsercizio(Anno)
                        ESERCIZIOF_1 = par.RicavaEsercizio(Anno + 1)

                        If Ch300.Checked = True Then
                            par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,FL_PREVENTIVI) Values (siscom_mi.seq_bol_schema.nextval, " & CONTRATTO.Value & ", " & idunita.Value & ", " & ESERCIZIOF & ", 300, " & par.VirgoleInPunti(lblImp300.Text) & " , 1, " & par.IfNull(myReader("nro_rate"), "1") & ", " & par.VirgoleInPunti(Format(lblImp300.Text / par.IfNull(myReader("nro_rate"), "1"), "0.00")) & " , " & Anno & ",1)"
                            par.cmd.ExecuteNonQuery()
                            'If Anno = Year(Now) Then
                            '    par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,FL_PREVENTIVI) Values (siscom_mi.seq_bol_schema.nextval, " & CONTRATTO.Value & ", " & idunita.Value & ", " & ESERCIZIOF_1 & ", 300, " & par.VirgoleInPunti(lblImp300.Text) & " , 1, " & par.IfNull(myReader("nro_rate"), "1") & ", " & par.VirgoleInPunti(Format(lblImp300.Text / par.IfNull(myReader("nro_rate"), "1"), "0.00")) & " , " & Anno + 1 & ",1)"
                            '    par.cmd.ExecuteNonQuery()
                            'End If
                        End If

                        If Ch301.Checked = True Then
                            par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,FL_PREVENTIVI) Values (siscom_mi.seq_bol_schema.nextval, " & CONTRATTO.Value & ", " & idunita.Value & ", " & ESERCIZIOF & ", 301, " & par.VirgoleInPunti(lblImp301.Text) & " , 1, " & par.IfNull(myReader("nro_rate"), "1") & ", " & par.VirgoleInPunti(Format(lblImp301.Text / par.IfNull(myReader("nro_rate"), "1"), "0.00")) & " , " & Anno & ",1)"
                            par.cmd.ExecuteNonQuery()
                            'If Anno = Year(Now) Then
                            '    par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,FL_PREVENTIVI) Values (siscom_mi.seq_bol_schema.nextval, " & CONTRATTO.Value & ", " & idunita.Value & ", " & ESERCIZIOF_1 & ", 301, " & par.VirgoleInPunti(lblImp301.Text) & " , 1, " & par.IfNull(myReader("nro_rate"), "1") & ", " & par.VirgoleInPunti(Format(lblImp301.Text / par.IfNull(myReader("nro_rate"), "1"), "0.00")) & " , " & Anno + 1 & ",1)"
                            '    par.cmd.ExecuteNonQuery()
                            'End If
                        End If

                        If Ch302.Checked = True Then
                            par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,FL_PREVENTIVI) Values (siscom_mi.seq_bol_schema.nextval, " & CONTRATTO.Value & ", " & idunita.Value & ", " & ESERCIZIOF & ", 302, " & par.VirgoleInPunti(lblImp302.Text) & " , 1, " & par.IfNull(myReader("nro_rate"), "1") & ", " & par.VirgoleInPunti(Format(lblImp302.Text / par.IfNull(myReader("nro_rate"), "1"), "0.00")) & " , " & Anno & ",1)"
                            par.cmd.ExecuteNonQuery()
                            'If Anno = Year(Now) Then
                            '    par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,FL_PREVENTIVI) Values (siscom_mi.seq_bol_schema.nextval, " & CONTRATTO.Value & ", " & idunita.Value & ", " & ESERCIZIOF_1 & ", 302, " & par.VirgoleInPunti(lblImp302.Text) & " , 1, " & par.IfNull(myReader("nro_rate"), "1") & ", " & par.VirgoleInPunti(Format(lblImp302.Text / par.IfNull(myReader("nro_rate"), "1"), "0.00")) & " , " & Anno + 1 & ",1)"
                            '    par.cmd.ExecuteNonQuery()
                            'End If
                        End If

                        If Ch303.Checked = True Then
                            par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,FL_PREVENTIVI) Values (siscom_mi.seq_bol_schema.nextval, " & CONTRATTO.Value & ", " & idunita.Value & ", " & ESERCIZIOF & ", 303, " & par.VirgoleInPunti(lblImp303.Text) & " , 1, " & par.IfNull(myReader("nro_rate"), "1") & ", " & par.VirgoleInPunti(Format(lblImp303.Text / par.IfNull(myReader("nro_rate"), "1"), "0.00")) & " , " & Anno & ",1)"
                            par.cmd.ExecuteNonQuery()
                            'If Anno = Year(Now) Then
                            '    par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,FL_PREVENTIVI) Values (siscom_mi.seq_bol_schema.nextval, " & CONTRATTO.Value & ", " & idunita.Value & ", " & ESERCIZIOF_1 & ", 303, " & par.VirgoleInPunti(lblImp303.Text) & " , 1, " & par.IfNull(myReader("nro_rate"), "1") & ", " & par.VirgoleInPunti(Format(lblImp303.Text / par.IfNull(myReader("nro_rate"), "1"), "0.00")) & " , " & Anno + 1 & ",1)"
                            '    par.cmd.ExecuteNonQuery()
                            'End If
                        End If

                        Response.Write("<script>alert('Operazione Effettuata! Premere ora il pulsante SALVA per visualizzare le modifiche!');window.opener.document.form1.txtModificato.value = '1';self.close();</script>")
                    Else
                        Response.Write("<script>alert('Attenzione, è obbligatorio inserire la data di decorrenza del contratto!');</script>")
                    End If
                Else
                    Response.Write("<script>alert('Attenzione, Il contratto è stato già stampato o si tratta di un contratto su abusivi!');</script>")
                End If
            Else
                Response.Write("<script>alert('Attenzione, è stata gia effettuata la stampa del rapporto! Operazione non possibile!');self.close();</script>")
            End If
            myReader.Close()



        Catch ex As Exception
            Label2.Visible = True
            Label2.Text = ex.Message
        End Try

    End Sub
End Class
