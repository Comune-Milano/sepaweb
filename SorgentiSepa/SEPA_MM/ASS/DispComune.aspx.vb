
Partial Class ASS_DispAler
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sIdAlloggio As String = "0"

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            imgManutentivo.Visible = False
            lblManutentivo.Visible = False

            sIdAlloggio = Request.QueryString("ID")
            DataMinima = Request.QueryString("D")
            txtid.Text = sIdAlloggio

            par.RiempiDList(Me, par.OracleConn, "cmbPiano", "select * from siscom_mi.tipo_livello_piano order by descrizione asc", "DESCRIZIONE", "COD")
            par.RiempiDListConVuoto(Me, par.OracleConn, "cmbTipo", "select * from T_TIPO_ALL_ERP ORDER BY COD ASC", "DESCRIZIONE", "COD")
            par.RiempiDList(Me, par.OracleConn, "cmbTipoVia", "select * from T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")



            'If Session.Item("ABB_ERP") = "1" Then
            '    cmbTipologia.Items.Add(New ListItem("ERP", "1"))

            'End If

            'If Session.Item("ABB_392") = "1" Then
            '    cmbTipologia.Items.Add(New ListItem("392/78", "2"))
            'End If

            'If Session.Item("ABB_431") = "1" Then
            '    cmbTipologia.Items.Add(New ListItem("431/98", "3"))
            'End If

            'If Session.Item("ABB_UD") = "1" Then
            '    cmbTipologia.Items.Add(New ListItem("USI DIVERSI", "4"))
            'End If



            Try
                par.OracleConn.Open()
                par.SettaCommand(par)


                par.cmd.CommandText = "SELECT ZONA_ALER.* FROM SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,ZONA_ALER WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND ZONA_ALER.COD = EDIFICI.ID_ZONA AND UNITA_IMMOBILIARI.ID=" & txtid.Text
                Dim myReader31 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader31.Read Then
                    cmbZona.Items.Add(New ListItem(Format(par.IfNull(myReader31("ZONA"), "--"), "00"), Format(par.IfNull(myReader31("ZONA"), "--"), "00")))
                Else
                    cmbZona.Items.Add(New ListItem("--", "-1"))
                End If
                myReader31.Close()


                par.cmd.CommandText = "select SCALE_EDIFICI.DESCRIZIONE AS SCALE, EDIFICI.NUM_ASCENSORI,indirizzi.civico,indirizzi.descrizione as via,comuni_nazioni.nome as comune_di,identificativi_catastali.foglio,identificativi_catastali.sub,identificativi_catastali.numero,UNITA_IMMOBILIARI.*  from SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.EDIFICI,siscom_mi.indirizzi,comuni_nazioni,siscom_mi.identificativi_catastali,siscom_mi.unita_immobiliari where UNITA_IMMOBILIARI.ID_SCALA=SCALE_EDIFICI.ID (+) AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND indirizzi.id=UNITA_IMMOBILIARI.id_indirizzo and comuni_nazioni.cod=EDIFICI.cod_comune and UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) and unita_immobiliari.id=" & txtid.Text
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    txtCodice.Text = par.IfNull(myReader("cod_unita_immobiliare"), "")
                    hiddenTipo.Value = par.IfNull(myReader("cod_tipologia"), "")

                    'Label18.Text = "javascript:window.open('../CENSIMENTO/InserimentoUniImmob.aspx?LE=1&ID=" & par.IfNull(myReader("cod_unita_immobiliare"), "") & "','Dettagli','height=530,top=0,left=0,width=674');"
                    Label18.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & par.IfNull(myReader("cod_unita_immobiliare"), "") & "','Dettagli','height=580,top=0,left=0,width=780');" & Chr(34) & ">Clicca qui per visualizzare i dettagli dell&#39;unità</a>"


                    If par.IfNull(myReader("ID_DESTINAZIONE_USO"), "") = "5" And Session.Item("ABB_UD") = "0" Then
                        Response.Write("<script>alert('Attenzione...unità di tipo USI DIVERSI. Non si dispone delle autorizzazioni per poter mettere in disponibilità questa unità!');</script>")
                        imgSalva.Enabled = False
                    End If

                    If (par.IfNull(myReader("ID_DESTINAZIONE_USO"), "1") = "6" Or par.IfNull(myReader("ID_DESTINAZIONE_USO"), "1") = "1" Or par.IfNull(myReader("ID_DESTINAZIONE_USO"), "") = "2") And Session.Item("ABB_ERP") = "0" Then
                        Response.Write("<script>alert('Attenzione...unità di tipo ERP. Non si dispone delle autorizzazioni per poter mettere in disponibilità questa unità!');</script>")
                        imgSalva.Enabled = False
                    End If


                    If par.IfNull(myReader("ID_DESTINAZIONE_USO"), "") = "3" And Session.Item("ABB_431") = "0" Then
                        Response.Write("<script>alert('Attenzione...unità di tipo 431/98. Non si dispone delle autorizzazioni per poter mettere in disponibilità questa unità!');</script>")
                        imgSalva.Enabled = False
                    End If

                    If par.IfNull(myReader("ID_DESTINAZIONE_USO"), "") = "4" And Session.Item("ABB_392") = "0" Then
                        Response.Write("<script>alert('Attenzione...unità di tipo 392/78. Non si dispone delle autorizzazioni per poter mettere in disponibilità questa unità!');</script>")
                        imgSalva.Enabled = False
                    End If


                    If par.IfNull(myReader("cod_tipologia"), "") <> "AL" Then

                        chBar.Visible = False
                        chHandicap.Visible = False

                        If Session.Item("ABB_UD") = "1" Then
                            cmbTipologia.Items.Add(New ListItem("USI DIVERSI", "4"))

                            If par.IfNull(myReader("ID_DESTINAZIONE_USO"), "") = "5" Then
                                cmbTipologia.SelectedIndex = -1
                                cmbTipologia.Items.FindByValue("4").Selected = True
                                cmbTipologia.Enabled = False
                            End If

                        End If

                        'If Session.Item("ABB_OA") = "1" Then
                        '    cmbTipologia.Items.Add(New ListItem("OCCUP.ABUSIVE", "5"))
                        'End If


                    Else

                        If Session.Item("ABB_ERP") = "1" Then
                            If par.IfNull(myReader("ID_DESTINAZIONE_USO"), "") <> "6" Then
                                cmbTipologia.Items.Add(New ListItem("ERP Sociale", "1"))
                                cmbTipologia.Items.Add(New ListItem("ERP Moderato", "6"))

                                If par.IfNull(myReader("ID_DESTINAZIONE_USO"), "") = "2" Then
                                    cmbTipologia.SelectedIndex = -1
                                    cmbTipologia.Items.FindByValue("6").Selected = True
                                    cmbTipologia.Enabled = False
                                End If

                                If par.IfNull(myReader("ID_DESTINAZIONE_USO"), "1") = "1" Then
                                    cmbTipologia.SelectedIndex = -1
                                    cmbTipologia.Items.FindByValue("1").Selected = True
                                    cmbTipologia.Enabled = False
                                End If
                            Else
                                cmbTipologia.Items.Add(New ListItem("ERP Sociale", "1"))
                                cmbTipologia.SelectedIndex = -1
                                cmbTipologia.Items.FindByValue("1").Selected = True
                                'cmbTipologia.Enabled = False
                            End If

                        End If

                        If Session.Item("ABB_392") = "1" Then
                            If par.IfNull(myReader("ID_DESTINAZIONE_USO"), "") <> "6" Then
                                cmbTipologia.Items.Add(New ListItem("392/78", "2"))
                                If par.IfNull(myReader("ID_DESTINAZIONE_USO"), "") = "4" Then
                                    cmbTipologia.SelectedIndex = -1
                                    cmbTipologia.Items.FindByValue("2").Selected = True
                                    cmbTipologia.Enabled = False
                                End If
                            End If
                        End If

                        If Session.Item("ABB_431") = "1" Then
                            If par.IfNull(myReader("ID_DESTINAZIONE_USO"), "") <> "6" Then
                                cmbTipologia.Items.Add(New ListItem("431/98", "3"))
                                If par.IfNull(myReader("ID_DESTINAZIONE_USO"), "") = "3" Then
                                    cmbTipologia.SelectedIndex = -1
                                    cmbTipologia.Items.FindByValue("3").Selected = True
                                    cmbTipologia.Enabled = False
                                End If
                            End If
                        End If

                        If Session.Item("ABB_OA") = "1" Then

                            cmbTipologia.Items.Add(New ListItem("OCCUP.ABUSIVA", "5"))


                        End If


                    End If
                    txtscala.Text = par.IfNull(myReader("scale"), "")
                    txtInterno.Text = par.IfNull(myReader("interno"), "")
                    txtfoglio.Text = par.IfNull(myReader("foglio"), "")
                    txtmappale.Text = par.IfNull(myReader("numero"), "")
                    txtsub.Text = par.IfNull(myReader("sub"), "")




                    par.cmd.CommandText = "select dimensioni.*  from SISCOM_MI.dimensioni where dimensioni.id_unita_immobiliare=" & txtid.Text
                    Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader11.Read
                        If myReader11("cod_tipologia") = "SUP_CONV" Then
                            txtSuperficie.Text = Format(par.IfNull(myReader11("VALORE"), "0"), "0.00")
                        End If

                        If myReader11("cod_tipologia") = "SUP_NETTA" Then
                            txtSupNetta.Text = Format(par.IfNull(myReader11("VALORE"), "0"), "0.00")
                        End If

                        If myReader11("cod_tipologia") = "SUP_LORDA" Then
                            txtSupLorda.Text = Format(par.IfNull(myReader11("VALORE"), "0"), "0.00")
                        End If

                        If myReader11("cod_tipologia") = "SUP_COMM" Then
                            txtSupCle.Text = Format(par.IfNull(myReader11("VALORE"), "0"), "0.00")
                        End If
                    Loop
                    myReader11.Close()




                    '100008010300A018
                    TXTCOMUNE.Text = UCase(par.IfNull(myReader("comune_di"), ""))
                    Dim TipoIndirizzo As String = "VIA"

                    TipoIndirizzo = RicavaVia1(par.IfNull(myReader("via"), "VIA"))
                    cmbTipoVia.Items.FindByText(TipoIndirizzo).Selected = True

                    txtIndirizzo.Text = RicavaInd(par.IfNull(myReader("VIA"), ""), TipoIndirizzo)
                    txtCivico.Text = par.IfNull(myReader("civico"), "")

                    chBar.Checked = True
                    If par.IfNull(myReader("NUM_ASCENSORI"), "0") <> "0" Then
                        chAscensore.Checked = True
                    End If

                    cmbPiano.Items.FindByValue(par.IfNull(myReader("cod_tipo_livello_piano"), "78")).Selected = True
                    IdAll = 0

                    If cmbTipologia.SelectedItem.Text <> "USI DIVERSI" Then
                        par.cmd.CommandText = "select ALLOGGI.*  from ALLOGGI where COD_ALLOGGIO='" & txtCodice.Text & "'"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            IdAll = myReader1("id")
                            'cmbZona.SelectedIndex = -1
                            'cmbZona.Items.FindByText(par.IfNull(myReader1("ZONA"), "--")).Selected = True

                            'If par.IfNull(myReader1("ZONA"), "") = "" Then
                            '    par.cmd.CommandText = "SELECT * FROM siscom_mi.UNITA_STATO_MANUTENTIVO where id_unita=" & txtid.Text & " order by nvl(data_s,'19000101') desc"
                            '    Dim myReader33 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            '    If myReader33.Read Then
                            '        cmbZona.SelectedIndex = -1
                            '        cmbZona.Items.FindByText(par.IfNull(myReader33("ZONA"), "--")).Selected = True
                            '    End If
                            '    myReader33.Close()
                            'End If

                            cmbTipo.SelectedIndex = -1
                            cmbTipo.Items.FindByValue(par.IfNull(myReader1("TIPO_ALLOGGIO"), "-1")).Selected = True

                            txtLocali.Text = par.IfNull(myReader1("NUM_LOCALI"), "0")
                            txtServizi.Text = par.IfNull(myReader1("NUM_SERVIZI"), "0")
                            txtDisponibile.Text = par.FormattaData(par.IfNull(myReader1("DATA_DISPONIBILITA"), ""))

                            If par.IfNull(myReader1("STATO"), "0") = "10" Then
                                chRiservata.Checked = True
                            Else
                                chRiservata.Checked = False
                            End If
                            txtNoteRiservata.Text = par.IfNull(myReader1("CONDIZIONE"), "")

                        End If
                        myReader1.Close()

                    Else
                        par.cmd.CommandText = "select *  from SISCOM_MI.UI_USI_DIVERSI where COD_ALLOGGIO='" & txtCodice.Text & "'"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            IdAll = myReader1("id")

                            'cmbZona.SelectedIndex = -1
                            'cmbZona.Items.FindByText(par.IfNull(myReader1("ZONA"), "-1")).Selected = True

                            cmbTipo.SelectedIndex = -1
                            cmbTipo.Items.FindByValue(par.IfNull(myReader1("TIPO_ALLOGGIO"), "-1")).Selected = True

                            txtLocali.Text = par.IfNull(myReader1("NUM_LOCALI"), "0")
                            txtServizi.Text = par.IfNull(myReader1("NUM_SERVIZI"), "0")
                            txtDisponibile.Text = par.FormattaData(par.IfNull(myReader1("DATA_DISPONIBILITA"), ""))

                            If par.IfNull(myReader1("STATO"), "0") = "10" Then
                                chRiservata.Checked = True
                            Else
                                chRiservata.Checked = False
                            End If
                            txtNoteRiservata.Text = par.IfNull(myReader1("CONDIZIONE"), "")

                        End If
                        myReader1.Close()

                    End If

                    par.cmd.CommandText = "select *  from SISCOM_MI.unita_stato_manutentivo where id_unita=" & txtid.Text
                    Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader123.HasRows = False Then
                        imgManutentivo.Visible = True
                        lblManutentivo.Visible = True
                    Else
                        If myReader123.Read = True Then
                            If par.IfNull(myReader123("tipo_riassegnabile"), "1") = "2" Then
                                Response.Write("<script>alert('Attenzione, non è possibile procedere perchè questa unità non assegnabile secondo quanto descritto nell\'ultima verifica dello stato manutentivo! E\' comunque possibile riservare questa unità.');window.close();</script>")
                                imgSalva.Enabled = False
                                imgSalva.Visible = False
                            End If

                            If par.IfNull(myReader123("DATA_S"), "") <> "" Then
                                DataMinima = par.IfNull(myReader123("DATA_S"), "")
                                Label19.Visible = True
                                Image1.Visible = True
                                Label19.Text = "La data di disponibilità non può essere precedente al " & DateAdd(DateInterval.Day, 1, CDate(par.FormattaData(DataMinima)))
                                txtDisponibile.Text = DateAdd(DateInterval.Day, 1, CDate(par.FormattaData(DataMinima)))
                                Image1.ToolTip = "Alloggio occupato fino al " & par.FormattaData(DataMinima) & " dal Contratto Cod. " & Request.QueryString("C")
                                Image1.Attributes.Add("onclick", "javascript:alert('" & "Alloggio Riassegnabile dal " & par.FormattaData(DataMinima) & "');")
                            End If

                            If par.IfNull(myReader123("NUM_LOCALI"), "") <> "" Then
                                txtLocali.Text = par.IfNull(myReader123("NUM_LOCALI"), "")
                            End If
                            If par.IfNull(myReader123("NUM_SERVIZI"), "") <> "" Then
                                txtServizi.Text = par.IfNull(myReader123("NUM_SERVIZI"), "")
                            End If
                            'If par.IfNull(myReader123("ZONA"), "-1") <> "-1" Then
                            '    cmbZona.SelectedIndex = -1
                            '    cmbZona.Items.FindByText(par.IfNull(myReader123("ZONA"), "-1")).Selected = True
                            'End If
                            If par.IfNull(myReader123("TIPO_ALLOGGIO"), "01") <> -1 Then
                                cmbTipo.SelectedIndex = -1
                                cmbTipo.Items.FindByValue(par.IfNull(myReader123("TIPO_ALLOGGIO"), "01")).Selected = True
                            End If

                            If par.IfNull(myReader123("HANDICAP"), "0") = "0" Then
                                chBar.Checked = True
                                chHandicap.Checked = False
                            Else
                                chBar.Checked = False
                                chHandicap.Checked = True
                            End If

                            'If par.IfNull(myReader123("h_motorio"), "0") = "0" Then
                            '    chHandicap.Checked = False
                            'Else
                            '    chHandicap.Checked = True
                            'End If


                        End If
                    End If
                    myReader123.Close()
                    If DataMinima <> "" Then
                        Label19.Visible = True
                        Image1.Visible = True
                        Label19.Text = "La data di disponibilità non può essere precedente al " & DateAdd(DateInterval.Day, 1, CDate(par.FormattaData(DataMinima)))
                        txtDisponibile.Text = DateAdd(DateInterval.Day, 1, CDate(par.FormattaData(DataMinima)))
                        Image1.ToolTip = "Alloggio occupato fino al " & par.FormattaData(DataMinima) & " dal Contratto Cod. " & Request.QueryString("C")
                        Image1.Attributes.Add("onclick", "javascript:alert('" & "Alloggio occupato fino al " & par.FormattaData(DataMinima) & " dal Contratto Cod. " & Request.QueryString("C") & "');")
                    Else
                        Label19.Visible = False
                        Image1.Visible = False
                    End If




                    'txtCodice.Enabled = False
                    'txtscala.Enabled = False
                    'txtInterno.Enabled = False
                    'txtfoglio.Enabled = False
                    'txtmappale.Enabled = False
                    'txtsub.Enabled = False
                    'txtSuperficie.Enabled = False
                    'TXTCOMUNE.Enabled = False
                    'cmbTipoVia.Enabled = False
                    'txtIndirizzo.Enabled = False
                    'txtCivico.Enabled = False
                    'cmbPiano.Enabled = False

                    'chBar.Checked = True
                    'chAscensore.Enabled = False
                Else
                    Response.Write("<script>alert('Attenzione, non è possibile procedere perchè i dati dell\'unità non sono completi. Verificare che sia stata inserita la superficie, indirizzo e dati catastali!');window.close();</script>")
                    imgSalva.Enabled = False
                    imgSalva.Visible = False
                    myReader.Close()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<script>history.go(-2);</script>")
                    'Response.Write("<script>document.location.href='RicercaUI.aspx';</script>")

                End If
                myReader.Close()





                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                txtCodice.Enabled = False
                txtscala.Enabled = False
                txtInterno.Enabled = False
                txtfoglio.Enabled = False
                txtmappale.Enabled = False
                txtsub.Enabled = False
                txtSuperficie.Enabled = False
                TXTCOMUNE.Enabled = False
                cmbTipoVia.Enabled = False
                txtIndirizzo.Enabled = False
                txtCivico.Enabled = False
                cmbPiano.Enabled = False
                ''txtServizi.Enabled = False
                'chBar.Checked = True
                chAscensore.Enabled = False
                txtSupCle.Enabled = False
                txtSupLorda.Enabled = False
                txtSupNetta.Enabled = False
                cmbZona.Enabled = False
                cmbTipo.Enabled = False

                txtLocali.Enabled = False
                txtServizi.Enabled = False
                txtDisponibile.Enabled = False

                If cmbZona.SelectedItem.Text = "--" Then
                    cmbZona.Enabled = True
                    imgSalva.Enabled = True
                    imgSalva.Visible = True
                End If

            Catch ex As Exception
                par.OracleConn.Close()
                If ex.Message = "Impossibile eseguire il reindirizzamento dopo l'invio delle intestazioni HTTP." Then
                    Response.Write("<script>document.location.href='RicercaUI.aspx';</script>")
                End If
                Label4.Text = ex.Message
                Label4.Visible = True
            End Try

        End If
        txtDisponibile.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

    End Sub

    Private Function RicavaVia1(ByVal indirizzo As String) As String
        Dim pos As Integer
        Dim via As String


        pos = InStr(1, indirizzo, " ")
        If pos > 0 Then
            via = Mid(indirizzo, 1, pos - 1)
            Select Case via
                Case "C.SO"
                    RicavaVia1 = "CORSO"
                Case "PIAZZA", "PZ.", "P.ZZA"
                    RicavaVia1 = "PIAZZA"
                Case "PIAZZALE", "P.LE"
                    RicavaVia1 = "PIAZZALE"
                Case "P.T"
                    RicavaVia1 = "PORTA"
                Case "S.T.R.", "STRADA"
                    RicavaVia1 = "STRADA"
                Case "V.", "VIA"
                    RicavaVia1 = "VIA"
                Case "VIALE", "V.LE"
                    RicavaVia1 = "VIALE"
                Case Else
                    RicavaVia1 = "VIA"
            End Select

        Else
            RicavaVia1 = ""
        End If

    End Function

    Private Function RicavaInd(ByVal indirizzo As String, ByVal TipoVia As String) As String
        RicavaInd = indirizzo

        RicavaInd = Trim(Replace(indirizzo, TipoVia, ""))

    End Function

    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click
        Dim POR As String = "0"
        Dim EQ As String = "0"
        Dim OA As String = "0"
        Dim MO As String = "0"
        Dim StatoAll As String = "5"

        If cmbZona.SelectedItem.Text = "--" And UCase(TXTCOMUNE.Text) = "MILANO" And chRiservata.Checked = False Then
            Response.Write("<script>alert('Inserire la zona!');</script>")
            Exit Sub
        End If

        If cmbTipoLOGIA.SelectedItem.Text = "" Or cmbTipoLOGIA.SelectedItem.Text = " " Then
            Response.Write("<script>alert('Inserire la tipologia dell\'abbinamento!');</script>")
            Exit Sub
        End If

        If (cmbTipo.SelectedItem.Text = "" Or cmbTipo.SelectedItem.Text = " ") And hiddenTipo.Value <> "B" And hiddenTipo.Value <> "H" And hiddenTipo.Value <> "I" And hiddenTipo.Value <> "O" And hiddenTipo.Value <> "T" And hiddenTipo.Value <> "V" And hiddenTipo.Value <> "G" And hiddenTipo.Value <> "K" Then
            Response.Write("<script>alert('Inserire la tipologia!');</script>")
            Exit Sub
        End If

        If txtCodice.Text = "" Then
            Response.Write("<script>alert('Inserire il codice alloggio!');</script>")
            Exit Sub
        End If

        If txtDisponibile.Text = "" Then
            Response.Write("<script>alert('Inserire la data di disponibilità!');</script>")
            Exit Sub
        End If

        If par.AggiustaData(txtDisponibile.Text) < DataMinima Then
            Response.Write("<script>alert('La data di disponibilità non può essere precedente al " & par.FormattaData(DataMinima) & " !');</script>")
            Exit Sub
        End If

        If txtLocali.Text = "" And hiddenTipo.Value <> "B" And hiddenTipo.Value <> "H" And hiddenTipo.Value <> "I" And hiddenTipo.Value <> "O" And hiddenTipo.Value <> "T" And hiddenTipo.Value <> "V" And hiddenTipo.Value <> "G" And hiddenTipo.Value <> "K" Then
            Response.Write("<script>alert('Inserire il numero di locali!');</script>")
            Exit Sub
        End If

        If txtServizi.Text = "" And hiddenTipo.Value <> "B" And hiddenTipo.Value <> "H" And hiddenTipo.Value <> "I" And hiddenTipo.Value <> "O" And hiddenTipo.Value <> "T" And hiddenTipo.Value <> "V" And hiddenTipo.Value <> "G" And hiddenTipo.Value <> "K" Then
            Response.Write("<script>alert('Inserire il numero di Servizi!');</script>")
            Exit Sub
        End If

        If (cmbTipologia.SelectedItem.Text = "ERP Sociale" Or cmbTipologia.SelectedItem.Text = "ERP Moderato") And (txtSuperficie.Text = "" Or txtSuperficie.Text = "0,00") Then
            Response.Write("<script>alert('La sup. Convenzionale è obbligatoria in caso di alloggi ERP Sociale o Moderato. Inserire tale superficie tramite l\'apposito modulo!');</script>")
            Exit Sub
        End If

        If cmbTipologia.SelectedItem.Text <> "ERP Sociale" And cmbTipologia.SelectedItem.Text <> "ERP Moderato" And (txtSuperficie.Text = "" Or txtSuperficie.Text = "0,00") And (txtSupCle.Text = "" Or txtSupCle.Text = "0,00") And (txtSupLorda.Text = "" Or txtSupLorda.Text = "0,00") And (txtSupNetta.Text = "" Or txtSupNetta.Text = "0,00") Then
            Response.Write("<script>alert('Nessuna tipologia di superficie specificata. Inserire tramite l\'apposito modulo!');</script>")
            Exit Sub
        End If

        If chRiservata.Checked = True Then
            StatoAll = "10"

        Else
            StatoAll = "5"
            txtNoteRiservata.Text = ""
        End If

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            If IdAll = 0 Then
                If cmbTipologia.SelectedItem.Value = "1" Or cmbTipologia.SelectedItem.Value = "6" Or cmbTipologia.SelectedItem.Value = "2" Or cmbTipologia.SelectedItem.Value = "3" Or cmbTipologia.SelectedItem.Value = "5" Then

                    If cmbTipologia.SelectedItem.Value = "1" Then
                        POR = "0"
                        EQ = "0"
                        OA = "0"
                        MO = "0"
                    End If
                    If cmbTipologia.SelectedItem.Value = "6" Then
                        POR = "0"
                        EQ = "0"
                        OA = "0"
                        MO = "1"
                    End If
                    If cmbTipologia.SelectedItem.Value = "2" Then
                        POR = "0"
                        EQ = "1"
                        OA = "0"
                        MO = "0"
                    End If
                    If cmbTipologia.SelectedItem.Value = "3" Then
                        POR = "1"
                        EQ = "0"
                        OA = "0"
                        MO = "0"
                    End If

                    If cmbTipologia.SelectedItem.Value = "5" Then
                        POR = "0"
                        EQ = "0"
                        OA = "1"
                        MO = "0"
                    End If


                    par.cmd.CommandText = "Insert into ALLOGGI (ID, PROPRIETA, ZONA, INDIRIZZO, NUM_CIVICO, " _
                                        & "NUM_ALLOGGIO, NUM_LOCALI, NUM_SERVIZI, TIPO_ALLOGGIO, PIANO, " _
                                        & "NOTE, SUP, EQCANONE, EXRESO, STATO, " _
                                        & "PRENOTATO, ASSEGNATO, ID_PRATICA, ASCENSORE, GAS, " _
                                        & "RISCALDAMENTO, BARRIERE_ARC, DATA_PRENOTATO, DATA_RESO, DATA_PROT, " _
                                        & "SETTORE, NUM_PG, DATA_COMUNICAZIONE, DATA_DISPONIBILITA, DATA_RITRASMISSIONE, " _
                                        & "PROTOCOLLI, ID_PRATICA_PRENOTATO, CUCINA, CUCININO, COD_ALLOGGIO, " _
                                        & "MOTIVAZIONE_RESO, TIPO_INDIRIZZO, ZONA_ALER, SCALA, COMUNE, " _
                                        & "CONDIZIONE, GESTIONE, TIPOLOGIA_GESTORE, VECCHIO_CODICE, FOGLIO, " _
                                        & "PARTICELLA, SUB, H_MOTORIO, FL_POR,FL_OA,FL_MOD) " _
                                        & "Values " _
                                        & "(seq_alloggi.nextval, 0, '" & cmbZona.SelectedItem.Value _
                                        & "', '" & par.PulisciStrSql(txtIndirizzo.Text) _
                                        & "', '" & par.PulisciStrSql(txtCivico.Text) & "', " _
                                        & "'" & par.PulisciStrSql(txtInterno.Text) & "', '" _
                                        & par.PulisciStrSql(txtLocali.Text) & "', '" & par.PulisciStrSql(txtServizi.Text) & "', " _
                                        & cmbTipo.SelectedItem.Value & ", '" & cmbPiano.SelectedItem.Value & "', " _
                                        & "'INSERITO DA WEB', " & par.VirgoleInPunti(txtSuperficie.Text) & " , '" & EQ & "', '0', " & StatoAll & ", " _
                                        & "'0', '0', NULL, '" & Valore01(chAscensore.Checked) & "', '0', " _
                                        & "'0', '" & Valore01(chBar.Checked) & "', '','','',NULL,NULL,'" & Format(Now, "yyyyMMdd") _
                                        & "','" & par.AggiustaData(txtDisponibile.Text) & "', NULL, " _
                                        & "NULL, NULL, " _
                                        & "'0', '0', '" & par.PulisciStrSql(UCase(txtCodice.Text)) & "', " _
                                        & "NULL, " & cmbTipoVia.SelectedItem.Value & " , 0, '" & par.PulisciStrSql(txtscala.Text) & "', '" & par.PulisciStrSql(UCase(TXTCOMUNE.Text)) & "', " _
                                        & "'" & par.PulisciStrSql(txtNoteRiservata.Text) & "', 9, 'ERP', 'D', '" & par.PulisciStrSql(txtfoglio.Text) & "', " _
                                        & "'" & par.PulisciStrSql(txtmappale.Text) & "', '" _
                                        & par.PulisciStrSql(txtsub.Text) & "' , '" & Valore01(chHandicap.Checked) _
                                        & "', '" & POR & "','" & OA & "','" & MO & "')"

                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "Insert into SISCOM_MI.UI_USI_DIVERSI (ID, PROPRIETA, ZONA, INDIRIZZO, NUM_CIVICO, " _
                                        & "NUM_ALLOGGIO, NUM_LOCALI, NUM_SERVIZI, TIPO_ALLOGGIO, PIANO, " _
                                        & "NOTE, SUP, EQCANONE, EXRESO, STATO, " _
                                        & "PRENOTATO, ASSEGNATO, ID_PRATICA, ASCENSORE, GAS, " _
                                        & "RISCALDAMENTO, BARRIERE_ARC, DATA_PRENOTATO, DATA_RESO, DATA_PROT, " _
                                        & "SETTORE, NUM_PG, DATA_COMUNICAZIONE, DATA_DISPONIBILITA, DATA_RITRASMISSIONE, " _
                                        & "PROTOCOLLI, ID_PRATICA_PRENOTATO, CUCINA, CUCININO, COD_ALLOGGIO, " _
                                        & "MOTIVAZIONE_RESO, TIPO_INDIRIZZO, ZONA_ALER, SCALA, COMUNE, " _
                                        & "CONDIZIONE, GESTIONE, TIPOLOGIA_GESTORE, VECCHIO_CODICE, FOGLIO, " _
                                        & "PARTICELLA, SUB, H_MOTORIO, FL_POR) " _
                                        & "Values " _
                                        & "(seq_alloggi.nextval, 0, '" & cmbZona.SelectedItem.Value _
                                        & "', '" & par.PulisciStrSql(txtIndirizzo.Text) _
                                        & "', '" & par.PulisciStrSql(txtCivico.Text) & "', " _
                                        & "'" & par.PulisciStrSql(txtInterno.Text) & "', '" _
                                        & par.PulisciStrSql(txtLocali.Text) & "', '" & par.PulisciStrSql(txtServizi.Text) & "', " _
                                        & cmbTipo.SelectedItem.Value & ", '" & cmbPiano.SelectedItem.Value & "', " _
                                        & "'INSERITO DA WEB', " & par.VirgoleInPunti(txtSuperficie.Text) & " , '0', '0', " & StatoAll & ", " _
                                        & "'0', '0', NULL, '" & Valore01(chAscensore.Checked) & "', '0', " _
                                        & "'0', '" & Valore01(chBar.Checked) & "', '','','',NULL,NULL,'" & Format(Now, "yyyyMMdd") _
                                        & "','" & par.AggiustaData(txtDisponibile.Text) & "', NULL, " _
                                        & "NULL, NULL, " _
                                        & "'0', '0', '" & par.PulisciStrSql(UCase(txtCodice.Text)) & "', " _
                                        & "NULL, " & cmbTipoVia.SelectedItem.Value & " , 0, '" & par.PulisciStrSql(txtscala.Text) & "', '" & par.PulisciStrSql(UCase(TXTCOMUNE.Text)) & "', " _
                                        & "'" & par.PulisciStrSql(txtNoteRiservata.Text) & "', 9, 'USD', 'D', '" & par.PulisciStrSql(txtfoglio.Text) & "', " _
                                        & "'" & par.PulisciStrSql(txtmappale.Text) & "', '" & par.PulisciStrSql(txtsub.Text) & "' , '" & Valore01(chHandicap.Checked) & "', '0')"

                    par.cmd.ExecuteNonQuery()
                End If


            Else

                If cmbTipologia.SelectedItem.Value = "1" Or cmbTipologia.SelectedItem.Value = "6" Or cmbTipologia.SelectedItem.Value = "2" Or cmbTipologia.SelectedItem.Value = "3" Or cmbTipologia.SelectedItem.Value = "5" Then

                    If cmbTipologia.SelectedItem.Value = "1" Then
                        POR = "0"
                        EQ = "0"
                        OA = "0"
                        MO = "0"
                    End If
                    If cmbTipologia.SelectedItem.Value = "6" Then
                        POR = "0"
                        EQ = "0"
                        OA = "0"
                        MO = "1"
                    End If
                    If cmbTipologia.SelectedItem.Value = "2" Then
                        POR = "0"
                        EQ = "1"
                        OA = "0"
                        MO = "0"
                    End If
                    If cmbTipologia.SelectedItem.Value = "3" Then
                        POR = "1"
                        EQ = "0"
                        OA = "0"
                        MO = "0"
                    End If
                    If cmbTipologia.SelectedItem.Value = "5" Then
                        POR = ""
                        EQ = "0"
                        OA = "1"
                        MO = "0"
                    End If

                    par.cmd.CommandText = "update ALLOGGI set ZONA='" & cmbZona.SelectedItem.Value _
                                          & "',INDIRIZZO='" & par.PulisciStrSql(txtIndirizzo.Text) _
                                          & "', NUM_CIVICO='" & par.PulisciStrSql(txtCivico.Text) _
                                          & "', NUM_ALLOGGIO='" & par.PulisciStrSql(txtInterno.Text) _
                                          & "', NUM_LOCALI='" & par.PulisciStrSql(txtLocali.Text) _
                                          & "', NUM_SERVIZI='" & par.PulisciStrSql(txtServizi.Text) _
                                          & "', TIPO_ALLOGGIO=" & cmbTipo.SelectedItem.Value _
                                          & ", PIANO='" & cmbPiano.SelectedItem.Value _
                                          & "', SUP=" & par.VirgoleInPunti(txtSuperficie.Text) _
                                          & ", EQCANONE='" & EQ _
                                          & "', STATO=" & StatoAll & ",CONDIZIONE='" & par.PulisciStrSql(txtNoteRiservata.Text) & "'," _
                                          & "PRENOTATO='0', ASSEGNATO='0', ID_PRATICA=null, " _
                                          & "ASCENSORE='" & Valore01(chAscensore.Checked) _
                                          & "', BARRIERE_ARC='" & Valore01(chBar.Checked) _
                                          & "', DATA_PRENOTATO='', DATA_RESO='', DATA_PROT='', " _
                                        & "DATA_DISPONIBILITA='" & par.AggiustaData(txtDisponibile.Text) _
                                        & "', ID_PRATICA_PRENOTATO=null, " _
                                        & "TIPO_INDIRIZZO=" & cmbTipoVia.SelectedItem.Value _
                                        & ", SCALA='" & par.PulisciStrSql(txtscala.Text) _
                                        & "', COMUNE='" & par.PulisciStrSql(UCase(TXTCOMUNE.Text)) _
                                        & "', FOGLIO='" & par.PulisciStrSql(txtfoglio.Text) _
                                        & "',PARTICELLA='" & par.PulisciStrSql(txtmappale.Text) _
                                        & "', SUB='" & par.PulisciStrSql(txtsub.Text) & "', H_MOTORIO='" & Valore01(chHandicap.Checked) _
                                        & "', FL_POR='" & POR & "',FL_OA='" & OA & "',FL_MOD='" & MO & "' where id=" & IdAll

                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "update SISCOM_MI.UI_USI_DIVERSI set ZONA='" & cmbZona.SelectedItem.Value _
                      & "',INDIRIZZO='" & par.PulisciStrSql(txtIndirizzo.Text) _
                      & "', NUM_CIVICO='" & par.PulisciStrSql(txtCivico.Text) _
                      & "', NUM_ALLOGGIO='" & par.PulisciStrSql(txtInterno.Text) _
                      & "', NUM_LOCALI='" & par.PulisciStrSql(txtLocali.Text) _
                       & "', NUM_SERVIZI='" & par.PulisciStrSql(txtServizi.Text) _
                      & "', TIPO_ALLOGGIO=" & cmbTipo.SelectedItem.Value _
                      & ", PIANO='" & cmbPiano.SelectedItem.Value _
                      & "', SUP=" & par.VirgoleInPunti(txtSuperficie.Text) _
                      & ", EQCANONE='" & EQ _
                      & "', STATO=" & StatoAll & ",CONDIZIONE='" & par.PulisciStrSql(txtNoteRiservata.Text) & "'," _
                      & "PRENOTATO='0', ASSEGNATO='0', ID_PRATICA=null, " _
                      & "ASCENSORE='" & Valore01(chAscensore.Checked) _
                      & "', BARRIERE_ARC='" & Valore01(chBar.Checked) _
                      & "', DATA_PRENOTATO='', DATA_RESO='', DATA_PROT='', " _
                    & "DATA_DISPONIBILITA='" & par.AggiustaData(txtDisponibile.Text) _
                    & "', ID_PRATICA_PRENOTATO=null, " _
                    & "TIPO_INDIRIZZO=" & cmbTipoVia.SelectedItem.Value _
                    & ", SCALA='" & par.PulisciStrSql(txtscala.Text) _
                    & "', COMUNE='" & par.PulisciStrSql(UCase(TXTCOMUNE.Text)) _
                    & "', FOGLIO='" & par.PulisciStrSql(txtfoglio.Text) _
                    & "',PARTICELLA='" & par.PulisciStrSql(txtmappale.Text) _
                    & "', SUB='" & par.PulisciStrSql(txtsub.Text) & "', H_MOTORIO='" & Valore01(chHandicap.Checked) _
                    & "', FL_POR='" & POR & "',condizione='" & par.PulisciStrSql(txtNoteRiservata.Text) & "' where id=" & IdAll
                    'par.cmd.CommandText = "Insert into SISCOM_MI.UI_USI_DIVERSI (ID, PROPRIETA, ZONA, INDIRIZZO, NUM_CIVICO, " _
                    '                    & "NUM_ALLOGGIO, NUM_LOCALI, NUM_SERVIZI, TIPO_ALLOGGIO, PIANO, " _
                    '                    & "NOTE, SUP, EQCANONE, EXRESO, STATO, " _
                    '                    & "PRENOTATO, ASSEGNATO, ID_PRATICA, ASCENSORE, GAS, " _
                    '                    & "RISCALDAMENTO, BARRIERE_ARC, DATA_PRENOTATO, DATA_RESO, DATA_PROT, " _
                    '                    & "SETTORE, NUM_PG, DATA_COMUNICAZIONE, DATA_DISPONIBILITA, DATA_RITRASMISSIONE, " _
                    '                    & "PROTOCOLLI, ID_PRATICA_PRENOTATO, CUCINA, CUCININO, COD_ALLOGGIO, " _
                    '                    & "MOTIVAZIONE_RESO, TIPO_INDIRIZZO, ZONA_ALER, SCALA, COMUNE, " _
                    '                    & "CONDIZIONE, GESTIONE, TIPOLOGIA_GESTORE, VECCHIO_CODICE, FOGLIO, " _
                    '                    & "PARTICELLA, SUB, H_MOTORIO, FL_POR) " _
                    '                    & "Values " _
                    '                    & "(seq_alloggi.nextval, 0, '" & cmbZona.SelectedItem.Value _
                    '                    & "', '" & par.PulisciStrSql(txtIndirizzo.Text) _
                    '                    & "', '" & par.PulisciStrSql(txtCivico.Text) & "', " _
                    '                    & "'" & par.PulisciStrSql(txtInterno.Text) & "', '" _
                    '                    & par.PulisciStrSql(txtLocali.Text) & "', NULL, " _
                    '                    & cmbTipo.SelectedItem.Value & ", '" & cmbPiano.SelectedItem.Value & "', " _
                    '                    & "'INSERITO DA WEB', " & par.VirgoleInPunti(txtSuperficie.Text) & " , '0', '0', 5, " _
                    '                    & "'0', '0', NULL, '" & Valore01(chAscensore.Checked) & "', '0', " _
                    '                    & "'0', '" & Valore01(chBar.Checked) & "', '','','',NULL,NULL,'" & Format(Now, "yyyyMMdd") _
                    '                    & "','" & par.AggiustaData(txtDisponibile.Text) & "', NULL, " _
                    '                    & "NULL, NULL, " _
                    '                    & "'0', '0', '" & par.PulisciStrSql(UCase(txtCodice.Text)) & "', " _
                    '                    & "NULL, " & cmbTipoVia.SelectedItem.Value & " , 0, '" & par.PulisciStrSql(txtscala.Text) & "', '" & par.PulisciStrSql(UCase(TXTCOMUNE.Text)) & "', " _
                    '                    & "'', 9, 'USD', '', '" & par.PulisciStrSql(txtfoglio.Text) & "', " _
                    '                    & "'" & par.PulisciStrSql(txtmappale.Text) & "', '" & par.PulisciStrSql(txtsub.Text) & "' , '" & Valore01(chHandicap.Checked) & "', '0')"

                    par.cmd.ExecuteNonQuery()
                End If



            End If

            par.cmd.CommandText = "UPDATE SISCOM_MI.UNITA_IMMOBILIARI SET COD_TIPO_DISPONIBILITA='LIBE' WHERE ID=" & txtid.Text
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE SISCOM_MI.UNITA_IMMOBILIARI SET COD_TIPO_DISPONIBILITA='LIBE' WHERE ID_UNITA_PRINCIPALE=" & txtid.Text
            par.cmd.ExecuteNonQuery()

            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_ASSEGNATE WHERE GENERATO_CONTRATTO=0 AND ID_UNITA=" & txtid.Text
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader1.Read Then
            par.cmd.CommandText = "UPDATE SISCOM_MI.UNITA_ASSEGNATE SET DATA_DISPONIBILITA='" & par.AggiustaData(txtDisponibile.Text) & "' WHERE GENERATO_CONTRATTO=0 AND ID_UNITA=" & txtid.Text
            par.cmd.ExecuteNonQuery()
            'End If
            'myReader1.Close()




            Response.Write("<script>alert('Operazione Effettuata!');</script>")
            Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")



            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Label4.Visible = True
            Label4.Text = ex.Message

        End Try
    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Public Property IdAll() As Long
        Get
            If Not (ViewState("par_IdAll") Is Nothing) Then
                Return CLng(ViewState("par_IdAll"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IdAll") = value
        End Set

    End Property

    Public Property DataMinima() As String
        Get
            If Not (ViewState("par_DataMinima") Is Nothing) Then
                Return CStr(ViewState("par_DataMinima"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_DataMinima") = value
        End Set

    End Property

End Class
