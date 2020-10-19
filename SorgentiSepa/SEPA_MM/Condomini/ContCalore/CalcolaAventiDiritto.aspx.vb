
Partial Class Contratti_ContCalore_CalcolaAventiDiritto
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim tipo As Integer
    Dim AnomalieCong As Integer = 0
    Public percentuale As Long = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Session.Item("OPERATORE") = "" Then
                Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            End If
            Dim Str As String = ""

            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='Caricamento in corso' ><br>Caricamento in corso...</br><div align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
            Str = Str & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"

            Response.Write(Str)
            Response.Flush()

            If Not IsPostBack Then
                txtAnnoCalcolo.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")

                If Request.QueryString("TIPO") = "NUOVO" Then
                    TipoCalcolo.Value = 0
                    Me.lblTitolo.Text = "Calcolo Preventivo Contributo Calore"
                    Me.btnAvviaCalcolo.Text = "GENERA ASSEGNATARI AVENTI DIRITTO"
                ElseIf Request.QueryString("TIPO") = "CONGUAGLIO" Then
                    TipoCalcolo.Value = 1
                    Me.txtAnnoCalcolo.Visible = False
                    Me.cmbAnniConsuntivabili.Visible = True
                    Me.lblTitolo.Text = "Calcolo Consuntivo Contributo Calore"
                    Me.btnAvviaCalcolo.Text = "CONSUNTIVO ASSEGNATARI AVENTI DIRITTO"
                End If
                'DefCalcolo(Format(Now, "yyyyMMdd"))
                DefCalcolo()

            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")

        End Try
    End Sub
    Private Sub DefCalcolo()

        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            'Dim idParametriContCalore As Integer = 0
            'par.cmd.CommandText = "select max(id) from siscom_mi.cont_calore_parametri " ' not in (select distinct(id_cont_calore) from siscom_mi.cont_calore_elaborazione)"
            'Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If lettore.Read Then
            '    idParametriContCalore = par.IfNull(lettore(0), 0)
            'End If
            'lettore.Close()
            'If idParametriContCalore > 0 Then




            '    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            '    Dim dt As New Data.DataTable
            '    da.Fill(dt)
            '    da.Dispose()

            '    If dt.Rows.Count = 1 Then

            '        Me.txtInizioVal.Text = par.FormattaData(dt.Rows(0).Item("DATA_INIZIO_VAL"))
            '        Me.txtFineVal.Text = par.FormattaData(dt.Rows(0).Item("DATA_FINE_VAL"))

            '        Me.txtNDecimali.Text = dt.Rows(0).Item("ARROTONDAMENTO")
            '        Me.txtNote.Text = dt.Rows(0).Item("NOTE")
            '        Me.txtMensilita.Text = dt.Rows(0).Item("MENSILITA")
            '        Me.txtValore.Text = dt.Rows(0).Item("VALORE")
            '        Me.txtArea.Text = dt.Rows(0).Item("AREA")
            '        Me.txtDimensione.Text = dt.Rows(0).Item("TIPODIM")
            '        Me.txtNDecimali.Text = dt.Rows(0).Item("ARROTONDAMENTO")


            '        ValoreMq.Value = dt.Rows(0).Item("VALORE")
            '        AreaLimite.Value = dt.Rows(0).Item("AREA_LIMITE")
            '        cfArrotond.Value = dt.Rows(0).Item("ARROTONDAMENTO")
            '        Me.TipoDimensione.Value = dt.Rows(0).Item("COD_TIPO_DIMENSIONE")
            '    End If
            If Request.QueryString("TIPO") = "CONGUAGLIO" Then
                par.caricaComboBox("select id,anno  from siscom_mi.cont_calore_anno where id_stato >= 2", Me.cmbAnniConsuntivabili, "id", "ANNO", True, -1)
                If Me.cmbAnniConsuntivabili.Items.Count = 1 Then
                    Me.btnAvviaCalcolo.Enabled = False
                    Response.Write("<script>alert('Approvare prima un Preventivo per procedere con il Consuntivo!');</script>")
                End If
            End If

            'Else
            'Response.Write("<script>alert('Impossibile trovare un contributo calore definito e per il quale non sono stati eseguiti dei calcoli!')</script>")
            'Me.btnAvviaCalcolo.Enabled = False
            'End If

            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")

        End Try

    End Sub
    Private Sub ControllaEsistePreventivo()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If



            Dim esiste As Boolean = False
            If tipo = 1 Then
                par.cmd.CommandText = "select id,id_stato from siscom_mi.cont_calore_anno where anno =" & Me.txtAnnoCalcolo.Text
            ElseIf tipo = 2 Then
                par.cmd.CommandText = "select id,id_stato from siscom_mi.cont_calore_anno where anno =" & Me.cmbAnniConsuntivabili.SelectedItem.ToString

            End If

            Dim letContributo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If letContributo.Read Then
                esiste = True

                Select Case letContributo("ID_STATO")
                    Case 1
                        If tipo = 1 Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "ConfRewritePreventivo();", True)


                        ElseIf tipo = 2 Then
                            Response.Write("<script>alert('Approvare il preventivo prima di procedere!')</script>")
                        End If
                    Case 2
                        If tipo = 1 Then
                            Response.Write("<script>alert('Preventivo già approvato per questo anno!Impossibile procedere!')</script>")

                        ElseIf tipo = 2 Then
                            CreaConguaglio(Me.ValoreMq.Value, Me.AreaLimite.Value, Me.cfArrotond.Value, Me.TipoDimensione.Value, tipo)
                        End If
                    Case 3
                        If tipo = 1 Then
                            Response.Write("<script>alert('Per l\'anno definito è stato già elaborato un consuntivo!Impossibile procedere')</script>")

                        ElseIf tipo = 2 Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "ConfRewriteConsuntivo();", True)

                        End If
                    Case 4
                        If tipo = 1 Then
                            Response.Write("<script>alert('Per l\'anno definito è stato già approvato un consuntivo!Impossibile procedere!')</script>")

                        ElseIf tipo = 2 Then
                            Response.Write("<script>alert('Per l\'anno definito è stato già approvato un consuntivo!Impossibile procedere!')</script>")

                        End If

                End Select

            End If
            letContributo.Close()

            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)
            End If

            If esiste = False Then
                CreaAventiDiritto(Me.ValoreMq.Value, Me.AreaLimite.Value, Me.cfArrotond.Value, Me.TipoDimensione.Value, tipo)
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>ControllaEsistePreventivo " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)

            End If

        End Try


    End Sub
    Private Function SettaParametriAnno() As Boolean
        SettaParametriAnno = True
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim anno As Integer = 0
            If Request.QueryString("TIPO") = "NUOVO" Then
                anno = Me.txtAnnoCalcolo.Text
            ElseIf Request.QueryString("TIPO") = "CONGUAGLIO" Then
                anno = Me.cmbAnniConsuntivabili.SelectedItem.Text
            End If
            par.cmd.CommandText = "SELECT cont_calore_parametri.ID,cont_calore_parametri.anno,arrotondamento,note,mensilita,valore," _
                    & "cod_tipo_dimensione,tipologia_dimensioni.descrizione AS tipodim,area_economica.descrizione AS area ,area_limite " _
                    & "FROM siscom_mi.cont_calore_parametri,siscom_mi.tipologia_dimensioni,siscom_mi.area_economica " _
                    & "WHERE siscom_mi.tipologia_dimensioni.cod = cod_tipo_dimensione " _
                    & "AND area_economica.ID = area_limite and anno = " & anno

            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                ValoreMq.Value = par.IfNull(lettore("VALORE"), 0)
                AreaLimite.Value = par.IfNull(lettore("AREA_LIMITE"), 0)
                cfArrotond.Value = par.IfNull(lettore("ARROTONDAMENTO"), 0)
                Me.TipoDimensione.Value = par.IfNull(lettore("COD_TIPO_DIMENSIONE"), 0)
                idContributo.Value = 0
            Else
                SettaParametriAnno = False
                Response.Write("<script>alert('Nessun parametro definito per questo anno');</script>")

            End If
            lettore.Close()
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)
            End If


        Catch ex As Exception
            SettaParametriAnno = False
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>SettaParametriAnno " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)

            End If

        End Try
    End Function
    Protected Sub btnAvviaCalcolo_Click(sender As Object, e As System.EventArgs) Handles btnAvviaCalcolo.Click
        Try
            If Me.ConfCrea.Value = 1 Then
                If Request.QueryString("TIPO") = "NUOVO" Then
                    tipo = 1

                ElseIf Request.QueryString("TIPO") = "CONGUAGLIO" Then
                    tipo = 2

                End If


                If Request.QueryString("TIPO") = "NUOVO" Then
                    If Not String.IsNullOrEmpty(Me.txtAnnoCalcolo.Text) Then
                        If SettaParametriAnno() = True Then
                            ControllaEsistePreventivo()
                        End If
                    End If

                ElseIf Request.QueryString("TIPO") = "CONGUAGLIO" Then
                    If Me.cmbAnniConsuntivabili.SelectedValue <> -1 Then
                        If SettaParametriAnno() = True Then
                            ControllaEsistePreventivo()
                        End If
                    End If
                End If



                'Response.Write("<script>document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>btnAvviaCalcolo_Click " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")

        End Try

    End Sub
    Private Sub CreaAventiDiritto(ByVal ValMq As Decimal, ByVal ArLimite As Integer, ByVal Precision As Integer, ByVal codTipoDimens As String, ByVal tipoCalc As Integer)
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            '**********apertura transazione
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            Dim esistono As Boolean = False


            '*************INSERT DEL NUOVO CONTRIBUTO CALORE PER L'ANNO INDICATO con stato 0 = DEFINIZIONE***********************

            par.cmd.CommandText = "select id from siscom_mi.cont_calore_anno where id_stato in (0,1) and anno = " & Me.txtAnnoCalcolo.Text
            Dim letContributo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If letContributo.Read Then
                idContributo.Value = par.IfNull(letContributo(0), 0)
            Else

                'insert del nuovo contributo calore
                par.cmd.CommandText = "select siscom_mi.seq_cont_calore.nextval from dual"
                Dim lettoreNuovo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreNuovo.Read Then
                    idContributo.Value = lettoreNuovo(0)
                End If
                lettoreNuovo.Close()
                par.cmd.CommandText = "insert into siscom_mi.cont_calore_anno(id,anno,id_stato) values (" & idContributo.Value & ", " & Me.txtAnnoCalcolo.Text & ",0)"
                par.cmd.ExecuteNonQuery()


            End If
            letContributo.Close()


            Dim condTipoGestione As String = " AND CONDOMINI.TIPO_GESTIONE = 'D'"

            par.cmd.CommandText = "SELECT distinct rapporti_utenza.ID as id_contratto,canoni_ec.id_area_economica FROM siscom_mi.canoni_ec ,siscom_mi.unita_contrattuale ,siscom_mi.rapporti_utenza " _
                                & "WHERE canoni_ec.id_contratto = unita_contrattuale.ID_CONTRATTO  " _
                                & "AND  siscom_mi.getstatocontratto2(rapporti_utenza.ID,0)<> 'CHIUSO' " _
                                & "AND rapporti_utenza.ID = unita_contrattuale.id_contratto " _
                                & "AND cod_tipologia_contr_loc = 'ERP'AND id_unita IN " _
                                & "(SELECT ID_UI FROM SISCOM_MI.COND_UI, SISCOM_MI.CONDOMINI WHERE CONDOMINI.ID = COND_UI.ID_CONDOMINIO " & condTipoGestione & ") " _
                                & "AND canoni_ec.data_calcolo IN (SELECT MAX(data_calcolo) " _
                                & "FROM siscom_mi.CANONI_EC a WHERE a.id_contratto = CANONI_EC.id_contratto) order by id_contratto asc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtContratti As New Data.DataTable
            da.Fill(dtContratti)
            da.Dispose()
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim importoRic As Decimal = 0
            Dim dimensione As Decimal
            Dim idAnagrafica As Integer
            Dim idUnita As Integer
            Dim Contatore As Long = 0
            Dim NUMERORIGHE As Long = 0
            Dim NumAnomalie As Integer = 0
            If dtContratti.Rows.Count > 0 Then
                Dim nessunInte As Boolean = False
                NUMERORIGHE = dtContratti.Rows.Count
                For Each rigaContratti As Data.DataRow In dtContratti.Rows

                    Contatore = Contatore + 1
                    percentuale = (Contatore * 100) / NUMERORIGHE

                    '**************** se l'anagrafe trovata risulta in area economica di protezione, allora mi preparo a scrivere il preventivo per quel contratto'********************
                    If rigaContratti.Item("id_area_economica") = 1 Then

                        par.cmd.CommandText = "SELECT id_unita ,soggetti_contrattuali.id_anagrafica,NVL(dimensioni.valore,0)AS DIMENSIONE " _
                                            & "FROM siscom_mi.unita_contrattuale,siscom_mi.soggetti_contrattuali,siscom_mi.dimensioni " _
                                            & "WHERE unita_contrattuale.id_contratto = " & rigaContratti.Item("ID_CONTRATTO") & " " _
                                            & "AND soggetti_contrattuali.id_contratto = unita_contrattuale.id_contratto " _
                                            & "AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' " _
                                            & "AND dimensioni.id_unita_immobiliare(+) = id_unita " _
                                            & "AND cod_tipologia = '" & codTipoDimens & "'"

                        lettore = par.cmd.ExecuteReader
                        dimensione = 0
                        importoRic = 0
                        idAnagrafica = 0
                        idUnita = 0
                        nessunInte = False
                        If lettore.Read Then
                            dimensione = par.IfNull(lettore("DIMENSIONE"), 0)
                            importoRic = Math.Round(par.IfNull(lettore("DIMENSIONE"), 0) * ValMq, Precision)
                            idAnagrafica = par.IfNull(lettore("id_anagrafica"), 0)
                            idUnita = par.IfNull(lettore("id_unita"), 0)
                        Else
                            nessunInte = True
                        End If
                        lettore.Close()



                        If dimensione > 0 And importoRic > 0 And idAnagrafica > 0 And idUnita > 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONT_CALORE_ELABORAZIONE ( ID_CONT_CALORE, ID_CONTRATTO, " _
                                                    & "TIPO_CALCOLO, ID_ANAGRAFICA, DIMENSIONE, " _
                                                    & "IMPORTO_RICONOSCIUTO, STATO,ID_UNITA ) " _
                                                    & "VALUES (" & idContributo.Value & "," & rigaContratti.Item("ID_CONTRATTO") & "," & tipoCalc & ", " _
                                                    & "" & idAnagrafica & ", " _
                                                    & "" & par.VirgoleInPunti(dimensione) & "," & par.VirgoleInPunti(importoRic) & ",-1," & idUnita & ") "
                            par.cmd.ExecuteNonQuery()


                        Else
                            Dim motivoAnomalia As String = ""
                            If nessunInte = True Then
                                motivoAnomalia = "IMPOSSIBILE TROVARE INTESTATARIO PER IL CONTRATTO ID " & rigaContratti.Item("ID_CONTRATTO")
                            ElseIf dimensione <= 0 Then
                                motivoAnomalia = "DIMENSIONE DELL'UNITA IMMOBILIARE NON TROVATA OPPURE MINORE/UGUALE A ZERO"
                            ElseIf importoRic <= 0 Then
                                motivoAnomalia = "IMPORTO RICONOSCIUTO MINORE/UGUALE A ZERO"
                            ElseIf idAnagrafica <= 0 Then
                                motivoAnomalia = "IMPOSSIBILE DEFINIRE L'ANAGRAFICA DELL'INTESTATARIO"
                            ElseIf idUnita <= 0 Then
                                idUnita = "null"
                                motivoAnomalia = "UNITA IMMOBILIARE NON TROVATA O NON DEFINIBILE"
                            End If

                            par.cmd.CommandText = "insert into siscom_mi.cont_calore_anomalie (id_cont_calore,id_contratto,id_unita,motivazione,tipo_calcolo) values " _
                                                & "(" & idContributo.Value & "," & RitornaNullseZero(rigaContratti.Item("ID_CONTRATTO")) & ", " & RitornaNullseZero(idUnita) & ",'" & par.PulisciStrSql(motivoAnomalia) & "',1)"
                            par.cmd.ExecuteNonQuery()
                            NumAnomalie += 1
                            'par.myTrans.Rollback()
                            'If par.OracleConn.State = Data.ConnectionState.Open Then
                            '    ''*********************CHIUSURA CONNESSIONE**********************
                            '    par.OracleConn.Close()
                            '    Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)
                            'End If
                            'Response.Write("<script>alert('ERRORE IN FASE DI GENERAZIONE DELL\'ELENCO DEGLI AVENTI DIRITTO!')</script>")
                            'Response.Write("<script>alert('OPERAZIONE ANNULLATA')</script>")
                            'Exit Sub
                        End If
                    End If
            percentuale = (Contatore * 100) / NUMERORIGHE
            Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
            Response.Flush()

            'Dim stringa As String = CStr(Math.Round(percentuale, 0)) & "px"
            ''ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "bar", "document.getElementById('barra').style.width = '" & stringa & "';", True)
            'Response.Write("<script>document.getElementById('barra').style.width = '" & stringa & "';</script>")


                Next


                par.cmd.CommandText = "update siscom_mi.CONT_CALORE_ANNO set id_stato = 1 where id = " & idContributo.Value
                par.cmd.ExecuteNonQuery()

                If NumAnomalie = 0 Then
                    Response.Write("<script>alert('Operazione completata, ed eseguita correttamete!');</script>")
                    Response.Write("<script>parent.main.location.replace('../pagina_home.aspx');</script>")
                Else
                    Response.Write("<script>alert('Operazione completata!\nSono state trovate " & NumAnomalie & " per le quali non è stato possibile calcolare il contributo calore\nVerrà caricato l\'elenco delle anomalie.');</script>")
                    Response.Write("<script>parent.main.location.replace('ElAnomalie.aspx?TIPO=NUOVO&IDCONTCALORE=" & idContributo.Value & "');</script>")

                End If
            Else
                Response.Write("<script>alert('Nessun assegnatario avente diritto trovato!')</script>")
                Response.Write("<script>alert('OPERAZIONE ANNULLATA')</script>")
            End If

            par.myTrans.Commit()
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)
            End If


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>CreaAventiDiritto " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            par.myTrans.Rollback()
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)

            End If

        End Try
    End Sub
    Private Function ControlloGiaInserito(ByVal idContratto As String, ByVal idUnita As String, ByVal idContCalore As String, ByVal tipo As Integer) As Boolean
        ControlloGiaInserito = False
        Try

            Dim condTipo As String = ""
            If tipo < 3 Then
                condTipo = " and tipo_calcolo in (1,2)"
            ElseIf tipo = 3 Then
                condTipo = " and tipo_calcolo = 3"
            End If

            par.cmd.CommandText = "select * from siscom_mi.cont_calore_elaborazione " _
                                    & "where id_cont_calore =" & idContCalore & " and id_unita =" & idUnita _
                                    & " and id_contratto = " & idContratto & " " & condTipo

            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.HasRows Then
                ControlloGiaInserito = True
            End If
            lettore.Close()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>CreaAventiDiritto " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")

        End Try
    End Function
    Private Sub CreaConguaglio(ByVal ValMq As Decimal, ByVal ArLimite As Integer, ByVal Precision As Integer, ByVal codTipoDimens As String, ByVal tipoCalc As Integer)
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            ' '' ''par.cmd.CommandText = "select id from siscom_mi.cont_calore_anno where id_stato in (0,1)"
            ' '' ''Dim letContributo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            ' '' ''If letContributo.Read Then
            ' '' ''    idContributo.Value = par.IfNull(letContributo(0), 0)
            ' '' ''End If
            ' '' ''letContributo.Close()

            Me.idContributo.Value = cmbAnniConsuntivabili.SelectedValue

            '**********apertura transazione
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            Dim esistono As Boolean = False
            Dim percentuale As Decimal = 0
            Dim Str As String = ""
            'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            'Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Caricamento in corso' ><br>Caricamento in corso...</br><div align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
            'Str = Str & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"

            'Str = "<div align='center' id='dvvvPre' style='position:absolute;z-index:300; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            'Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Elaborazione in corso...attendere...' ><br>Caricamento in corso...</br><div align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
            'Str = Str & "<br /><input id='perc' type='text' style ='font-family: Arial, Helvetica, sans-serif;font-size: 7pt;font-weight: 700; width: 30px;color: #0000FF;text-align: center;' readonly='readonly'></div><br/><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"
            'Response.Write(Str)
            'Response.Flush()


            Dim condTipoGestione As String = " AND CONDOMINI.TIPO_GESTIONE = 'D'"

            par.cmd.CommandText = "SELECT  distinct rapporti_utenza.ID as id_contratto FROM siscom_mi.unita_contrattuale ,siscom_mi.rapporti_utenza " _
                                & "WHERE " _
                                & " rapporti_utenza.ID = unita_contrattuale.id_contratto " _
                                & "AND cod_tipologia_contr_loc = 'ERP'AND id_unita IN " _
                                & "(SELECT ID_UI FROM SISCOM_MI.COND_UI, SISCOM_MI.CONDOMINI WHERE CONDOMINI.ID = COND_UI.ID_CONDOMINIO " & condTipoGestione & ") "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtContratti As New Data.DataTable
            da.Fill(dtContratti)
            da.Dispose()


            'par.cmd.CommandText = "select cont_calore_elaborazione.*,'0' as Controllato  from siscom_mi.cont_calore_elaborazione where id_cont_calore = " _
            '                    & idContCalore & " and stato = 1"

            'da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dtPrevApprovato As New Data.DataTable
            'da.Fill(dtPrevApprovato)



            Dim Contatore As Long = 0
            Dim NUMERORIGHE As Long = 0
            If dtContratti.Rows.Count > 0 Then
                NUMERORIGHE = dtContratti.Rows.Count
                For Each rigaContratti As Data.DataRow In dtContratti.Rows



                    par.cmd.CommandText = "Select * from siscom_mi.canoni_ec where id_contratto = " & rigaContratti.Item("ID_CONTRATTO") _
                                        & " and fine_validita_can>='" & cmbAnniConsuntivabili.SelectedItem.ToString & "0101' " _
                                        & "order by data_calcolo asc"
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                    Dim dtCanoni As New Data.DataTable
                    da.Fill(dtCanoni)
                    If dtCanoni.Rows.Count > 0 Then
                        If calcolaConguaglio(dtCanoni, ArLimite, rigaContratti.Item("ID_CONTRATTO"), ValMq, Precision) = False Then
                            Response.Write("<script>alert('Impossibile completare!Si sono verificati degli errori!')</script>")
                            par.myTrans.Rollback()
                            Exit Sub
                        End If
                    End If
                    Contatore += 1
                    percentuale = (Contatore * 100) / NUMERORIGHE
                    Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                    Response.Flush()

                Next
            End If

            par.cmd.CommandText = "update siscom_mi.CONT_CALORE_ANNO set id_stato = 3 where id = " & idContributo.Value
            par.cmd.ExecuteNonQuery()
            par.myTrans.Commit()




            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)
            End If


            If AnomalieCong = 0 Then
                Response.Write("<script>alert('Operazione completata, ed eseguita correttamete!');</script>")
                Response.Write("<script>parent.main.location.replace('../pagina_home.aspx');</script>")
            Else
                Response.Write("<script>alert('Operazione completata!\nSono state trovate " & AnomalieCong & " per le quali non è stato possibile calcolare il consuntivo del contributo calore\nVerrà caricato l\'elenco delle anomalie.');</script>")
                Response.Write("<script>parent.main.location.replace('ElAnomalie.aspx?TIPO=CONGUAGLIO&IDCONTCALORE=" & idContributo.Value & "?TIPO=CONGUAGLIO');</script>")
            End If


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>CreaAventiDiritto " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            par.myTrans.Rollback()
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)

            End If

        End Try

    End Sub
    Private Function calcolaConguaglio(ByVal dtCanoni As Data.DataTable, ByVal ArLimite As Integer, ByVal idContAnalizzato As Integer, ByVal valmq As Decimal, ByVal Precision As Integer) As Boolean
        calcolaConguaglio = True
        Try

            Dim elaborato As Boolean = False
            Dim dtgiorni As Data.DataTable
            dtgiorni = CreaDtGiorni(Me.cmbAnniConsuntivabili.SelectedItem.ToString)
            For Each riga As Data.DataRow In dtCanoni.Rows
                '*************se sono in protezione allora aggiorno il valore di controllo a true
                If riga.Item("ID_AREA_ECONOMICA") = 1 Then
                    Dim Qresult As Data.DataRow() = dtgiorni.Select("DATA>=" & riga.Item("INIZIO_VALIDITA_CAN") & " and DATA<=" & riga.Item("FINE_VALIDITA_CAN") & "")
                    For Each row As Data.DataRow In Qresult
                        row.Item("BIT") = True
                    Next
                End If
            Next

            Dim totValidi As Integer = dtgiorni.Select("BIT = TRUE").Length


            If totValidi > 0 Then
                If InsertConguaglio(dtgiorni.Rows.Count, totValidi, idContAnalizzato, TipoDimensione.Value, valmq, Precision) = False Then
                    calcolaConguaglio = False
                End If
            End If



        Catch ex As Exception
            calcolaConguaglio = False
        End Try

    End Function
    Private Function InsertConguaglio(ByVal giorniSolari As Integer, ByVal giorniMerito As Integer, ByVal idContratto As Integer, ByVal tipoDimens As String, ByVal valMq As Decimal, ByVal Precision As Integer) As Boolean
        InsertConguaglio = True
        Try

            par.cmd.CommandText = "SELECT id_unita ,soggetti_contrattuali.id_anagrafica,NVL(dimensioni.valore,0)AS DIMENSIONE " _
                                & "FROM siscom_mi.unita_contrattuale,siscom_mi.soggetti_contrattuali,siscom_mi.dimensioni " _
                                & "WHERE unita_contrattuale.id_contratto = " & idContratto & " " _
                                & "AND soggetti_contrattuali.id_contratto = unita_contrattuale.id_contratto " _
                                & "AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' " _
                                & "AND dimensioni.id_unita_immobiliare(+) = id_unita " _
                                & "AND cod_tipologia = '" & tipoDimens & "'"

            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim dimensione = 0
            Dim impCalcolato = 0
            Dim idAnagrafica = 0
            Dim idUnita = 0
            Dim nessunInte = False
            Dim Dovuto As Decimal = 0

            If lettore.Read Then
                dimensione = par.IfNull(lettore("DIMENSIONE"), 0)
                impCalcolato = Math.Round(par.IfNull(lettore("DIMENSIONE"), 0) * valMq, Precision)
                idAnagrafica = par.IfNull(lettore("id_anagrafica"), 0)
                idUnita = par.IfNull(lettore("id_unita"), 0)
            Else
                nessunInte = True
            End If
            lettore.Close()

            impCalcolato = impCalcolato / giorniSolari 'ottengo l'importo giornaliero
            Dovuto = impCalcolato * giorniMerito 'ottengo il dovuto in base ai giorni trascorsi nell'area limite definita

            'ora verifico se avevo calcolato un preventivo del contributo calore su questo contratto
            Dim ImpPreventivato As Decimal = 0
            par.cmd.CommandText = "select IMPORTO_RICONOSCIUTO from siscom_mi.cont_calore_elaborazione where id_contratto = " & idContratto & " and id_cont_calore = " & idContributo.Value
            Dim letPreventivo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If letPreventivo.Read Then
                ImpPreventivato = par.IfNull(letPreventivo("IMPORTO_RICONOSCIUTO"), 0)
            End If
            letPreventivo.Close()

            If dimensione > 0 And idAnagrafica > 0 And idUnita > 0 Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONT_CALORE_ELABORAZIONE ( ID_CONT_CALORE, ID_CONTRATTO, " _
                    & "TIPO_CALCOLO, ID_ANAGRAFICA, DIMENSIONE, " _
                    & "IMPORTO_RICONOSCIUTO, STATO,ID_UNITA ) " _
                    & "VALUES (" & idContributo.Value & "," & idContratto & ",3, " _
                    & idAnagrafica & ", " _
                    & "" & par.VirgoleInPunti(dimensione) & "," & par.VirgoleInPunti(Dovuto - ImpPreventivato) & ",-1," & idUnita & ") "
                par.cmd.ExecuteNonQuery()

            Else
                Dim motivoAnomalia As String = ""
                If nessunInte = True Then
                    motivoAnomalia = "IMPOSSIBILE TROVARE INTESTATARIO PER IL CONTRATTO ID " & idContratto
                ElseIf dimensione <= 0 Then
                    motivoAnomalia = "DIMENSIONE DELL'UNITA IMMOBILIARE NON TROVATA OPPURE MINORE/UGUALE A ZERO"
                ElseIf idAnagrafica <= 0 Then
                    motivoAnomalia = "IMPOSSIBILE DEFINIRE L'ANAGRAFICA DELL'INTESTATARIO"
                ElseIf idUnita <= 0 Then
                    motivoAnomalia = "UNITA IMMOBILIARE NON TROVATA O NON DEFINIBILE"
                End If

                par.cmd.CommandText = "insert into siscom_mi.cont_calore_anomalie (id_cont_calore,id_contratto,id_unita,motivazione,tipo_calcolo) values " _
                                    & "(" & idContributo.Value & "," & RitornaNullseZero(idContratto) & ", " & RitornaNullseZero(idUnita) & ",'" & par.PulisciStrSql(motivoAnomalia) & "',3)"
                par.cmd.ExecuteNonQuery()
                AnomalieCong += 1

            End If

        Catch ex As Exception
            InsertConguaglio = False
        End Try
    End Function
    Private Function CreaDtGiorni(ByVal anno As Integer) As Data.DataTable
        CreaDtGiorni = New Data.DataTable

        CreaDtGiorni.Columns.Add("DATA", GetType(String))
        CreaDtGiorni.Columns.Add("BIT", GetType(Boolean))

        Try



            Dim data1 As Date = New Date(anno, 1, 1)
            Dim data2 As Date = New Date(anno, 12, 31)

            Dim datax As Date = data1

            While datax <= data2
                Dim data_anno As String

                data_anno = datax.ToString("yyyyMMdd")
                Dim riga = CreaDtGiorni.NewRow

                riga("DATA") = data_anno
                riga("BIT") = False

                CreaDtGiorni.Rows.Add(riga)

                datax = datax.AddDays(1)
            End While

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>CreaDtGiorni " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            par.myTrans.Rollback()
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)

            End If


        End Try
    End Function
    Protected Sub btnContinuaP_Click(sender As Object, e As System.EventArgs) Handles btnContinuaP.Click
        If Request.QueryString("TIPO") = "NUOVO" Then
            tipo = 1
            If DeleteElaborazione() = True Then
                CreaAventiDiritto(Me.ValoreMq.Value, Me.AreaLimite.Value, Me.cfArrotond.Value, Me.TipoDimensione.Value, tipo)
            End If

        ElseIf Request.QueryString("TIPO") = "CONGUAGLIO" Then
            tipo = 2
            If DeleteElaborazione() = True Then
                CreaConguaglio(Me.ValoreMq.Value, Me.AreaLimite.Value, Me.cfArrotond.Value, Me.TipoDimensione.Value, tipo)
            End If

        End If
    End Sub
    Private Function DeleteElaborazione() As Boolean
        DeleteElaborazione = True
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If tipo = 1 Then
                par.cmd.CommandText = "select id from siscom_mi.cont_calore_anno where id_stato in (0,1) and anno = " & Me.txtAnnoCalcolo.Text
                Dim letContributo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If letContributo.Read Then
                    idContributo.Value = par.IfNull(letContributo(0), 0)
                End If
                letContributo.Close()
                If idContributo.Value > 0 Then
                    'cancello l'elaborazione del preventivo
                    par.cmd.CommandText = "delete from siscom_mi.cont_calore_elaborazione where tipo_calcolo = 1 and id_cont_calore = " & idContributo.Value
                    par.cmd.ExecuteNonQuery()
                    'cancello le anomalie legate al preventivo
                    par.cmd.CommandText = "delete from siscom_mi.cont_calore_anomalie where id_cont_calore = " & idContributo.Value
                    par.cmd.ExecuteNonQuery()

                Else
                    DeleteElaborazione = False

                End If
            ElseIf tipo = 2 Then
                par.cmd.CommandText = "select id from siscom_mi.cont_calore_anno where id_stato = 3 and anno = " & Me.cmbAnniConsuntivabili.SelectedItem.Text
                Dim letContributo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If letContributo.Read Then
                    idContributo.Value = par.IfNull(letContributo(0), 0)
                End If
                letContributo.Close()
                'cancello l'elaborazione del consuntivo
                If idContributo.Value > 0 Then
                    par.cmd.CommandText = "delete from siscom_mi.cont_calore_elaborazione where tipo_calcolo = 3 and id_cont_calore = " & idContributo.Value
                    par.cmd.ExecuteNonQuery()
                    'cancello le anomalie legate al consuntivo
                    par.cmd.CommandText = "delete from siscom_mi.cont_calore_anomalie where id_cont_calore = " & idContributo.Value
                    par.cmd.ExecuteNonQuery()
                Else
                    DeleteElaborazione = False
                End If

            End If


            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)

            End If

        Catch ex As Exception
            DeleteElaborazione = False

            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>DeleteElaborazione " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)

            End If



        End Try
    End Function
    Private Function RitornaNullseZero(ByVal val As String) As String
        RitornaNullseZero = val
        If Not String.IsNullOrEmpty(val) Then
            If CInt(val) = 0 Then
                RitornaNullseZero = "null"
            End If
        End If

    End Function
End Class
