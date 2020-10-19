Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Dom_Decisioni
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If	
        If Not IsPostBack Then
            CaricaMotivazioni()
            'lblAuto.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../VSA/Autorizzazioni.aspx?IDom=" & CType(Me.Page, Object).lIdDomanda & "','Dettagli');" & Chr(34) & ">Autorizzazioni</a>"


            If CType(Me.Page.FindControl("tipoRichiesta"), HiddenField).Value = "3" Then
                ChkMotivi.Visible = True
                ChkMotiviRies.Visible = True
                btnRiduzCanone.Text = "APPLICA REVISIONE CANONE"
                lblTitolo.Text = "Decisioni per Riduzione Canone"
                btnRiduzCanone.Attributes.Add("onclick", "javascript:document.getElementById('H1').value='0';ConfRiduzCanone();")
            ElseIf CType(Me.Page.FindControl("tipoRichiesta"), HiddenField).Value = "2" Then
                ChkMotivi.Visible = True
                ChkMotiviRies.Visible = True
                btnRiduzCanone.Text = "AUTORIZZA AMPLIAMENTO"
                lblTitolo.Text = "Decisioni per Ampliamento"
                btnRiduzCanone.Attributes.Add("onclick", "javascript:document.getElementById('H1').value='0';ConfRiduzCanone();")
            ElseIf CType(Me.Page.FindControl("tipoRichiesta"), HiddenField).Value = "0" Then
                ChkMotivi.Visible = True
                ChkMotiviRies.Visible = True
                btnRiduzCanone.Text = "AUTORIZZA VOLTURA"
                lblTitolo.Text = "Decisioni per Voltura"
                btnRiduzCanone.Attributes.Add("onclick", "javascript:document.getElementById('H1').value='0';ConfRiduzCanone();")
            ElseIf CType(Me.Page.FindControl("tipoRichiesta"), HiddenField).Value = "1" Or CType(Me.Page.FindControl("tipoRichiesta"), HiddenField).Value = "6" Then
                ChkMotivi.Visible = True
                ChkMotiviRies.Visible = True
                btnRiduzCanone.Text = "AUTORIZZA SUBENTRO"
                lblTitolo.Text = "Decisioni per Subentro"
                btnRiduzCanone.Attributes.Add("onclick", "javascript:document.getElementById('H1').value='0';ConfRiduzCanone();")
            ElseIf CType(Me.Page.FindControl("tipoRichiesta"), HiddenField).Value = "7" Then
                ChkMotivi.Visible = True
                ChkMotiviRies.Visible = True
                btnRiduzCanone.Text = "AUTORIZZA OSPITALITA'"
                lblTitolo.Text = "Decisioni per Ospitalità"
                btnRiduzCanone.Attributes.Add("onclick", "javascript:document.getElementById('H1').value='0';ConfRiduzCanone();")
            ElseIf CType(Me.Page.FindControl("tipoRichiesta"), HiddenField).Value = "5" Then
                ChkMotivi.Visible = True
                ChkMotiviRies.Visible = True
                btnRiduzCanone.Text = "AUTORIZZA CAMBIO CONSENSUALE"
                lblTitolo.Text = "Decisioni per Cambio Consensuale"
                btnRiduzCanone.Attributes.Add("onclick", "javascript:document.getElementById('H1').value='0';ConfRiduzCanone();")
            ElseIf CType(Me.Page.FindControl("tipoRichiesta"), HiddenField).Value = "4" Then
                ChkMotivi.Visible = True
                ChkMotiviRies.Visible = True
                btnRiduzCanone.Text = "AUTORIZZA CAMBIO ALLOGGIO"
                lblTitolo.Text = "Decisioni per Cambio Alloggio ex art.22"
                btnRiduzCanone.Attributes.Add("onclick", "javascript:document.getElementById('H1').value='0';ConfRiduzCanone();")
            ElseIf CType(Me.Page.FindControl("tipoRichiesta"), HiddenField).Value = "8" Then
                ChkMotivi.Visible = True
                ChkMotiviRies.Visible = True
                btnRiduzCanone.Text = "AUTORIZZA CREAZ. RU ABUSIVO"
                lblTitolo.Text = "Decisioni per Creazione Posizione Abusiva"
                btnRiduzCanone.Attributes.Add("onclick", "javascript:document.getElementById('H1').value='0';ConfRiduzCanone();")
            ElseIf CType(Me.Page.FindControl("tipoRichiesta"), HiddenField).Value = "9" Then
                ChkMotivi.Visible = True
                ChkMotiviRies.Visible = True
                btnRiduzCanone.Text = "AUTORIZZA ABUS.SMO AMM.VO ART.15 C.2 RR 1/2004"
                lblTitolo.Text = "Decisioni per Abusivismo Amm.vo Art.15 C.2 RR 1/2004"
                'btnRiduzCanone.Style.Value = "display:none;"

            ElseIf CType(Me.Page.FindControl("tipoRichiesta"), HiddenField).Value = "10" Then
                ChkMotivi.Visible = True
                ChkMotiviRies.Visible = True
                btnRiduzCanone.Text = "AUTORIZZA CAMBIO CONTRATTUALE"
                lblTitolo.Text = "Decisioni per Cambio Contrattuale"
                btnRiduzCanone.Attributes.Add("onclick", "javascript:document.getElementById('H1').value='0';ConfRiduzCanone();")
            End If

            GestioneStatoD()

            '**********modifiche campi***********
            Dim CTRL As Control
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
                ElseIf TypeOf CTRL Is RadioButtonList Then
                    DirectCast(CTRL, RadioButtonList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                End If
            Next



        End If
        Me.rdbListDecisione.Attributes.Add("onclick", "javascript:visibleMotivazioni()")
        Me.rdbListRiesame.Attributes.Add("onclick", "javascript:visibleMotivazioni()")
        Me.rdbListDecis0.Attributes.Add("onclick", "javascript:visibleMotivazioni()")
        Me.rdbListRies0.Attributes.Add("onclick", "javascript:visibleMotivazioni()")
        Me.txtdataAUTORIZ.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        Me.txtDataOsserv.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        Me.chkOsserv.Attributes.Add("onclick", "javascript:visibleDataOsserv()")
    End Sub

    Private Sub CaricaMotivazioni()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select id,descrizione from t_cond_esito_negativo where id<>46 and id<>48 and motivo_domanda_vsa = " & CType(Me.Page.FindControl("Dom_Dichiara_Cambi1").FindControl("cmbTipoRichiesta"), DropDownList).SelectedItem.Value & " ORDER BY ID ASC"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            While lettore.Read
                Me.ChkMotivi.Items.Add(New ListItem(par.IfNull(lettore("descrizione"), " "), par.IfNull(lettore("id"), -1)))
                Me.ChkMotiviRies.Items.Add(New ListItem(par.IfNull(lettore("descrizione"), " "), par.IfNull(lettore("id"), -1)))
            End While
            lettore.Close()

            For i As Integer = 0 To ChkMotivi.Items.Count - 1
                ChkMotivi.Items(i).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';") 'copiaMotivazioni(this);
                ChkMotiviRies.Items(i).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';") 'copiaMotivazioni(this);
            Next


            If Not String.IsNullOrEmpty(CType(Me.Page, Object).lIdDomanda) Then
                '******************* MOTIVAZIONI NEGATIVE PER PRIMA DECISIONE *****************
                par.cmd.CommandText = "select * from vsa_dom_esiti_neg,t_cond_esito_negativo where id_domanda = " & CType(Me.Page, Object).lIdDomanda & " and vsa_dom_esiti_neg.id_cond_esito = t_cond_esito_negativo.id and cod_decisione=3"
                lettore = par.cmd.ExecuteReader
                While lettore.Read
                    Me.ChkMotivi.Items.FindByValue(lettore("id_cond_esito")).Selected = True
                End While
                lettore.Close()

                '******************* MOTIVAZIONI NEGATIVE PER RIESAME *****************
                par.cmd.CommandText = "select * from vsa_dom_esiti_neg,t_cond_esito_negativo where id_domanda = " & CType(Me.Page, Object).lIdDomanda & " and vsa_dom_esiti_neg.id_cond_esito = t_cond_esito_negativo.id and cod_decisione=6"
                lettore = par.cmd.ExecuteReader
                While lettore.Read
                    Me.ChkMotiviRies.Items.FindByValue(lettore("id_cond_esito")).Selected = True
                End While
                lettore.Close()
            End If


            If CType(Me.Page.FindControl("btnSalva"), ImageButton).Visible = False Then
                DisattivaTutto()
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub


    Public Sub GestioneStatoD()
        Try
            '*******************APERURA CONNESSIONE*********************
            Dim note1 As String = ""
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader

            If CType(Me.Page.FindControl("tipoRichiesta"), HiddenField).Value = "10" Then
                rdbListDecis0.Items.FindByValue("8").Enabled = False
                rdbListDecisione.Items.FindByValue("3").Enabled = False
            End If
            par.cmd.CommandText = "SELECT COD_DECISIONE,data FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA = " & CType(Me.Page, Object).lIdDomanda & " ORDER BY COD_DECISIONE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim dt As New Data.DataTable()
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt.Rows
                    Select Case row.Item("COD_DECISIONE")
                        Case 1

                            Me.ChkDecidi.Checked = True
                            Me.txtDataDecisione.Text = par.FormattaData(row.Item("DATA"))
                            par.cmd.CommandText = "SELECT cod_decisione, note,data FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA = " & CType(Me.Page, Object).lIdDomanda & " AND (COD_DECISIONE = 2 OR COD_DECISIONE = 3 OR COD_DECISIONE = 7 OR COD_DECISIONE = 8) ORDER BY COD_DECISIONE ASC"
                            lettore = par.cmd.ExecuteReader
                            If lettore.HasRows Then
                                While lettore.Read
                                    If par.IfNull(lettore("COD_DECISIONE"), 3) = 2 Or par.IfNull(lettore("COD_DECISIONE"), 3) = 3 Then
                                        Me.rdbListDecisione.SelectedValue = par.IfNull(lettore("COD_DECISIONE"), 3)
                                        Me.txtDataScelta.Text = par.FormattaData(par.IfNull(lettore("DATA"), ""))
                                        Me.txtNoteDecisione.Text = par.IfNull(lettore("NOTE"), "")
                                    End If

                                    If par.IfNull(lettore("COD_DECISIONE"), 8) = 7 Or par.IfNull(lettore("COD_DECISIONE"), 8) = 8 Then
                                        Me.rdbListDecis0.SelectedValue = par.IfNull(lettore("COD_DECISIONE"), 7)
                                        Me.txtNoteDec0.Text = par.IfNull(lettore("NOTE"), "")
                                    End If

                                End While
                            Else
                                Me.txtDataScelta.Text = ""
                            End If
                            lettore.Close()

                            'If lettore.Read Then
                            '    If par.IfNull(lettore("COD_DECISIONE"), 3) = 2 Or par.IfNull(lettore("COD_DECISIONE"), 3) = 3 Then
                            '        Me.rdbListDecisione.SelectedValue = par.IfNull(lettore("COD_DECISIONE"), 3)
                            '    End If
                            '    If par.IfNull(lettore("COD_DECISIONE"), 3) = 7 Or par.IfNull(lettore("COD_DECISIONE"), 3) = 8 Then
                            '        Me.rdbListDecis0.SelectedValue = par.IfNull(lettore("COD_DECISIONE"), 7)
                            '    End If
                            '    Me.txtDataScelta.Text = par.FormattaData(par.IfNull(lettore("DATA"), ""))

                            '    'Me.rdbListDecisione.Enabled = False

                            '    Me.txtNoteDecisione.Text = par.IfNull(lettore("NOTE"), "")
                            'Else
                            '    'Me.txtNoteDecisione.Text = ""
                            '    Me.txtDataScelta.Text = ""

                            'End If
                            'lettore.Close()
                        Case 4
                            Me.chkRiesame.Checked = True
                            Me.txtDataRiesame.Text = par.FormattaData(row.Item("DATA"))

                            par.cmd.CommandText = "SELECT data,cod_decisione,note FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA = " & CType(Me.Page, Object).lIdDomanda & "  AND (COD_DECISIONE = 5 OR COD_DECISIONE = 6 OR COD_DECISIONE = 9 OR COD_DECISIONE = 10) ORDER BY COD_DECISIONE ASC"
                            lettore = par.cmd.ExecuteReader
                            If lettore.HasRows Then
                                While lettore.Read
                                    If par.IfNull(lettore("COD_DECISIONE"), 6) = 5 Or par.IfNull(lettore("COD_DECISIONE"), 6) = 6 Then
                                        Me.rdbListRiesame.SelectedValue = par.IfNull(lettore("COD_DECISIONE"), 6)
                                        Me.txtDatasceltaR.Text = par.FormattaData(par.IfNull(lettore("DATA"), ""))
                                        Me.txtNoteRiesame.Text = par.IfNull(lettore("NOTE"), "")
                                    End If

                                    If par.IfNull(lettore("COD_DECISIONE"), 9) = 9 Or par.IfNull(lettore("COD_DECISIONE"), 9) = 10 Then
                                        Me.rdbListRies0.SelectedValue = par.IfNull(lettore("COD_DECISIONE"), 9)
                                        Me.txtNoteRies0.Text = par.IfNull(lettore("NOTE"), "")
                                    End If
                                End While
                            Else
                                Me.txtNoteRiesame.Text = ""
                                Me.txtDatasceltaR.Text = ""
                            End If
                            lettore.Close()

                            'If lettore.Read Then
                            '    If par.IfNull(lettore("COD_DECISIONE"), 6) = 5 Or par.IfNull(lettore("COD_DECISIONE"), 6) = 6 Then
                            '        Me.rdbListRiesame.SelectedValue = par.IfNull(lettore("COD_DECISIONE"), 6)
                            '    End If
                            '    If par.IfNull(lettore("COD_DECISIONE"), 9) = 9 Or par.IfNull(lettore("COD_DECISIONE"), 9) = 10 Then
                            '        Me.rdbListRies0.SelectedValue = par.IfNull(lettore("COD_DECISIONE"), 9)
                            '    End If
                            '    'Me.rdbListRiesame.Enabled = False
                            '    Me.txtNoteRiesame.Text = par.IfNull(lettore("NOTE"), "")
                            '    Me.txtDatasceltaR.Text = par.FormattaData(par.IfNull(lettore("DATA"), ""))
                            'Else
                            '    Me.txtNoteRiesame.Text = ""
                            '    Me.txtDatasceltaR.Text = ""
                            'End If
                            'lettore.Close()
                    End Select
                Next
            Else
                Me.txtDataRiesame.Text = ""
                Me.txtDataDecisione.Text = ""
            End If

            'par.cmd.CommandText = "select * from vsa_dom_esiti_neg,t_cond_esito_negativo where id_domanda = " & CType(Me.Page, Object).lIdDomanda & " and vsa_dom_esiti_neg.id_cond_esito = t_cond_esito_negativo.id"
            'lettore = par.cmd.ExecuteReader
            'While lettore.Read
            '    note1 &= par.IfNull(lettore("descrizione"), "") & vbCrLf
            'End While
            'lettore.Close()

            'txtNoteDecisione.Text = note1

            StatoFinestra()

            '######## Credito teorico ########
            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID = " & CType(Me.Page, Object).lIdDomanda & " AND CREDITO_TEORICO IS NOT NULL"

            Dim fl_contabilizzato As Integer = 0

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                Dim tipo As String = "Debito"
                If par.IfNull(myReader("CREDITO_TEORICO"), 0) < 0 Then
                    tipo = "Credito"
                End If
                'lblCredito.Text = "" & tipo & " teorico: <b><a href='DettaglioCredito.aspx?ID=" & CType(Me.Page, Object).lIdDomanda & "' target='_blank'>" & Format(Math.Abs(par.IfNull(myReader("CREDITO_TEORICO"), 0)), "##,##0.00") & "</a></b> - Dal: <b>" & par.FormattaData(myReader("CT_DAL")) & "</b> Al: <b>" & par.FormattaData(myReader("CT_AL")) & "</b> - <a href='DettaglioTeorico.aspx?ID=" & CType(Me.Page, Object).lIdDomanda & "' target='_blank'>Dettagli Bollettato</a>"
                lblCredito.Text = "" & tipo & " teorico: <b><a href='DettaglioCredito.aspx?ID=" & CType(Me.Page, Object).lIdDomanda & "' target='_blank'>" & Format(Math.Abs(par.IfNull(myReader("CREDITO_TEORICO"), 0)), "##,##0.00") & "</a></b> - Dal: <b>" & par.FormattaData(myReader("CT_DAL")) & "</b> Al: <b>" & par.FormattaData(myReader("CT_AL")) & "</b>"

                fl_contabilizzato = myReader("fl_contabilizzato")
            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID = " & CType(Me.Page, Object).lIdDomanda
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If myReader("FL_OSSERVAZIONI") = 1 Then
                    chkOsserv.Checked = True
                    txtDataOsserv.Text = par.FormattaData(par.IfNull(myReader("DATA_OSSERVAZIONI"), ""))
                Else
                    chkOsserv.Checked = False
                End If
                dataPr = par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), ""))
                txtdataAUTORIZ.Text = par.FormattaData(par.IfNull(myReader("DATA_AUTORIZZAZIONE"), ""))
            End If
            myReader.Close()

            Dim GiaRidotto As Integer = 0
            par.cmd.CommandText = "SELECT * FROM EVENTI_BANDI_VSA WHERE ID_DOMANDA = " & CType(Me.Page, Object).lIdDomanda & " AND (COD_EVENTO = 'F179' or COD_EVENTO = 'F165' or COD_EVENTO = 'F196' or COD_EVENTO = 'F197' or COD_EVENTO = 'F204' or COD_EVENTO='F208' or COD_EVENTO='F210' or COD_EVENTO='F212' or COD_EVENTO='F213')"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                GiaRidotto = 1
                autorizzFinale.Value = 1
                chkOsserv.Enabled = False
                txtDataOsserv.Enabled = False
                If fl_contabilizzato = "1" Then
                    btnContabilizza.Visible = False
                Else
                    If CType(Me.Page.FindControl("tipoRichiesta"), HiddenField).Value = "3" And Session.Item("OP_NORM_VSA") = 1 Then
                        btnContabilizza.Visible = True
                    End If
                End If

            End If
            lettore.Close()
            If Me.Page.FindControl("btnSalva").Visible = False Or GiaRidotto = 1 Then
                DisattivaTutto()
            End If


        Catch ex As Exception
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub
    Private Sub StatoFinestra()

        If par.IfEmpty(rdbListDecisione.SelectedValue, 0) = 3 Then
            Me.ChkMotivi.Enabled = True
        End If
        If par.IfEmpty(rdbListRiesame.SelectedValue, 0) = 6 Then
            Me.ChkMotiviRies.Enabled = True
        End If
        If Session.Item("OP_NORM_VSA") = 1 Then
            Me.ChkDecidi.Enabled = True
            Me.rdbListDecis0.Enabled = True
        End If



        If Me.ChkDecidi.Checked = True And Session.Item("OP_RESP_VSA") = 1 Then
            Me.rdbListDecisione.Enabled = True
            Me.ChkMotivi.Enabled = True
            Me.txtNoteDecisione.ReadOnly = False
            Me.ChkDecidi.Enabled = False
            Me.rdbListDecis0.Enabled = False
            Me.txtNoteDec0.Enabled = False
            Me.ChkMotiviRies.Visible = False
        End If

        If par.IfEmpty(rdbListDecisione.SelectedValue, 0) >= 2 Then
            If Session.Item("OP_RESP_VSA") = 1 Then
                Me.rdbListDecisione.Enabled = False
                Me.txtNoteDecisione.ReadOnly = False
                Me.ChkDecidi.Enabled = False
                Me.rdbListDecis0.Enabled = False
                Me.txtNoteDec0.Enabled = False
            Else
                Me.rdbListDecisione.Enabled = True
                Me.txtNoteDecisione.ReadOnly = True
                Me.ChkMotivi.Enabled = False
                Me.ChkDecidi.Enabled = False
                Me.rdbListDecis0.Enabled = False
                Me.txtNoteDec0.Enabled = False
            End If

            If rdbListDecisione.SelectedValue = 3 And Session.Item("OP_NORM_VSA") = 1 Then
                Me.rdbListDecisione.Enabled = True
                Me.chkRiesame.Enabled = True
                Me.rdbListRies0.Enabled = True
                chkOsserv.Enabled = True
                txtDataOsserv.Enabled = True
                Me.btnRiduzCanone.Enabled = False
                Me.txtdataAUTORIZ.Enabled = False
                Me.txtNoteDecisione.ReadOnly = False
                'Me.ChkMotivi.Enabled = False
                'Me.txtNoteDecisione.ReadOnly = True
            ElseIf rdbListDecisione.SelectedValue = 2 And Session.Item("OP_NORM_VSA") = 1 Then
                Me.rdbListDecisione.Enabled = True
                Me.txtdataAUTORIZ.Enabled = True
                Me.btnRiduzCanone.Enabled = True
                Me.chkRiesame.Enabled = False
                Me.rdbListRies0.Enabled = False
                Me.txtNoteRies0.Enabled = False
                chkOsserv.Enabled = False
                txtDataOsserv.Enabled = False
                Me.txtNoteDecisione.ReadOnly = False
                'Me.ChkMotivi.Enabled = False
            End If

        End If




        If Me.chkRiesame.Checked = True And Session.Item("OP_RESP_VSA") = 1 Then
            Me.txtNoteDecisione.ReadOnly = True
            Me.rdbListRiesame.Enabled = True
            Me.txtNoteRiesame.ReadOnly = False
            Me.rdbListDecisione.Enabled = False
            Me.txtNoteDecisione.Enabled = False
            Me.ChkMotivi.Enabled = False
            Me.chkRiesame.Enabled = False
            Me.rdbListRies0.Enabled = False
            Me.txtNoteRies0.Enabled = False
            Me.ChkMotiviRies.Visible = True
            Me.ChkMotiviRies.Enabled = True
            chkOsserv.Enabled = True
            txtDataOsserv.Enabled = True
        End If

        If par.IfEmpty(rdbListRiesame.SelectedValue, 0) >= 5 Then
            If Session.Item("OP_RESP_VSA") = 1 Then

                Me.txtNoteRiesame.ReadOnly = False
                Me.chkRiesame.Enabled = False
                Me.rdbListRies0.Enabled = False
                Me.txtNoteRies0.Enabled = False
                chkOsserv.Enabled = True
                txtDataOsserv.Enabled = True
            Else
                Me.rdbListRiesame.Enabled = False
                Me.txtNoteRiesame.ReadOnly = True
                Me.chkRiesame.Enabled = False
                Me.rdbListRies0.Enabled = False
                Me.txtNoteRies0.Enabled = False
                Me.ChkMotiviRies.Enabled = False
            End If

            If rdbListRiesame.SelectedValue = 5 And Session.Item("OP_NORM_VSA") = 1 Then
                chkOsserv.Enabled = True
                txtDataOsserv.Enabled = True
                Me.rdbListRiesame.Enabled = True
                btnRiduzCanone.Enabled = True
                Me.txtNoteRiesame.ReadOnly = False
                Me.txtdataAUTORIZ.Enabled = True
            Else
                'chkOsserv.Enabled = False
                'txtDataOsserv.Enabled = False

                ''''Me.rdbListRiesame.Enabled = False
                'Me.txtNoteRiesame.ReadOnly = True
                btnRiduzCanone.Enabled = False
                Me.txtdataAUTORIZ.Enabled = False
                ''''Me.ChkMotiviRies.Enabled = False
                'Me.txtNoteRiesame.Enabled = False
            End If



        End If

        If Session.Item("OP_RESP_VSA") = 1 And Session.Item("OP_NORM_VSA") = 0 Then
            Me.ChkDecidi.Enabled = False
            Me.rdbListDecis0.Enabled = False
            Me.rdbListRies0.Enabled = False
            Me.chkRiesame.Enabled = False
        End If
    End Sub

    Public Sub DisattivaTutto()


        Me.ChkDecidi.Enabled = False
        Me.txtDataDecisione.Enabled = False
        Me.chkRiesame.Enabled = False
        Me.rdbListDecisione.Enabled = False
        Me.rdbListRiesame.Enabled = False
        Me.txtNoteDecisione.Enabled = False
        Me.txtNoteRiesame.Enabled = False
        Me.btnRiduzCanone.Enabled = False
        Me.ChkMotivi.Enabled = False
        Me.ChkMotiviRies.Enabled = False
        Me.txtdataAUTORIZ.Enabled = False
        'Me.btnContabilizza.Enabled = False


    End Sub

    Protected Sub btnRiduzCanone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRiduzCanone.Click
        If CType(Me.Page.FindControl("ConfRidCan"), HiddenField).Value = 1 Then
            If CType(Me.Page.FindControl("lblISBAR"), Label).Text > 0 Or CType(Me.Page.FindControl("ISEEnullo"), HiddenField).Value = 1 Then
                If txtdataAUTORIZ.Text = "" Then
                    'Response.Write("<script>alert('Attenzione! Inserire la data di autorizzazione della domanda!');</script>")
                    txtdataAUTORIZ.Text = par.FormattaData(Format(Now, "yyyyMMdd"))
                    If par.AggiustaData(txtdataAUTORIZ.Text) < par.AggiustaData(dataPr) Then
                        Response.Write("<script>alert('Attenzione...La data di autorizzazione non può essere antecedente alla data di presentazione della domanda!')</script>")
                    Else
                        CType(Me.Page, Object).ApplicaRiduzCanone()
                        CType(Me.Page.FindControl("H1"), HiddenField).Value = 1
                        CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 0
                        GestioneStatoD()
                    End If
                Else
                    If IsDate(txtdataAUTORIZ.Text) = False Then
                        Response.Write("<script>alert('Attenzione! La data di autorizzazione inserita non è valida!')</script>")
                    Else
                        If par.AggiustaData(txtdataAUTORIZ.Text) < par.AggiustaData(dataPr) Then
                            Response.Write("<script>alert('Attenzione...La data di autorizzazione non può essere antecedente alla data di presentazione della domanda!')</script>")
                        Else
                            CType(Me.Page, Object).ApplicaRiduzCanone()
                            CType(Me.Page.FindControl("H1"), HiddenField).Value = 1
                            CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 0
                            GestioneStatoD()
                        End If
                    End If
                End If
            Else
                Response.Write("<script>alert('IMPOSSIBILE PROCEDERE! ISEE NON CALCOLATO!\nPer procedere salvare prima la domanda e poi cliccare su stampa!');</script>")

            End If
        End If


    End Sub

    Protected Sub ChkMotivi_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ChkMotivi.SelectedIndexChanged

        Dim noteNega As String
        noteNega = txtNoteDecisione.Text

        For Each itemMotivi As ListItem In ChkMotivi.Items
            If itemMotivi.Selected = True Then
                If noteNega <> "" Then
                    If Not noteNega.Contains(itemMotivi.Text) Then
                        If Not Right(noteNega, 2).Contains(",") Then
                            noteNega &= ", " & itemMotivi.Text
                        Else
                            noteNega &= itemMotivi.Text
                        End If
                    End If
                Else
                    noteNega &= itemMotivi.Text & ", "
                End If
            Else
                If noteNega.Contains(itemMotivi.Text & ",") Then
                    noteNega = noteNega.Replace(itemMotivi.Text & ", ", "")
                Else
                    noteNega.Contains(itemMotivi.Text)
                    noteNega = noteNega.Replace(itemMotivi.Text, "")
                End If
            End If
        Next
        If Right(noteNega, 2).Contains(",") Then
            noteNega = noteNega.Substring(0, noteNega.Length - 2)
        End If
        txtNoteDecisione.Text = noteNega
    End Sub

    Protected Sub btnContabilizza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContabilizza.Click
        If CType(Me.Page.FindControl("ConfRidCan"), HiddenField).Value = 1 Then
            CType(Me.Page, Object).ApplicaContabilizzazione()
        End If
    End Sub

    Protected Sub ChkMotiviRies_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ChkMotiviRies.SelectedIndexChanged
        Dim noteNegaR As String
        noteNegaR = txtNoteRiesame.Text

        For Each itemMotiviRies As ListItem In ChkMotiviRies.Items
            If itemMotiviRies.Selected = True Then
                If noteNegaR <> "" Then
                    If Not noteNegaR.Contains(itemMotiviRies.Text) Then
                        If Not Right(noteNegaR, 2).Contains(",") Then
                            noteNegaR &= ", " & itemMotiviRies.Text
                        Else
                            noteNegaR &= itemMotiviRies.Text
                        End If
                    End If
                Else
                    noteNegaR &= itemMotiviRies.Text & ", "
                End If
            Else
                If noteNegaR.Contains(itemMotiviRies.Text & ",") Then
                    noteNegaR = noteNegaR.Replace(itemMotiviRies.Text & ", ", "")
                Else
                    noteNegaR.Contains(itemMotiviRies.Text)
                    noteNegaR = noteNegaR.Replace(itemMotiviRies.Text, "")
                End If
            End If
        Next
        If Right(noteNegaR, 2).Contains(",") Then
            noteNegaR = noteNegaR.Substring(0, noteNegaR.Length - 2)
        End If
        txtNoteRiesame.Text = noteNegaR

    End Sub

    Public Property dataPr() As String
        Get
            If Not (ViewState("par_dataPr") Is Nothing) Then
                Return CStr(ViewState("par_dataPr"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataPr") = value
        End Set
    End Property

    Protected Sub rdbListDecisione_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdbListDecisione.SelectedIndexChanged

    End Sub
End Class
