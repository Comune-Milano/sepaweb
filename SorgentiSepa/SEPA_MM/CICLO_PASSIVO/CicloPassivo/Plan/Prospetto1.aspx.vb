
Partial Class Contabilita_CicloPassivo_Plan_Prospetto1
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim ds As New Data.DataSet()
    Dim dlist As CheckBoxList
    Dim da As Oracle.DataAccess.Client.OracleDataAdapter
    Dim ElencoOperatori As String()



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If IsPostBack = False Then
            pianoF.Value = Request.QueryString("P")
            standard.Value = Request.QueryString("S")
            SettaIndici()
            If pianoF.Value = "-1" Then
                NuovoPiano()
                ImgCompleto.Visible = False
            Else
                CaricaPiano()
            End If

            S1txtCod3.Text = "1.03"
            S1txtCod3.ReadOnly = True
            S1txtCod4.Text = "1.04"
            S1txtCod4.ReadOnly = True
            S1txtCod5.Text = "1.05"
            S1txtCod5.ReadOnly = True
            S1txtCod6.Text = "1.06"
            S1txtCod6.ReadOnly = True
            S1txtCod7.Text = "1.07"
            S1txtCod7.ReadOnly = True
            S1txtCod8.Text = "1.08"
            S1txtCod8.ReadOnly = True
            S1txtCod9.Text = "1.09"
            S1txtCod9.ReadOnly = True
            S1txtCod10.Text = "1.10"
            S1txtCod10.ReadOnly = True
            S1txtCod11.Text = "1.11"
            S1txtCod11.ReadOnly = True
            S1txtCod12.Text = "1.12"
            S1txtCod12.ReadOnly = True
            S1txtCod13.Text = "1.13"
            S1txtCod13.ReadOnly = True
            S1txtCod14.Text = "1.14"
            S1txtCod14.ReadOnly = True
            S1txtCod15.Text = "1.15"
            S1txtCod15.ReadOnly = True
            S1txtCod16.Text = "1.16"
            S1txtCod16.ReadOnly = True
            S1txtCod17.Text = "1.17"
            S1txtCod17.ReadOnly = True
            S1txtCod18.Text = "1.18"
            S1txtCod18.ReadOnly = True
            S1txtCod19.Text = "1.19"
            S1txtCod19.ReadOnly = True
            S1txtCod20.Text = "1.20"
            S1txtCod20.ReadOnly = True

            S2txtCod5.Text = "2.05"
            S2txtCod5.ReadOnly = True
            S2txtCod6.Text = "2.06"
            S2txtCod6.ReadOnly = True
            S2txtCod7.Text = "2.07"
            S2txtCod7.ReadOnly = True
            S2txtCod8.Text = "2.08"
            S2txtCod8.ReadOnly = True
            S2txtCod9.Text = "2.09"
            S2txtCod9.ReadOnly = True
            S2txtCod10.Text = "2.10"
            S2txtCod10.ReadOnly = True
            S2txtCod11.Text = "2.11"
            S2txtCod11.ReadOnly = True
            S2txtCod12.Text = "2.12"
            S2txtCod12.ReadOnly = True
            S2txtCod13.Text = "2.13"
            S2txtCod13.ReadOnly = True
            S2txtCod14.Text = "2.14"
            S2txtCod14.ReadOnly = True
            S2txtCod15.Text = "2.15"
            S2txtCod15.ReadOnly = True
            S2txtCod16.Text = "2.16"
            S2txtCod16.ReadOnly = True
            S2txtCod17.Text = "2.17"
            S2txtCod17.ReadOnly = True
            S2txtCod18.Text = "2.18"
            S2txtCod18.ReadOnly = True
            S2txtCod19.Text = "2.19"
            S2txtCod19.ReadOnly = True
            S2txtCod20.Text = "2.20"
            S2txtCod20.ReadOnly = True


            S3txtCod3.Text = "3.03"
            S3txtCod3.ReadOnly = True
            S3txtCod4.Text = "3.04"
            S3txtCod4.ReadOnly = True
            S3txtCod5.Text = "3.05"
            S3txtCod5.ReadOnly = True
            S4txtCod1.Text = "4.01"
            S4txtCod1.ReadOnly = True
            S4txtCod2.Text = "4.02"
            S4txtCod2.ReadOnly = True
            S4txtCod3.Text = "4.03"
            S4txtCod3.ReadOnly = True
            S4txtCod4.Text = "4.04"
            S4txtCod4.ReadOnly = True
            S4txtCod5.Text = "4.05"
            S4txtCod5.ReadOnly = True

            If Request.QueryString("S") = "1" Then
                DisabilitaStandard()
            End If

        End If

        If testovoce.value <> "" Then
            lblTestoVoce.Text = testovoce.value
        End If




        If lblStato.Text = "STATO:NUOVO" Then
            Dim CTRL As Control
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('Modificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('Modificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('Modificato').value='1';")
                End If
            Next
            'statop.Value = "0"
        Else
            Dim CTRL As Control
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
                ElseIf TypeOf CTRL Is ImageButton Then
                    DirectCast(CTRL, ImageButton).Visible = False
                ElseIf TypeOf CTRL Is Web.UI.WebControls.Image Then
                    If DirectCast(CTRL, Web.UI.WebControls.Image).ID <> "imgEsci" And DirectCast(CTRL, Web.UI.WebControls.Image).ID <> "imgStampa" And DirectCast(CTRL, Web.UI.WebControls.Image).ID <> "imgEventi" Then
                        DirectCast(CTRL, Web.UI.WebControls.Image).Visible = False
                    End If
                End If
            Next
            statop.Value = "1"

        End If
    End Sub

    Private Function DisabilitaStandard()
        S1txtCod1.ReadOnly = True
        S1txtVoce1.ReadOnly = True

        S1txtCod2.ReadOnly = True
        S1txtVoce2.ReadOnly = True

        'S1txtCod3.ReadOnly = True
        'S1txtVoce3.ReadOnly = True

        'S1txtCod4.ReadOnly = True
        'S1txtVoce4.ReadOnly = True

        'S1txtCod5.ReadOnly = True
        'S1txtVoce5.ReadOnly = True

        'S1txtCod6.ReadOnly = True
        'S1txtVoce6.ReadOnly = True

        'S1txtCod7.ReadOnly = True
        'S1txtVoce7.ReadOnly = True

        'S1txtCod7.ReadOnly = True
        'S1txtVoce7.ReadOnly = True

        'S1txtCod8.ReadOnly = True
        'S1txtVoce8.ReadOnly = True

        'S1txtCod9.ReadOnly = True
        'S1txtVoce9.ReadOnly = True

        'S1txtCod10.ReadOnly = True
        'S1txtVoce10.ReadOnly = True

        S2txtCod1.ReadOnly = True
        S2txtVoce1.ReadOnly = True

        S2txtCod2.ReadOnly = True
        S2txtVoce2.ReadOnly = True

        S2txtCod3.ReadOnly = True
        S2txtVoce3.ReadOnly = True

        S2txtCod4.ReadOnly = True
        S2txtVoce4.ReadOnly = True

        'S2txtCod5.ReadOnly = True
        'S2txtVoce5.ReadOnly = True

        'S2txtCod6.ReadOnly = True
        'S2txtVoce6.ReadOnly = True

        'S2txtCod7.ReadOnly = True
        'S2txtVoce7.ReadOnly = True

        'S2txtCod8.ReadOnly = True
        'S2txtVoce8.ReadOnly = True

        'S2txtCod9.ReadOnly = True
        'S2txtVoce9.ReadOnly = True

        'S2txtCod10.ReadOnly = True
        'S2txtVoce10.ReadOnly = True

        'S2txtCod11.ReadOnly = True
        'S2txtVoce11.ReadOnly = True

        'S2txtCod12.ReadOnly = True
        'S2txtVoce12.ReadOnly = True

        'S2txtCod13.ReadOnly = True
        'S2txtVoce13.ReadOnly = True

        'S2txtCod14.ReadOnly = True
        'S2txtVoce14.ReadOnly = True

        'S2txtCod15.ReadOnly = True
        'S2txtVoce15.ReadOnly = True

        'S2txtCod16.ReadOnly = True
        'S2txtVoce16.ReadOnly = True

        S3txtCod1.ReadOnly = True
        S3txtVoce1.ReadOnly = True

        S3txtCod2.ReadOnly = True
        S3txtVoce2.ReadOnly = True
    End Function

    Function CaricaOpDb(ByVal indice As Long) As String
        par.cmd.CommandText = "select * from SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_VOCE=" & indice & " ORDER BY ID_OPERATORE asc"
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        CaricaOpDb = ""
        While myReader2.Read
            CaricaOpDb = CaricaOpDb & par.IfNull(myReader2("ID_OPERATORE"), "-1") & "#"
        End While
        myReader2.Close()
    End Function


    Function CaricaPiano()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)



            par.cmd.CommandText = "select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID=" & pianoF.Value & " and PF_STATI.ID=PF_MAIN.ID_STATO and t_esercizio_finanziario.id=pf_main.id_esercizio_finanziario"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Label1.Text = myReader5("inizio") & "-" & myReader5("fine")
                per.Value = Label1.Text
                lblStato.Text = "STATO:" & par.IfNull(myReader5("stato"), "")
                idEF.Value = myReader5("id_esercizio_finanziario")
            End If
            myReader5.Close()

            'par.cmd.CommandText = "select * from SISCOM_MI.T_VOCI_PIANI_FINANZIARI ORDER BY descrizione asc"
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'cmbVoceSchema.Items.Add(New ListItem("---", "-1"))
            'While myReader1.Read
            '    cmbVoceSchema.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), ""), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()
            'cmbVoceSchema.SelectedIndex = -1
            'cmbVoceSchema.Items.FindByValue("-1").Selected = True

            dlist = ListaOperatori

            da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID,COGNOME||' '||NOME AS ""DESCRIZIONE"" FROM OPERATORI WHERE MOD_CICLO_P=1 AND BP_COMPILAZIONE=1 and id_ufficio<>0 and cognome is not null and  ID_CAF=2 and fl_eliminato='0' ORDER BY cognome ASC,nome asc", par.OracleConn)
            da.Fill(ds)

            dlist.Items.Clear()
            dlist.DataSource = ds
            dlist.DataTextField = "DESCRIZIONE"
            dlist.DataValueField = "ID"

            dlist.DataBind()

            da.Dispose()
            da = Nothing

            dlist.DataSource = Nothing
            dlist = Nothing

            ds.Clear()
            ds.Dispose()
            ds = Nothing


            CaricaValori(1, S1txtVoce1, S1txtCod1, S1Op1, S1Sotto1)
            CaricaValori(2, S1txtVoce2, S1txtCod2, S1Op2, S1Sotto2)
            CaricaValori(3, S1txtVoce3, S1txtCod3, S1Op3, S1Sotto3)
            CaricaValori(4, S1txtVoce4, S1txtCod4, S1Op4, S1Sotto4)
            CaricaValori(5, S1txtVoce5, S1txtCod5, S1Op5, S1Sotto5)
            CaricaValori(6, S1txtVoce6, S1txtCod6, S1Op6, S1Sotto6)
            CaricaValori(7, S1txtVoce7, S1txtCod7, S1Op7, S1Sotto7)
            CaricaValori(8, S1txtVoce8, S1txtCod8, S1Op8, S1Sotto8)
            CaricaValori(9, S1txtVoce9, S1txtCod9, S1Op9, S1Sotto9)
            CaricaValori(10, S1txtVoce10, S1txtCod10, S1Op10, S1Sotto10)
            CaricaValori(11, S1txtVoce11, S1txtCod11, S1Op11, S1Sotto11)
            CaricaValori(12, S1txtVoce12, S1txtCod12, S1Op12, S1Sotto12)
            CaricaValori(13, S1txtVoce13, S1txtCod13, S1Op13, S1Sotto13)
            CaricaValori(14, S1txtVoce14, S1txtCod14, S1Op14, S1Sotto14)
            CaricaValori(15, S1txtVoce15, S1txtCod15, S1Op15, S1Sotto15)
            CaricaValori(16, S1txtVoce16, S1txtCod16, S1Op16, S1Sotto16)
            CaricaValori(17, S1txtVoce17, S1txtCod17, S1Op17, S1Sotto17)
            CaricaValori(18, S1txtVoce18, S1txtCod18, S1Op18, S1Sotto18)
            CaricaValori(19, S1txtVoce19, S1txtCod19, S1Op19, S1Sotto19)
            CaricaValori(20, S1txtVoce20, S1txtCod20, S1Op20, S1Sotto20)


            CaricaValori(21, S2txtVoce1, S2txtCod1, S2Op1, S2Sotto1)
            CaricaValori(22, S2txtVoce2, S2txtCod2, S2Op2, S2Sotto2)
            CaricaValori(23, S2txtVoce3, S2txtCod3, S2Op3, S2Sotto3)
            CaricaValori(24, S2txtVoce4, S2txtCod4, S2Op4, S2Sotto4)
            CaricaValori(25, S2txtVoce5, S2txtCod5, S2Op5, S2Sotto5)
            CaricaValori(26, S2txtVoce6, S2txtCod6, S2Op6, S2Sotto6)
            CaricaValori(27, S2txtVoce7, S2txtCod7, S2Op7, S2Sotto7)
            CaricaValori(28, S2txtVoce8, S2txtCod8, S2Op8, S2Sotto8)
            CaricaValori(29, S2txtVoce9, S2txtCod9, S2Op9, S2Sotto9)
            CaricaValori(30, S2txtVoce10, S2txtCod10, S2Op10, S2Sotto10)
            CaricaValori(31, S2txtVoce11, S2txtCod11, S2Op11, S2Sotto11)
            CaricaValori(32, S2txtVoce12, S2txtCod12, S2Op12, S2Sotto12)
            CaricaValori(33, S2txtVoce13, S2txtCod13, S2Op13, S2Sotto13)
            CaricaValori(34, S2txtVoce14, S2txtCod14, S2Op14, S2Sotto14)
            CaricaValori(35, S2txtVoce15, S2txtCod15, S2Op15, S2Sotto15)
            CaricaValori(36, S2txtVoce16, S2txtCod16, S2Op16, S2Sotto16)
            CaricaValori(37, S2txtVoce17, S2txtCod17, S2Op17, S2Sotto17)
            CaricaValori(38, S2txtVoce18, S2txtCod18, S2Op18, S2Sotto18)
            CaricaValori(39, S2txtVoce19, S2txtCod19, S2Op19, S2Sotto19)
            CaricaValori(40, S2txtVoce20, S2txtCod20, S2Op20, S2Sotto20)


            CaricaValori(41, S3txtVoce1, S3txtCod1, S3Op1, S3Sotto1)
            CaricaValori(42, S3txtVoce2, S3txtCod2, S3Op2, S3Sotto2)
            CaricaValori(43, S3txtVoce3, S3txtCod3, S3Op3, S3Sotto3)
            CaricaValori(44, S3txtVoce4, S3txtCod4, S3Op4, S3Sotto4)
            CaricaValori(45, S3txtVoce5, S3txtCod5, S3Op5, S3Sotto5)

            CaricaValori(46, S4txtVoce1, S4txtCod1, S4Op1, S4Sotto1)
            CaricaValori(47, S4txtVoce2, S4txtCod2, S4Op2, S4Sotto2)
            CaricaValori(48, S4txtVoce3, S4txtCod3, S4Op3, S4Sotto3)
            CaricaValori(49, S4txtVoce4, S4txtCod4, S4Op4, S4Sotto4)
            CaricaValori(50, S4txtVoce5, S4txtCod5, S4Op5, S4Sotto5)


            par.cmd.CommandText = "select * from operatori where id=" & Session.Item("ID_OPERATORE")
            myReader5 = par.cmd.ExecuteReader()
            If myReader5.Read Then
                If par.IfNull(myReader5("BP_FORMALIZZAZIONE_L"), "1") = "1" Then
                    Dim CTRL As Control
                    For Each CTRL In Me.form1.Controls
                        If TypeOf CTRL Is TextBox Then
                            DirectCast(CTRL, TextBox).Enabled = False
                        ElseIf TypeOf CTRL Is DropDownList Then
                            DirectCast(CTRL, DropDownList).Enabled = False
                        ElseIf TypeOf CTRL Is CheckBox Then
                            DirectCast(CTRL, CheckBox).Enabled = False
                        ElseIf TypeOf CTRL Is ImageButton Then
                            DirectCast(CTRL, ImageButton).Visible = False
                        ElseIf TypeOf CTRL Is Web.UI.WebControls.Image Then
                            If DirectCast(CTRL, Web.UI.WebControls.Image).ID <> "imgEsci" And DirectCast(CTRL, Web.UI.WebControls.Image).ID <> "imgStampa" And DirectCast(CTRL, Web.UI.WebControls.Image).ID <> "imgEventi" Then
                                DirectCast(CTRL, Web.UI.WebControls.Image).Visible = False
                            End If
                        End If
                    Next
                    statop.Value = "1"
                End If
            End If
            myReader5.Close()


           


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Function CaricaCaselle(ByVal indice As Integer, ByVal DescrizioneVoce As TextBox, ByVal Codice As TextBox, ByVal Id As HiddenField)

        Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader
        par.cmd.CommandText = "select * from SISCOM_MI.T_VOCI_PIANI_FINANZIARI where id=" & indice
        myReader11 = par.cmd.ExecuteReader()
        If myReader11.Read Then
            DescrizioneVoce.Text = par.IfNull(myReader11("descrizione"), "")
            Codice.Text = par.IfNull(myReader11("cod"), "")
            Id.Value = par.IfNull(myReader11("id"), "-1")
            Codice.Attributes.Add("ID_CAPITOLO", par.IfNull(myReader11("ID_CAPITOLO"), "null"))
        End If
        myReader11.Close()
    End Function


    Function ValorizzaSottoSottoVoci(ByVal Testo As String, ByVal SottoVoce As String)
        'Dim Valori() As String
        'Dim seps() As Char = {"#"}
        'Dim i As Long = 0
        'Dim j As Integer = 1

        'Valori = Testo.Split(seps)

        Select Case SottoVoce
            Case "1.01.01"
                S1Sotto1Sotto1.Value = Testo
            Case "1.01.02"
                S1Sotto1Sotto2.Value = Testo
            Case "1.01.03"
                S1Sotto1Sotto3.Value = Testo
            Case "1.01.04"
                S1Sotto1Sotto4.Value = Testo
            Case "1.01.05"
                S1Sotto1Sotto5.Value = Testo
            Case "1.01.06"
                S1Sotto1Sotto6.Value = Testo
            Case "1.01.07"
                S1Sotto1Sotto7.Value = Testo
            Case "1.01.08"
                S1Sotto1Sotto8.Value = Testo
            Case "1.01.09"
                S1Sotto1Sotto9.Value = Testo
            Case "1.01.10"
                S1Sotto1Sotto10.Value = Testo

            Case "1.02.01"
                S1Sotto2Sotto1.Value = Testo
            Case "1.02.02"
                S1Sotto2Sotto2.Value = Testo
            Case "1.02.03"
                S1Sotto2Sotto3.Value = Testo
            Case "1.02.04"
                S1Sotto2Sotto4.Value = Testo
            Case "1.02.05"
                S1Sotto2Sotto5.Value = Testo
            Case "1.02.06"
                S1Sotto2Sotto6.Value = Testo
            Case "1.02.07"
                S1Sotto2Sotto7.Value = Testo
            Case "1.02.08"
                S1Sotto2Sotto8.Value = Testo
            Case "1.02.09"
                S1Sotto2Sotto9.Value = Testo
            Case "1.02.10"
                S1Sotto2Sotto10.Value = Testo

            Case "1.03.01"
                S1Sotto3Sotto3.Value = Testo
            Case "1.03.02"
                S1Sotto3Sotto2.Value = Testo
            Case "1.03.03"
                S1Sotto3Sotto3.Value = Testo
            Case "1.03.04"
                S1Sotto3Sotto4.Value = Testo
            Case "1.03.05"
                S1Sotto3Sotto5.Value = Testo
            Case "1.03.06"
                S1Sotto3Sotto6.Value = Testo
            Case "1.03.07"
                S1Sotto3Sotto7.Value = Testo
            Case "1.03.08"
                S1Sotto3Sotto8.Value = Testo
            Case "1.03.09"
                S1Sotto3Sotto9.Value = Testo
            Case "1.03.10"
                S1Sotto3Sotto10.Value = Testo

            Case "1.04.01"
                S1Sotto4Sotto1.Value = Testo
            Case "1.04.02"
                S1Sotto4Sotto2.Value = Testo
            Case "1.04.03"
                S1Sotto4Sotto3.Value = Testo
            Case "1.04.04"
                S1Sotto4Sotto4.Value = Testo
            Case "1.04.05"
                S1Sotto4Sotto5.Value = Testo
            Case "1.04.06"
                S1Sotto4Sotto6.Value = Testo
            Case "1.04.07"
                S1Sotto4Sotto7.Value = Testo
            Case "1.04.08"
                S1Sotto4Sotto8.Value = Testo
            Case "1.04.09"
                S1Sotto4Sotto9.Value = Testo
            Case "1.04.10"
                S1Sotto4Sotto10.Value = Testo

            Case "1.05.01"
                S1Sotto5Sotto1.Value = Testo
            Case "1.05.02"
                S1Sotto5Sotto2.Value = Testo
            Case "1.05.03"
                S1Sotto5Sotto3.Value = Testo
            Case "1.05.04"
                S1Sotto5Sotto4.Value = Testo
            Case "1.05.05"
                S1Sotto5Sotto5.Value = Testo
            Case "1.05.06"
                S1Sotto5Sotto6.Value = Testo
            Case "1.05.07"
                S1Sotto5Sotto7.Value = Testo
            Case "1.05.08"
                S1Sotto5Sotto8.Value = Testo
            Case "1.05.09"
                S1Sotto5Sotto9.Value = Testo
            Case "1.05.10"
                S1Sotto5Sotto10.Value = Testo

            Case "1.06.01"
                S1Sotto6Sotto1.Value = Testo
            Case "1.06.02"
                S1Sotto6Sotto2.Value = Testo
            Case "1.06.03"
                S1Sotto6Sotto3.Value = Testo
            Case "1.06.04"
                S1Sotto6Sotto4.Value = Testo
            Case "1.06.05"
                S1Sotto6Sotto5.Value = Testo
            Case "1.06.06"
                S1Sotto6Sotto6.Value = Testo
            Case "1.06.07"
                S1Sotto6Sotto7.Value = Testo
            Case "1.06.08"
                S1Sotto6Sotto8.Value = Testo
            Case "1.06.09"
                S1Sotto6Sotto9.Value = Testo
            Case "1.06.10"
                S1Sotto6Sotto10.Value = Testo

            Case "1.07.01"
                S1Sotto7Sotto1.Value = Testo
            Case "1.07.02"
                S1Sotto7Sotto2.Value = Testo
            Case "1.07.03"
                S1Sotto7Sotto3.Value = Testo
            Case "1.07.04"
                S1Sotto7Sotto4.Value = Testo
            Case "1.07.05"
                S1Sotto7Sotto5.Value = Testo
            Case "1.07.06"
                S1Sotto7Sotto6.Value = Testo
            Case "1.07.07"
                S1Sotto7Sotto7.Value = Testo
            Case "1.07.08"
                S1Sotto7Sotto8.Value = Testo
            Case "1.07.09"
                S1Sotto7Sotto9.Value = Testo
            Case "1.07.10"
                S1Sotto7Sotto10.Value = Testo

            Case "1.08.01"
                S1Sotto8Sotto1.Value = Testo
            Case "1.08.02"
                S1Sotto8Sotto2.Value = Testo
            Case "1.08.03"
                S1Sotto8Sotto3.Value = Testo
            Case "1.08.04"
                S1Sotto8Sotto4.Value = Testo
            Case "1.08.05"
                S1Sotto8Sotto5.Value = Testo
            Case "1.08.06"
                S1Sotto8Sotto6.Value = Testo
            Case "1.08.07"
                S1Sotto8Sotto7.Value = Testo
            Case "1.08.08"
                S1Sotto8Sotto8.Value = Testo
            Case "1.08.09"
                S1Sotto8Sotto9.Value = Testo
            Case "1.08.10"
                S1Sotto8Sotto10.Value = Testo

            Case "1.09.01"
                S1Sotto9Sotto1.Value = Testo
            Case "1.09.02"
                S1Sotto9Sotto2.Value = Testo
            Case "1.09.03"
                S1Sotto9Sotto3.Value = Testo
            Case "1.09.04"
                S1Sotto9Sotto4.Value = Testo
            Case "1.09.05"
                S1Sotto9Sotto5.Value = Testo
            Case "1.09.06"
                S1Sotto9Sotto6.Value = Testo
            Case "1.09.07"
                S1Sotto9Sotto7.Value = Testo
            Case "1.09.08"
                S1Sotto9Sotto8.Value = Testo
            Case "1.09.09"
                S1Sotto9Sotto9.Value = Testo
            Case "1.09.10"
                S1Sotto9Sotto10.Value = Testo

            Case "1.10.01"
                S1Sotto10Sotto1.Value = Testo
            Case "1.10.02"
                S1Sotto10Sotto2.Value = Testo
            Case "1.10.03"
                S1Sotto1Sotto3.Value = Testo
            Case "1.10.04"
                S1Sotto10Sotto4.Value = Testo
            Case "1.10.05"
                S1Sotto10Sotto5.Value = Testo
            Case "1.10.06"
                S1Sotto10Sotto6.Value = Testo
            Case "1.10.07"
                S1Sotto10Sotto7.Value = Testo
            Case "1.10.08"
                S1Sotto10Sotto8.Value = Testo
            Case "1.10.09"
                S1Sotto10Sotto9.Value = Testo
            Case "1.10.10"
                S1Sotto10Sotto10.Value = Testo

            Case "1.11.01"
                S1Sotto11Sotto1.Value = Testo
            Case "1.11.02"
                S1Sotto11Sotto2.Value = Testo
            Case "1.11.03"
                S1Sotto11Sotto3.Value = Testo
            Case "1.11.04"
                S1Sotto11Sotto4.Value = Testo
            Case "1.11.05"
                S1Sotto11Sotto5.Value = Testo
            Case "1.11.06"
                S1Sotto11Sotto6.Value = Testo
            Case "1.11.07"
                S1Sotto11Sotto7.Value = Testo
            Case "1.11.08"
                S1Sotto11Sotto8.Value = Testo
            Case "1.11.09"
                S1Sotto11Sotto9.Value = Testo
            Case "1.11.10"
                S1Sotto11Sotto10.Value = Testo

            Case "1.12.01"
                S1Sotto12Sotto1.Value = Testo
            Case "1.12.02"
                S1Sotto12Sotto2.Value = Testo
            Case "1.12.03"
                S1Sotto12Sotto3.Value = Testo
            Case "1.12.04"
                S1Sotto12Sotto4.Value = Testo
            Case "1.12.05"
                S1Sotto12Sotto5.Value = Testo
            Case "1.12.06"
                S1Sotto12Sotto6.Value = Testo
            Case "1.12.07"
                S1Sotto12Sotto7.Value = Testo
            Case "1.12.08"
                S1Sotto12Sotto8.Value = Testo
            Case "1.12.09"
                S1Sotto12Sotto9.Value = Testo
            Case "1.12.10"
                S1Sotto12Sotto10.Value = Testo

            Case "1.13.01"
                S1Sotto13Sotto1.Value = Testo
            Case "1.13.02"
                S1Sotto13Sotto2.Value = Testo
            Case "1.13.03"
                S1Sotto13Sotto3.Value = Testo
            Case "1.13.04"
                S1Sotto13Sotto4.Value = Testo
            Case "1.13.05"
                S1Sotto13Sotto5.Value = Testo
            Case "1.13.06"
                S1Sotto13Sotto6.Value = Testo
            Case "1.13.07"
                S1Sotto13Sotto7.Value = Testo
            Case "1.13.08"
                S1Sotto13Sotto8.Value = Testo
            Case "1.13.09"
                S1Sotto13Sotto9.Value = Testo
            Case "1.13.10"
                S1Sotto13Sotto10.Value = Testo

            Case "1.14.01"
                S1Sotto14Sotto1.Value = Testo
            Case "1.14.02"
                S1Sotto14Sotto2.Value = Testo
            Case "1.14.03"
                S1Sotto14Sotto3.Value = Testo
            Case "1.14.04"
                S1Sotto14Sotto4.Value = Testo
            Case "1.14.05"
                S1Sotto14Sotto5.Value = Testo
            Case "1.14.06"
                S1Sotto14Sotto6.Value = Testo
            Case "1.14.07"
                S1Sotto14Sotto7.Value = Testo
            Case "1.14.08"
                S1Sotto14Sotto8.Value = Testo
            Case "1.14.09"
                S1Sotto14Sotto9.Value = Testo
            Case "1.14.10"
                S1Sotto14Sotto10.Value = Testo

            Case "1.15.01"
                S1Sotto15Sotto1.Value = Testo
            Case "1.15.02"
                S1Sotto15Sotto2.Value = Testo
            Case "1.15.03"
                S1Sotto15Sotto3.Value = Testo
            Case "1.15.04"
                S1Sotto15Sotto4.Value = Testo
            Case "1.15.05"
                S1Sotto15Sotto5.Value = Testo
            Case "1.15.06"
                S1Sotto15Sotto6.Value = Testo
            Case "1.15.07"
                S1Sotto15Sotto7.Value = Testo
            Case "1.15.08"
                S1Sotto15Sotto8.Value = Testo
            Case "1.15.09"
                S1Sotto15Sotto9.Value = Testo
            Case "1.15.10"
                S1Sotto15Sotto10.Value = Testo

            Case "1.16.01"
                S1Sotto16Sotto1.Value = Testo
            Case "1.16.02"
                S1Sotto16Sotto2.Value = Testo
            Case "1.16.03"
                S1Sotto16Sotto3.Value = Testo
            Case "1.16.04"
                S1Sotto16Sotto4.Value = Testo
            Case "1.16.05"
                S1Sotto16Sotto5.Value = Testo
            Case "1.16.06"
                S1Sotto16Sotto6.Value = Testo
            Case "1.16.07"
                S1Sotto16Sotto7.Value = Testo
            Case "1.16.08"
                S1Sotto16Sotto8.Value = Testo
            Case "1.16.09"
                S1Sotto16Sotto9.Value = Testo
            Case "1.16.10"
                S1Sotto16Sotto10.Value = Testo

            Case "1.17.01"
                S1Sotto17Sotto1.Value = Testo
            Case "1.17.02"
                S1Sotto17Sotto2.Value = Testo
            Case "1.17.03"
                S1Sotto17Sotto3.Value = Testo
            Case "1.17.04"
                S1Sotto17Sotto4.Value = Testo
            Case "1.17.05"
                S1Sotto17Sotto5.Value = Testo
            Case "1.17.06"
                S1Sotto17Sotto6.Value = Testo
            Case "1.17.07"
                S1Sotto17Sotto7.Value = Testo
            Case "1.17.08"
                S1Sotto17Sotto8.Value = Testo
            Case "1.17.09"
                S1Sotto17Sotto9.Value = Testo
            Case "1.17.10"
                S1Sotto17Sotto10.Value = Testo

            Case "1.18.01"
                S1Sotto18Sotto1.Value = Testo
            Case "1.18.02"
                S1Sotto18Sotto2.Value = Testo
            Case "1.18.03"
                S1Sotto18Sotto3.Value = Testo
            Case "1.18.04"
                S1Sotto18Sotto4.Value = Testo
            Case "1.18.05"
                S1Sotto18Sotto5.Value = Testo
            Case "1.18.06"
                S1Sotto18Sotto6.Value = Testo
            Case "1.18.07"
                S1Sotto18Sotto7.Value = Testo
            Case "1.18.08"
                S1Sotto18Sotto8.Value = Testo
            Case "1.18.09"
                S1Sotto18Sotto9.Value = Testo
            Case "1.18.10"
                S1Sotto18Sotto10.Value = Testo

            Case "1.19.01"
                S1Sotto19Sotto1.Value = Testo
            Case "1.19.02"
                S1Sotto19Sotto2.Value = Testo
            Case "1.19.03"
                S1Sotto19Sotto3.Value = Testo
            Case "1.19.04"
                S1Sotto19Sotto4.Value = Testo
            Case "1.19.05"
                S1Sotto19Sotto5.Value = Testo
            Case "1.19.06"
                S1Sotto19Sotto6.Value = Testo
            Case "1.19.07"
                S1Sotto19Sotto7.Value = Testo
            Case "1.19.08"
                S1Sotto19Sotto8.Value = Testo
            Case "1.19.09"
                S1Sotto19Sotto9.Value = Testo
            Case "1.19.10"
                S1Sotto19Sotto10.Value = Testo

            Case "1.20.01"
                S1Sotto20Sotto1.Value = Testo
            Case "1.20.02"
                S1Sotto20Sotto2.Value = Testo
            Case "1.20.03"
                S1Sotto20Sotto3.Value = Testo
            Case "1.20.04"
                S1Sotto20Sotto4.Value = Testo
            Case "1.20.05"
                S1Sotto20Sotto5.Value = Testo
            Case "1.20.06"
                S1Sotto20Sotto6.Value = Testo
            Case "1.20.07"
                S1Sotto20Sotto7.Value = Testo
            Case "1.20.08"
                S1Sotto20Sotto8.Value = Testo
            Case "1.20.09"
                S1Sotto20Sotto9.Value = Testo
            Case "1.20.10"
                S1Sotto20Sotto10.Value = Testo



            Case "2.01.01"
                S2Sotto1Sotto1.Value = Testo
            Case "2.01.02"
                S2Sotto1Sotto2.Value = Testo
            Case "2.01.03"
                S2Sotto1Sotto3.Value = Testo
            Case "2.01.04"
                S2Sotto1Sotto4.Value = Testo
            Case "2.01.05"
                S2Sotto1Sotto5.Value = Testo
            Case "2.01.06"
                S2Sotto1Sotto6.Value = Testo
            Case "2.01.07"
                S2Sotto1Sotto7.Value = Testo
            Case "2.01.08"
                S2Sotto1Sotto8.Value = Testo
            Case "2.01.09"
                S2Sotto1Sotto9.Value = Testo
            Case "2.01.10"
                S2Sotto1Sotto10.Value = Testo

            Case "2.02.01"
                S2Sotto2Sotto1.Value = Testo
            Case "2.02.02"
                S2Sotto2Sotto2.Value = Testo
            Case "2.02.03"
                S2Sotto2Sotto3.Value = Testo
            Case "2.02.04"
                S2Sotto2Sotto4.Value = Testo
            Case "2.02.05"
                S2Sotto2Sotto5.Value = Testo
            Case "2.02.06"
                S2Sotto2Sotto6.Value = Testo
            Case "2.02.07"
                S2Sotto2Sotto7.Value = Testo
            Case "2.02.08"
                S2Sotto2Sotto8.Value = Testo
            Case "2.02.09"
                S2Sotto2Sotto9.Value = Testo
            Case "2.02.10"
                S2Sotto2Sotto10.Value = Testo

            Case "2.03.01"
                S2Sotto3Sotto3.Value = Testo
            Case "2.03.02"
                S2Sotto3Sotto2.Value = Testo
            Case "2.03.03"
                S2Sotto3Sotto3.Value = Testo
            Case "2.03.04"
                S2Sotto3Sotto4.Value = Testo
            Case "2.03.05"
                S2Sotto3Sotto5.Value = Testo
            Case "2.03.06"
                S2Sotto3Sotto6.Value = Testo
            Case "2.03.07"
                S2Sotto3Sotto7.Value = Testo
            Case "2.03.08"
                S2Sotto3Sotto8.Value = Testo
            Case "2.03.09"
                S2Sotto3Sotto9.Value = Testo
            Case "2.03.10"
                S2Sotto3Sotto10.Value = Testo

            Case "2.04.01"
                S2Sotto4Sotto1.Value = Testo
            Case "2.04.02"
                S2Sotto4Sotto2.Value = Testo
            Case "2.04.03"
                S2Sotto4Sotto3.Value = Testo
            Case "2.04.04"
                S2Sotto4Sotto4.Value = Testo
            Case "2.04.05"
                S2Sotto4Sotto5.Value = Testo
            Case "2.04.06"
                S2Sotto4Sotto6.Value = Testo
            Case "2.04.07"
                S2Sotto4Sotto7.Value = Testo
            Case "2.04.08"
                S2Sotto4Sotto8.Value = Testo
            Case "2.04.09"
                S2Sotto4Sotto9.Value = Testo
            Case "2.04.10"
                S2Sotto4Sotto10.Value = Testo

            Case "2.05.01"
                S2Sotto5Sotto1.Value = Testo
            Case "2.05.02"
                S2Sotto5Sotto2.Value = Testo
            Case "2.05.03"
                S2Sotto5Sotto3.Value = Testo
            Case "2.05.04"
                S2Sotto5Sotto4.Value = Testo
            Case "2.05.05"
                S2Sotto5Sotto5.Value = Testo
            Case "2.05.06"
                S2Sotto5Sotto6.Value = Testo
            Case "2.05.07"
                S2Sotto5Sotto7.Value = Testo
            Case "2.05.08"
                S2Sotto5Sotto8.Value = Testo
            Case "2.05.09"
                S2Sotto5Sotto9.Value = Testo
            Case "2.05.10"
                S2Sotto5Sotto10.Value = Testo

            Case "2.06.01"
                S2Sotto6Sotto1.Value = Testo
            Case "2.06.02"
                S2Sotto6Sotto2.Value = Testo
            Case "2.06.03"
                S2Sotto6Sotto3.Value = Testo
            Case "2.06.04"
                S2Sotto6Sotto4.Value = Testo
            Case "2.06.05"
                S2Sotto6Sotto5.Value = Testo
            Case "2.06.06"
                S2Sotto6Sotto6.Value = Testo
            Case "2.06.07"
                S2Sotto6Sotto7.Value = Testo
            Case "2.06.08"
                S2Sotto6Sotto8.Value = Testo
            Case "2.06.09"
                S2Sotto6Sotto9.Value = Testo
            Case "2.06.10"
                S2Sotto6Sotto10.Value = Testo

            Case "2.07.01"
                S2Sotto7Sotto1.Value = Testo
            Case "2.07.02"
                S2Sotto7Sotto2.Value = Testo
            Case "2.07.03"
                S2Sotto7Sotto3.Value = Testo
            Case "2.07.04"
                S2Sotto7Sotto4.Value = Testo
            Case "2.07.05"
                S2Sotto7Sotto5.Value = Testo
            Case "2.07.06"
                S2Sotto7Sotto6.Value = Testo
            Case "2.07.07"
                S2Sotto7Sotto7.Value = Testo
            Case "2.07.08"
                S2Sotto7Sotto8.Value = Testo
            Case "2.07.09"
                S2Sotto7Sotto9.Value = Testo
            Case "2.07.10"
                S2Sotto7Sotto10.Value = Testo

            Case "2.08.01"
                S2Sotto8Sotto1.Value = Testo
            Case "2.08.02"
                S2Sotto8Sotto2.Value = Testo
            Case "2.08.03"
                S2Sotto8Sotto3.Value = Testo
            Case "2.08.04"
                S2Sotto8Sotto4.Value = Testo
            Case "2.08.05"
                S2Sotto8Sotto5.Value = Testo
            Case "2.08.06"
                S2Sotto8Sotto6.Value = Testo
            Case "2.08.07"
                S2Sotto8Sotto7.Value = Testo
            Case "2.08.08"
                S2Sotto8Sotto8.Value = Testo
            Case "2.08.09"
                S2Sotto8Sotto9.Value = Testo
            Case "2.08.10"
                S2Sotto8Sotto10.Value = Testo

            Case "2.09.01"
                S2Sotto9Sotto1.Value = Testo
            Case "2.09.02"
                S2Sotto9Sotto2.Value = Testo
            Case "2.09.03"
                S2Sotto9Sotto3.Value = Testo
            Case "2.09.04"
                S2Sotto9Sotto4.Value = Testo
            Case "2.09.05"
                S2Sotto9Sotto5.Value = Testo
            Case "2.09.06"
                S2Sotto9Sotto6.Value = Testo
            Case "2.09.07"
                S2Sotto9Sotto7.Value = Testo
            Case "2.09.08"
                S2Sotto9Sotto8.Value = Testo
            Case "2.09.09"
                S2Sotto9Sotto9.Value = Testo
            Case "2.09.10"
                S2Sotto9Sotto10.Value = Testo

            Case "2.10.01"
                S2Sotto10Sotto1.Value = Testo
            Case "2.10.02"
                S2Sotto10Sotto2.Value = Testo
            Case "2.10.03"
                S2Sotto1Sotto3.Value = Testo
            Case "2.10.04"
                S2Sotto10Sotto4.Value = Testo
            Case "2.10.05"
                S2Sotto10Sotto5.Value = Testo
            Case "2.10.06"
                S2Sotto10Sotto6.Value = Testo
            Case "2.10.07"
                S2Sotto10Sotto7.Value = Testo
            Case "2.10.08"
                S2Sotto10Sotto8.Value = Testo
            Case "2.10.09"
                S2Sotto10Sotto9.Value = Testo
            Case "2.10.10"
                S2Sotto10Sotto10.Value = Testo

            Case "2.11.01"
                S2Sotto11Sotto1.Value = Testo
            Case "2.11.02"
                S2Sotto11Sotto2.Value = Testo
            Case "2.11.03"
                S2Sotto11Sotto3.Value = Testo
            Case "2.11.04"
                S2Sotto11Sotto4.Value = Testo
            Case "2.11.05"
                S2Sotto11Sotto5.Value = Testo
            Case "2.11.06"
                S2Sotto11Sotto6.Value = Testo
            Case "2.11.07"
                S2Sotto11Sotto7.Value = Testo
            Case "2.11.08"
                S2Sotto11Sotto8.Value = Testo
            Case "2.11.09"
                S2Sotto11Sotto9.Value = Testo
            Case "2.11.10"
                S2Sotto11Sotto10.Value = Testo

            Case "2.12.01"
                S2Sotto12Sotto1.Value = Testo
            Case "2.12.02"
                S2Sotto12Sotto2.Value = Testo
            Case "2.12.03"
                S2Sotto12Sotto3.Value = Testo
            Case "2.12.04"
                S2Sotto12Sotto4.Value = Testo
            Case "2.12.05"
                S2Sotto12Sotto5.Value = Testo
            Case "2.12.06"
                S2Sotto12Sotto6.Value = Testo
            Case "2.12.07"
                S2Sotto12Sotto7.Value = Testo
            Case "2.12.08"
                S2Sotto12Sotto8.Value = Testo
            Case "2.12.09"
                S2Sotto12Sotto9.Value = Testo
            Case "2.12.10"
                S2Sotto12Sotto10.Value = Testo

            Case "2.13.01"
                S2Sotto13Sotto1.Value = Testo
            Case "2.13.02"
                S2Sotto13Sotto2.Value = Testo
            Case "2.13.03"
                S2Sotto13Sotto3.Value = Testo
            Case "2.13.04"
                S2Sotto13Sotto4.Value = Testo
            Case "2.13.05"
                S2Sotto13Sotto5.Value = Testo
            Case "2.13.06"
                S2Sotto13Sotto6.Value = Testo
            Case "2.13.07"
                S2Sotto13Sotto7.Value = Testo
            Case "2.13.08"
                S2Sotto13Sotto8.Value = Testo
            Case "2.13.09"
                S2Sotto13Sotto9.Value = Testo
            Case "2.13.10"
                S2Sotto13Sotto10.Value = Testo

            Case "2.14.01"
                S2Sotto14Sotto1.Value = Testo
            Case "2.14.02"
                S2Sotto14Sotto2.Value = Testo
            Case "2.14.03"
                S2Sotto14Sotto3.Value = Testo
            Case "2.14.04"
                S2Sotto14Sotto4.Value = Testo
            Case "2.14.05"
                S2Sotto14Sotto5.Value = Testo
            Case "2.14.06"
                S2Sotto14Sotto6.Value = Testo
            Case "2.14.07"
                S2Sotto14Sotto7.Value = Testo
            Case "2.14.08"
                S2Sotto14Sotto8.Value = Testo
            Case "2.14.09"
                S2Sotto14Sotto9.Value = Testo
            Case "2.14.10"
                S2Sotto14Sotto10.Value = Testo

            Case "2.15.01"
                S2Sotto15Sotto1.Value = Testo
            Case "2.15.02"
                S2Sotto15Sotto2.Value = Testo
            Case "2.15.03"
                S2Sotto15Sotto3.Value = Testo
            Case "2.15.04"
                S2Sotto15Sotto4.Value = Testo
            Case "2.15.05"
                S2Sotto15Sotto5.Value = Testo
            Case "2.15.06"
                S2Sotto15Sotto6.Value = Testo
            Case "2.15.07"
                S2Sotto15Sotto7.Value = Testo
            Case "2.15.08"
                S2Sotto15Sotto8.Value = Testo
            Case "2.15.09"
                S2Sotto15Sotto9.Value = Testo
            Case "2.15.10"
                S2Sotto15Sotto10.Value = Testo

            Case "2.16.01"
                S2Sotto16Sotto1.Value = Testo
            Case "2.16.02"
                S2Sotto16Sotto2.Value = Testo
            Case "2.16.03"
                S2Sotto16Sotto3.Value = Testo
            Case "2.16.04"
                S2Sotto16Sotto4.Value = Testo
            Case "2.16.05"
                S2Sotto16Sotto5.Value = Testo
            Case "2.16.06"
                S2Sotto16Sotto6.Value = Testo
            Case "2.16.07"
                S2Sotto16Sotto7.Value = Testo
            Case "2.16.08"
                S2Sotto16Sotto8.Value = Testo
            Case "2.16.09"
                S2Sotto16Sotto9.Value = Testo
            Case "2.16.10"
                S2Sotto16Sotto10.Value = Testo

            Case "2.17.01"
                S2Sotto17Sotto1.Value = Testo
            Case "2.17.02"
                S2Sotto17Sotto2.Value = Testo
            Case "2.17.03"
                S2Sotto17Sotto3.Value = Testo
            Case "2.17.04"
                S2Sotto17Sotto4.Value = Testo
            Case "2.17.05"
                S2Sotto17Sotto5.Value = Testo
            Case "2.17.06"
                S2Sotto17Sotto6.Value = Testo
            Case "2.17.07"
                S2Sotto17Sotto7.Value = Testo
            Case "2.17.08"
                S2Sotto17Sotto8.Value = Testo
            Case "2.17.09"
                S2Sotto17Sotto9.Value = Testo
            Case "2.17.10"
                S2Sotto17Sotto10.Value = Testo

            Case "2.18.01"
                S2Sotto18Sotto1.Value = Testo
            Case "2.18.02"
                S2Sotto18Sotto2.Value = Testo
            Case "2.18.03"
                S2Sotto18Sotto3.Value = Testo
            Case "2.18.04"
                S2Sotto18Sotto4.Value = Testo
            Case "2.18.05"
                S2Sotto18Sotto5.Value = Testo
            Case "2.18.06"
                S2Sotto18Sotto6.Value = Testo
            Case "2.18.07"
                S2Sotto18Sotto7.Value = Testo
            Case "2.18.08"
                S2Sotto18Sotto8.Value = Testo
            Case "2.18.09"
                S2Sotto18Sotto9.Value = Testo
            Case "2.18.10"
                S2Sotto18Sotto10.Value = Testo

            Case "2.19.01"
                S2Sotto19Sotto1.Value = Testo
            Case "2.19.02"
                S2Sotto19Sotto2.Value = Testo
            Case "2.19.03"
                S2Sotto19Sotto3.Value = Testo
            Case "2.19.04"
                S2Sotto19Sotto4.Value = Testo
            Case "2.19.05"
                S2Sotto19Sotto5.Value = Testo
            Case "2.19.06"
                S2Sotto19Sotto6.Value = Testo
            Case "2.19.07"
                S2Sotto19Sotto7.Value = Testo
            Case "2.19.08"
                S2Sotto19Sotto8.Value = Testo
            Case "2.19.09"
                S2Sotto19Sotto9.Value = Testo
            Case "2.19.10"
                S2Sotto19Sotto10.Value = Testo

            Case "2.20.01"
                S2Sotto20Sotto1.Value = Testo
            Case "2.20.02"
                S2Sotto20Sotto2.Value = Testo
            Case "2.20.03"
                S2Sotto20Sotto3.Value = Testo
            Case "2.20.04"
                S2Sotto20Sotto4.Value = Testo
            Case "2.20.05"
                S2Sotto20Sotto5.Value = Testo
            Case "2.20.06"
                S2Sotto20Sotto6.Value = Testo
            Case "2.20.07"
                S2Sotto20Sotto7.Value = Testo
            Case "2.20.08"
                S2Sotto20Sotto8.Value = Testo
            Case "2.20.09"
                S2Sotto20Sotto9.Value = Testo
            Case "2.20.10"
                S2Sotto20Sotto10.Value = Testo



            Case "3.01.01"
                S3Sotto1Sotto1.Value = Testo
            Case "3.01.02"
                S3Sotto1Sotto2.Value = Testo
            Case "3.01.03"
                S3Sotto1Sotto3.Value = Testo
            Case "3.01.04"
                S3Sotto1Sotto4.Value = Testo
            Case "3.01.05"
                S3Sotto1Sotto5.Value = Testo
            Case "3.01.06"
                S3Sotto1Sotto6.Value = Testo
            Case "3.01.07"
                S3Sotto1Sotto7.Value = Testo
            Case "3.01.08"
                S3Sotto1Sotto8.Value = Testo
            Case "3.01.09"
                S3Sotto1Sotto9.Value = Testo
            Case "3.01.10"
                S3Sotto1Sotto10.Value = Testo

            Case "3.02.01"
                S3Sotto2Sotto1.Value = Testo
            Case "3.02.02"
                S3Sotto2Sotto2.Value = Testo
            Case "3.02.03"
                S3Sotto2Sotto3.Value = Testo
            Case "3.02.04"
                S3Sotto2Sotto4.Value = Testo
            Case "3.02.05"
                S3Sotto2Sotto5.Value = Testo
            Case "3.02.06"
                S3Sotto2Sotto6.Value = Testo
            Case "3.02.07"
                S3Sotto2Sotto7.Value = Testo
            Case "3.02.08"
                S3Sotto2Sotto8.Value = Testo
            Case "3.02.09"
                S3Sotto2Sotto9.Value = Testo
            Case "3.02.10"
                S3Sotto2Sotto10.Value = Testo

            Case "3.03.01"
                S3Sotto3Sotto3.Value = Testo
            Case "3.03.02"
                S3Sotto3Sotto2.Value = Testo
            Case "3.03.03"
                S3Sotto3Sotto3.Value = Testo
            Case "3.03.04"
                S3Sotto3Sotto4.Value = Testo
            Case "3.03.05"
                S3Sotto3Sotto5.Value = Testo
            Case "3.03.06"
                S3Sotto3Sotto6.Value = Testo
            Case "3.03.07"
                S3Sotto3Sotto7.Value = Testo
            Case "3.03.08"
                S3Sotto3Sotto8.Value = Testo
            Case "3.03.09"
                S3Sotto3Sotto9.Value = Testo
            Case "3.03.10"
                S3Sotto3Sotto10.Value = Testo

            Case "3.04.01"
                S3Sotto4Sotto1.Value = Testo
            Case "3.04.02"
                S3Sotto4Sotto2.Value = Testo
            Case "3.04.03"
                S3Sotto4Sotto3.Value = Testo
            Case "3.04.04"
                S3Sotto4Sotto4.Value = Testo
            Case "3.04.05"
                S3Sotto4Sotto5.Value = Testo
            Case "3.04.06"
                S3Sotto4Sotto6.Value = Testo
            Case "3.04.07"
                S3Sotto4Sotto7.Value = Testo
            Case "3.04.08"
                S3Sotto4Sotto8.Value = Testo
            Case "3.04.09"
                S3Sotto4Sotto9.Value = Testo
            Case "3.04.10"
                S3Sotto4Sotto10.Value = Testo

            Case "3.05.01"
                S3Sotto5Sotto1.Value = Testo
            Case "3.05.02"
                S3Sotto5Sotto2.Value = Testo
            Case "3.05.03"
                S3Sotto5Sotto3.Value = Testo
            Case "3.05.04"
                S3Sotto5Sotto4.Value = Testo
            Case "3.05.05"
                S3Sotto5Sotto5.Value = Testo
            Case "3.05.06"
                S3Sotto5Sotto6.Value = Testo
            Case "3.05.07"
                S3Sotto5Sotto7.Value = Testo
            Case "3.05.08"
                S3Sotto5Sotto8.Value = Testo
            Case "3.05.09"
                S3Sotto5Sotto9.Value = Testo
            Case "3.05.10"
                S3Sotto5Sotto10.Value = Testo



            Case "4.01.01"
                S4Sotto1Sotto1.Value = Testo
            Case "4.01.02"
                S4Sotto1Sotto2.Value = Testo
            Case "4.01.03"
                S4Sotto1Sotto3.Value = Testo
            Case "4.01.04"
                S4Sotto1Sotto4.Value = Testo
            Case "4.01.05"
                S4Sotto1Sotto5.Value = Testo
            Case "4.01.06"
                S4Sotto1Sotto6.Value = Testo
            Case "4.01.07"
                S4Sotto1Sotto7.Value = Testo
            Case "4.01.08"
                S4Sotto1Sotto8.Value = Testo
            Case "4.01.09"
                S4Sotto1Sotto9.Value = Testo
            Case "4.01.10"
                S4Sotto1Sotto10.Value = Testo

            Case "4.02.01"
                S4Sotto2Sotto1.Value = Testo
            Case "4.02.02"
                S4Sotto2Sotto2.Value = Testo
            Case "4.02.03"
                S4Sotto2Sotto3.Value = Testo
            Case "4.02.04"
                S4Sotto2Sotto4.Value = Testo
            Case "4.02.05"
                S4Sotto2Sotto5.Value = Testo
            Case "4.02.06"
                S4Sotto2Sotto6.Value = Testo
            Case "4.02.07"
                S4Sotto2Sotto7.Value = Testo
            Case "4.02.08"
                S4Sotto2Sotto8.Value = Testo
            Case "4.02.09"
                S4Sotto2Sotto9.Value = Testo
            Case "4.02.10"
                S4Sotto2Sotto10.Value = Testo

            Case "4.03.01"
                S4Sotto3Sotto3.Value = Testo
            Case "4.03.02"
                S4Sotto3Sotto2.Value = Testo
            Case "4.03.03"
                S4Sotto3Sotto3.Value = Testo
            Case "4.03.04"
                S4Sotto3Sotto4.Value = Testo
            Case "4.03.05"
                S4Sotto3Sotto5.Value = Testo
            Case "4.03.06"
                S4Sotto3Sotto6.Value = Testo
            Case "4.03.07"
                S4Sotto3Sotto7.Value = Testo
            Case "4.03.08"
                S4Sotto3Sotto8.Value = Testo
            Case "4.03.09"
                S4Sotto3Sotto9.Value = Testo
            Case "4.03.10"
                S4Sotto3Sotto10.Value = Testo

            Case "4.04.01"
                S4Sotto4Sotto1.Value = Testo
            Case "4.04.02"
                S4Sotto4Sotto2.Value = Testo
            Case "4.04.03"
                S4Sotto4Sotto3.Value = Testo
            Case "4.04.04"
                S4Sotto4Sotto4.Value = Testo
            Case "4.04.05"
                S4Sotto4Sotto5.Value = Testo
            Case "4.04.06"
                S4Sotto4Sotto6.Value = Testo
            Case "4.04.07"
                S4Sotto4Sotto7.Value = Testo
            Case "4.04.08"
                S4Sotto4Sotto8.Value = Testo
            Case "4.04.09"
                S4Sotto4Sotto9.Value = Testo
            Case "4.04.10"
                S4Sotto4Sotto10.Value = Testo

            Case "4.05.01"
                S4Sotto5Sotto1.Value = Testo
            Case "4.05.02"
                S4Sotto5Sotto2.Value = Testo
            Case "4.05.03"
                S4Sotto5Sotto3.Value = Testo
            Case "4.05.04"
                S4Sotto5Sotto4.Value = Testo
            Case "4.05.05"
                S4Sotto5Sotto5.Value = Testo
            Case "4.05.06"
                S4Sotto5Sotto6.Value = Testo
            Case "4.05.07"
                S4Sotto5Sotto7.Value = Testo
            Case "4.05.08"
                S4Sotto5Sotto8.Value = Testo
            Case "4.05.09"
                S4Sotto5Sotto9.Value = Testo
            Case "4.05.10"
                S4Sotto5Sotto10.Value = Testo


        End Select
    End Function


    Function CaricaValori(ByVal indice As Integer, ByVal DescrizioneVoce As TextBox, ByVal Codice As TextBox, ByVal Id As HiddenField, ByVal SottoVoce As HiddenField)
        Dim Valore As String = ""
        Dim ValoreSottoVoce As String = ""

        Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader
        par.cmd.CommandText = "select * from SISCOM_MI.PF_VOCI where id_voce_madre is null and id_piano_finanziario=" & pianoF.Value & " AND indice=" & indice
        myReader11 = par.cmd.ExecuteReader()
        If myReader11.Read Then
            DescrizioneVoce.Text = par.IfNull(myReader11("descrizione"), "")
            DescrizioneVoce.Attributes.Add("ID", par.IfNull(myReader11("id"), -1))
            Codice.Text = par.IfNull(myReader11("codice"), "")
            Codice.Attributes.Add("ID_CAPITOLO", par.IfNull(myReader11("ID_CAPITOLO"), "NULL"))
            Id.Value = CaricaOpDb(par.IfNull(myReader11("id"), -1))

            'metto sotto voci 1 livello
            Dim myReader12 As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = "select * from SISCOM_MI.PF_VOCI where id_voce_madre =" & par.IfNull(myReader11("id"), -1) & " order by codice asc"
            myReader12 = par.cmd.ExecuteReader()
            Do While myReader12.Read
                Valore = Valore & par.IfNull(myReader12("descrizione"), "") & "#"

                'metto sotto sotto voci 2 livello
                Dim myReader13 As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "select * from SISCOM_MI.PF_VOCI where id_voce_madre =" & par.IfNull(myReader12("id"), -1) & " order by codice asc"
                myReader13 = par.cmd.ExecuteReader()
                Do While myReader13.Read
                    ValoreSottoVoce = ValoreSottoVoce & par.IfNull(myReader13("descrizione"), "") & "#"
                Loop
                myReader13.Close()
                If ValoreSottoVoce <> "" Then
                    ValoreSottoVoce = Mid(ValoreSottoVoce, 1, Len(ValoreSottoVoce) - 1)
                End If
                ValorizzaSottoSottoVoci(ValoreSottoVoce, myReader12("codice"))
                ValoreSottoVoce = ""
            Loop
            myReader12.Close()
            If Valore <> "" Then
                Valore = Mid(Valore, 1, Len(Valore) - 1)
            End If
            SottoVoce.Value = Valore
        End If
        myReader11.Close()
    End Function

    Function NuovoPiano()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Label1.Text = par.DeCripta(Request.QueryString("PERIODO"))
            per.Value = Label1.Text
            idEF.Value = par.DeCripta(Request.QueryString("ID"))
            lblStato.Text = "STATO:NUOVO"

            dlist = ListaOperatori

            da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID,COGNOME||' '||NOME AS ""DESCRIZIONE"" FROM OPERATORI WHERE MOD_CICLO_P=1 AND BP_COMPILAZIONE=1 and id_ufficio<>0 and COGNOME IS NOT NULL AND ID_CAF=2 and fl_eliminato='0' ORDER BY COGNOME ASC,NOME ASC", par.OracleConn)
            da.Fill(ds)

            dlist.Items.Clear()
            dlist.DataSource = ds
            dlist.DataTextField = "DESCRIZIONE"
            dlist.DataValueField = "ID"
            dlist.DataBind()

            da.Dispose()
            da = Nothing

            dlist.DataSource = Nothing
            dlist = Nothing

            ds.Clear()
            ds.Dispose()
            ds = Nothing


            If Request.QueryString("S") = "1" Then
                CaricaCaselle(1, S1txtVoce1, S1txtCod1, S1voce1)
                CaricaCaselle(2, S1txtVoce2, S1txtCod2, S1voce2)




                CaricaCaselle(7, S2txtVoce1, S2txtCod1, S2voce1)
                CaricaCaselle(8, S2txtVoce2, S2txtCod2, S2voce2)
                CaricaCaselle(9, S2txtVoce3, S2txtCod3, S2voce3)
                CaricaCaselle(10, S2txtVoce4, S2txtCod4, S2voce4)




                CaricaCaselle(11, S3txtVoce1, S3txtCod1, S3voce1)
                CaricaCaselle(12, S3txtVoce2, S3txtCod2, S3voce2)


                S1Sotto2.Value = "Imposta di registro contratti di locazione#Imposta di bollo contratti di locazione#Rimborso agli inquilini per danni#Altre Spese#Spese legali per recupero morosità#Altre spese legali#Interessi su depositi cauzionali e interessi vari#Sfratti ed escomi, traslochi e deposito masserizie#Gestione amministrazione condomini (compreso MO parti comuni e spese all.sfitti)#Gestione amministrazione autogestioni"
                S2Sotto2.Value = "Servizi generali (90% portierato, pulizie, verde, energia elettrica, etc#Riscaldamento e acqua calda#Ascensori#Varie (imposta di bollo a carico inquilini)#Conguaglio oneri accessori#Rettifica per alloggi sfitti a carico della proprietà#Morosità alloggi in condominio e autogestioni"
                S2Sotto3.Value = "Portierato 10%#Oneri accessori per alloggi sfitti"
                S2Sotto4.Value = "Manutenzione programmata#Manutenzione ordinaria (del tipo riparativo)#Pronto intervento#Manutenzione straordinaria"
            End If


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Function Scegli()
        Select Case casella.Value
            Case "S1voce1", "S1voce2", "S1voce3", "S1voce4", "S1voce5", "S1voce6", "S1voce7", "S1voce8", "S1voce9", "S1voce10", "S1voce11", "S1voce12", "S1voce13", "S1voce14", "S1voce15", "S1voce16", "S1voce17", "S1voce18", "S1voce19", "S1voce20"
                casella.Value = ""
                apri.Value = "SEZ1"
            Case "S2voce1", "S2voce2", "S2voce3", "S2voce4", "S2voce5", "S1voce6", "S2voce7", "S2voce8", "S2voce9", "S2voce10", "S2voce11", "S2voce12", "S2voce13", "S2voce14", "S2voce15", "S2voce16", "S2voce17", "S2voce18", "S2voce19", "S2voce20"
                casella.Value = ""
                apri.Value = "SEZ2"

            Case "S3voce1", "S3voce2", "S3voce3", "S3voce4""S3voce5"

                casella.Value = ""
                apri.Value = "SEZ3"

            Case "S4voce1", "S4voce2", "S4voce3", "S4voce4", "S4voce5"

                casella.Value = ""
                apri.Value = "SEZ4"

        End Select
    End Function

    Function Operatori(ByVal testo As String)
        Dim pos As Integer
        Dim Valore1 As String

        pos = 1
        Valore1 = ""
        Do While pos <= Len(testo)
            If Mid(testo, pos, 1) <> "#" Then
                Valore1 = Valore1 & Mid(testo, pos, 1)
            Else
                ReDim Preserve ElencoOperatori(kk)
                ElencoOperatori(kk) = Valore1
                kk = kk + 1
                Valore1 = ""
            End If
            pos = pos + 1
        Loop
        pos = pos + 1
    End Function

    Public Property kk() As Long
        Get
            If Not (ViewState("par_kk") Is Nothing) Then
                Return CLng(ViewState("par_kk"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_kk") = value
        End Set
    End Property



    Function InserisciValore(ByVal PianoF As Long, ByVal DescrizioneVoce As TextBox, ByVal Codice As TextBox, ByVal indice As HiddenField, ByVal progressivo As Integer, ByVal SottoVoci As HiddenField, ByVal SottoSottoVoce1 As HiddenField, ByVal SottoSottoVoce2 As HiddenField, ByVal SottoSottoVoce3 As HiddenField, ByVal SottoSottoVoce4 As HiddenField, ByVal SottoSottoVoce5 As HiddenField, ByVal SottoSottoVoce6 As HiddenField, ByVal SottoSottoVoce7 As HiddenField, ByVal SottoSottoVoce8 As HiddenField, ByVal SottoSottoVoce9 As HiddenField, ByVal SottoSottoVoce10 As HiddenField)
        Dim s As Long = 0
        Dim indice7 As Long = 0
        Dim IndiceSottoVoce As Long = 0


        If DirectCast(DescrizioneVoce, TextBox).Attributes("ID").ToUpper.ToString = "-1" Then
            If par.IfEmpty(DescrizioneVoce.Text, "") <> "" Then
                par.cmd.CommandText = "insert into siscom_mi.PF_VOCI (id,id_piano_finanziario,codice,descrizione," _
                                    & "id_voce_madre,INDICE,ID_CAPITOLO) values (siscom_mi.seq_PF_VOCI.nextval," & PianoF _
                                    & ",'" & par.PulisciStrSql(Codice.Text) & "','" _
                                    & par.PulisciStrSql(DescrizioneVoce.Text) & "',null," & progressivo & "," & DirectCast(Codice, TextBox).Attributes("ID_CAPITOLO").ToUpper.ToString & ")"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "select siscom_mi.SEQ_PF_VOCI.currval from dual"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    indice7 = myReaderA(0)
                    Operatori(indice.Value)
                    For s = 0 To kk - 1
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_VOCI_OPERATORI (id_voce,id_operatore) values (" & myReaderA(0) & "," & ElencoOperatori(s) & ")"
                        par.cmd.ExecuteNonQuery()
                    Next
                    kk = 0

                    DescrizioneVoce.Attributes.Add("ID", myReaderA(0))
                End If
                myReaderA.Close()

                Dim Valori() As String
                Dim seps() As Char = {"#"}
                Dim i As Long = 0
                Dim j As Integer = 1

                Dim SottoValori() As String
                Dim i1 As Long = 0
                Dim j1 As Integer = 1

                Valori = SottoVoci.Value.Split(seps)
                For i = 0 To Valori.Length - 1
                    If Valori(i) <> "" Then
                        par.cmd.CommandText = "insert into siscom_mi.PF_VOCI (id,id_piano_finanziario,codice,descrizione," _
                            & "id_voce_madre,INDICE,ID_CAPITOLO) values (siscom_mi.seq_PF_VOCI.nextval," & PianoF _
                            & ",'" & Codice.Text & "." & Format(j, "00") & "','" _
                            & par.PulisciStrSql(Valori(i)) & "'," & indice7 & "," & j & "," & DirectCast(Codice, TextBox).Attributes("ID_CAPITOLO").ToUpper.ToString & ")"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select siscom_mi.SEQ_PF_VOCI.currval from dual"
                        Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderB.Read Then
                            IndiceSottoVoce = myReaderB(0)
                        End If
                        myReaderB.Close()
                        i1 = 0
                        j1 = 1

                        Select Case i
                            Case 0
                                SottoValori = SottoSottoVoce1.Value.Split(seps)
                            Case 1
                                SottoValori = SottoSottoVoce2.Value.Split(seps)
                            Case 2
                                SottoValori = SottoSottoVoce3.Value.Split(seps)
                            Case 3
                                SottoValori = SottoSottoVoce4.Value.Split(seps)
                            Case 4
                                SottoValori = SottoSottoVoce5.Value.Split(seps)
                            Case 5
                                SottoValori = SottoSottoVoce6.Value.Split(seps)
                            Case 6
                                SottoValori = SottoSottoVoce7.Value.Split(seps)
                            Case 7
                                SottoValori = SottoSottoVoce8.Value.Split(seps)
                            Case 8
                                SottoValori = SottoSottoVoce9.Value.Split(seps)
                            Case 9
                                SottoValori = SottoSottoVoce10.Value.Split(seps)
                        End Select

                        For i1 = 0 To SottoValori.Length - 1
                            If SottoValori(i1) <> "" Then
                                par.cmd.CommandText = "insert into siscom_mi.PF_VOCI (id,id_piano_finanziario,codice,descrizione," _
                                                    & "id_voce_madre,INDICE,ID_CAPITOLO) values (siscom_mi.seq_PF_VOCI.nextval," & PianoF _
                                                    & ",'" & Codice.Text & "." & Format(j, "00") & "." & Format(j1, "00") & "','" _
                                                    & par.PulisciStrSql(SottoValori(i1)) & "'," & IndiceSottoVoce & "," & j1 & "," & DirectCast(Codice, TextBox).Attributes("ID_CAPITOLO").ToUpper.ToString & ")"
                                par.cmd.ExecuteNonQuery()
                                j1 = j1 + 1
                            End If
                        Next




                        j = j + 1
                    End If
                Next




                ReDim ElencoOperatori(0)

            End If
        Else
            If par.IfEmpty(DescrizioneVoce.Text, "") <> "" Then
                par.cmd.CommandText = "UPDATE siscom_mi.PF_VOCI set descrizione='" & par.PulisciStrSql(DescrizioneVoce.Text) _
                                    & "',codice='" & par.PulisciStrSql(Codice.Text) _
                                    & "',ID_CAPITOLO=" & DirectCast(Codice, TextBox).Attributes("ID_CAPITOLO").ToUpper.ToString & " where id=" & DirectCast(DescrizioneVoce, TextBox).Attributes("ID").ToUpper.ToString
                par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "delete from siscom_mi.pf_voci_operatori where id_voce=" & DirectCast(DescrizioneVoce, TextBox).Attributes("ID").ToUpper.ToString
                'par.cmd.ExecuteNonQuery()

                Operatori(indice.Value)
                For s = 0 To kk - 1

                    par.cmd.CommandText = "select * from SISCOM_MI.PF_VOCI_OPERATORI where id_voce=" & DirectCast(DescrizioneVoce, TextBox).Attributes("ID").ToUpper.ToString & " and id_operatore=" & ElencoOperatori(s)
                    Dim myReader111 As Oracle.DataAccess.Client.OracleDataReader
                    myReader111 = par.cmd.ExecuteReader()
                    If myReader111.HasRows = False Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_VOCI_OPERATORI (id_voce,id_operatore) values (" & DirectCast(DescrizioneVoce, TextBox).Attributes("ID").ToUpper.ToString & "," & ElencoOperatori(s) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReader111.Close()
                Next
                kk = 0
                ReDim ElencoOperatori(0)

                par.cmd.CommandText = "delete from siscom_mi.pf_voci where id_voce_madre=" & DirectCast(DescrizioneVoce, TextBox).Attributes("ID").ToUpper.ToString
                par.cmd.ExecuteNonQuery()

                Dim Valori() As String
                Dim seps() As Char = {"#"}
                Dim i As Long = 0
                Dim j As Integer = 1

                Dim SottoValori() As String
                Dim i1 As Long = 0
                Dim j1 As Integer = 1

                Valori = SottoVoci.Value.Split(seps)
                For i = 0 To Valori.Length - 1
                    If Valori(i) <> "" Then
                        par.cmd.CommandText = "insert into siscom_mi.PF_VOCI (id,id_piano_finanziario,codice,descrizione," _
                            & "id_voce_madre,INDICE,ID_CAPITOLO) values (siscom_mi.seq_PF_VOCI.nextval," & PianoF _
                            & ",'" & Codice.Text & "." & Format(j, "00") & "','" _
                            & par.PulisciStrSql(Valori(i)) & "'," & DirectCast(DescrizioneVoce, TextBox).Attributes("ID").ToUpper.ToString & "," & j & "," & DirectCast(Codice, TextBox).Attributes("ID_CAPITOLO").ToUpper.ToString & ")"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select siscom_mi.SEQ_PF_VOCI.currval from dual"
                        Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderB.Read Then
                            IndiceSottoVoce = myReaderB(0)
                        End If
                        myReaderB.Close()
                        i1 = 0
                        j1 = 1

                        Select Case i
                            Case 0
                                SottoValori = SottoSottoVoce1.Value.Split(seps)
                            Case 1
                                SottoValori = SottoSottoVoce2.Value.Split(seps)
                            Case 2
                                SottoValori = SottoSottoVoce3.Value.Split(seps)
                            Case 3
                                SottoValori = SottoSottoVoce4.Value.Split(seps)
                            Case 4
                                SottoValori = SottoSottoVoce5.Value.Split(seps)
                            Case 5
                                SottoValori = SottoSottoVoce6.Value.Split(seps)
                            Case 6
                                SottoValori = SottoSottoVoce7.Value.Split(seps)
                            Case 7
                                SottoValori = SottoSottoVoce8.Value.Split(seps)
                            Case 8
                                SottoValori = SottoSottoVoce9.Value.Split(seps)
                            Case 9
                                SottoValori = SottoSottoVoce10.Value.Split(seps)
                        End Select

                        For i1 = 0 To SottoValori.Length - 1
                            If SottoValori(i1) <> "" Then
                                par.cmd.CommandText = "insert into siscom_mi.PF_VOCI (id,id_piano_finanziario,codice,descrizione," _
                                                    & "id_voce_madre,INDICE,ID_CAPITOLO) values (siscom_mi.seq_PF_VOCI.nextval," & PianoF _
                                                    & ",'" & Codice.Text & "." & Format(j, "00") & "." & Format(j1, "00") & "','" _
                                                    & par.PulisciStrSql(SottoValori(i1)) & "'," & IndiceSottoVoce & "," & j1 & "," & DirectCast(Codice, TextBox).Attributes("ID_CAPITOLO").ToUpper.ToString & ")"
                                par.cmd.ExecuteNonQuery()
                                j1 = j1 + 1
                            End If
                        Next




                        j = j + 1
                    End If
                Next

                'Dim Valori() As String
                'Dim seps() As Char = {"#"}
                'Dim i As Long = 0
                'Dim j As Integer = 1

                'Valori = SottoVoci.Value.Split(seps)
                'For i = 0 To Valori.Length - 1
                '    If Valori(i) <> "" Then
                '        par.cmd.CommandText = "insert into siscom_mi.PF_VOCI (id,id_piano_finanziario,codice,descrizione," _
                '            & "id_voce_madre,INDICE,ID_CAPITOLO) values (siscom_mi.seq_PF_VOCI.nextval," & PianoF _
                '            & ",'" & Codice.Text & "." & Format(j, "00") & "','" _
                '            & par.PulisciStrSql(Valori(i)) & "'," & DirectCast(DescrizioneVoce, TextBox).Attributes("ID").ToUpper.ToString & "," & j & "," & DirectCast(Codice, TextBox).Attributes("ID_CAPITOLO").ToUpper.ToString & ")"
                '        par.cmd.ExecuteNonQuery()
                '        j = j + 1
                '    End If
                'Next

            Else
                par.cmd.CommandText = "delete from siscom_mi.pf_voci where id=" & DirectCast(DescrizioneVoce, TextBox).Attributes("ID").ToUpper.ToString
                par.cmd.ExecuteNonQuery()

            End If
        End If
    End Function


    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        If salvaok.Value = "1" Then
            Try
                Dim idPiano As Long = 0
                Dim i As Integer = 0
                Dim s As Long = 0
                Dim NuovoPF As Boolean = False


                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                If pianoF.Value = "-1" Then
                    NuovoPF = True
                Else
                    idPiano = pianoF.Value
                End If


                If NuovoPF = True Then

                    par.cmd.CommandText = "insert into siscom_mi.PF_MAIN (id,id_esercizio_finanziario,ID_STATO) values (siscom_mi.seq_PF_MAIN.nextval," & idEF.Value & ",0)"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "select siscom_mi.seq_PF_MAIN.currval from dual"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        idPiano = myReader(0)
                        pianoF.Value = idPiano
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_STRUTTURA) " _
                                                        & "VALUES (" & pianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                                       & "'F81','',-1)"
                    par.cmd.ExecuteNonQuery()

                Else
                    If Modificato.Value = "1" Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_STRUTTURA) " _
                                            & "VALUES (" & pianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                           & "'F02',''," & Session.Item("ID_STRUTTURA") & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                End If

                par.cmd.CommandText = "delete from siscom_mi.pf_voci_struttura where id_voce in (select id from siscom_mi.pf_voci where id_piano_finanziario=" & idPiano & ")"
                par.cmd.ExecuteNonQuery()

                InserisciValore(idPiano, S1txtVoce1, S1txtCod1, S1Op1, 1, S1Sotto1, S1Sotto1Sotto1, S1Sotto1Sotto2, S1Sotto1Sotto3, S1Sotto1Sotto4, S1Sotto1Sotto5, S1Sotto1Sotto6, S1Sotto1Sotto7, S1Sotto1Sotto8, S1Sotto1Sotto9, S1Sotto1Sotto10)
                InserisciValore(idPiano, S1txtVoce2, S1txtCod2, S1Op2, 2, S1Sotto2, S1Sotto2Sotto1, S1Sotto2Sotto2, S1Sotto2Sotto3, S1Sotto2Sotto4, S1Sotto2Sotto5, S1Sotto2Sotto6, S1Sotto2Sotto7, S1Sotto2Sotto8, S1Sotto2Sotto9, S1Sotto2Sotto10)
                InserisciValore(idPiano, S1txtVoce3, S1txtCod3, S1Op3, 3, S1Sotto3, S1Sotto3Sotto1, S1Sotto3Sotto2, S1Sotto3Sotto3, S1Sotto3Sotto4, S1Sotto3Sotto5, S1Sotto3Sotto6, S1Sotto3Sotto7, S1Sotto3Sotto8, S1Sotto3Sotto9, S1Sotto3Sotto10)
                InserisciValore(idPiano, S1txtVoce4, S1txtCod4, S1Op4, 4, S1Sotto4, S1Sotto4Sotto1, S1Sotto4Sotto2, S1Sotto4Sotto3, S1Sotto4Sotto4, S1Sotto4Sotto5, S1Sotto4Sotto6, S1Sotto4Sotto7, S1Sotto4Sotto8, S1Sotto4Sotto9, S1Sotto4Sotto10)
                InserisciValore(idPiano, S1txtVoce5, S1txtCod5, S1Op5, 5, S1Sotto5, S1Sotto5Sotto1, S1Sotto5Sotto2, S1Sotto5Sotto3, S1Sotto5Sotto4, S1Sotto5Sotto5, S1Sotto5Sotto6, S1Sotto5Sotto7, S1Sotto5Sotto8, S1Sotto5Sotto9, S1Sotto5Sotto10)
                InserisciValore(idPiano, S1txtVoce6, S1txtCod6, S1Op6, 6, S1Sotto6, S1Sotto6Sotto1, S1Sotto6Sotto2, S1Sotto6Sotto3, S1Sotto6Sotto4, S1Sotto6Sotto5, S1Sotto6Sotto6, S1Sotto6Sotto7, S1Sotto6Sotto8, S1Sotto6Sotto9, S1Sotto6Sotto10)
                InserisciValore(idPiano, S1txtVoce7, S1txtCod7, S1Op7, 7, S1Sotto7, S1Sotto7Sotto1, S1Sotto7Sotto2, S1Sotto7Sotto3, S1Sotto7Sotto4, S1Sotto7Sotto5, S1Sotto7Sotto6, S1Sotto7Sotto7, S1Sotto7Sotto8, S1Sotto7Sotto9, S1Sotto7Sotto10)
                InserisciValore(idPiano, S1txtVoce8, S1txtCod8, S1Op8, 8, S1Sotto8, S1Sotto8Sotto1, S1Sotto8Sotto2, S1Sotto8Sotto3, S1Sotto8Sotto4, S1Sotto8Sotto5, S1Sotto8Sotto6, S1Sotto8Sotto7, S1Sotto8Sotto8, S1Sotto8Sotto9, S1Sotto8Sotto10)
                InserisciValore(idPiano, S1txtVoce9, S1txtCod9, S1Op9, 9, S1Sotto9, S1Sotto9Sotto1, S1Sotto9Sotto2, S1Sotto9Sotto3, S1Sotto9Sotto4, S1Sotto9Sotto5, S1Sotto9Sotto6, S1Sotto9Sotto7, S1Sotto9Sotto8, S1Sotto9Sotto9, S1Sotto9Sotto10)
                InserisciValore(idPiano, S1txtVoce10, S1txtCod10, S1Op10, 10, S1Sotto10, S1Sotto10Sotto1, S1Sotto10Sotto2, S1Sotto1Sotto3, S1Sotto10Sotto4, S1Sotto10Sotto5, S1Sotto10Sotto6, S1Sotto10Sotto7, S1Sotto10Sotto8, S1Sotto10Sotto9, S1Sotto10Sotto10)
                InserisciValore(idPiano, S1txtVoce11, S1txtCod11, S1Op11, 11, S1Sotto11, S1Sotto11Sotto1, S1Sotto11Sotto2, S1Sotto1Sotto3, S1Sotto11Sotto4, S1Sotto11Sotto5, S1Sotto11Sotto6, S1Sotto11Sotto7, S1Sotto11Sotto8, S1Sotto11Sotto9, S1Sotto11Sotto10)
                InserisciValore(idPiano, S1txtVoce12, S1txtCod12, S1Op12, 12, S1Sotto12, S1Sotto12Sotto1, S1Sotto12Sotto2, S1Sotto1Sotto3, S1Sotto12Sotto4, S1Sotto12Sotto5, S1Sotto12Sotto6, S1Sotto12Sotto7, S1Sotto12Sotto8, S1Sotto12Sotto9, S1Sotto12Sotto10)
                InserisciValore(idPiano, S1txtVoce13, S1txtCod13, S1Op13, 13, S1Sotto13, S1Sotto13Sotto1, S1Sotto13Sotto2, S1Sotto1Sotto3, S1Sotto13Sotto4, S1Sotto13Sotto5, S1Sotto13Sotto6, S1Sotto13Sotto7, S1Sotto13Sotto8, S1Sotto13Sotto9, S1Sotto13Sotto10)
                InserisciValore(idPiano, S1txtVoce14, S1txtCod14, S1Op14, 14, S1Sotto14, S1Sotto14Sotto1, S1Sotto14Sotto2, S1Sotto1Sotto3, S1Sotto14Sotto4, S1Sotto14Sotto5, S1Sotto14Sotto6, S1Sotto14Sotto7, S1Sotto14Sotto8, S1Sotto14Sotto9, S1Sotto14Sotto10)
                InserisciValore(idPiano, S1txtVoce15, S1txtCod15, S1Op15, 15, S1Sotto15, S1Sotto15Sotto1, S1Sotto15Sotto2, S1Sotto1Sotto3, S1Sotto15Sotto4, S1Sotto15Sotto5, S1Sotto15Sotto6, S1Sotto15Sotto7, S1Sotto15Sotto8, S1Sotto15Sotto9, S1Sotto15Sotto10)
                InserisciValore(idPiano, S1txtVoce16, S1txtCod16, S1Op16, 16, S1Sotto16, S1Sotto16Sotto1, S1Sotto16Sotto2, S1Sotto1Sotto3, S1Sotto16Sotto4, S1Sotto16Sotto5, S1Sotto16Sotto6, S1Sotto16Sotto7, S1Sotto16Sotto8, S1Sotto16Sotto9, S1Sotto16Sotto10)
                InserisciValore(idPiano, S1txtVoce17, S1txtCod17, S1Op17, 17, S1Sotto17, S1Sotto17Sotto1, S1Sotto17Sotto2, S1Sotto1Sotto3, S1Sotto17Sotto4, S1Sotto17Sotto5, S1Sotto17Sotto6, S1Sotto17Sotto7, S1Sotto17Sotto8, S1Sotto17Sotto9, S1Sotto17Sotto10)
                InserisciValore(idPiano, S1txtVoce18, S1txtCod18, S1Op18, 18, S1Sotto18, S1Sotto18Sotto1, S1Sotto18Sotto2, S1Sotto1Sotto3, S1Sotto18Sotto4, S1Sotto18Sotto5, S1Sotto18Sotto6, S1Sotto18Sotto7, S1Sotto18Sotto8, S1Sotto18Sotto9, S1Sotto18Sotto10)
                InserisciValore(idPiano, S1txtVoce19, S1txtCod19, S1Op19, 19, S1Sotto19, S1Sotto19Sotto1, S1Sotto19Sotto2, S1Sotto1Sotto3, S1Sotto19Sotto4, S1Sotto19Sotto5, S1Sotto19Sotto6, S1Sotto19Sotto7, S1Sotto19Sotto8, S1Sotto19Sotto9, S1Sotto19Sotto10)
                InserisciValore(idPiano, S1txtVoce20, S1txtCod20, S1Op20, 20, S1Sotto20, S1Sotto20Sotto1, S1Sotto20Sotto2, S1Sotto1Sotto3, S1Sotto20Sotto4, S1Sotto20Sotto5, S1Sotto20Sotto6, S1Sotto20Sotto7, S1Sotto20Sotto8, S1Sotto20Sotto9, S1Sotto20Sotto10)

                InserisciValore(idPiano, S2txtVoce1, S2txtCod1, S2Op1, 21, S2Sotto1, S2Sotto1Sotto1, S2Sotto1Sotto2, S2Sotto1Sotto3, S2Sotto1Sotto4, S2Sotto1Sotto5, S2Sotto1Sotto6, S2Sotto1Sotto7, S2Sotto1Sotto8, S2Sotto1Sotto9, S2Sotto1Sotto10)
                InserisciValore(idPiano, S2txtVoce2, S2txtCod2, S2Op2, 22, S2Sotto2, S2Sotto2Sotto1, S2Sotto2Sotto2, S2Sotto2Sotto3, S2Sotto2Sotto4, S2Sotto2Sotto5, S2Sotto2Sotto6, S2Sotto2Sotto7, S2Sotto2Sotto8, S2Sotto2Sotto9, S2Sotto2Sotto10)
                InserisciValore(idPiano, S2txtVoce3, S2txtCod3, S2Op3, 23, S2Sotto3, S2Sotto3Sotto1, S2Sotto3Sotto2, S2Sotto3Sotto3, S2Sotto3Sotto4, S2Sotto3Sotto5, S2Sotto3Sotto6, S2Sotto3Sotto7, S2Sotto3Sotto8, S2Sotto3Sotto9, S2Sotto3Sotto10)
                InserisciValore(idPiano, S2txtVoce4, S2txtCod4, S2Op4, 24, S2Sotto4, S2Sotto4Sotto1, S2Sotto4Sotto2, S2Sotto4Sotto3, S2Sotto4Sotto4, S2Sotto4Sotto5, S2Sotto4Sotto6, S2Sotto4Sotto7, S2Sotto4Sotto8, S2Sotto4Sotto9, S2Sotto4Sotto10)
                InserisciValore(idPiano, S2txtVoce5, S2txtCod5, S2Op5, 25, S2Sotto5, S2Sotto5Sotto1, S2Sotto5Sotto2, S2Sotto5Sotto3, S2Sotto5Sotto4, S2Sotto5Sotto5, S2Sotto5Sotto6, S2Sotto5Sotto7, S2Sotto5Sotto8, S2Sotto5Sotto9, S2Sotto5Sotto10)
                InserisciValore(idPiano, S2txtVoce6, S2txtCod6, S2Op6, 26, S2Sotto6, S2Sotto6Sotto1, S2Sotto6Sotto2, S2Sotto6Sotto3, S2Sotto6Sotto4, S2Sotto6Sotto5, S2Sotto6Sotto6, S2Sotto6Sotto7, S2Sotto6Sotto8, S2Sotto6Sotto9, S2Sotto6Sotto10)
                InserisciValore(idPiano, S2txtVoce7, S2txtCod7, S2Op7, 27, S2Sotto7, S2Sotto7Sotto1, S2Sotto7Sotto2, S2Sotto7Sotto3, S2Sotto7Sotto4, S2Sotto7Sotto5, S2Sotto7Sotto6, S2Sotto7Sotto7, S2Sotto7Sotto8, S2Sotto7Sotto9, S2Sotto7Sotto10)
                InserisciValore(idPiano, S2txtVoce8, S2txtCod8, S2Op8, 28, S2Sotto8, S2Sotto8Sotto1, S2Sotto8Sotto2, S2Sotto8Sotto3, S2Sotto8Sotto4, S2Sotto8Sotto5, S2Sotto8Sotto6, S2Sotto8Sotto7, S2Sotto8Sotto8, S2Sotto8Sotto9, S2Sotto8Sotto10)
                InserisciValore(idPiano, S2txtVoce9, S2txtCod9, S2Op9, 29, S2Sotto9, S2Sotto9Sotto1, S2Sotto9Sotto2, S2Sotto9Sotto3, S2Sotto9Sotto4, S2Sotto9Sotto5, S2Sotto9Sotto6, S2Sotto9Sotto7, S2Sotto9Sotto8, S2Sotto9Sotto9, S2Sotto9Sotto10)
                InserisciValore(idPiano, S2txtVoce10, S2txtCod10, S2Op10, 30, S2Sotto10, S2Sotto10Sotto1, S2Sotto10Sotto2, S2Sotto10Sotto3, S2Sotto10Sotto4, S2Sotto10Sotto5, S2Sotto10Sotto6, S2Sotto10Sotto7, S2Sotto10Sotto8, S2Sotto10Sotto9, S2Sotto10Sotto10)
                InserisciValore(idPiano, S2txtVoce11, S2txtCod11, S2Op11, 31, S2Sotto11, S2Sotto11Sotto1, S2Sotto11Sotto2, S2Sotto11Sotto3, S2Sotto11Sotto4, S2Sotto11Sotto5, S2Sotto11Sotto6, S2Sotto11Sotto7, S2Sotto11Sotto8, S2Sotto11Sotto9, S2Sotto11Sotto10)
                InserisciValore(idPiano, S2txtVoce12, S2txtCod12, S2Op12, 32, S2Sotto12, S2Sotto12Sotto1, S2Sotto12Sotto2, S2Sotto12Sotto3, S2Sotto12Sotto4, S2Sotto12Sotto5, S2Sotto12Sotto6, S2Sotto12Sotto7, S2Sotto12Sotto8, S2Sotto12Sotto9, S2Sotto12Sotto10)
                InserisciValore(idPiano, S2txtVoce13, S2txtCod13, S2Op13, 33, S2Sotto13, S2Sotto13Sotto1, S2Sotto13Sotto2, S2Sotto13Sotto3, S2Sotto13Sotto4, S2Sotto13Sotto5, S2Sotto13Sotto6, S2Sotto13Sotto7, S2Sotto13Sotto8, S2Sotto13Sotto9, S2Sotto13Sotto10)
                InserisciValore(idPiano, S2txtVoce14, S2txtCod14, S2Op14, 34, S2Sotto14, S2Sotto14Sotto1, S2Sotto14Sotto2, S2Sotto14Sotto3, S2Sotto14Sotto4, S2Sotto14Sotto5, S2Sotto14Sotto6, S2Sotto14Sotto7, S2Sotto14Sotto8, S2Sotto14Sotto9, S2Sotto14Sotto10)
                InserisciValore(idPiano, S2txtVoce15, S2txtCod15, S2Op15, 35, S2Sotto15, S2Sotto15Sotto1, S2Sotto15Sotto2, S2Sotto15Sotto3, S2Sotto15Sotto4, S2Sotto15Sotto5, S2Sotto15Sotto6, S2Sotto15Sotto7, S2Sotto15Sotto8, S2Sotto15Sotto9, S2Sotto15Sotto10)
                InserisciValore(idPiano, S2txtVoce16, S2txtCod16, S2Op16, 36, S2Sotto16, S2Sotto16Sotto1, S2Sotto16Sotto2, S2Sotto16Sotto3, S2Sotto16Sotto4, S2Sotto16Sotto5, S2Sotto16Sotto6, S2Sotto16Sotto7, S2Sotto16Sotto8, S2Sotto16Sotto9, S2Sotto16Sotto10)
                InserisciValore(idPiano, S2txtVoce17, S2txtCod17, S2Op17, 37, S2Sotto17, S2Sotto17Sotto1, S2Sotto17Sotto2, S2Sotto17Sotto3, S2Sotto17Sotto4, S2Sotto17Sotto5, S2Sotto17Sotto6, S2Sotto17Sotto7, S2Sotto17Sotto8, S2Sotto17Sotto9, S2Sotto17Sotto10)
                InserisciValore(idPiano, S2txtVoce18, S2txtCod18, S2Op18, 38, S2Sotto18, S2Sotto18Sotto1, S2Sotto18Sotto2, S2Sotto18Sotto3, S2Sotto18Sotto4, S2Sotto18Sotto5, S2Sotto18Sotto6, S2Sotto18Sotto7, S2Sotto18Sotto8, S2Sotto18Sotto9, S2Sotto18Sotto10)
                InserisciValore(idPiano, S2txtVoce19, S2txtCod19, S2Op19, 39, S2Sotto19, S2Sotto19Sotto1, S2Sotto19Sotto2, S2Sotto19Sotto3, S2Sotto19Sotto4, S2Sotto19Sotto5, S2Sotto19Sotto6, S2Sotto19Sotto7, S2Sotto19Sotto8, S2Sotto19Sotto9, S2Sotto19Sotto10)
                InserisciValore(idPiano, S2txtVoce20, S2txtCod20, S2Op20, 40, S2Sotto20, S2Sotto20Sotto1, S2Sotto20Sotto2, S2Sotto20Sotto3, S2Sotto20Sotto4, S2Sotto20Sotto5, S2Sotto20Sotto6, S2Sotto20Sotto7, S2Sotto20Sotto8, S2Sotto20Sotto9, S2Sotto20Sotto10)


                InserisciValore(idPiano, S3txtVoce1, S3txtCod1, S3Op1, 41, S3Sotto1, S3Sotto1Sotto1, S3Sotto1Sotto2, S3Sotto1Sotto3, S3Sotto1Sotto4, S3Sotto1Sotto5, S3Sotto1Sotto6, S3Sotto1Sotto7, S3Sotto1Sotto8, S3Sotto1Sotto9, S3Sotto1Sotto10)
                InserisciValore(idPiano, S3txtVoce2, S3txtCod2, S3Op2, 42, S3Sotto2, S3Sotto2Sotto1, S3Sotto2Sotto2, S3Sotto2Sotto3, S3Sotto2Sotto4, S3Sotto2Sotto5, S3Sotto2Sotto6, S3Sotto2Sotto7, S3Sotto2Sotto8, S3Sotto2Sotto9, S3Sotto2Sotto10)
                InserisciValore(idPiano, S3txtVoce3, S3txtCod3, S3Op3, 43, S3Sotto3, S3Sotto2Sotto1, S3Sotto3Sotto2, S3Sotto3Sotto3, S3Sotto3Sotto4, S3Sotto3Sotto5, S3Sotto3Sotto6, S3Sotto3Sotto7, S3Sotto3Sotto8, S3Sotto3Sotto9, S3Sotto3Sotto10)
                InserisciValore(idPiano, S3txtVoce4, S3txtCod4, S3Op4, 44, S3Sotto4, S3Sotto4Sotto1, S3Sotto4Sotto2, S3Sotto4Sotto3, S3Sotto4Sotto4, S3Sotto4Sotto5, S3Sotto4Sotto6, S3Sotto4Sotto7, S3Sotto4Sotto8, S3Sotto4Sotto9, S3Sotto4Sotto10)
                InserisciValore(idPiano, S3txtVoce5, S3txtCod5, S3Op5, 45, S3Sotto5, S3Sotto5Sotto1, S3Sotto5Sotto2, S3Sotto5Sotto3, S3Sotto5Sotto4, S3Sotto5Sotto5, S3Sotto5Sotto6, S3Sotto5Sotto7, S3Sotto5Sotto8, S3Sotto5Sotto9, S3Sotto5Sotto10)

                InserisciValore(idPiano, S4txtVoce1, S4txtCod1, S4Op1, 46, S4Sotto1, S4Sotto1Sotto1, S4Sotto1Sotto2, S4Sotto1Sotto3, S4Sotto1Sotto4, S4Sotto1Sotto5, S4Sotto1Sotto6, S4Sotto1Sotto7, S4Sotto1Sotto8, S4Sotto1Sotto9, S4Sotto1Sotto10)
                InserisciValore(idPiano, S4txtVoce2, S4txtCod2, S4Op2, 47, S4Sotto2, S4Sotto2Sotto1, S4Sotto2Sotto2, S4Sotto2Sotto3, S4Sotto2Sotto4, S4Sotto2Sotto5, S4Sotto2Sotto6, S4Sotto2Sotto7, S4Sotto2Sotto8, S4Sotto2Sotto9, S4Sotto2Sotto10)
                InserisciValore(idPiano, S4txtVoce3, S4txtCod3, S4Op3, 48, S4Sotto3, S4Sotto3Sotto1, S4Sotto3Sotto2, S4Sotto3Sotto3, S4Sotto3Sotto4, S4Sotto3Sotto5, S4Sotto3Sotto6, S4Sotto3Sotto7, S4Sotto3Sotto8, S4Sotto3Sotto9, S4Sotto3Sotto10)
                InserisciValore(idPiano, S4txtVoce4, S4txtCod4, S4Op4, 49, S4Sotto4, S4Sotto4Sotto1, S4Sotto4Sotto2, S4Sotto4Sotto3, S4Sotto4Sotto4, S4Sotto4Sotto5, S4Sotto4Sotto6, S4Sotto4Sotto7, S4Sotto4Sotto8, S4Sotto4Sotto9, S4Sotto4Sotto10)
                InserisciValore(idPiano, S4txtVoce5, S4txtCod5, S4Op5, 50, S4Sotto5, S4Sotto5Sotto1, S4Sotto5Sotto2, S4Sotto5Sotto3, S4Sotto5Sotto4, S4Sotto5Sotto5, S4Sotto5Sotto6, S4Sotto5Sotto7, S4Sotto5Sotto8, S4Sotto5Sotto9, S4Sotto5Sotto10)


                par.cmd.CommandText = "select * from siscom_mi.pf_voci where id_piano_finanziario=" & idPiano
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader1.Read
                    'par.cmd.CommandText = "select distinct id_ufficio from operatori, siscom_mi.pf_voci_operatori where (id_voce=" & myReader1("id") & " or id_voce=" & par.IfNull(myReader1("id_voce_madre"), "NULL") & ") and id_operatore=operatori.id"
                    par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.TAB_FILIALI"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader2.Read
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_VOCI_STRUTTURA (id_voce,id_STRUTTURA) values (" & myReader1("id") & "," & myReader2("id") & ")"
                        par.cmd.ExecuteNonQuery()
                    Loop
                    myReader2.Close()
                Loop
                myReader1.Close()


                par.cmd.CommandText = "select * from siscom_mi.tab_servizi_voci_base order by cod_voce asc"
                myReader1 = par.cmd.ExecuteReader()
                Do While myReader1.Read
                    par.cmd.CommandText = "select id from siscom_mi.pf_voci where id_piano_finanziario=" & idPiano & " and codice='" & myReader1("cod_voce") & "'"
                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        'par.cmd.CommandText = "INSERT INTO SISCOM_MI.tab_servizi_voci VALUES (SISCOM_MI.SEQ_TAB_SERVIZI_VOCI.NEXTVAL," & myReader1("ID_SERVIZIO") & ",'" & par.PulisciStrSql(myReader1("DESCRIZIONE")) & "'," & myReader1("TIPO_CARICO") & "," & par.VirgoleInPunti(myReader1("PERC_REVERSIBILITA")) & "," & par.VirgoleInPunti(myReader1("IVA_CANONE")) & "," & par.VirgoleInPunti(myReader1("IVA_CONSUMO")) & "," & myReader1("QUOTA_PREVENTIVA") & "," & myReader1("NO_MOD") & "," & myReaderA("ID") & ")"
                        'par.cmd.ExecuteNonQuery()

                        Dim IvaCanone As Integer = 0
                        par.cmd.CommandText = "SELECT MAX(VALORE) AS IVA FROM siscom_mi.IVA WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA FROM siscom_mi.IVA A WHERE A.VALORE = " & par.VirgoleInPunti(myReader1("IVA_CANONE")) & ")"
                        Dim MyReaderIva As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If MyReaderIva.Read Then
                            IvaCanone = par.PuntiInVirgole(par.IfNull(MyReaderIva("IVA"), 0))
                        End If
                        MyReaderIva.Close()

                        Dim IvaConsumo As Integer = 0
                        par.cmd.CommandText = "SELECT MAX(VALORE) AS IVA FROM siscom_mi.IVA WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA FROM siscom_mi.IVA A WHERE A.VALORE = " & par.VirgoleInPunti(myReader1("IVA_CONSUMO")) & ")"
                        MyReaderIva = par.cmd.ExecuteReader
                        If MyReaderIva.Read Then
                            IvaConsumo = par.PuntiInVirgole(par.IfNull(MyReaderIva("IVA"), 0))
                        End If
                        MyReaderIva.Close()


                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.tab_servizi_voci(ID,ID_SERVIZIO,DESCRIZIONE,TIPO_CARICO,PERC_REVERSIBILITA,IVA_CANONE,IVA_CONSUMO,QUOTA_PREVENTIVA,NO_MOD,ID_VOCE,ID_CATEGORIA) " _
                                            & "VALUES (SISCOM_MI.SEQ_TAB_SERVIZI_VOCI.NEXTVAL," & myReader1("ID_SERVIZIO") & ",'" & par.PulisciStrSql(myReader1("DESCRIZIONE")) & "'," & myReader1("TIPO_CARICO") & "," & par.VirgoleInPunti(myReader1("PERC_REVERSIBILITA")) & "," & IvaCanone & "," & IvaConsumo & "," & myReader1("QUOTA_PREVENTIVA") & "," & myReader1("NO_MOD") & "," & myReaderA("ID") & "," & par.IfNull(myReader1("ID_CATEGORIA"), "NULL") & ")"
                        par.cmd.ExecuteNonQuery()



                    End If
                    myReaderA.Close()
                Loop
                myReader1.Close()

                If NuovoPF = True Then
                End If



                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Modificato.Value = "0"
                Response.Write("<script>alert('Operazione Effettuata!');</script>")

            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End Try
        End If
    End Sub



    Function SettaIndici()
        S1txtVoce1.Attributes.Add("ID", -1)
        S1txtVoce2.Attributes.Add("ID", -1)
        S1txtVoce3.Attributes.Add("ID", -1)
        S1txtVoce4.Attributes.Add("ID", -1)
        S1txtVoce5.Attributes.Add("ID", -1)
        S1txtVoce6.Attributes.Add("ID", -1)
        S1txtVoce7.Attributes.Add("ID", -1)
        S1txtVoce8.Attributes.Add("ID", -1)
        S1txtVoce9.Attributes.Add("ID", -1)
        S1txtVoce10.Attributes.Add("ID", -1)
        S1txtVoce11.Attributes.Add("ID", -1)
        S1txtVoce12.Attributes.Add("ID", -1)
        S1txtVoce13.Attributes.Add("ID", -1)
        S1txtVoce14.Attributes.Add("ID", -1)
        S1txtVoce15.Attributes.Add("ID", -1)
        S1txtVoce16.Attributes.Add("ID", -1)
        S1txtVoce17.Attributes.Add("ID", -1)
        S1txtVoce18.Attributes.Add("ID", -1)
        S1txtVoce19.Attributes.Add("ID", -1)
        S1txtVoce20.Attributes.Add("ID", -1)

        S2txtVoce1.Attributes.Add("ID", -1)
        S2txtVoce2.Attributes.Add("ID", -1)
        S2txtVoce3.Attributes.Add("ID", -1)
        S2txtVoce4.Attributes.Add("ID", -1)
        S2txtVoce5.Attributes.Add("ID", -1)
        S2txtVoce6.Attributes.Add("ID", -1)
        S2txtVoce7.Attributes.Add("ID", -1)
        S2txtVoce8.Attributes.Add("ID", -1)
        S2txtVoce9.Attributes.Add("ID", -1)
        S2txtVoce10.Attributes.Add("ID", -1)
        S2txtVoce11.Attributes.Add("ID", -1)
        S2txtVoce12.Attributes.Add("ID", -1)
        S2txtVoce13.Attributes.Add("ID", -1)
        S2txtVoce14.Attributes.Add("ID", -1)
        S2txtVoce15.Attributes.Add("ID", -1)
        S2txtVoce16.Attributes.Add("ID", -1)
        S2txtVoce17.Attributes.Add("ID", -1)
        S2txtVoce18.Attributes.Add("ID", -1)
        S2txtVoce19.Attributes.Add("ID", -1)
        S2txtVoce20.Attributes.Add("ID", -1)

        S3txtVoce1.Attributes.Add("ID", -1)
        S3txtVoce2.Attributes.Add("ID", -1)
        S3txtVoce3.Attributes.Add("ID", -1)
        S3txtVoce4.Attributes.Add("ID", -1)
        S3txtVoce5.Attributes.Add("ID", -1)

        S4txtVoce1.Attributes.Add("ID", -1)
        S4txtVoce2.Attributes.Add("ID", -1)
        S4txtVoce3.Attributes.Add("ID", -1)
        S4txtVoce4.Attributes.Add("ID", -1)
        S4txtVoce5.Attributes.Add("ID", -1)


        S1txtCod1.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod2.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod3.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod4.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod5.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod6.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod7.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod8.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod9.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod10.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod11.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod12.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod13.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod14.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod15.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod16.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod17.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod18.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod19.Attributes.Add("ID_CAPITOLO", "12")
        S1txtCod20.Attributes.Add("ID_CAPITOLO", "12")

        S2txtCod1.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod2.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod3.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod4.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod5.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod6.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod7.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod8.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod9.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod10.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod11.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod12.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod13.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod14.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod15.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod16.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod17.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod18.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod19.Attributes.Add("ID_CAPITOLO", "12")
        S2txtCod20.Attributes.Add("ID_CAPITOLO", "12")

        S3txtCod1.Attributes.Add("ID_CAPITOLO", "12")
        S3txtCod2.Attributes.Add("ID_CAPITOLO", "12")
        S3txtCod3.Attributes.Add("ID_CAPITOLO", "12")
        S3txtCod4.Attributes.Add("ID_CAPITOLO", "12")
        S3txtCod5.Attributes.Add("ID_CAPITOLO", "12")

        S4txtCod1.Attributes.Add("ID_CAPITOLO", "12")
        S4txtCod2.Attributes.Add("ID_CAPITOLO", "12")
        S4txtCod3.Attributes.Add("ID_CAPITOLO", "12")
        S4txtCod4.Attributes.Add("ID_CAPITOLO", "12")
        S4txtCod5.Attributes.Add("ID_CAPITOLO", "12")


    End Function


    Function CaricaOperatori()
        Dim i As Integer = 0
        Dim s As Integer = 0

        For i = 0 To ListaOperatori.Items.Count - 1
            If ListaOperatori.Items(i).Selected Then
                ListaOperatori.Items(i).Selected = False
            End If
        Next


        Select Case casella.Value
            Case "S1voce1"
                Operatori(S1Op1.Value)

            Case "S1voce2"
                Operatori(S1Op2.Value)

            Case "S1voce3"
                Operatori(S1Op3.Value)

            Case "S1voce4"
                Operatori(S1Op4.Value)

            Case "S1voce5"
                Operatori(S1Op5.Value)

            Case "S1voce6"
                Operatori(S1Op6.Value)

            Case "S1voce7"
                Operatori(S1Op7.Value)

            Case "S1voce8"
                Operatori(S1Op8.Value)

            Case "S1voce9"
                Operatori(S1Op9.Value)

            Case "S1voce10"
                Operatori(S1Op10.Value)

            Case "S1voce11"
                Operatori(S1Op11.Value)

            Case "S1voce12"
                Operatori(S1Op12.Value)

            Case "S1voce13"
                Operatori(S1Op13.Value)

            Case "S1voce14"
                Operatori(S1Op14.Value)

            Case "S1voce15"
                Operatori(S1Op15.Value)

            Case "S1voce16"
                Operatori(S1Op16.Value)

            Case "S1voce17"
                Operatori(S1Op17.Value)

            Case "S1voce18"
                Operatori(S1Op18.Value)

            Case "S1voce19"
                Operatori(S1Op19.Value)

            Case "S1voce20"
                Operatori(S1Op20.Value)

            Case "S2voce1"
                Operatori(S2Op1.Value)


            Case "S2voce2"
                Operatori(S2Op2.Value)


            Case "S2voce3"
                Operatori(S2Op3.Value)


            Case "S2voce4"
                Operatori(S2Op4.Value)


            Case "S2voce5"
                Operatori(S2Op5.Value)


            Case "S2voce6"
                Operatori(S2Op6.Value)


            Case "S2voce7"
                Operatori(S2Op7.Value)

            Case "S2voce8"
                Operatori(S2Op8.Value)


            Case "S2voce9"
                Operatori(S2Op9.Value)


            Case "S2voce10"
                Operatori(S2Op10.Value)

            Case "S2voce11"
                Operatori(S2Op11.Value)

            Case "S2voce12"
                Operatori(S2Op12.Value)

            Case "S2voce13"
                Operatori(S2Op13.Value)

            Case "S2voce14"
                Operatori(S2Op14.Value)

            Case "S2voce15"
                Operatori(S2Op15.Value)

            Case "S2voce16"
                Operatori(S2Op16.Value)


            Case "S2voce17"
                Operatori(S2Op17.Value)

            Case "S2voce18"
                Operatori(S2Op18.Value)

            Case "S2voce19"
                Operatori(S2Op19.Value)

            Case "S2voce20"
                Operatori(S2Op20.Value)


            Case "S3voce1"
                Operatori(S3Op1.Value)


            Case "S3voce2"
                Operatori(S3Op2.Value)


            Case "S3voce3"
                Operatori(S3Op3.Value)


            Case "S3voce4"
                Operatori(S3Op4.Value)


            Case "S3voce5"
                Operatori(S3Op5.Value)


            Case "S4voce1"
                Operatori(S4Op1.Value)


            Case "S4voce2"
                Operatori(S4Op2.Value)


            Case "S4voce3"
                Operatori(S4Op3.Value)


            Case "S4voce4"
                Operatori(S4Op4.Value)


            Case "S4voce5"
                Operatori(S4Op5.Value)

            Case Else
                kk = 0
                ReDim ElencoOperatori(0)
        End Select

        For s = 0 To kk - 1
            For i = 0 To ListaOperatori.Items.Count - 1
                If ListaOperatori.Items(i).Value = ElencoOperatori(s) Then
                    ListaOperatori.Items(i).Selected = True
                End If
            Next
        Next
        kk = 0
        ReDim ElencoOperatori(0)


    End Function



    Protected Sub ImgCompleto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgCompleto.Click

        If salvaok.Value = "1" Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "UPDATE siscom_mi.PF_MAIN SET ID_STATO=1 WHERE ID=" & pianoF.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_STRUTTURA) " _
                                    & "VALUES (" & pianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F80',''," & Session.Item("ID_STRUTTURA") & ")"
                par.cmd.ExecuteNonQuery()

                ImgProcedi.Visible = False

                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Modificato.Value = "0"
                Response.Write("<script>alert('Operazione Effettuata!');</script>")
                statop.Value = "1"

                Dim CTRL As Control
                For Each CTRL In Me.form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Enabled = False
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Enabled = False
                    ElseIf TypeOf CTRL Is CheckBox Then
                        DirectCast(CTRL, CheckBox).Enabled = False
                    ElseIf TypeOf CTRL Is ImageButton Then
                        DirectCast(CTRL, ImageButton).Visible = False
                    ElseIf TypeOf CTRL Is Web.UI.WebControls.Image Then
                        If DirectCast(CTRL, Web.UI.WebControls.Image).ID <> "imgEsci" And DirectCast(CTRL, Web.UI.WebControls.Image).ID <> "imgStampa" And DirectCast(CTRL, Web.UI.WebControls.Image).ID <> "imgEventi" Then
                            DirectCast(CTRL, Web.UI.WebControls.Image).Visible = False
                        End If
                    End If
                Next

                lblStato.Text = "STATO:CARICAMENTO IMPORTI"

            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End Try
        End If
    End Sub



    Protected Sub AggOperatoriS1V1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V1.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V2.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V3.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V4.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V5.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V6_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V6.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V7_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V7.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V8_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V8.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V9_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V9.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V10_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V10.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V1.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V10_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V10.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V2.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V3.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V4.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V5.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V6_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V6.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V7_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V7.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V8_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V8.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V9_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V9.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS3V1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS3V1.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS3V2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS3V2.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS3V3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS3V3.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS3V4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS3V4.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS3V5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS3V5.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS4V1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS4V1.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS4V2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS4V2.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS4V3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS4V3.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS4V4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS4V4.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS4V5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS4V5.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V11_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V11.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V12_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V12.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V13_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V13.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V14_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V14.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V15_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V15.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V16_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V16.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V11_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V11.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V12_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V12.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V13_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V13.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V14_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V14.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V15_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V15.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V16_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V16.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V17_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V17.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V18_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V18.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V19_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V19.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS1V20_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS1V20.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V17_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V17.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V18_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V18.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V19_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V19.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub AggOperatoriS2V20_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AggOperatoriS2V20.Click
        visualizza.Value = "1"
        CaricaOperatori()
    End Sub

    Protected Sub imgInserisciOperatore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgInserisciOperatore.Click
        Dim i As Integer = 0
        Dim sOperatori As String = ""


        For i = 0 To ListaOperatori.Items.Count - 1
            If ListaOperatori.Items(i).Selected Then
                sOperatori = sOperatori & ListaOperatori.Items(i).Value & "#"
            End If
        Next


        If sOperatori = "" Then
            Response.Write("<script>alert('Selezionare gli operatori dalla lista!');</script>")
            Scegli()
            Exit Sub
        End If

        Select Case casella.Value
            Case "S1voce1"
                S1Op1.Value = sOperatori
                ' casella.Value = ""
                apri.Value = "SEZ1"
            Case "S1voce2"
                S1Op2.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"
            Case "S1voce3"
                S1Op3.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"
            Case "S1voce4"
                S1Op4.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"
            Case "S1voce5"
                S1Op5.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"
            Case "S1voce6"
                S1Op6.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"
            Case "S1voce7"
                S1Op7.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"
            Case "S1voce8"
                S1Op8.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"
            Case "S1voce9"
                S1Op9.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"
            Case "S1voce10"
                S1Op10.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"

            Case "S1voce11"
                S1Op11.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"

            Case "S1voce12"
                S1Op12.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"

            Case "S1voce13"
                S1Op13.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"

            Case "S1voce14"
                S1Op14.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"
            Case "S1voce15"
                S1Op15.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"

            Case "S1voce16"
                S1Op16.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"

            Case "S1voce17"
                S1Op17.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"

            Case "S1voce18"
                S1Op18.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"

            Case "S1voce19"
                S1Op19.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"

            Case "S1voce20"
                S1Op20.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ1"

            Case "S2voce1"
                S2Op1.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"
            Case "S2voce2"
                S2Op2.Value = sOperatori
                ' casella.Value = ""
                apri.Value = "SEZ2"
            Case "S2voce3"
                S2Op3.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"
            Case "S2voce4"
                S2Op4.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"
            Case "S2voce5"
                S2Op5.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"
            Case "S2voce6"
                S2Op6.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"
            Case "S2voce7"
                S2Op7.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"
            Case "S2voce8"
                S2Op8.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"
            Case "S2voce9"
                S2Op9.Value = sOperatori
                casella.Value = ""
                apri.Value = "SEZ2"
            Case "S2voce10"
                S2Op10.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"
            Case "S2voce11"
                S2Op11.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"
            Case "S2voce12"
                S2Op12.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"
            Case "S2voce13"
                S2Op13.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"
            Case "S2voce14"
                S2Op14.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"
            Case "S2voce15"
                S2Op15.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"
            Case "S2voce16"
                S2Op16.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"

            Case "S2voce17"
                S2Op17.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"

            Case "S2voce18"
                S2Op18.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"

            Case "S2voce19"
                S2Op19.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"

            Case "S2voce20"
                S2Op20.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ2"

            Case "S3voce1"
                S3Op1.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ3"
            Case "S3voce2"
                S3Op2.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ3"
            Case "S3voce3"
                S3Op3.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ3"
            Case "S3voce4"
                S3Op4.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ3"
            Case "S3voce5"
                S3Op5.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ3"
            Case "S4voce1"
                S4Op1.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ4"
            Case "S4voce2"
                S4Op2.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ4"
            Case "S4voce3"
                S4Op3.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ4"
            Case "S4voce4"
                S4Op4.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ4"
            Case "S4voce5"
                S4Op5.Value = sOperatori
                'casella.Value = ""
                apri.Value = "SEZ4"
        End Select

        For i = 0 To ListaOperatori.Items.Count - 1
            If ListaOperatori.Items(i).Selected Then
                ListaOperatori.Items(i).Selected = False
            End If
        Next
        sOperatori = ""


    End Sub


End Class
