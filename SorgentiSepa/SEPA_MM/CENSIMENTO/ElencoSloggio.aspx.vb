
Partial Class CENSIMENTO_ElencoSloggio
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If




        If Not IsPostBack Then




            idunita.Value = Request.QueryString("ID")
            EVENTO.Value = Request.QueryString("T")
            idcontratto.Value = Request.QueryString("C")

            chiamante.Value = Request.QueryString("A")

            If Session.Item("CONT_DISDETTE") = "1" Then
                lettura.Value = "2"
            End If




            If Request.QueryString("L") = "2" Then
                Dim CTRL1 As Control
                For Each CTRL1 In Me.form1.Controls
                    If TypeOf CTRL1 Is TextBox Then
                        DirectCast(CTRL1, TextBox).Enabled = False
                    ElseIf TypeOf CTRL1 Is DropDownList Then
                        DirectCast(CTRL1, DropDownList).Enabled = False
                    ElseIf TypeOf CTRL1 Is CheckBox Then
                        DirectCast(CTRL1, CheckBox).Enabled = False
                    End If
                Next





            End If




            CaricaDati()
            CaricaElenco()


            Session.Add("INDIRIZZOUNITA", Label21.Text)
            Session.Add("CODICEUI", CODICE.Value)
            Session.Add("QUARTIERE", quartiere.Value)
            Session.Add("VIA", via_civico.Value)
            Session.Add("SCALA", scala.Value)
            Session.Add("PIANO", piano.Value)
            Session.Add("SUP_MQ", sup_mq.Value)
            Session.Add("INTERNO", interno.Value)



            'Dim CTRL As Control
            'For Each CTRL In Me.form1.Controls
            '    If TypeOf CTRL Is TextBox Then
            '        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('Modificato').value='1';")
            '    ElseIf TypeOf CTRL Is DropDownList Then
            '        DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('Modificato').value='1';")
            '    ElseIf TypeOf CTRL Is CheckBox Then
            '        DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('Modificato').value='1';")
            '    End If
            'Next
        End If


    End Sub


    Private Sub CaricaElenco()

        Dim dt As New Data.DataTable()


        Try
            Label10.Visible = False
            ' par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "SELECT SL_SLOGGIO.ID AS ID, SL_SLOGGIO.ID_UNITA_IMMOBILIARE AS ID_UNITA, " _
                & "SL_STATO_SLOGGIO.DESCRIZIONE AS STATO, SL_SLOGGIO.ID_STATO AS ID_STATO,  CASE WHEN sl_sloggio.data_sloggio IS NULL THEN '' ELSE  to_char(to_date(sl_sloggio.data_sloggio, 'yyyyMMdd'), 'dd/MM/yyyy') END AS data_sloggio, " _
        & "CASE WHEN sl_sloggio.data_pre_sloggio IS NULL THEN '' ELSE  to_char(to_date(sl_sloggio.data_pre_sloggio, 'yyyyMMdd'), 'dd/MM/yyyy') END AS data_presloggio " _
                                  & "FROM SISCOM_MI.SL_SLOGGIO, SISCOM_MI.SL_STATO_SLOGGIO " _
                                  & "WHERE SL_SLOGGIO.ID_UNITA_IMMOBILIARE = '" & idunita.Value & "'" _
                                  & "AND SL_SLOGGIO.ID_STATO = SL_STATO_SLOGGIO.ID ORDER BY ID"
                                  



            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)



            dgElenco.DataSource = dt
            dgElenco.DataBind()
            Session.Add("dtComp", dt)







        Catch ex As Exception
          
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Label10.Visible = True
            Label10.Text = ex.Message
        End Try


    End Sub







    Private Sub CaricaDati()
        Try
            Label10.Visible = False

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select unita_immobiliari.id_unita_principale, tipologia_unita_immobiliari.descrizione as tipounita,unita_immobiliari.cod_tipologia,unita_immobiliari.cod_tipo_disponibilita,complessi_immobiliari.id as idq, identificativi_catastali.superficie_mq as SUP_MQ ,COMPLESSI_IMMOBILIARI.ID_QUARTIERE AS ID_QUART, TAB_QUARTIERI.NOME AS NOME_QUART, edifici.id as idf,edifici.fl_piano_vendita,EDIFICI.GEST_RISC_DIR,edifici.condominio,tipo_livello_piano.descrizione as miopiano,(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA,indirizzi.cap,comuni_nazioni.nome as comune,unita_immobiliari.id_destinazione_uso,SISCOM_MI.UNITA_IMMOBILIARI.interno,UNITA_IMMOBILIARI.id as idunita,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,indirizzi.descrizione,indirizzi.civico,indirizzi.cap from siscom_mi.tipologia_unita_immobiliari, siscom_mi.identificativi_catastali, siscom_mi.tipo_livello_piano,siscom_mi.tab_quartieri,comuni_nazioni,siscom_mi.indirizzi,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici,SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE tipologia_unita_immobiliari.cod=unita_immobiliari.cod_tipologia and COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND edifici.id=unita_immobiliari.id_edificio and unita_immobiliari.cod_tipo_livello_piano=tipo_livello_piano.cod (+) and indirizzi.cod_comune=comuni_nazioni.cod (+) and COMPLESSI_IMMOBILIARI.ID_QUARTIERE = tab_quartieri.id and unita_immobiliari.id_catastale=identificativi_catastali.id (+) and unita_immobiliari.id_indirizzo=indirizzi.id (+) and unita_immobiliari.ID=" & idunita.Value
            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader3.Read Then
                '
                CODICE.Value = par.IfNull(myReader3("COD_UNITA_IMMOBILIARE"), "")


                Label21.Text = "Codice: " & CODICE.Value & " (" & par.IfNull(myReader3("tipounita"), "") & ") Indirizzo: " & par.IfNull(myReader3("descrizione"), "") & ", " & par.IfNull(myReader3("civico"), "") _
                 & par.IfNull(myReader3("cap"), "") & " " & par.IfNull(myReader3("comune"), "") & " Interno: " & par.IfNull(myReader3("interno"), "") & " Scala: " & par.IfNull(myReader3("scala"), "") & " Piano: " & par.IfNull(myReader3("miopiano"), "")

                via_civico.Value = par.IfNull(myReader3("descrizione"), "") & ", " & par.IfNull(myReader3("civico"), "")
                scala.Value = par.IfNull(myReader3("scala"), "")
                piano.Value = par.IfNull(myReader3("miopiano"), "")
                quartiere.Value = par.IfNull(myReader3("nome_quart"), "")
                sup_mq.Value = par.IfNull(myReader3("sup_mq"), "")
                interno.Value = par.IfNull(myReader3("interno"), "")





                If par.IfNull(myReader3("fl_piano_vendita"), "0") = "1" Then
                    lblPianoVendita.Visible = True
                    lblPianoVendita.Text = "<b>Unità inserita nel piano vendita!!</b>"
                Else
                    lblPianoVendita.Visible = False
                End If

                'If Mid(CODICE.Value, 1, 6) = "000000" Then
                '    Response.Write("<script>alert('Attenzione...Non è possibile effettuare la verifica dello stato manutentivo su una unità VIRTUALE! I campi saranno disabilitati!\nSi ricorda che la data di sloggio per i rapporti virtuali va inserita direttamente nella maschera del rapporto.');</script>")
                '    Dim CTRL1 As Control
                '    For Each CTRL1 In Me.form1.Controls
                '        If TypeOf CTRL1 Is TextBox Then
                '            DirectCast(CTRL1, TextBox).Enabled = False
                '        ElseIf TypeOf CTRL1 Is DropDownList Then
                '            DirectCast(CTRL1, DropDownList).Enabled = False
                '        ElseIf TypeOf CTRL1 Is CheckBox Then
                '            DirectCast(CTRL1, CheckBox).Enabled = False
                '        End If
                '    Next



                'End If


                'If par.IfNull(myReader3("cod_tipo_disponibilita"), "INDEF") = "INDEF" Then
                '    Response.Write("<script>alert('Attenzione...Non è possibile effettuare la verifica dello stato manutentivo su una unità con stato NON DEFINIBILE.\nI campi saranno disabilitati!\nSe si vogliono gestire i dati di questa unità, contattare gli uffici preposti del Comune di Milano.');</script>")
                '    Dim CTRL1 As Control
                '    For Each CTRL1 In Me.form1.Controls
                '        If TypeOf CTRL1 Is TextBox Then
                '            DirectCast(CTRL1, TextBox).Enabled = False
                '        ElseIf TypeOf CTRL1 Is DropDownList Then
                '            DirectCast(CTRL1, DropDownList).Enabled = False
                '        ElseIf TypeOf CTRL1 Is CheckBox Then
                '            DirectCast(CTRL1, CheckBox).Enabled = False
                '        End If
                '    Next


                'End If

                'If par.IfNull(myReader3("id_destinazione_uso"), -1) = -1 Then
                '    Response.Write("<script>alert('Attenzione...destinazione d\'uso non specificata. I campi saranno disabilitati!');</script>")
                '    Dim CTRL1 As Control
                '    For Each CTRL1 In Me.form1.Controls
                '        If TypeOf CTRL1 Is TextBox Then
                '            DirectCast(CTRL1, TextBox).Enabled = False
                '        ElseIf TypeOf CTRL1 Is DropDownList Then
                '            DirectCast(CTRL1, DropDownList).Enabled = False
                '        ElseIf TypeOf CTRL1 Is CheckBox Then
                '            DirectCast(CTRL1, CheckBox).Enabled = False
                '        End If
                '    Next


                'End If

                'If par.IfNull(myReader3("id_unita_principale"), -1) <> -1 Then
                '    Response.Write("<script>alert('Attenzione...Non è possibile effettuare la verifica dello stato menutentivo su una pertinenza. I campi saranno disabilitati!');</script>")
                '    Dim CTRL1 As Control
                '    For Each CTRL1 In Me.form1.Controls
                '        If TypeOf CTRL1 Is TextBox Then
                '            DirectCast(CTRL1, TextBox).Enabled = False
                '        ElseIf TypeOf CTRL1 Is DropDownList Then
                '            DirectCast(CTRL1, DropDownList).Enabled = False
                '        ElseIf TypeOf CTRL1 Is CheckBox Then
                '            DirectCast(CTRL1, CheckBox).Enabled = False
                '        End If
                '    Next


                'End If

            End If
            myReader3.Close()

            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If



        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:CaricaDati " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub


    Public Property Inserimento() As Long
        Get
            If Not (ViewState("par_Inserimento") Is Nothing) Then
                Return CLng(ViewState("par_Inserimento"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_Inserimento") = value
        End Set

    End Property

    Public Property IndiceComplesso() As String
        Get
            If Not (ViewState("par_IndiceComplesso") Is Nothing) Then
                Return CStr(ViewState("par_IndiceComplesso"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IndiceComplesso") = value
        End Set

    End Property




   


    Protected Sub dgElenco_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgElenco.ItemDataBound


        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;SelColo=OldColor;this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;SelColo=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=SelColo;}SelColo=OldColor;Selezionato=this;this.style.backgroundColor='red'; document.getElementById('id_sloggio').value='" & e.Item.Cells(0).Text & "'; document.getElementById('id_stato').value='" & e.Item.Cells(2).Text & "'")
            ' e.Item.Attributes.Add("onDblclick", "document.getElementById('id_riga').value='" & e.Item.Cells(0).Text & "'; document.getElementById('id_scheda').value='" & e.Item.Cells(1).Text & "'; document.getElementById('modifica_btn').click();")

        End If






    End Sub




    Protected Sub btn_modifica_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_modifica.Click

        If id_sloggio.Value = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Nessuna voce selezionata!');", True)
        Else

            Response.Redirect("GestSloggio.aspx?PROVENIENZA=1&ID=" & idunita.Value & "&IDSTATO=" & id_stato.Value & "&IDSLOGGIO=" & id_sloggio.Value)
        End If
    End Sub
End Class
