
Partial Class ASS_GestionePreferenze
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim myreaderj As Oracle.DataAccess.Client.OracleDataReader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            txt_supMax.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            txt_supMax.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
            txt_supMax.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")

            txt_supMin.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            txt_supMin.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
            txt_supMin.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")



            If Not IsPostBack Then



                lIdDomanda = Request.QueryString("ID")
                TIPO = Request.QueryString("T")
                txt_idDomanda.Value = Request.QueryString("ID")
                txt_tipo.Value = Request.QueryString("T")



                CaricaCombo()
                CaricaDatiNominativo()
                ScegliCarica()



                H1.Value = "1"


                If Request.QueryString("PROV") = "0" Then
                    FrmSolaLettura(Me)
                    btn_salva.Visible = False
                End If

            Else

                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey111", "document.getElementById('caricamento').style.visibility = 'hidden';", True)

            End If

            ' Me.ddl_localita1.Attributes.Add("onchange", "javascript:if (document.getElementById('caricamento') != null) {document.getElementById('caricamento').style.visibility = 'visible';}")


            SettaControlModifiche(Me)


        Catch ex As Exception
            '  chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try


    End Sub


    Private Sub SettaControlModifiche(ByVal obj As Control)
        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                SettaControlModifiche(CTRL)
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onclick", "javascript:document.getElementById('frmModify').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';if (document.getElementById('caricamento') != null) {document.getElementById('caricamento').style.visibility = 'visible';}")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('frmModify').value='1';")
            ElseIf TypeOf CTRL Is Button Then
                DirectCast(CTRL, Button).Attributes.Add("onclick", "javascript:document.getElementById('caricamento').style.visibility ='visible';")
            ElseIf TypeOf CTRL Is ImageButton Then
                DirectCast(CTRL, ImageButton).Attributes.Add("onclick", "javascript:document.getElementById('caricamento').style.visibility ='visible';")
            End If
        Next
        '   DirectCast(btn_salva, ImageButton).Attributes.Add("onclick", "javascript:document.getElementById('caricamento').style.visibility ='visible';")
    End Sub

    Private Sub CaricaComboPiani()

        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT COD AS CODICE, (DESCRIZIONE || ' (' || LIVELLO|| ')') AS PIANO, LIVELLO AS LIV FROM SISCOM_MI.TIPO_LIVELLO_PIANO"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()

            Dim k As Integer = 0
            Do While k < dt.Rows.Count
                Dim rowScart As Data.DataRow = dt.Rows(k)
                If Not IsDBNull(rowScart.ItemArray(2)) Then

                Else
                    rowScart.Delete()
                End If

                k = k + 1
            Loop
            dt.AcceptChanges()

            ddl_pianoaCon.DataSource=dt
            ddl_pianoaCon.DataTextField = "PIANO"
            ddl_pianoaCon.DataValueField = "CODICE"
            ddl_pianoaCon.DataBind()
            ddl_pianoaCon.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_pianoaCon.SelectedValue = "-1"

           
            ddl_pianodaCon.DataSource = dt
            ddl_pianodaCon.DataTextField = "PIANO"
            ddl_pianodaCon.DataValueField = "CODICE"
            ddl_pianodaCon.DataBind()
            ddl_pianodaCon.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_pianodaCon.SelectedValue = "-1"


            ddl_pianoaSenza.DataSource = dt
            ddl_pianoaSenza.DataTextField = "PIANO"
            ddl_pianoaSenza.DataValueField = "CODICE"
            ddl_pianoaSenza.DataBind()
            ddl_pianoaSenza.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_pianoaSenza.SelectedValue = "-1"


            ddl_pianodaSenza.DataSource = dt
            ddl_pianodaSenza.DataTextField = "PIANO"
            ddl_pianodaSenza.DataValueField = "CODICE"
            ddl_pianodaSenza.DataBind()
            ddl_pianodaSenza.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_pianodaSenza.SelectedValue = "-1"










            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If




        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If




            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub


    Private Sub CaricaCombo()

        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If



            Dim Stringa As String = ""

            ddl_indirizzo1.Enabled = True
            ddl_quartiere1.Enabled = True




            ddl_zona1.Enabled = False
            ddl_quartiere1.Enabled = False
            ddl_indirizzo1.Enabled = False
            ddl_complesso1.Enabled = False
            ddl_edificio1.Enabled = False

            ddl_zona1ex.Enabled = False
            ddl_quartiere1ex.Enabled = False
            ddl_indirizzo1ex.Enabled = False
            ddl_complesso1ex.Enabled = False
            ddl_edificio1ex.Enabled = False







            ddl_localita2.Enabled = False
            ddl_zona2.Enabled = False
            ddl_quartiere2.Enabled = False
            ddl_indirizzo2.Enabled = False
            ddl_complesso2.Enabled = False
            ddl_edificio2.Enabled = False

            ddl_localita3.Enabled = False
            ddl_zona3.Enabled = False
            ddl_quartiere3.Enabled = False
            ddl_indirizzo3.Enabled = False
            ddl_complesso3.Enabled = False
            ddl_edificio3.Enabled = False

            '------------------------nuovo
            ddl_localita4.Enabled = False
            ddl_zona4.Enabled = False
            ddl_quartiere4.Enabled = False
            ddl_indirizzo4.Enabled = False
            ddl_complesso4.Enabled = False
            ddl_edificio4.Enabled = False


            ddl_localita5.Enabled = False
            ddl_zona5.Enabled = False
            ddl_quartiere5.Enabled = False
            ddl_indirizzo5.Enabled = False
            ddl_complesso5.Enabled = False
            ddl_edificio5.Enabled = False

            '---------------------------------



            ddl_localita2ex.Enabled = False
            ddl_zona2ex.Enabled = False
            ddl_quartiere2ex.Enabled = False
            ddl_indirizzo2ex.Enabled = False
            ddl_complesso2ex.Enabled = False
            ddl_edificio2ex.Enabled = False

            ddl_localita3ex.Enabled = False
            ddl_zona3ex.Enabled = False
            ddl_quartiere3ex.Enabled = False
            ddl_indirizzo3ex.Enabled = False
            ddl_complesso3ex.Enabled = False
            ddl_edificio3ex.Enabled = False

            '--------------------nuovo----------

            ddl_localita4ex.Enabled = False
            ddl_zona4ex.Enabled = False
            ddl_quartiere4ex.Enabled = False
            ddl_indirizzo4ex.Enabled = False
            ddl_complesso4ex.Enabled = False
            ddl_edificio4ex.Enabled = False


            ddl_localita5ex.Enabled = False
            ddl_zona5ex.Enabled = False
            ddl_quartiere5ex.Enabled = False
            ddl_indirizzo5ex.Enabled = False
            ddl_complesso5ex.Enabled = False
            ddl_edificio5ex.Enabled = False


            '------------------------------------------------------

            'ddl_piano2SA.Enabled = False
            'ddl_piano3SA.Enabled = False

            'ddl_piano2CA.Enabled = False
            'ddl_piano3CA.Enabled = False
            ''----------------------------nuovo-------------------
            'ddl_piano4SA.Enabled = False
            'ddl_piano5SA.Enabled = False

            'ddl_piano4CA.Enabled = False
            'ddl_piano5CA.Enabled = False

            '----------------------------



            'carica località di tutti i box (situazione iniziale)


            par.RiempiDList(Me, par.OracleConn, "ddl_localita1", "SELECT distinct comuni_nazioni.ID, comuni_nazioni.nome, comuni_nazioni.cod FROM  sepa.comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.edifici WHERE comuni_nazioni.cod = edifici.cod_comune and complessi_immobiliari.id = edifici.id_complesso order by nome", "NOME", "ID")
            ddl_localita1.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_localita1.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_localita2", "SELECT distinct comuni_nazioni.ID, comuni_nazioni.nome, comuni_nazioni.cod FROM  sepa.comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.edifici WHERE comuni_nazioni.cod = edifici.cod_comune and complessi_immobiliari.id = edifici.id_complesso order by nome", "NOME", "ID")
            ddl_localita2.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_localita2.Items.FindByText("TUTTI").Selected = True



            par.RiempiDList(Me, par.OracleConn, "ddl_localita3", "SELECT distinct comuni_nazioni.ID, comuni_nazioni.nome, comuni_nazioni.cod FROM  sepa.comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.edifici WHERE comuni_nazioni.cod = edifici.cod_comune and complessi_immobiliari.id = edifici.id_complesso order by nome", "NOME", "ID")
            ddl_localita3.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_localita3.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_localita4", "SELECT distinct comuni_nazioni.ID, comuni_nazioni.nome, comuni_nazioni.cod FROM  sepa.comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.edifici WHERE comuni_nazioni.cod = edifici.cod_comune and complessi_immobiliari.id = edifici.id_complesso order by nome", "NOME", "ID")
            ddl_localita4.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_localita4.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_localita5", "SELECT distinct comuni_nazioni.ID, comuni_nazioni.nome, comuni_nazioni.cod FROM  sepa.comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.edifici WHERE comuni_nazioni.cod = edifici.cod_comune and complessi_immobiliari.id = edifici.id_complesso order by nome", "NOME", "ID")
            ddl_localita5.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_localita5.Items.FindByText("TUTTI").Selected = True








            par.RiempiDList(Me, par.OracleConn, "ddl_localita1ex", "SELECT distinct comuni_nazioni.ID, comuni_nazioni.nome, comuni_nazioni.cod FROM  sepa.comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.edifici WHERE comuni_nazioni.cod = edifici.cod_comune and complessi_immobiliari.id = edifici.id_complesso order by nome", "NOME", "ID")
            ddl_localita1ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_localita1ex.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_localita2ex", "SELECT distinct comuni_nazioni.ID, comuni_nazioni.nome, comuni_nazioni.cod FROM  sepa.comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.edifici WHERE comuni_nazioni.cod = edifici.cod_comune and complessi_immobiliari.id = edifici.id_complesso order by nome", "NOME", "ID")
            ddl_localita2ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_localita2ex.Items.FindByText("TUTTI").Selected = True



            par.RiempiDList(Me, par.OracleConn, "ddl_localita3ex", "SELECT distinct comuni_nazioni.ID, comuni_nazioni.nome, comuni_nazioni.cod FROM  sepa.comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.edifici WHERE comuni_nazioni.cod = edifici.cod_comune and complessi_immobiliari.id = edifici.id_complesso order by nome", "NOME", "ID")
            ddl_localita3ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_localita3ex.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_localita4ex", "SELECT distinct comuni_nazioni.ID, comuni_nazioni.nome, comuni_nazioni.cod FROM  sepa.comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.edifici WHERE comuni_nazioni.cod = edifici.cod_comune and complessi_immobiliari.id = edifici.id_complesso order by nome", "NOME", "ID")
            ddl_localita4ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_localita4ex.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "ddl_localita5ex", "SELECT distinct comuni_nazioni.ID, comuni_nazioni.nome, comuni_nazioni.cod FROM  sepa.comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.edifici WHERE comuni_nazioni.cod = edifici.cod_comune and complessi_immobiliari.id = edifici.id_complesso order by nome", "NOME", "ID")
            ddl_localita5ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_localita5ex.Items.FindByText("TUTTI").Selected = True

            'carica zone di tutti i box (situazione iniziale)


            par.RiempiDList(Me, par.OracleConn, "ddl_zona1", "select * from zona_aler order by zona asc", "ZONA", "COD")
            ddl_zona1.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_zona1.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_zona2", "select * from zona_aler order by zona asc", "ZONA", "COD")
            ddl_zona2.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_zona2.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_zona3", "select * from zona_aler order by zona asc", "ZONA", "COD")
            ddl_zona3.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_zona3.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "ddl_zona4", "select * from zona_aler order by zona asc", "ZONA", "COD")
            ddl_zona4.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_zona4.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "ddl_zona5", "select * from zona_aler order by zona asc", "ZONA", "COD")
            ddl_zona5.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_zona5.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_zona1ex", "select * from zona_aler order by zona asc", "ZONA", "COD")
            ddl_zona1ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_zona1ex.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_zona2ex", "select * from zona_aler order by zona asc", "ZONA", "COD")
            ddl_zona2ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_zona2ex.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_zona3ex", "select * from zona_aler order by zona asc", "ZONA", "COD")
            ddl_zona3ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_zona3ex.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "ddl_zona4ex", "select * from zona_aler order by zona asc", "ZONA", "COD")
            ddl_zona4ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_zona4ex.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_zona5ex", "select * from zona_aler order by zona asc", "ZONA", "COD")
            ddl_zona5ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_zona5ex.Items.FindByText("TUTTI").Selected = True






            'carica quartieri di tutti i box (situazione iniziale)



            par.RiempiDList(Me, par.OracleConn, "ddl_quartiere1", "SELECT distinct tab_quartieri.nome, tab_quartieri.id FROM  siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari WHERE tab_quartieri.id (+)= complessi_immobiliari.id_quartiere order by nome asc", "NOME", "ID")
            ddl_quartiere1.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_quartiere1.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_quartiere2", "SELECT distinct tab_quartieri.nome, tab_quartieri.id FROM  siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari WHERE tab_quartieri.id (+)= complessi_immobiliari.id_quartiere order by nome asc", "NOME", "ID")
            ddl_quartiere2.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_quartiere2.Items.FindByText("TUTTI").Selected = True



            par.RiempiDList(Me, par.OracleConn, "ddl_quartiere3", "SELECT distinct tab_quartieri.nome, tab_quartieri.id FROM  siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari WHERE tab_quartieri.id (+)= complessi_immobiliari.id_quartiere order by nome asc", "NOME", "ID")
            ddl_quartiere3.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_quartiere3.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "ddl_quartiere4", "SELECT distinct tab_quartieri.nome, tab_quartieri.id FROM  siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari WHERE tab_quartieri.id (+)= complessi_immobiliari.id_quartiere order by nome asc", "NOME", "ID")
            ddl_quartiere4.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_quartiere4.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "ddl_quartiere5", "SELECT distinct tab_quartieri.nome, tab_quartieri.id FROM  siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari WHERE tab_quartieri.id (+)= complessi_immobiliari.id_quartiere order by nome asc", "NOME", "ID")
            ddl_quartiere5.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_quartiere5.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_quartiere1ex", "SELECT distinct tab_quartieri.nome, tab_quartieri.id FROM  siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari WHERE tab_quartieri.id (+)= complessi_immobiliari.id_quartiere order by nome asc", "NOME", "ID")
            ddl_quartiere1ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_quartiere1ex.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_quartiere2ex", "SELECT distinct tab_quartieri.nome, tab_quartieri.id FROM  siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari WHERE tab_quartieri.id (+)= complessi_immobiliari.id_quartiere order by nome asc", "NOME", "ID")
            ddl_quartiere2ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_quartiere2ex.Items.FindByText("TUTTI").Selected = True



            par.RiempiDList(Me, par.OracleConn, "ddl_quartiere3ex", "SELECT distinct tab_quartieri.nome, tab_quartieri.id FROM  siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari WHERE tab_quartieri.id (+)= complessi_immobiliari.id_quartiere order by nome asc", "NOME", "ID")
            ddl_quartiere3ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_quartiere3ex.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "ddl_quartiere4ex", "SELECT distinct tab_quartieri.nome, tab_quartieri.id FROM  siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari WHERE tab_quartieri.id (+)= complessi_immobiliari.id_quartiere order by nome asc", "NOME", "ID")
            ddl_quartiere4ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_quartiere4ex.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_quartiere5ex", "SELECT distinct tab_quartieri.nome, tab_quartieri.id FROM  siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari WHERE tab_quartieri.id (+)= complessi_immobiliari.id_quartiere order by nome asc", "NOME", "ID")
            ddl_quartiere5ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_quartiere5ex.Items.FindByText("TUTTI").Selected = True



            'carica indirizzi di tutti i box (situazione iniziale)



            par.RiempiDList(Me, par.OracleConn, "ddl_indirizzo1", "SELECT DISTINCT descrizione FROM siscom_mi.indirizzi WHERE ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) ORDER BY descrizione ASC", "DESCRIZIONE", "DESCRIZIONE")
            ddl_indirizzo1.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_indirizzo1.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_indirizzo2", "SELECT DISTINCT descrizione FROM siscom_mi.indirizzi WHERE ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) ORDER BY descrizione ASC", "DESCRIZIONE", "DESCRIZIONE")
            ddl_indirizzo2.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_indirizzo2.Items.FindByText("TUTTI").Selected = True



            par.RiempiDList(Me, par.OracleConn, "ddl_indirizzo3", "SELECT DISTINCT descrizione FROM siscom_mi.indirizzi WHERE ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) ORDER BY descrizione ASC", "DESCRIZIONE", "DESCRIZIONE")
            ddl_indirizzo3.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_indirizzo3.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_indirizzo4", "SELECT DISTINCT descrizione FROM siscom_mi.indirizzi WHERE ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) ORDER BY descrizione ASC", "DESCRIZIONE", "DESCRIZIONE")
            ddl_indirizzo4.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_indirizzo4.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "ddl_indirizzo5", "SELECT DISTINCT descrizione FROM siscom_mi.indirizzi WHERE ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) ORDER BY descrizione ASC", "DESCRIZIONE", "DESCRIZIONE")
            ddl_indirizzo5.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_indirizzo5.Items.FindByText("TUTTI").Selected = True



            par.RiempiDList(Me, par.OracleConn, "ddl_indirizzo1ex", "SELECT DISTINCT descrizione FROM siscom_mi.indirizzi WHERE ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) ORDER BY descrizione ASC", "DESCRIZIONE", "DESCRIZIONE")
            ddl_indirizzo1ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_indirizzo1ex.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_indirizzo2ex", "SELECT DISTINCT descrizione FROM siscom_mi.indirizzi WHERE ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) ORDER BY descrizione ASC", "DESCRIZIONE", "DESCRIZIONE")
            ddl_indirizzo2ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_indirizzo2ex.Items.FindByText("TUTTI").Selected = True



            par.RiempiDList(Me, par.OracleConn, "ddl_indirizzo3ex", "SELECT DISTINCT descrizione FROM siscom_mi.indirizzi WHERE ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) ORDER BY descrizione ASC", "DESCRIZIONE", "DESCRIZIONE")
            ddl_indirizzo3ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_indirizzo3ex.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_indirizzo4ex", "SELECT DISTINCT descrizione FROM siscom_mi.indirizzi WHERE ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) ORDER BY descrizione ASC", "DESCRIZIONE", "DESCRIZIONE")
            ddl_indirizzo4ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_indirizzo4ex.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_indirizzo5ex", "SELECT DISTINCT descrizione FROM siscom_mi.indirizzi WHERE ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) ORDER BY descrizione ASC", "DESCRIZIONE", "DESCRIZIONE")
            ddl_indirizzo5ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_indirizzo5ex.Items.FindByText("TUTTI").Selected = True


            'carica piani di tutti i box

            'par.RiempiDList(Me, par.OracleConn, "ddl_piano1SA", "SELECT * FROM SISCOM_MI.TIPO_LIVELLO_PIANO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            'ddl_piano1SA.Items.Add(New ListItem("TUTTI", "-1"))
            'ddl_piano1SA.Items.FindByText("TUTTI").Selected = True

            'par.RiempiDList(Me, par.OracleConn, "ddl_piano2SA", "SELECT * FROM SISCOM_MI.TIPO_LIVELLO_PIANO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            'ddl_piano2SA.Items.Add(New ListItem("TUTTI", "-1"))
            'ddl_piano2SA.Items.FindByText("TUTTI").Selected = True

            'par.RiempiDList(Me, par.OracleConn, "ddl_piano3SA", "SELECT * FROM SISCOM_MI.TIPO_LIVELLO_PIANO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            'ddl_piano3SA.Items.Add(New ListItem("TUTTI", "-1"))
            'ddl_piano3SA.Items.FindByText("TUTTI").Selected = True

            'par.RiempiDList(Me, par.OracleConn, "ddl_piano4SA", "SELECT * FROM SISCOM_MI.TIPO_LIVELLO_PIANO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            'ddl_piano4SA.Items.Add(New ListItem("TUTTI", "-1"))
            'ddl_piano4SA.Items.FindByText("TUTTI").Selected = True

            'par.RiempiDList(Me, par.OracleConn, "ddl_piano5SA", "SELECT * FROM SISCOM_MI.TIPO_LIVELLO_PIANO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            'ddl_piano5SA.Items.Add(New ListItem("TUTTI", "-1"))
            'ddl_piano5SA.Items.FindByText("TUTTI").Selected = True



            'par.RiempiDList(Me, par.OracleConn, "ddl_piano1CA", "SELECT * FROM SISCOM_MI.TIPO_LIVELLO_PIANO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            'ddl_piano1CA.Items.Add(New ListItem("TUTTI", "-1"))
            'ddl_piano1CA.Items.FindByText("TUTTI").Selected = True

            'par.RiempiDList(Me, par.OracleConn, "ddl_piano2CA", "SELECT * FROM SISCOM_MI.TIPO_LIVELLO_PIANO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            'ddl_piano2CA.Items.Add(New ListItem("TUTTI", "-1"))
            'ddl_piano2CA.Items.FindByText("TUTTI").Selected = True


            'par.RiempiDList(Me, par.OracleConn, "ddl_piano3CA", "SELECT * FROM SISCOM_MI.TIPO_LIVELLO_PIANO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            'ddl_piano3CA.Items.Add(New ListItem("TUTTI", "-1"))
            'ddl_piano3CA.Items.FindByText("TUTTI").Selected = True

            'par.RiempiDList(Me, par.OracleConn, "ddl_piano4CA", "SELECT * FROM SISCOM_MI.TIPO_LIVELLO_PIANO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            'ddl_piano4CA.Items.Add(New ListItem("TUTTI", "-1"))
            'ddl_piano4CA.Items.FindByText("TUTTI").Selected = True


            'par.RiempiDList(Me, par.OracleConn, "ddl_piano5CA", "SELECT * FROM SISCOM_MI.TIPO_LIVELLO_PIANO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            'ddl_piano5CA.Items.Add(New ListItem("TUTTI", "-1"))
            'ddl_piano5CA.Items.FindByText("TUTTI").Selected = True


            'carica COMPLESSI di tutti i box (situazione iniziale)



            par.RiempiDList(Me, par.OracleConn, "ddl_complesso1", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_complesso1.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_complesso1.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_complesso2", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_complesso2.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_complesso2.Items.FindByText("TUTTI").Selected = True



            par.RiempiDList(Me, par.OracleConn, "ddl_complesso3", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_complesso3.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_complesso3.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "ddl_complesso4", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_complesso4.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_complesso4.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "ddl_complesso5", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_complesso5.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_complesso5.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_complesso1ex", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_complesso1ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_complesso1ex.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_complesso2ex", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_complesso2ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_complesso2ex.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_complesso3ex", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_complesso3ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_complesso3ex.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "ddl_complesso4ex", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_complesso4ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_complesso4ex.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "ddl_complesso5ex", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_complesso5ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_complesso5ex.Items.FindByText("TUTTI").Selected = True


            'carica EDIFICI di tutti i box (situazione iniziale)



            par.RiempiDList(Me, par.OracleConn, "ddl_edificio1", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID <> 1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_edificio1.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_edificio1.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_edificio2", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID <> 1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_edificio2.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_edificio2.Items.FindByText("TUTTI").Selected = True



            par.RiempiDList(Me, par.OracleConn, "ddl_edificio3", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID <> 1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_edificio3.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_edificio3.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "ddl_edificio4", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID <> 1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_edificio4.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_edificio4.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "ddl_edificio5", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID <> 1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_edificio5.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_edificio5.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_edificio1ex", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID <> 1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_edificio1ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_edificio1ex.Items.FindByText("TUTTI").Selected = True


            par.RiempiDList(Me, par.OracleConn, "ddl_edificio2ex", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID <> 1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_edificio2ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_edificio2ex.Items.FindByText("TUTTI").Selected = True



            par.RiempiDList(Me, par.OracleConn, "ddl_edificio3ex", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID <> 1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_edificio3ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_edificio3ex.Items.FindByText("TUTTI").Selected = True



            par.RiempiDList(Me, par.OracleConn, "ddl_edificio4ex", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID <> 1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_edificio4ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_edificio4ex.Items.FindByText("TUTTI").Selected = True



            par.RiempiDList(Me, par.OracleConn, "ddl_edificio5ex", "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID <> 1 ORDER BY DENOMINAZIONE ASC", "DENOMINAZIONE", "ID")
            ddl_edificio5ex.Items.Add(New ListItem("TUTTI", "-1"))
            ddl_edificio5ex.Items.FindByText("TUTTI").Selected = True



            CaricaComboPiani()

            'CONTROLLO SE PRESENTE ID_DOMANDA (DATI_PRE-SALVATI)










            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If




        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If




            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub

    Private Sub CaricaDatiNominativo()

        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If



            Select Case TIPO
                Case "ART.22 C.10"

                    par.cmd.CommandText = " SELECT DOMANDE_BANDO_VSA.DATA_PG AS DATA_PG, (COMP_NUCLEO_VSA.COGNOME || ' ' || COMP_NUCLEO_VSA.NOME) AS NOMINATIVO,  DOMANDE_BANDO_VSA.PG AS PROTOCOLLO " _
                        & "FROM DOMANDE_BANDO_VSA,COMP_NUCLEO_VSA,DICHIARAZIONI_VSA WHERE DOMANDE_BANDO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND COMP_NUCLEO_VSA.PROGR=0 AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND " _
                        & " (domande_bando_vsa.ID NOT IN ( " _
                        & "SELECT DISTINCT id_pratica " _
                        & "FROM rel_prat_all_ccaa_erp " _
                        & "WHERE esito <> 3 " _
                        & " AND esito <> 4 " _
                        & " AND ultimo = 1) " _
                        & "OR domande_bando_vsa.ID IN (SELECT DISTINCT id_pratica " _
                        & "FROM rel_prat_all_ccaa_erp " _
                        & " WHERE esito = 0 AND ultimo = 1)" _
                        & " ) " _
                        & "AND (domande_bando_vsa.fl_proposta = '0' " _
                        & "OR domande_bando_vsa.fl_proposta IS NULL " _
                        & " ) AND domande_bando_vsa.id_stato = '9' " _
                        & "AND DOMANDE_BANDO_VSA.ID=" & lIdDomanda

                    Dim myReaderZ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderZ.Read Then
                        lbl_datapg.Text = par.FormattaData(par.IfNull(myReaderZ("DATA_PG"), ""))
                        lbl_nominativo.Text = par.IfNull(myReaderZ("NOMINATIVO"), "")
                        lbl_protocollo.Text = par.IfNull(myReaderZ("PROTOCOLLO"), "")

                    End If

                    myReaderZ.Close()

                    lbl_tipobando.Text = par.IfNull(TIPO, "")

                Case "BANDO CAMBI"

                    Dim Ms_Stringa1 As String = "(BANDI_GRADUATORIA_DEF_cambi.Tipo = 0 or BANDI_GRADUATORIA_DEF_cambi.Tipo = 1) "
                    par.cmd.CommandText = "  SELECT DOMANDE_BANDO_CAMBI.DATA_PG AS DATA_PG, (COMP_NUCLEO_CAMBI.COGNOME || ' ' || COMP_NUCLEO_CAMBI.NOME) AS NOMINATIVO, DOMANDE_BANDO_CAMBI.PG AS PROTOCOLLO " _
                               & "FROM BANDI_GRADUATORIA_DEF_cambi,DOMANDE_BANDO_cambi,T_TIPO_PRATICHE,COMP_NUCLEO_cambi,DICHIARAZIONI_cambi " _
                               & " WHERE  domande_bando_cambi.fl_controlla_requisiti='2' " _
                               & "  and domande_bando_cambi.FL_ASS_ESTERNA='1' and DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND BANDI_GRADUATORIA_DEF_cambi.ID_DOMANDA=DOMANDE_BANDO_cambi.ID (+) AND " & Ms_Stringa1 _
                               & " and DOMANDE_BANDO_cambi.PROGR_COMPONENTE=COMP_NUCLEO_cambi.PROGR AND COMP_NUCLEO_cambi.ID_DICHIARAZIONE=DOMANDE_BANDO_cambi.ID_DICHIARAZIONE AND " _
                               & "  DOMANDE_BANDO_cambi.TIPO_PRATICA=T_TIPO_PRATICHE.COD (+) AND " _
                               & " (DOMANDE_BANDO_cambi.ID_STATO='9') AND (DOMANDE_BANDO_cambi.FL_PROPOSTA='0' OR DOMANDE_BANDO_cambi.FL_PROPOSTA IS NULL) " _
                               & "AND DOMANDE_BANDO_CAMBI.ID=" & lIdDomanda _
                               & " AND (DOMANDE_BANDO_cambi.ID NOT IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO<>3 and ESITO<>4 AND ULTIMO=1) OR DOMANDE_BANDO_cambi.ID IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO=0 AND ULTIMO=1))" _
                               & " ORDER BY domande_bando_cambi.isbarc_r desc"


                    Dim myReaderZ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderZ.Read Then
                        lbl_datapg.Text = par.FormattaData(par.IfNull(myReaderZ("DATA_PG"), ""))
                        lbl_nominativo.Text = par.IfNull(myReaderZ("NOMINATIVO"), "")
                        lbl_protocollo.Text = par.IfNull(myReaderZ("PROTOCOLLO"), "")

                    End If

                    myReaderZ.Close()

                    lbl_tipobando.Text = par.IfNull(TIPO, "")

                Case "Art14"

                    Dim Ms_Stringa1 As String = "BANDI_GRADUATORIA_DEF.Tipo = 1 "
                    par.cmd.CommandText = "  SELECT DOMANDE_BANDO.DATA_PG, (COMP_NUCLEO.COGNOME || ' ' || COMP_NUCLEO.NOME) AS NOMINATIVO,  DOMANDE_BANDO.PG AS PROTOCOLLO " _
                               & "FROM BANDI_GRADUATORIA_DEF,DOMANDE_BANDO,T_TIPO_PRATICHE,COMP_NUCLEO,DICHIARAZIONI " _
                               & " WHERE  domande_bando.fl_controlla_requisiti='2' " _
                               & " AND DOMANDE_BANDO.ID=" & lIdDomanda _
                               & "  and domande_bando.FL_ASS_ESTERNA='1' and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND BANDI_GRADUATORIA_DEF.ID_DOMANDA=DOMANDE_BANDO.ID (+) AND " & Ms_Stringa1 _
                               & " and DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND " _
                               & "  DOMANDE_BANDO.TIPO_PRATICA=T_TIPO_PRATICHE.COD (+) AND " _
                               & " (DOMANDE_BANDO.ID_STATO='9' or DOMANDE_BANDO.ID_STATO='8') AND (DOMANDE_BANDO.FL_PROPOSTA='0' OR DOMANDE_BANDO.FL_PROPOSTA IS NULL) " _
                               & " AND (DOMANDE_BANDO.ID NOT IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO<>3 and ESITO<>4 AND ULTIMO=1) OR DOMANDE_BANDO.ID IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO=0 AND ULTIMO=1))" _
                               & " ORDER BY domande_bando.isbarc_r desc"

                    Dim myReaderZ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderZ.Read Then
                        lbl_datapg.Text = par.FormattaData(par.IfNull(myReaderZ("DATA_PG"), ""))
                        lbl_nominativo.Text = par.IfNull(myReaderZ("NOMINATIVO"), "")
                        lbl_protocollo.Text = par.IfNull(myReaderZ("PROTOCOLLO"), "")

                    End If

                    myReaderZ.Close()

                    lbl_tipobando.Text = "ERP-Art14"

                Case "Art15"

                    Dim Ms_Stringa1 As String = "BANDI_GRADUATORIA_DEF.Tipo = 2"

                    par.cmd.CommandText = " SELECT DOMANDE_BANDO.DATA_PG AS DATA_PG, (COMP_NUCLEO.COGNOME || ' ' || COMP_NUCLEO.NOME) AS NOMINATIVO,  DOMANDE_BANDO.PG AS PROTOCOLLO " _
                               & " FROM DICHIARAZIONI,BANDI_GRADUATORIA_DEF,DOMANDE_BANDO,T_TIPO_PRATICHE,COMP_NUCLEO " _
                               & " WHERE  domande_bando.fl_controlla_requisiti='2' " _
                               & " and domande_bando.FL_ASS_ESTERNA='1' and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND BANDI_GRADUATORIA_DEF.ID_DOMANDA=DOMANDE_BANDO.ID (+) AND " & Ms_Stringa1 _
                               & " and DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND " _
                               & "  DOMANDE_BANDO.TIPO_PRATICA=T_TIPO_PRATICHE.COD (+) AND " _
                               & "  DOMANDE_BANDO.ID=" & lIdDomanda _
                               & " AND (DOMANDE_BANDO.FL_PROPOSTA=0 OR DOMANDE_BANDO.FL_PROPOSTA IS NULL) " _
                               & " AND (DOMANDE_BANDO.ID NOT IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO<>3 and ESITO<>4 AND ULTIMO=1) OR DOMANDE_BANDO.ID IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO=0 AND ULTIMO=1))" _
                               & " ORDER BY domande_bando.isbarc_r desc"

                    Dim myReaderZ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderZ.Read Then
                        lbl_datapg.Text = par.FormattaData(par.IfNull(myReaderZ("DATA_PG"), ""))
                        lbl_nominativo.Text = par.IfNull(myReaderZ("NOMINATIVO"), "")
                        lbl_protocollo.Text = par.IfNull(myReaderZ("PROTOCOLLO"), "")

                    End If

                    myReaderZ.Close()

                    lbl_tipobando.Text = "ERP-Art15"

                Case Else

                    Dim Ms_Stringa1 As String = "(BANDI_GRADUATORIA_DEF.Tipo = 0 or BANDI_GRADUATORIA_DEF.Tipo = 1) "
                    par.cmd.CommandText = "  SELECT DOMANDE_BANDO.DATA_PG AS DATA_PG, (COMP_NUCLEO.COGNOME || ' ' || COMP_NUCLEO.NOME) AS NOMINATIVO,  DOMANDE_BANDO.PG AS PROTOCOLLO " _
                               & "FROM BANDI_GRADUATORIA_DEF,DOMANDE_BANDO,T_TIPO_PRATICHE,COMP_NUCLEO,DICHIARAZIONI " _
                               & " WHERE  domande_bando.fl_controlla_requisiti='2' " _
                               & " AND DOMANDE_BANDO.ID=" & lIdDomanda _
                               & "  and domande_bando.FL_ASS_ESTERNA='1' and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND BANDI_GRADUATORIA_DEF.ID_DOMANDA=DOMANDE_BANDO.ID (+) AND " & Ms_Stringa1 _
                               & " and DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND " _
                               & "  DOMANDE_BANDO.TIPO_PRATICA=T_TIPO_PRATICHE.COD (+) AND " _
                               & " (DOMANDE_BANDO.ID_STATO='9' or DOMANDE_BANDO.ID_STATO='8') AND (DOMANDE_BANDO.FL_PROPOSTA='0' OR DOMANDE_BANDO.FL_PROPOSTA IS NULL) " _
                               & " AND (DOMANDE_BANDO.ID NOT IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO<>3 and ESITO<>4 AND ULTIMO=1) OR DOMANDE_BANDO.ID IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO=0 AND ULTIMO=1))" _
                               & " ORDER BY domande_bando.isbarc_r desc"

                    Dim myReaderZ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderZ.Read Then
                        lbl_datapg.Text = par.FormattaData(par.IfNull(myReaderZ("DATA_PG"), ""))
                        lbl_nominativo.Text = par.IfNull(myReaderZ("NOMINATIVO"), "")
                        lbl_protocollo.Text = par.IfNull(myReaderZ("PROTOCOLLO"), "")

                    End If

                    myReaderZ.Close()

                    lbl_tipobando.Text = "ERP-IDONEE"


            End Select

            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If


        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub


    Private Sub ScegliCarica()

        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim MyReaderJ As Oracle.DataAccess.Client.OracleDataReader
            Try
                Select Case TIPO
                    Case "ART.22 C.10"
                        par.cmd.CommandText = "SELECT  * FROM DOMANDE_PREFERENZE_VSA WHERE ID_DOMANDA=" & lIdDomanda & " FOR UPDATE NOWAIT"
                    Case "BANDO CAMBI"
                        par.cmd.CommandText = "SELECT  * FROM DOMANDE_PREFERENZE_CAMBI WHERE ID_DOMANDA=" & lIdDomanda & " FOR UPDATE NOWAIT"
                    Case Else
                        par.cmd.CommandText = "SELECT  * FROM DOMANDE_PREFERENZE WHERE ID_DOMANDA=" & lIdDomanda & " FOR UPDATE NOWAIT"
                End Select
                If Not String.IsNullOrEmpty(par.cmd.CommandText) Then
                    MyReaderJ = par.cmd.ExecuteReader
                    If MyReaderJ.Read Then
                        CaricaDati(MyReaderJ)
                    End If
                    MyReaderJ.Close()
                Else

                End If

                Select Case TIPO
                    Case "ART.22 C.10"
                        par.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_ESCL_VSA WHERE ID_DOMANDA=" & lIdDomanda & " FOR UPDATE NOWAIT"
                    Case "BANDO CAMBI"
                        par.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_ESCL_CAMBI WHERE ID_DOMANDA=" & lIdDomanda & " FOR UPDATE NOWAIT"
                    Case Else

                        par.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_ESCLUSIONI WHERE ID_DOMANDA=" & lIdDomanda & " FOR UPDATE NOWAIT"

                End Select
                If Not String.IsNullOrEmpty(par.cmd.CommandText) Then

                    MyReaderJ = par.cmd.ExecuteReader
                    If MyReaderJ.Read Then
                        CaricaDati2(MyReaderJ)
                    End If
                    MyReaderJ.Close()
                Else

                End If


                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                Session.Add("LAVORAZIONE", "1")


            Catch EX1 As Oracle.DataAccess.Client.OracleException
                Select Case TIPO
                    Case "ART.22 C.10"
                        par.cmd.CommandText = "SELECT  * FROM DOMANDE_PREFERENZE_VSA WHERE ID_DOMANDA=" & lIdDomanda
                    Case "BANDO CAMBI"
                        par.cmd.CommandText = "SELECT  * FROM DOMANDE_PREFERENZE_CAMBI WHERE ID_DOMANDA=" & lIdDomanda
                    Case Else
                        par.cmd.CommandText = "SELECT  * FROM DOMANDE_PREFERENZE WHERE ID_DOMANDA=" & lIdDomanda
                End Select
                If Not String.IsNullOrEmpty(par.cmd.CommandText) Then
                    MyReaderJ = par.cmd.ExecuteReader
                    If MyReaderJ.Read Then
                        CaricaDati(MyReaderJ)
                        FrmSolaLettura(Me)
                        btn_salva.Visible = False
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Attenzione! Pratica aperta da un altro utente. Non è possibile effettuare modifiche!');", True)
                    End If
                    MyReaderJ.Close()
                Else

                End If



                Select Case TIPO
                    Case "ART.22 C.10"
                        par.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_ESCL_VSA WHERE ID_DOMANDA=" & lIdDomanda
                    Case "BANDO CAMBI"
                        par.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_ESCL_CAMBI WHERE ID_DOMANDA=" & lIdDomanda
                    Case Else

                        par.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_ESCLUSIONI WHERE ID_DOMANDA=" & lIdDomanda

                End Select
                If Not String.IsNullOrEmpty(par.cmd.CommandText) Then

                    MyReaderJ = par.cmd.ExecuteReader
                    If MyReaderJ.Read Then
                        CaricaDati2(MyReaderJ)
                        FrmSolaLettura(Me)
                        btn_salva.Visible = False
                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Attenzione! Pratica aperta da un altro utente. Non è possibile effettuare modifiche!');", True)
                    End If
                    MyReaderJ.Close()
                Else

                End If



            End Try


          
        Catch ex As Exception
            'AGGIUNGERE ESCI
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            HttpContext.Current.Session.Remove("TRANSAZIONE")
            HttpContext.Current.Session.Remove("CONNESSIONE")
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaDati2(ByVal MyReaderJ As Oracle.DataAccess.Client.OracleDataReader)
        Try

             
                    ddl_localita1ex.SelectedIndex = -1
                    ddl_localita1ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_LOCALITA1"), -1), "-1")).Selected = True


                    If ddl_localita1ex.SelectedItem.Text <> "TUTTI" Then



                        ddl_zona1ex.Enabled = True
                        ddl_quartiere1ex.Enabled = True
                        ddl_indirizzo1ex.Enabled = True
                        ddl_complesso1ex.Enabled = True
                        ddl_edificio1ex.Enabled = True



                        ddl_localita2ex.Enabled = True


                        FiltroLocalita(ddl_localita1ex, ddl_quartiere1ex, ddl_indirizzo1ex, ddl_zona1ex, ddl_complesso1ex, ddl_edificio1ex)
                    Else




                        ddl_localita2ex.Enabled = False
                        ddl_zona2ex.Enabled = False
                        ddl_quartiere2ex.Enabled = False
                        ddl_indirizzo2ex.Enabled = False
                        ddl_complesso2ex.Enabled = False
                        ddl_edificio2ex.Enabled = False



                        ddl_localita3ex.Enabled = False
                        ddl_zona3ex.Enabled = False
                        ddl_quartiere3ex.Enabled = False
                        ddl_indirizzo3ex.Enabled = False
                        ddl_complesso3ex.Enabled = False
                        ddl_edificio3ex.Enabled = False


                        ddl_zona1ex.Enabled = False
                        ddl_quartiere1ex.Enabled = False
                        ddl_indirizzo1ex.Enabled = False
                        ddl_complesso1ex.Enabled = False
                        ddl_edificio1ex.Enabled = False



                        ddl_localita4ex.Enabled = False
                        ddl_zona4ex.Enabled = False
                        ddl_quartiere4ex.Enabled = False
                        ddl_indirizzo4ex.Enabled = False
                        ddl_complesso4ex.Enabled = False
                        ddl_edificio4ex.Enabled = False


                        ddl_localita5ex.Enabled = False
                        ddl_zona5ex.Enabled = False
                        ddl_quartiere5ex.Enabled = False
                        ddl_indirizzo5ex.Enabled = False
                        ddl_complesso5ex.Enabled = False
                        ddl_edificio5ex.Enabled = False



                    End If







                    ddl_localita2ex.SelectedIndex = -1
                    ddl_localita2ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_LOCALITA2"), -1), "-1")).Selected = True

                    If ddl_localita2ex.SelectedItem.Text <> "TUTTI" Then



                        ddl_zona2ex.Enabled = True
                        ddl_quartiere2ex.Enabled = True
                        ddl_indirizzo2ex.Enabled = True
                        ddl_complesso2ex.Enabled = True
                        ddl_edificio2ex.Enabled = True


                        ddl_localita3ex.Enabled = True


                        FiltroLocalita(ddl_localita2ex, ddl_quartiere2ex, ddl_indirizzo2ex, ddl_zona2ex, ddl_complesso2ex, ddl_edificio2ex)


                    Else



                        ddl_zona2ex.Enabled = False
                        ddl_quartiere2ex.Enabled = False
                        ddl_indirizzo2ex.Enabled = False
                        ddl_complesso2ex.Enabled = False
                        ddl_edificio2ex.Enabled = False


                        ddl_localita3ex.Enabled = False
                        ddl_zona3ex.Enabled = False
                        ddl_quartiere3ex.Enabled = False
                        ddl_indirizzo3ex.Enabled = False
                        ddl_complesso3ex.Enabled = False
                        ddl_edificio3ex.Enabled = False

                        ddl_localita4ex.Enabled = False
                        ddl_zona4ex.Enabled = False
                        ddl_quartiere4ex.Enabled = False
                        ddl_indirizzo4ex.Enabled = False
                        ddl_complesso4ex.Enabled = False
                        ddl_edificio4ex.Enabled = False



                        ddl_localita5ex.Enabled = False
                        ddl_zona5ex.Enabled = False
                        ddl_quartiere5ex.Enabled = False
                        ddl_indirizzo5ex.Enabled = False
                        ddl_complesso5ex.Enabled = False
                        ddl_edificio5ex.Enabled = False


                    End If









                    ddl_localita3ex.SelectedIndex = -1
                    ddl_localita3ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_LOCALITA3"), -1), "-1")).Selected = True


                    If ddl_localita3ex.SelectedItem.Text <> "TUTTI" Then

                        ddl_zona3ex.Enabled = True
                        ddl_quartiere3ex.Enabled = True
                        ddl_indirizzo3ex.Enabled = True
                        ddl_complesso3ex.Enabled = True
                        ddl_edificio3ex.Enabled = True


                        ddl_localita4ex.Enabled = True
                        FiltroLocalita(ddl_localita3ex, ddl_quartiere3ex, ddl_indirizzo3ex, ddl_zona3ex, ddl_complesso3ex, ddl_edificio3ex)

                    Else



                        ddl_zona3ex.Enabled = False
                        ddl_quartiere3ex.Enabled = False
                        ddl_indirizzo3ex.Enabled = False
                        ddl_complesso3ex.Enabled = False
                        ddl_edificio3ex.Enabled = False


                        ddl_localita4ex.Enabled = False
                        ddl_zona4ex.Enabled = False
                        ddl_quartiere4ex.Enabled = False
                        ddl_indirizzo4ex.Enabled = False
                        ddl_complesso4ex.Enabled = False
                        ddl_edificio4ex.Enabled = False



                        ddl_localita5ex.Enabled = False
                        ddl_zona5ex.Enabled = False
                        ddl_quartiere5ex.Enabled = False
                        ddl_indirizzo5ex.Enabled = False
                        ddl_complesso5ex.Enabled = False
                        ddl_edificio5ex.Enabled = False



                    End If




                    ddl_localita4ex.SelectedIndex = -1
                    ddl_localita4ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_LOCALITA4"), -1), "-1")).Selected = True


                    If ddl_localita4ex.SelectedItem.Text <> "TUTTI" Then

                        ddl_zona4ex.Enabled = True
                        ddl_quartiere4ex.Enabled = True
                        ddl_indirizzo4ex.Enabled = True
                        ddl_complesso4ex.Enabled = True
                        ddl_edificio4ex.Enabled = True


                        ddl_localita5ex.Enabled = True
                        FiltroLocalita(ddl_localita4ex, ddl_quartiere4ex, ddl_indirizzo4ex, ddl_zona4ex, ddl_complesso4ex, ddl_edificio4ex)

                    Else






                        '   ddl_localita4ex.Enabled = False
                        ddl_zona4ex.Enabled = False
                        ddl_quartiere4ex.Enabled = False
                        ddl_indirizzo4ex.Enabled = False
                        ddl_complesso4ex.Enabled = False
                        ddl_edificio4ex.Enabled = False



                        ddl_localita5ex.Enabled = False
                        ddl_zona5ex.Enabled = False
                        ddl_quartiere5ex.Enabled = False
                        ddl_indirizzo5ex.Enabled = False
                        ddl_complesso5ex.Enabled = False
                        ddl_edificio5ex.Enabled = False



                    End If



                    ddl_localita5ex.SelectedIndex = -1
                    ddl_localita5ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_LOCALITA5"), -1), "-1")).Selected = True


                    If ddl_localita5ex.SelectedItem.Text <> "TUTTI" Then

                        ddl_zona5ex.Enabled = True
                        ddl_quartiere5ex.Enabled = True
                        ddl_indirizzo5ex.Enabled = True
                        ddl_complesso5ex.Enabled = True
                        ddl_edificio5ex.Enabled = True


                        FiltroLocalita(ddl_localita5ex, ddl_quartiere5ex, ddl_indirizzo5ex, ddl_zona5ex, ddl_complesso5ex, ddl_edificio5ex)

                    Else





                        '  ddl_localita5ex.Enabled = False
                        ddl_zona5ex.Enabled = False
                        ddl_quartiere5ex.Enabled = False
                        ddl_indirizzo5ex.Enabled = False
                        ddl_complesso5ex.Enabled = False
                        ddl_edificio5ex.Enabled = False



                    End If






                    ddl_zona1ex.SelectedIndex = -1
                    ddl_zona1ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_ZONA1"), -1), "-1")).Selected = True

                    ddl_zona2ex.SelectedIndex = -1
                    ddl_zona2ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_ZONA2"), -1), "-1")).Selected = True

                    ddl_zona3ex.SelectedIndex = -1
                    ddl_zona3ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_ZONA3"), -1), "-1")).Selected = True

                    ddl_zona4ex.SelectedIndex = -1
                    ddl_zona4ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_ZONA4"), -1), "-1")).Selected = True

                    ddl_zona5ex.SelectedIndex = -1
                    ddl_zona5ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_ZONA5"), -1), "-1")).Selected = True



                    ddl_quartiere1ex.SelectedIndex = -1
                    ddl_quartiere1ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_QUART1"), -1), "-1")).Selected = True

                    ddl_quartiere2ex.SelectedIndex = -1
                    ddl_quartiere2ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_QUART2"), -1), "-1")).Selected = True

                    ddl_quartiere3ex.SelectedIndex = -1
                    ddl_quartiere3ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_QUART3"), -1), "-1")).Selected = True

                    ddl_quartiere4ex.SelectedIndex = -1
                    ddl_quartiere4ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_QUART4"), -1), "-1")).Selected = True

                    ddl_quartiere5ex.SelectedIndex = -1
                    ddl_quartiere5ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_QUART5"), -1), "-1")).Selected = True


                    ddl_indirizzo1ex.SelectedIndex = -1
                    ddl_indirizzo1ex.Items.FindByText(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_INDIRIZZO1"), " "), "TUTTI")).Selected = True

                    ddl_indirizzo2ex.SelectedIndex = -1
                    ddl_indirizzo2ex.Items.FindByText(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_INDIRIZZO2"), " "), "TUTTI")).Selected = True

                    ddl_indirizzo3ex.SelectedIndex = -1
                    ddl_indirizzo3ex.Items.FindByText(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_INDIRIZZO3"), " "), "TUTTI")).Selected = True

                    ddl_indirizzo4ex.SelectedIndex = -1
                    ddl_indirizzo4ex.Items.FindByText(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_INDIRIZZO4"), " "), "TUTTI")).Selected = True

                    ddl_indirizzo5ex.SelectedIndex = -1
                    ddl_indirizzo5ex.Items.FindByText(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_INDIRIZZO5"), " "), "TUTTI")).Selected = True


                    ddl_complesso1ex.SelectedIndex = -1
                    ddl_complesso1ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_COMPLESSO1"), -1), "-1")).Selected = True

                    ddl_complesso2ex.SelectedIndex = -1
                    ddl_complesso2ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_COMPLESSO2"), -1), "-1")).Selected = True

                    ddl_complesso3ex.SelectedIndex = -1
                    ddl_complesso3ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_COMPLESSO3"), -1), "-1")).Selected = True

                    ddl_complesso4ex.SelectedIndex = -1
                    ddl_complesso4ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_COMPLESSO4"), -1), "-1")).Selected = True

                    ddl_complesso5ex.SelectedIndex = -1
                    ddl_complesso5ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_COMPLESSO5"), -1), "-1")).Selected = True


                    ddl_edificio1ex.SelectedIndex = -1
                    ddl_edificio1ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_EDIFICIO1"), -1), "-1")).Selected = True

                    ddl_edificio2ex.SelectedIndex = -1
                    ddl_edificio2ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_EDIFICIO2"), -1), "-1")).Selected = True

                    ddl_edificio3ex.SelectedIndex = -1
                    ddl_edificio3ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_EDIFICIO3"), -1), "-1")).Selected = True

                    ddl_edificio4ex.SelectedIndex = -1
                    ddl_edificio4ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_EDIFICIO4"), -1), "-1")).Selected = True

                    ddl_edificio5ex.SelectedIndex = -1
                    ddl_edificio5ex.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("ESCL_EDIFICIO5"), -1), "-1")).Selected = True




            

        Catch ex As Exception

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            HttpContext.Current.Session.Remove("TRANSAZIONE")
            HttpContext.Current.Session.Remove("CONNESSIONE")
            Session.Item("LAVORAZIONE") = "0"

        End Try
    End Sub



    Private Sub CaricaDati(ByVal MyReaderJ As Oracle.DataAccess.Client.OracleDataReader)

        Try
  

                   
                        ddl_localita1.SelectedIndex = -1
                        ddl_localita1.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_LOCALITA1"), -1), "-1")).Selected = True

                        If ddl_localita1.SelectedItem.Text <> "TUTTI" Then


                            ddl_zona1.Enabled = True
                            ddl_quartiere1.Enabled = True
                            ddl_indirizzo1.Enabled = True
                            ddl_complesso1.Enabled = True
                            ddl_edificio1.Enabled = True




                            ddl_localita2.Enabled = True

                            FiltroLocalita(ddl_localita1, ddl_quartiere1, ddl_indirizzo1, ddl_zona1, ddl_complesso1, ddl_edificio1)
                        Else




                            ddl_zona1.Enabled = False
                            ddl_quartiere1.Enabled = False
                            ddl_indirizzo1.Enabled = False
                            ddl_complesso1.Enabled = False
                            ddl_edificio1.Enabled = False


                            ddl_localita2.Enabled = False
                            ddl_zona2.Enabled = False
                            ddl_quartiere2.Enabled = False
                            ddl_indirizzo2.Enabled = False
                            ddl_complesso2.Enabled = False
                            ddl_edificio2.Enabled = False

                            ddl_localita3.Enabled = False
                            ddl_zona3.Enabled = False
                            ddl_quartiere3.Enabled = False
                            ddl_indirizzo3.Enabled = False
                            ddl_complesso3.Enabled = False
                            ddl_edificio3.Enabled = False


                            ddl_localita4.Enabled = False
                            ddl_zona4.Enabled = False
                            ddl_quartiere4.Enabled = False
                            ddl_indirizzo4.Enabled = False
                            ddl_complesso4.Enabled = False
                            ddl_edificio4.Enabled = False


                            ddl_localita5.Enabled = False
                            ddl_zona5.Enabled = False
                            ddl_quartiere5.Enabled = False
                            ddl_indirizzo5.Enabled = False
                            ddl_complesso5.Enabled = False
                            ddl_edificio5.Enabled = False


                        End If




                        ddl_localita2.SelectedIndex = -1
                        ddl_localita2.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_LOCALITA2"), -1), "-1")).Selected = True

                        If ddl_localita2.SelectedItem.Text <> "TUTTI" Then


                            ddl_localita2.Enabled = True
                            ddl_zona2.Enabled = True
                            ddl_quartiere2.Enabled = True
                            ddl_indirizzo2.Enabled = True
                            ddl_complesso2.Enabled = True
                            ddl_edificio2.Enabled = True




                            ddl_localita3.Enabled = True

                            FiltroLocalita(ddl_localita2, ddl_quartiere2, ddl_indirizzo2, ddl_zona2, ddl_complesso2, ddl_edificio2)

                        Else


                            ddl_zona2.Enabled = False
                            ddl_quartiere2.Enabled = False
                            ddl_indirizzo2.Enabled = False
                            ddl_complesso2.Enabled = False
                            ddl_edificio2.Enabled = False


                            ddl_localita3.Enabled = False
                            ddl_zona3.Enabled = False
                            ddl_quartiere3.Enabled = False
                            ddl_indirizzo3.Enabled = False
                            ddl_complesso3.Enabled = False
                            ddl_edificio3.Enabled = False


                            ddl_localita4.Enabled = False
                            ddl_zona4.Enabled = False
                            ddl_quartiere4.Enabled = False
                            ddl_indirizzo4.Enabled = False
                            ddl_complesso4.Enabled = False
                            ddl_edificio4.Enabled = False


                            ddl_localita5.Enabled = False
                            ddl_zona5.Enabled = False
                            ddl_quartiere5.Enabled = False
                            ddl_indirizzo5.Enabled = False
                            ddl_complesso5.Enabled = False
                            ddl_edificio5.Enabled = False






                        End If


                        ddl_localita3.SelectedIndex = -1
                        ddl_localita3.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_LOCALITA3"), -1), "-1")).Selected = True




                        If ddl_localita3.SelectedItem.Text <> "TUTTI" Then

                            ddl_zona3.Enabled = True
                            ddl_quartiere3.Enabled = True
                            ddl_indirizzo3.Enabled = True
                            ddl_complesso3.Enabled = True
                            ddl_edificio3.Enabled = True

                            ddl_localita4.Enabled = True

                            FiltroLocalita(ddl_localita3, ddl_quartiere3, ddl_indirizzo3, ddl_zona3, ddl_complesso3, ddl_edificio3)

                        Else

                            ddl_zona3.Enabled = False
                            ddl_quartiere3.Enabled = False
                            ddl_indirizzo3.Enabled = False
                            ddl_complesso3.Enabled = False
                            ddl_edificio3.Enabled = False

                            ddl_localita4.Enabled = False
                            ddl_zona4.Enabled = False
                            ddl_quartiere4.Enabled = False
                            ddl_indirizzo4.Enabled = False
                            ddl_complesso4.Enabled = False
                            ddl_edificio4.Enabled = False


                            ddl_localita5.Enabled = False
                            ddl_zona5.Enabled = False
                            ddl_quartiere5.Enabled = False
                            ddl_indirizzo5.Enabled = False
                            ddl_complesso5.Enabled = False
                            ddl_edificio5.Enabled = False



                        End If




                        ddl_localita4.SelectedIndex = -1
                        ddl_localita4.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_LOCALITA4"), -1), "-1")).Selected = True




                        If ddl_localita4.SelectedItem.Text <> "TUTTI" Then

                            ddl_zona4.Enabled = True
                            ddl_quartiere4.Enabled = True
                            ddl_indirizzo4.Enabled = True
                            ddl_complesso4.Enabled = True
                            ddl_edificio4.Enabled = True

                            ddl_localita5.Enabled = True

                            FiltroLocalita(ddl_localita4, ddl_quartiere4, ddl_indirizzo4, ddl_zona4, ddl_complesso4, ddl_edificio4)

                        Else



                            '  ddl_localita4.Enabled = False
                            ddl_zona4.Enabled = False
                            ddl_quartiere4.Enabled = False
                            ddl_indirizzo4.Enabled = False
                            ddl_complesso4.Enabled = False
                            ddl_edificio4.Enabled = False


                            ddl_localita5.Enabled = False
                            ddl_zona5.Enabled = False
                            ddl_quartiere5.Enabled = False
                            ddl_indirizzo5.Enabled = False
                            ddl_complesso5.Enabled = False
                            ddl_edificio5.Enabled = False



                        End If



                        ddl_localita5.SelectedIndex = -1
                        ddl_localita5.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_LOCALITA5"), -1), "-1")).Selected = True




                        If ddl_localita5.SelectedItem.Text <> "TUTTI" Then

                            ddl_zona5.Enabled = True
                            ddl_quartiere5.Enabled = True
                            ddl_indirizzo5.Enabled = True
                            ddl_complesso5.Enabled = True
                            ddl_edificio5.Enabled = True



                            FiltroLocalita(ddl_localita5, ddl_quartiere5, ddl_indirizzo5, ddl_zona5, ddl_complesso5, ddl_edificio5)

                        Else




                            '    ddl_localita5.Enabled = False
                            ddl_zona5.Enabled = False
                            ddl_quartiere5.Enabled = False
                            ddl_indirizzo5.Enabled = False
                            ddl_complesso5.Enabled = False
                            ddl_edificio5.Enabled = False



                        End If



                        ddl_zona1.SelectedIndex = -1
                        ddl_zona1.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_ZONA1"), -1), "-1")).Selected = True

                        ddl_zona2.SelectedIndex = -1
                        ddl_zona2.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_ZONA2"), -1), "-1")).Selected = True

                        ddl_zona3.SelectedIndex = -1
                        ddl_zona3.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_ZONA3"), -1), "-1")).Selected = True

                        ddl_zona4.SelectedIndex = -1
                        ddl_zona4.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_ZONA4"), -1), "-1")).Selected = True

                        ddl_zona5.SelectedIndex = -1
                        ddl_zona5.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_ZONA5"), -1), "-1")).Selected = True

                        ddl_quartiere1.SelectedIndex = -1
                        ddl_quartiere1.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_QUART1"), -1), "-1")).Selected = True

                        ddl_quartiere2.SelectedIndex = -1
                        ddl_quartiere2.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_QUART2"), -1), "-1")).Selected = True

                        ddl_quartiere3.SelectedIndex = -1
                        ddl_quartiere3.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_QUART3"), -1), "-1")).Selected = True

                        ddl_quartiere4.SelectedIndex = -1
                        ddl_quartiere4.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_QUART4"), -1), "-1")).Selected = True

                        ddl_quartiere5.SelectedIndex = -1
                        ddl_quartiere5.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_QUART5"), -1), "-1")).Selected = True

                        ddl_indirizzo1.SelectedIndex = -1
                        ddl_indirizzo1.Items.FindByText(par.IfEmpty(par.IfNull(MyReaderJ("PREF_INDIRIZZO1"), ""), "TUTTI")).Selected = True

                        ddl_indirizzo2.SelectedIndex = -1
                        ddl_indirizzo2.Items.FindByText(par.IfEmpty(par.IfNull(MyReaderJ("PREF_INDIRIZZO2"), ""), "TUTTI")).Selected = True

                        ddl_indirizzo4.SelectedIndex = -1
                        ddl_indirizzo4.Items.FindByText(par.IfEmpty(par.IfNull(MyReaderJ("PREF_INDIRIZZO4"), ""), "TUTTI")).Selected = True

                        ddl_indirizzo5.SelectedIndex = -1
                        ddl_indirizzo5.Items.FindByText(par.IfEmpty(par.IfNull(MyReaderJ("PREF_INDIRIZZO5"), ""), "TUTTI")).Selected = True

                        ddl_indirizzo3.SelectedIndex = -1
                        ddl_indirizzo3.Items.FindByText(par.IfEmpty(par.IfNull(MyReaderJ("PREF_INDIRIZZO3"), ""), "TUTTI")).Selected = True


                        ddl_complesso1.SelectedIndex = -1
                        ddl_complesso1.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_COMPLESSO1"), -1), "-1")).Selected = True

                        ddl_complesso2.SelectedIndex = -1
                        ddl_complesso2.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_COMPLESSO2"), -1), "-1")).Selected = True

                        ddl_complesso3.SelectedIndex = -1
                        ddl_complesso3.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_COMPLESSO3"), -1), "-1")).Selected = True

                        ddl_complesso4.SelectedIndex = -1
                        ddl_complesso4.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_COMPLESSO4"), -1), "-1")).Selected = True

                        ddl_complesso5.SelectedIndex = -1
                        ddl_complesso5.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_COMPLESSO5"), -1), "-1")).Selected = True


                        ddl_edificio1.SelectedIndex = -1
                        ddl_edificio1.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_EDIFICIO1"), -1), "-1")).Selected = True

                        ddl_edificio2.SelectedIndex = -1
                        ddl_edificio2.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_EDIFICIO2"), -1), "-1")).Selected = True

                        ddl_edificio3.SelectedIndex = -1
                        ddl_edificio3.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_EDIFICIO3"), -1), "-1")).Selected = True

                        ddl_edificio4.SelectedIndex = -1
                        ddl_edificio4.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_EDIFICIO4"), -1), "-1")).Selected = True

                        ddl_edificio5.SelectedIndex = -1
                        ddl_edificio5.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_EDIFICIO5"), -1), "-1")).Selected = True






            ddl_pianodaCon.SelectedIndex = -1
            ddl_pianodaCon.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_PIANI_DA_CON"), -1), "-1")).Selected = True

            If ddl_pianodaCon.SelectedItem.Text <> "TUTTI" Then
                ddl_pianoaCon.Enabled = True
            Else
                ddl_pianoaCon.Enabled = False
            End If


            ddl_pianoaCon.SelectedIndex = -1
            ddl_pianoaCon.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_PIANI_A_CON"), -1), "-1")).Selected = True


            ddl_pianodaSenza.SelectedIndex = -1
            ddl_pianodaSenza.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_PIANI_DA_SENZA"), -1), "-1")).Selected = True

            If ddl_pianodaSenza.SelectedItem.Text <> "TUTTI" Then
                ddl_pianoaSenza.Enabled = True
            Else
                ddl_pianoaSenza.Enabled = False
            End If


            ddl_pianoaSenza.SelectedIndex = -1
            ddl_pianoaSenza.Items.FindByValue(par.IfEmpty(par.IfNull(MyReaderJ("PREF_PIANI_A_SENZA"), -1), "-1")).Selected = True







                        txt_supMin.Text = par.PuntiInVirgole(par.IfNull(MyReaderJ("PREF_SUP_MIN"), ""))

                        If txt_supMin.Text <> "" Then

                            txt_supMin.Text = FormatNumber(txt_supMin.Text, 2)

                        End If

                        txt_supMax.Text = par.PuntiInVirgole(par.IfNull(MyReaderJ("PREF_SUP_MAX"), ""))

                        If txt_supMax.Text <> "" Then

                            txt_supMax.Text = FormatNumber(txt_supMax.Text, 2)

                        End If




                        If par.IfNull(MyReaderJ("PREF_CONDOMINIO"), "0") = "1" Then
                            chk_condominio.Checked = True
                        Else
                            chk_condominio.Checked = False
                        End If


                        If par.IfNull(MyReaderJ("PREF_BARRIERE"), "0") = "1" Then
                            chk_barr.Checked = True
                        Else
                            chk_barr.Checked = False
                        End If


                        txt_note.Text = par.IfNull(MyReaderJ("PREF_NOTE"), "")


                   
                 














            ''*********************CHIUSURA CONNESSIONE**********************


            '    If par.OracleConn.State = Data.ConnectionState.Open Then
            '        par.OracleConn.Close()
            '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '    End If


            'Catch EX1 As Oracle.DataAccess.Client.OracleException
            '    If EX1.Number = 54 Then



            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Attenzione! Pratica aperta da un altro utente. Non è possibile effettuare modifiche!');", True)

            '        '   CaricaDati()
            '        FrmSolaLettura(Me)
            '        btn_salva.Visible = False

            '        Session.Item("LAVORAZIONE") = "0"


            '    End If




        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub



    Public Property lIdDomanda() As String
        Get
            If Not (ViewState("par_lIdDomanda") Is Nothing) Then
                Return CStr(ViewState("par_lIdDomanda"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdDomanda") = value
        End Set

    End Property

    Public Property TIPO() As String
        Get
            If Not (ViewState("par_TIPO") Is Nothing) Then
                Return CStr(ViewState("par_TIPO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_TIPO") = value
        End Set

    End Property




    Protected Sub btn_salva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_salva.Click

        Try


            If Not IsNothing(CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)) Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If

            If Not IsNothing(CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)) Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            End If





            frmModify.Value = 0

            'controllo opzioni aree geografiche preferenza/escluse uguali

            If ddlConfronto(ddl_localita1, ddl_quartiere1, ddl_localita1ex, ddl_quartiere1ex) <> 0 Or ddlConfronto(ddl_localita1, ddl_quartiere1, ddl_localita2ex, ddl_quartiere2ex) <> 0 Or ddlConfronto(ddl_localita1, ddl_quartiere1, ddl_localita3ex, ddl_quartiere3ex) <> 0 Or ddlConfronto(ddl_localita1, ddl_quartiere1, ddl_localita4ex, ddl_quartiere4ex) <> 0 Or ddlConfronto(ddl_localita1, ddl_quartiere1, ddl_localita5ex, ddl_quartiere5ex) <> 0 Then

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Attenzione! Una o più opzioni relative alle posizioni geografiche di preferenza e alle posizioni geografiche escluse sono uguali! ');", True)

                Exit Sub

            End If

            If ddlConfronto(ddl_localita2, ddl_quartiere2, ddl_localita1ex, ddl_quartiere1ex) <> 0 Or ddlConfronto(ddl_localita2, ddl_quartiere2, ddl_localita2ex, ddl_quartiere2ex) <> 0 Or ddlConfronto(ddl_localita2, ddl_quartiere2, ddl_localita3ex, ddl_quartiere3ex) <> 0 Or ddlConfronto(ddl_localita2, ddl_quartiere2, ddl_localita4ex, ddl_quartiere4ex) <> 0 Or ddlConfronto(ddl_localita2, ddl_quartiere2, ddl_localita5ex, ddl_quartiere5ex) <> 0 Then

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Attenzione! Una o più opzioni relative alle posizioni geografiche di preferenza e alle posizioni geografiche escluse sono uguali! ');", True)

                Exit Sub

            End If

            If ddlConfronto(ddl_localita3, ddl_quartiere3, ddl_localita1ex, ddl_quartiere1ex) <> 0 Or ddlConfronto(ddl_localita3, ddl_quartiere3, ddl_localita2ex, ddl_quartiere2ex) <> 0 Or ddlConfronto(ddl_localita3, ddl_quartiere3, ddl_localita3ex, ddl_quartiere3ex) <> 0 Or ddlConfronto(ddl_localita3, ddl_quartiere3, ddl_localita4ex, ddl_quartiere4ex) <> 0 Or ddlConfronto(ddl_localita3, ddl_quartiere3, ddl_localita5ex, ddl_quartiere5ex) <> 0 Then

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Attenzione! Una o più opzioni relative alle posizioni geografiche di preferenza e alle posizioni geografiche escluse sono uguali! ');", True)

                Exit Sub

            End If


            If ddlConfronto(ddl_localita4, ddl_quartiere4, ddl_localita1ex, ddl_quartiere1ex) <> 0 Or ddlConfronto(ddl_localita4, ddl_quartiere4, ddl_localita2ex, ddl_quartiere2ex) <> 0 Or ddlConfronto(ddl_localita4, ddl_quartiere4, ddl_localita3ex, ddl_quartiere3ex) <> 0 Or ddlConfronto(ddl_localita4, ddl_quartiere4, ddl_localita4ex, ddl_quartiere4ex) <> 0 Or ddlConfronto(ddl_localita4, ddl_quartiere4, ddl_localita5ex, ddl_quartiere5ex) <> 0 Then

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Attenzione! Una o più opzioni relative alle posizioni geografiche di preferenza e alle posizioni geografiche escluse sono uguali! ');", True)

                Exit Sub

            End If



            If ddlConfronto(ddl_localita5, ddl_quartiere5, ddl_localita1ex, ddl_quartiere1ex) <> 0 Or ddlConfronto(ddl_localita5, ddl_quartiere5, ddl_localita2ex, ddl_quartiere2ex) <> 0 Or ddlConfronto(ddl_localita5, ddl_quartiere5, ddl_localita3ex, ddl_quartiere3ex) <> 0 Or ddlConfronto(ddl_localita5, ddl_quartiere5, ddl_localita4ex, ddl_quartiere4ex) <> 0 Or ddlConfronto(ddl_localita5, ddl_quartiere5, ddl_localita5ex, ddl_quartiere5ex) <> 0 Then

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Attenzione! Una o più opzioni relative alle posizioni geografiche di preferenza e alle posizioni geografiche escluse sono uguali! ');", True)

                Exit Sub

            End If







            '  controllo opzioni piani esclusi con/senza ascensore uguali

            If ddlConfrontoPiani(ddl_pianodaCon, ddl_pianoaCon) <> 0 Or ddlConfrontoPiani(ddl_pianodaSenza, ddl_pianoaSenza) <> 0 Then

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Attenzione! Una o più opzioni relative ai piani esclusi con/senza ascensore sono uguali! ');", True)

                Exit Sub

            End If


     


            If txt_supMin.Text <> "" And txt_supMax.Text <> "" Then

                If CDec(txt_supMin.Text) > CDec(txt_supMax.Text) Then

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Attenzione! La superficie minima non può essere maggiore della superficie massima di preferenza! ');", True)

                    Exit Sub
                End If
            End If







            If ddlLocalitaVuota(ddl_localita1, ddl_quartiere1, ddl_indirizzo1, ddl_zona1, ddl_complesso1, ddl_edificio1) <> 0 Or ddlLocalitaVuota(ddl_localita2, ddl_quartiere2, ddl_indirizzo2, ddl_zona2, ddl_complesso2, ddl_edificio2) <> 0 Or ddlLocalitaVuota(ddl_localita3, ddl_quartiere3, ddl_indirizzo3, ddl_zona3, ddl_complesso3, ddl_edificio3) <> 0 Or ddlLocalitaVuota(ddl_localita4, ddl_quartiere4, ddl_indirizzo4, ddl_zona4, ddl_complesso4, ddl_edificio4) <> 0 Or ddlLocalitaVuota(ddl_localita5, ddl_quartiere5, ddl_indirizzo5, ddl_zona5, ddl_complesso5, ddl_edificio5) <> 0 Or ddlLocalitaVuota(ddl_localita1ex, ddl_quartiere1ex, ddl_indirizzo1ex, ddl_zona1ex, ddl_complesso1ex, ddl_edificio1ex) <> 0 Or ddlLocalitaVuota(ddl_localita2ex, ddl_quartiere2ex, ddl_indirizzo2ex, ddl_zona2ex, ddl_complesso2ex, ddl_edificio2ex) <> 0 Or ddlLocalitaVuota(ddl_localita3ex, ddl_quartiere3ex, ddl_indirizzo3ex, ddl_zona3ex, ddl_complesso3ex, ddl_edificio3ex) <> 0 Or ddlLocalitaVuota(ddl_localita4ex, ddl_quartiere4ex, ddl_indirizzo4ex, ddl_zona4ex, ddl_complesso4ex, ddl_edificio4ex) <> 0 Or ddlLocalitaVuota(ddl_localita5ex, ddl_quartiere5ex, ddl_indirizzo5ex, ddl_zona5ex, ddl_complesso5ex, ddl_edificio5ex) <> 0 Then

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Attenzione! E\' necessario inserire la località e/o il quartiere di preferenza! ');", True)
                Exit Sub

            End If




            'controllo piani da/a

            If controlloRangePiani(ddl_pianodaSenza.SelectedItem.Text, ddl_pianoaSenza.SelectedItem.Text) = 1 Or controlloRangePiani(ddl_pianodaCon.SelectedItem.Text, ddl_pianoaCon.SelectedItem.Text) = 1 Then
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "alert('Il range di preferenza dei piani non è valido! L\'opzione \'da\' deve essere maggiore dell\'opzione \'a\'');", True)
                Exit Sub
            End If






            '-----------------SALVATAGGIO




            Dim strSQL As String = ""
            Select Case TIPO
                Case "ART.22 C.10"
                    par.cmd.CommandText = "SELECT  * FROM DOMANDE_PREFERENZE_VSA WHERE ID_DOMANDA=" & lIdDomanda
                    myreaderj = par.cmd.executereader()
                    If myReaderJ.Read Then




                        If ddl_localita1.SelectedValue <> "-1" Then
                            strSQL = "PREF_LOCALITA1=" & ddl_localita1.SelectedValue & " ,"
                        Else
                            strSQL = "PREF_LOCALITA1= NULL ,"
                        End If

                        If ddl_localita2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_LOCALITA2=" & ddl_localita2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_LOCALITA2= NULL ,"
                        End If

                        If ddl_localita3.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_LOCALITA3=" & ddl_localita3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_LOCALITA3= NULL ,"
                        End If

                        If ddl_localita4.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_LOCALITA4=" & ddl_localita4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_LOCALITA4= NULL ,"
                        End If

                        If ddl_localita5.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_LOCALITA5=" & ddl_localita5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_LOCALITA5= NULL ,"
                        End If



                        If ddl_zona1.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_ZONA1=" & ddl_zona1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_ZONA1= NULL ,"
                        End If

                        If ddl_zona2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_ZONA2=" & ddl_zona2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_ZONA2= NULL ,"
                        End If

                        If ddl_zona3.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_ZONA3=" & ddl_zona3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_ZONA3= NULL ,"
                        End If

                        If ddl_zona4.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_ZONA4=" & ddl_zona4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_ZONA4= NULL ,"
                        End If

                        If ddl_zona5.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_ZONA5=" & ddl_zona5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_ZONA5= NULL ,"
                        End If



                        If ddl_quartiere1.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_QUART1=" & ddl_quartiere1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_QUART1= NULL ,"
                        End If

                        If ddl_quartiere2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_QUART2=" & ddl_quartiere2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_QUART2= NULL ,"
                        End If

                        If ddl_quartiere3.SelectedValue <> -1 Then
                            strSQL = strSQL & "PREF_QUART3=" & ddl_quartiere3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_QUART3= NULL ,"
                        End If

                        If ddl_quartiere4.SelectedValue <> -1 Then
                            strSQL = strSQL & "PREF_QUART4=" & ddl_quartiere4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_QUART4= NULL ,"
                        End If

                        If ddl_quartiere5.SelectedValue <> -1 Then
                            strSQL = strSQL & "PREF_QUART5=" & ddl_quartiere5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_QUART5= NULL ,"
                        End If


                        If ddl_indirizzo1.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_INDIRIZZO1='" & par.PulisciStrSql(ddl_indirizzo1.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "PREF_INDIRIZZO1= NULL ,"
                        End If

                        If ddl_indirizzo2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_INDIRIZZO2='" & par.PulisciStrSql(ddl_indirizzo2.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "PREF_INDIRIZZO2= NULL ,"
                        End If

                        If ddl_indirizzo3.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_INDIRIZZO3='" & par.PulisciStrSql(ddl_indirizzo3.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "PREF_INDIRIZZO3= NULL ,"
                        End If

                        If ddl_indirizzo4.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_INDIRIZZO4='" & par.PulisciStrSql(ddl_indirizzo4.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "PREF_INDIRIZZO4= NULL ,"
                        End If

                        If ddl_indirizzo5.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_INDIRIZZO5='" & par.PulisciStrSql(ddl_indirizzo5.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "PREF_INDIRIZZO5= NULL ,"
                        End If



                        If ddl_complesso1.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_COMPLESSO1=" & ddl_complesso1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_COMPLESSO1= NULL ,"
                        End If

                        If ddl_complesso2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_COMPLESSO2=" & ddl_complesso2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_COMPLESSO2= NULL ,"
                        End If

                        If ddl_complesso3.SelectedValue <> -1 Then
                            strSQL = strSQL & "PREF_COMPLESSO3=" & ddl_complesso3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_COMPLESSO3= NULL ,"
                        End If

                        If ddl_complesso4.SelectedValue <> -1 Then
                            strSQL = strSQL & "PREF_COMPLESSO4=" & ddl_complesso4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_COMPLESSO4= NULL ,"
                        End If

                        If ddl_complesso5.SelectedValue <> -1 Then
                            strSQL = strSQL & "PREF_COMPLESSO5=" & ddl_complesso5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_COMPLESSO5= NULL ,"
                        End If







                        If ddl_edificio1.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_EDIFICIO1=" & ddl_edificio1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_EDIFICIO1= NULL ,"
                        End If

                        If ddl_edificio2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_EDIFICIO2=" & ddl_edificio2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_EDIFICIO2= NULL ,"
                        End If

                        If ddl_edificio3.SelectedValue <> -1 Then
                            strSQL = strSQL & "PREF_EDIFICIO3=" & ddl_edificio3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_EDIFICIO3= NULL ,"
                        End If

                        If ddl_edificio4.SelectedValue <> -1 Then
                            strSQL = strSQL & "PREF_EDIFICIO4=" & ddl_edificio4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_EDIFICIO4= NULL ,"
                        End If

                        If ddl_edificio5.SelectedValue <> -1 Then
                            strSQL = strSQL & "PREF_EDIFICIO5=" & ddl_edificio5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_EDIFICIO5= NULL ,"
                        End If




                        If ddl_pianodaCon.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_PIANI_DA_CON='" & ddl_pianodaCon.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & "PREF_PIANI_DA_CON= NULL ,"
                        End If



                        If ddl_pianoaCon.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_PIANI_A_CON='" & ddl_pianoaCon.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & "PREF_PIANI_A_CON= NULL ,"
                        End If


                        If ddl_pianodaSenza.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_PIANI_DA_SENZA='" & ddl_pianodaSenza.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & "PREF_PIANI_DA_SENZA= NULL ,"
                        End If



                        If ddl_pianoaSenza.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_PIANI_A_SENZA='" & ddl_pianoaSenza.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & "PREF_PIANI_A_SENZA= NULL ,"
                        End If









                        If txt_supMax.Text <> "" Then
                            strSQL = strSQL & "PREF_SUP_MAX=" & par.VirgoleInPunti(CDec(txt_supMax.Text)) & " ,"
                        Else
                            strSQL = strSQL & "PREF_SUP_MAX = NULL ,"
                        End If


                        If txt_supMin.Text <> "" Then
                            strSQL = strSQL & "PREF_SUP_MIN=" & par.VirgoleInPunti(CDec(txt_supMin.Text)) & " ,"
                        Else
                            strSQL = strSQL & "PREF_SUP_MIN = NULL ,"
                        End If

                        If txt_note.Text <> "" Then
                            strSQL = strSQL & "PREF_NOTE='" & par.PulisciStrSql(txt_note.Text) & "' "
                        Else
                            strSQL = strSQL & "PREF_NOTE = NULL "
                        End If


                        par.cmd.CommandText = "update DOMANDE_PREFERENZE_VSA SET PREF_CONDOMINIO='" & ChkZerUno(chk_condominio) & "', " _
                                            & " PREF_BARRIERE='" & ChkZerUno(chk_barr) & "', " _
                                            & " " & strSQL & " " _
                                            & " WHERE ID_DOMANDA=" & lIdDomanda


                        par.cmd.ExecuteNonQuery()



                    Else



                        '  ---INSERT


                        If ddl_localita1.SelectedValue <> "-1" Then
                            strSQL = " " & ddl_localita1.SelectedValue & " ,"
                        Else
                            strSQL = " NULL ,"
                        End If

                        If ddl_localita2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_zona1.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_quartiere1.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_indirizzo1.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo1.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo2.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo3.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "  NULL ,"
                        End If

                        If ddl_indirizzo4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo4.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "  NULL ,"
                        End If

                        If ddl_indirizzo5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo5.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "  NULL ,"
                        End If




                        If ddl_complesso1.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If







                        If ddl_edificio1.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If





                        If ddl_pianodaCon.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & ddl_pianodaCon.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_pianoaCon.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & ddl_pianoaCon.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If


                        If ddl_pianodaSenza.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & ddl_pianodaSenza.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_pianoaSenza.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & ddl_pianoaSenza.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If




                        If txt_supMax.Text <> "" Then
                            strSQL = strSQL & " " & par.VirgoleInPunti(CDec(txt_supMax.Text)) & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If


                        If txt_supMin.Text <> "" Then
                            strSQL = strSQL & " " & par.VirgoleInPunti(CDec(txt_supMin.Text)) & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If txt_note.Text <> "" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(txt_note.Text) & "' "
                        Else
                            strSQL = strSQL & " NULL "
                        End If





                        par.cmd.CommandText = "Insert into DOMANDE_PREFERENZE_VSA (ID, ID_DOMANDA, PREF_LOCALITA1, PREF_LOCALITA2, PREF_LOCALITA3, PREF_LOCALITA4, PREF_LOCALITA5, " _
                                            & " PREF_ZONA1, PREF_ZONA2, PREF_ZONA3, PREF_ZONA4, PREF_ZONA5, " _
                                            & " PREF_QUART1, PREF_QUART2, PREF_QUART3, PREF_QUART4, PREF_QUART5, " _
                                            & " PREF_INDIRIZZO1, PREF_INDIRIZZO2, PREF_INDIRIZZO3, PREF_INDIRIZZO4, PREF_INDIRIZZO5, " _
                                            & " PREF_COMPLESSO1, PREF_COMPLESSO2, PREF_COMPLESSO3, PREF_COMPLESSO4, PREF_COMPLESSO5, " _
                                            & " PREF_EDIFICIO1, PREF_EDIFICIO2, PREF_EDIFICIO3, PREF_EDIFICIO4, PREF_EDIFICIO5, " _
                                            & " PREF_PIANI_DA_CON, PREF_PIANI_A_CON, PREF_PIANI_DA_SENZA, PREF_PIANI_A_SENZA, " _
                                            & " PREF_SUP_MAX, PREF_SUP_MIN, PREF_NOTE) Values " _
                                       & "(SEQ_DOMANDE_PREFERENZE_VSA.NEXTVAL," & lIdDomanda & ", " & strSQL & ")"
                        par.cmd.ExecuteNonQuery()






                    End If
                    myReaderJ.Close()

                    strSQL = ""

                    par.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_ESCL_VSA WHERE ID_DOMANDA=" & lIdDomanda
                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then

                        If ddl_localita1ex.SelectedValue <> "-1" Then
                            strSQL = "ESCL_LOCALITA1=" & ddl_localita1ex.SelectedValue & " ,"
                        Else
                            strSQL = "ESCL_LOCALITA1= NULL ,"
                        End If

                        If ddl_localita2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_LOCALITA2=" & ddl_localita2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_LOCALITA2= NULL ,"
                        End If

                        If ddl_localita3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_LOCALITA3=" & ddl_localita3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_LOCALITA3= NULL ,"
                        End If

                        If ddl_localita4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_LOCALITA4=" & ddl_localita4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_LOCALITA4= NULL ,"
                        End If

                        If ddl_localita5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_LOCALITA5=" & ddl_localita5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_LOCALITA5= NULL ,"
                        End If



                        If ddl_zona1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_ZONA1=" & ddl_zona1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_ZONA1= NULL ,"
                        End If

                        If ddl_zona2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_ZONA2=" & ddl_zona2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_ZONA2= NULL ,"
                        End If

                        If ddl_zona3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_ZONA3=" & ddl_zona3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_ZONA3= NULL ,"
                        End If

                        If ddl_zona4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_ZONA4=" & ddl_zona4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_ZONA4= NULL ,"
                        End If

                        If ddl_zona5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_ZONA5=" & ddl_zona5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_ZONA5= NULL ,"
                        End If



                        If ddl_quartiere1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_QUART1=" & ddl_quartiere1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_QUART1= NULL ,"
                        End If

                        If ddl_quartiere2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_QUART2=" & ddl_quartiere2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_QUART2= NULL ,"
                        End If

                        If ddl_quartiere3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_QUART3=" & ddl_quartiere3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_QUART3= NULL ,"
                        End If

                        If ddl_quartiere4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_QUART4=" & ddl_quartiere4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_QUART4= NULL ,"
                        End If

                        If ddl_quartiere5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_QUART5=" & ddl_quartiere5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_QUART5= NULL ,"
                        End If



                        If ddl_indirizzo1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_INDIRIZZO1='" & par.PulisciStrSql(ddl_indirizzo1ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "ESCL_INDIRIZZO1= NULL ,"
                        End If

                        If ddl_indirizzo2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_INDIRIZZO2='" & par.PulisciStrSql(ddl_indirizzo2ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "ESCL_INDIRIZZO2= NULL ,"
                        End If

                        If ddl_indirizzo3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_INDIRIZZO3='" & par.PulisciStrSql(ddl_indirizzo3ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "ESCL_INDIRIZZO3= NULL ,"
                        End If

                        If ddl_indirizzo4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_INDIRIZZO4='" & par.PulisciStrSql(ddl_indirizzo4ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "ESCL_INDIRIZZO4= NULL ,"
                        End If

                        If ddl_indirizzo5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_INDIRIZZO5='" & par.PulisciStrSql(ddl_indirizzo5ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "ESCL_INDIRIZZO5= NULL ,"
                        End If



                        If ddl_complesso1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_COMPLESSO1=" & ddl_complesso1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_COMPLESSO1= NULL ,"
                        End If

                        If ddl_complesso2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_COMPLESSO2=" & ddl_complesso2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_COMPLESSO2= NULL ,"
                        End If

                        If ddl_complesso3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_COMPLESSO3=" & ddl_complesso3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_COMPLESSO3= NULL ,"
                        End If

                        If ddl_complesso4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_COMPLESSO4=" & ddl_complesso4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_COMPLESSO4= NULL ,"
                        End If

                        If ddl_complesso5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_COMPLESSO5=" & ddl_complesso5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_COMPLESSO5= NULL ,"
                        End If





                        If ddl_edificio1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_EDIFICIO1=" & ddl_edificio1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_EDIFICIO1= NULL ,"
                        End If

                        If ddl_edificio2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_EDIFICIO2=" & ddl_edificio2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_EDIFICIO2= NULL ,"
                        End If

                        If ddl_edificio3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_EDIFICIO3=" & ddl_edificio3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_EDIFICIO3= NULL ,"
                        End If

                        If ddl_edificio4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_EDIFICIO4=" & ddl_edificio4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_EDIFICIO4= NULL ,"
                        End If

                        If ddl_edificio5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_EDIFICIO5=" & ddl_edificio5ex.SelectedValue & " "
                        Else
                            strSQL = strSQL & "ESCL_EDIFICIO5= NULL "
                        End If





                        par.cmd.CommandText = "update DOMANDE_PREFERENZE_ESCL_VSA SET " & strSQL & " " _
                                            & " WHERE ID_DOMANDA=" & lIdDomanda


                        par.cmd.ExecuteNonQuery()

                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Operazione Completata! ');", True)


                    Else

                        '---- insert



                        If ddl_localita1ex.SelectedValue <> "-1" Then
                            strSQL = " " & ddl_localita1ex.SelectedValue & " ,"
                        Else
                            strSQL = " NULL ,"
                        End If

                        If ddl_localita2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_zona1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_quartiere1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_indirizzo1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo1ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo2ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo3ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo4ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo5ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If





                        If ddl_complesso1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If






                        If ddl_edificio1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio5ex.SelectedValue & " "
                        Else
                            strSQL = strSQL & " NULL "
                        End If










                        par.cmd.CommandText = "Insert into DOMANDE_PREFERENZE_ESCL_VSA (ID, ID_DOMANDA, ESCL_LOCALITA1, ESCL_LOCALITA2, ESCL_LOCALITA3, ESCL_LOCALITA4, ESCL_LOCALITA5, " _
                                           & " ESCL_ZONA1, ESCL_ZONA2, ESCL_ZONA3, ESCL_ZONA4, ESCL_ZONA5, " _
                                           & " ESCL_QUART1, ESCL_QUART2, ESCL_QUART3, ESCL_QUART4, ESCL_QUART5, " _
                                           & " ESCL_INDIRIZZO1, ESCL_INDIRIZZO2, ESCL_INDIRIZZO3, ESCL_INDIRIZZO4, ESCL_INDIRIZZO5, " _
                                           & " ESCL_COMPLESSO1, ESCL_COMPLESSO2, ESCL_COMPLESSO3, ESCL_COMPLESSO4, ESCL_COMPLESSO5, " _
                                           & " ESCL_EDIFICIO1, ESCL_EDIFICIO2, ESCL_EDIFICIO3, ESCL_EDIFICIO4, ESCL_EDIFICIO5) Values " _
                                           & "(SEQ_DOMANDE_PREFERENZE_ESCL_VS.NEXTVAL," & lIdDomanda & ", " & strSQL & ")"
                        par.cmd.ExecuteNonQuery()

                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Operazione Completata! ');", True)

                    End If
                    myReaderJ.Close()












                Case "BANDO CAMBI"
                    par.cmd.CommandText = "SELECT  * FROM DOMANDE_PREFERENZE_CAMBI WHERE ID_DOMANDA=" & lIdDomanda
                    myreaderj = par.cmd.executereader()
                    If myReaderJ.Read Then



                        If ddl_localita1.SelectedValue <> "-1" Then
                            strSQL = "PREF_LOCALITA1=" & ddl_localita1.SelectedValue & " ,"
                        Else
                            strSQL = "PREF_LOCALITA1= NULL ,"
                        End If

                        If ddl_localita2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_LOCALITA2=" & ddl_localita2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_LOCALITA2= NULL ,"
                        End If

                        If ddl_localita3.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_LOCALITA3=" & ddl_localita3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_LOCALITA3= NULL ,"
                        End If

                        If ddl_localita4.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_LOCALITA4=" & ddl_localita4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_LOCALITA4= NULL ,"
                        End If


                        If ddl_localita5.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_LOCALITA5=" & ddl_localita5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_LOCALITA5= NULL ,"
                        End If





                        If ddl_zona1.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_ZONA1=" & ddl_zona1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_ZONA1= NULL ,"
                        End If

                        If ddl_zona2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_ZONA2=" & ddl_zona2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_ZONA2= NULL ,"
                        End If

                        If ddl_zona3.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_ZONA3=" & ddl_zona3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_ZONA3= NULL ,"
                        End If

                        If ddl_zona4.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_ZONA4=" & ddl_zona4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_ZONA4= NULL ,"
                        End If


                        If ddl_zona5.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_ZONA5=" & ddl_zona5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_ZONA5= NULL ,"
                        End If


                        If ddl_quartiere1.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_QUART1=" & ddl_quartiere1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_QUART1= NULL ,"
                        End If

                        If ddl_quartiere2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_QUART2=" & ddl_quartiere2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_QUART2= NULL ,"
                        End If

                        If ddl_quartiere3.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_QUART3=" & ddl_quartiere3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_QUART3= NULL ,"
                        End If

                        If ddl_quartiere4.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_QUART4=" & ddl_quartiere4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_QUART4= NULL ,"
                        End If

                        If ddl_quartiere5.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_QUART5=" & ddl_quartiere5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_QUART5= NULL ,"
                        End If



                        If ddl_indirizzo1.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_INDIRIZZO1='" & par.PulisciStrSql(ddl_indirizzo1.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "PREF_INDIRIZZO1= NULL ,"
                        End If

                        If ddl_indirizzo2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_INDIRIZZO2='" & par.PulisciStrSql(ddl_indirizzo2.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "PREF_INDIRIZZO2= NULL ,"
                        End If

                        If ddl_indirizzo3.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_INDIRIZZO3='" & par.PulisciStrSql(ddl_indirizzo3.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "PREF_INDIRIZZO3= NULL ,"
                        End If

                        If ddl_indirizzo4.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_INDIRIZZO4='" & par.PulisciStrSql(ddl_indirizzo4.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "PREF_INDIRIZZO4= NULL ,"
                        End If

                        If ddl_indirizzo5.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_INDIRIZZO5='" & par.PulisciStrSql(ddl_indirizzo5.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "PREF_INDIRIZZO5= NULL ,"
                        End If






                        If ddl_complesso1.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_COMPLESSO1=" & ddl_complesso1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_COMPLESSO1= NULL ,"
                        End If

                        If ddl_complesso2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_COMPLESSO2=" & ddl_complesso2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_COMPLESSO2= NULL ,"
                        End If

                        If ddl_complesso3.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_COMPLESSO3=" & ddl_complesso3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_COMPLESSO3= NULL ,"
                        End If

                        If ddl_complesso4.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_COMPLESSO4=" & ddl_complesso4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_COMPLESSO4= NULL ,"
                        End If

                        If ddl_complesso5.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_COMPLESSO5=" & ddl_complesso5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_COMPLESSO5= NULL ,"
                        End If






                        If ddl_edificio1.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_EDIFICIO1=" & ddl_edificio1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_EDIFICIO1= NULL ,"
                        End If

                        If ddl_edificio2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_EDIFICIO2=" & ddl_edificio2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_EDIFICIO2= NULL ,"
                        End If

                        If ddl_edificio3.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_EDIFICIO3=" & ddl_edificio3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_EDIFICIO3= NULL ,"
                        End If

                        If ddl_edificio4.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_EDIFICIO4=" & ddl_edificio4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_EDIFICIO4= NULL ,"
                        End If

                        If ddl_edificio5.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_EDIFICIO5=" & ddl_edificio5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_EDIFICIO5= NULL ,"
                        End If




                        If ddl_pianodaCon.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_PIANI_DA_CON='" & ddl_pianodaCon.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & "PREF_PIANI_DA_CON= NULL ,"
                        End If



                        If ddl_pianoaCon.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_PIANI_A_CON='" & ddl_pianoaCon.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & "PREF_PIANI_A_CON= NULL ,"
                        End If


                        If ddl_pianodaSenza.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_PIANI_DA_SENZA='" & ddl_pianodaSenza.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & "PREF_PIANI_DA_SENZA= NULL ,"
                        End If



                        If ddl_pianoaSenza.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_PIANI_A_SENZA='" & ddl_pianoaSenza.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & "PREF_PIANI_A_SENZA= NULL ,"
                        End If






                        If txt_supMax.Text <> "" Then
                            strSQL = strSQL & "PREF_SUP_MAX=" & par.VirgoleInPunti(CDec(txt_supMax.Text)) & " ,"
                        Else
                            strSQL = strSQL & "PREF_SUP_MAX = NULL ,"
                        End If


                        If txt_supMin.Text <> "" Then
                            strSQL = strSQL & "PREF_SUP_MIN=" & par.VirgoleInPunti(CDec(txt_supMin.Text)) & " ,"
                        Else
                            strSQL = strSQL & "PREF_SUP_MIN = NULL ,"
                        End If

                        If txt_note.Text <> "" Then
                            strSQL = strSQL & "PREF_NOTE='" & par.PulisciStrSql(txt_note.Text) & "' "
                        Else
                            strSQL = strSQL & "PREF_NOTE = NULL "
                        End If


                        par.cmd.CommandText = "update DOMANDE_PREFERENZE_CAMBI SET PREF_CONDOMINIO='" & ChkZerUno(chk_condominio) & "', " _
                                            & " PREF_BARRIERE='" & ChkZerUno(chk_barr) & "', " _
                                            & " " & strSQL & " " _
                                            & " WHERE ID_DOMANDA=" & lIdDomanda


                        par.cmd.ExecuteNonQuery()



                    Else



                        '  ---INSERT


                        If ddl_localita1.SelectedValue <> "-1" Then
                            strSQL = " " & ddl_localita1.SelectedValue & " ,"
                        Else
                            strSQL = " NULL ,"
                        End If

                        If ddl_localita2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_zona1.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_quartiere1.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_indirizzo1.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo1.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo2.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo3.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo4.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo5.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If






                        If ddl_complesso1.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If






                        If ddl_edificio1.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_pianodaCon.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & ddl_pianodaCon.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_pianoaCon.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & ddl_pianoaCon.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If


                        If ddl_pianodaSenza.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & ddl_pianodaSenza.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_pianoaSenza.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & ddl_pianoaSenza.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If







                        If txt_supMax.Text <> "" Then
                            strSQL = strSQL & " " & par.VirgoleInPunti(CDec(txt_supMax.Text)) & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If


                        If txt_supMin.Text <> "" Then
                            strSQL = strSQL & " " & par.VirgoleInPunti(CDec(txt_supMin.Text)) & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If txt_note.Text <> "" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(txt_note.Text) & "' "
                        Else
                            strSQL = strSQL & " NULL "
                        End If





                        par.cmd.CommandText = "Insert into DOMANDE_PREFERENZE_CAMBI (ID, ID_DOMANDA, PREF_LOCALITA1, PREF_LOCALITA2, PREF_LOCALITA3, PREF_LOCALITA4, PREF_LOCALITA5, " _
                                            & " PREF_ZONA1, PREF_ZONA2, PREF_ZONA3, PREF_ZONA4, PREF_ZONA5, " _
                                            & " PREF_QUART1, PREF_QUART2, PREF_QUART3, PREF_QUART4, PREF_QUART5, " _
                                            & " PREF_INDIRIZZO1, PREF_INDIRIZZO2, PREF_INDIRIZZO3, PREF_INDIRIZZO4, PREF_INDIRIZZO5, " _
                                            & " PREF_COMPLESSO1, PREF_COMPLESSO2, PREF_COMPLESSO3, PREF_COMPLESSO4, PREF_COMPLESSO5, " _
                                            & " PREF_EDIFICIO1, PREF_EDIFICIO2, PREF_EDIFICIO3, PREF_EDIFICIO4, PREF_EDIFICIO5, " _
                                            & " PREF_PIANI_DA_CON, PREF_PIANI_A_CON, PREF_PIANI_DA_SENZA, PREF_PIANI_A_SENZA, " _
                                            & " PREF_SUP_MAX, PREF_SUP_MIN, PREF_NOTE) Values " _
                                       & "(SEQ_DOMANDE_PREFERENZE_CAMBI.NEXTVAL," & lIdDomanda & ", " & strSQL & ")"
                        par.cmd.ExecuteNonQuery()






                    End If
                    myReaderJ.Close()

                    strSQL = ""

                    par.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_ESCL_CAMBI WHERE ID_DOMANDA=" & lIdDomanda
                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then

                        If ddl_localita1ex.SelectedValue <> "-1" Then
                            strSQL = "ESCL_LOCALITA1=" & ddl_localita1ex.SelectedValue & " ,"
                        Else
                            strSQL = "ESCL_LOCALITA1= NULL ,"
                        End If

                        If ddl_localita2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_LOCALITA2=" & ddl_localita2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_LOCALITA2= NULL ,"
                        End If

                        If ddl_localita3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_LOCALITA3=" & ddl_localita3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_LOCALITA3= NULL ,"
                        End If

                        If ddl_localita4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_LOCALITA4=" & ddl_localita4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_LOCALITA4= NULL ,"
                        End If

                        If ddl_localita5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_LOCALITA5=" & ddl_localita5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_LOCALITA5= NULL ,"
                        End If



                        If ddl_zona1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_ZONA1=" & ddl_zona1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_ZONA1= NULL ,"
                        End If

                        If ddl_zona2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_ZONA2=" & ddl_zona2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_ZONA2= NULL ,"
                        End If

                        If ddl_zona3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_ZONA3='" & ddl_zona3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_ZONA3= NULL ,"
                        End If

                        If ddl_zona4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_ZONA4='" & ddl_zona4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_ZONA4= NULL ,"
                        End If

                        If ddl_zona5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_ZONA5='" & ddl_zona5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_ZONA5= NULL ,"
                        End If



                        If ddl_quartiere1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_QUART1=" & ddl_quartiere1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_QUART1= NULL ,"
                        End If

                        If ddl_quartiere2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_QUART2=" & ddl_quartiere2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_QUART2= NULL ,"
                        End If

                        If ddl_quartiere3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_QUART3=" & ddl_quartiere3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_QUART3= NULL ,"
                        End If

                        If ddl_quartiere4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_QUART4=" & ddl_quartiere4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_QUART4= NULL ,"
                        End If

                        If ddl_quartiere5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_QUART5=" & ddl_quartiere5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_QUART5= NULL ,"
                        End If



                        If ddl_indirizzo1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_INDIRIZZO1='" & par.PulisciStrSql(ddl_indirizzo1ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "ESCL_INDIRIZZO1= NULL ,"
                        End If

                        If ddl_indirizzo2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_INDIRIZZO2='" & par.PulisciStrSql(ddl_indirizzo2ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "ESCL_INDIRIZZO2= NULL ,"
                        End If

                        If ddl_indirizzo3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_INDIRIZZO3='" & par.PulisciStrSql(ddl_indirizzo3ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "ESCL_INDIRIZZO3= NULL ,"
                        End If


                        If ddl_indirizzo4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_INDIRIZZO4='" & par.PulisciStrSql(ddl_indirizzo4ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "ESCL_INDIRIZZO4= NULL ,"
                        End If


                        If ddl_indirizzo5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_INDIRIZZO5='" & par.PulisciStrSql(ddl_indirizzo5ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & "ESCL_INDIRIZZO5= NULL ,"
                        End If




                        If ddl_complesso1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_COMPLESSO1=" & ddl_complesso1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_COMPLESSO1= NULL ,"
                        End If

                        If ddl_complesso2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_COMPLESSO2=" & ddl_complesso2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_COMPLESSO2= NULL ,"
                        End If

                        If ddl_complesso3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_COMPLESSO3=" & ddl_complesso3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_COMPLESSO3= NULL ,"
                        End If

                        If ddl_complesso4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_COMPLESSO4=" & ddl_complesso4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_COMPLESSO4= NULL ,"
                        End If

                        If ddl_complesso5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_COMPLESSO5=" & ddl_complesso5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_COMPLESSO5= NULL ,"
                        End If





                        If ddl_edificio1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_EDIFICIO1=" & ddl_edificio1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_EDIFICIO1= NULL ,"
                        End If

                        If ddl_edificio2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_EDIFICIO2=" & ddl_edificio2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_EDIFICIO2= NULL ,"
                        End If

                        If ddl_edificio3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_EDIFICIO3=" & ddl_edificio3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_EDIFICIO3= NULL ,"
                        End If

                        If ddl_edificio4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_EDIFICIO4=" & ddl_edificio4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_EDIFICIO4= NULL ,"
                        End If

                        If ddl_edificio5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_EDIFICIO5=" & ddl_edificio5ex.SelectedValue & " "
                        Else
                            strSQL = strSQL & "ESCL_EDIFICIO5= NULL "
                        End If







                        par.cmd.CommandText = "update DOMANDE_PREFERENZE_ESCL_CAMBI SET " & strSQL & " " _
                                            & " WHERE ID_DOMANDA=" & lIdDomanda


                        par.cmd.ExecuteNonQuery()

                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Operazione Completata! ');", True)

                    Else

                        '---- insert



                        If ddl_localita1ex.SelectedValue <> "-1" Then
                            strSQL = " " & ddl_localita1ex.SelectedValue & " ,"
                        Else
                            strSQL = " NULL ,"
                        End If

                        If ddl_localita2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_zona1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_quartiere1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_indirizzo1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo1ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo2ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo3ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo4ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo5ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If







                        If ddl_complesso1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If




                        If ddl_edificio1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio5ex.SelectedValue & " "
                        Else
                            strSQL = strSQL & " NULL "
                        End If






                        par.cmd.CommandText = "Insert into DOMANDE_PREFERENZE_ESCL_CAMBI (ID, ID_DOMANDA, ESCL_LOCALITA1, ESCL_LOCALITA2, ESCL_LOCALITA3, ESCL_LOCALITA4, ESCL_LOCALITA5, " _
                                           & " ESCL_ZONA1, ESCL_ZONA2, ESCL_ZONA3, ESCL_ZONA4, ESCL_ZONA5, " _
                                           & " ESCL_QUART1, ESCL_QUART2, ESCL_QUART3, ESCL_QUART4, ESCL_QUART5, " _
                                           & " ESCL_INDIRIZZO1, ESCL_INDIRIZZO2, ESCL_INDIRIZZO3, ESCL_INDIRIZZO4, ESCL_INDIRIZZO5, " _
                                           & " ESCL_COMPLESSO1, ESCL_COMPLESSO2, ESCL_COMPLESSO3, ESCL_COMPLESSO4, ESCL_COMPLESSO5, " _
                                           & " ESCL_EDIFICIO1, ESCL_EDIFICIO2, ESCL_EDIFICIO3,  ESCL_EDIFICIO4, ESCL_EDIFICIO5) Values " _
                                      & "(SEQ_DOMANDE_PREFERENZE_ESCL_CA.NEXTVAL," & lIdDomanda & ", " & strSQL & ")"
                        par.cmd.ExecuteNonQuery()

                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Operazione Completata! ');", True)

                    End If
                    myReaderJ.Close()













                Case Else
                    par.cmd.CommandText = "SELECT  * FROM DOMANDE_PREFERENZE WHERE ID_DOMANDA=" & lIdDomanda
                    myreaderj = par.cmd.executereader()
                    If myReaderJ.Read Then



                        If ddl_localita1.SelectedValue <> "-1" Then
                            strSQL = "PREF_LOCALITA1=" & ddl_localita1.SelectedValue & " ,"
                        Else
                            strSQL = "PREF_LOCALITA1= NULL ,"
                        End If

                        If ddl_localita2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_LOCALITA2=" & ddl_localita2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_LOCALITA2= NULL ,"
                        End If

                        If ddl_localita3.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_LOCALITA3=" & ddl_localita3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_LOCALITA3= NULL ,"
                        End If

                        If ddl_localita4.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_LOCALITA4=" & ddl_localita4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_LOCALITA4= NULL ,"
                        End If

                        If ddl_localita5.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_LOCALITA5=" & ddl_localita5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_LOCALITA5= NULL ,"
                        End If



                        If ddl_zona1.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_ZONA1=" & ddl_zona1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_ZONA1= NULL ,"
                        End If


                        If ddl_zona2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_ZONA2=" & ddl_zona2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_ZONA2= NULL ,"
                        End If


                        If ddl_zona3.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_ZONA3=" & ddl_zona3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_ZONA3= NULL ,"
                        End If

                        If ddl_zona4.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_ZONA4=" & ddl_zona4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_ZONA4= NULL ,"
                        End If

                        If ddl_zona5.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_ZONA5=" & ddl_zona5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_ZONA5= NULL ,"
                        End If





                        If ddl_quartiere1.SelectedValue <> "-1" Then

                            strSQL = strSQL & "PREF_QUART1=" & ddl_quartiere1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_QUART1= NULL ,"
                        End If

                        If ddl_quartiere2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_QUART2=" & ddl_quartiere2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_QUART2= NULL ,"
                        End If

                        If ddl_quartiere3.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_QUART3=" & ddl_quartiere3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_QUART3= NULL ,"
                        End If

                        If ddl_quartiere4.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_QUART4=" & ddl_quartiere4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_QUART4= NULL ,"
                        End If

                        If ddl_quartiere5.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_QUART5=" & ddl_quartiere5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_QUART5= NULL ,"
                        End If






                        If ddl_indirizzo1.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_INDIRIZZO1='" & par.PulisciStrSql(ddl_indirizzo1.SelectedItem.Value) & "' ,"
                        Else
                            strSQL = strSQL & "PREF_INDIRIZZO1= NULL ,"
                        End If

                        If ddl_indirizzo2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_INDIRIZZO2='" & par.PulisciStrSql(ddl_indirizzo2.SelectedItem.Value) & "' ,"
                        Else
                            strSQL = strSQL & "PREF_INDIRIZZO2= NULL ,"
                        End If


                        If ddl_indirizzo3.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_INDIRIZZO3='" & par.PulisciStrSql(ddl_indirizzo3.SelectedItem.Value) & "' ,"
                        Else
                            strSQL = strSQL & "PREF_INDIRIZZO3= NULL ,"
                        End If

                        If ddl_indirizzo4.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_INDIRIZZO4='" & par.PulisciStrSql(ddl_indirizzo4.SelectedItem.Value) & "' ,"
                        Else
                            strSQL = strSQL & "PREF_INDIRIZZO4= NULL ,"
                        End If

                        If ddl_indirizzo5.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_INDIRIZZO5='" & par.PulisciStrSql(ddl_indirizzo5.SelectedItem.Value) & "' ,"
                        Else
                            strSQL = strSQL & "PREF_INDIRIZZO5= NULL ,"
                        End If




                        If ddl_complesso1.SelectedValue <> "-1" Then

                            strSQL = strSQL & "PREF_COMPLESSO1=" & ddl_complesso1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_COMPLESSO1= NULL ,"
                        End If

                        If ddl_complesso2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_COMPLESSO2=" & ddl_complesso2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_COMPLESSO2= NULL ,"
                        End If

                        If ddl_complesso3.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_COMPLESSO3=" & ddl_complesso3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_COMPLESSO3= NULL ,"
                        End If

                        If ddl_complesso4.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_COMPLESSO4=" & ddl_complesso4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_COMPLESSO4= NULL ,"
                        End If

                        If ddl_complesso5.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_COMPLESSO5=" & ddl_complesso5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_COMPLESSO5= NULL ,"
                        End If





                        If ddl_edificio1.SelectedValue <> "-1" Then

                            strSQL = strSQL & "PREF_EDIFICIO1=" & ddl_edificio1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_EDIFICIO1= NULL ,"
                        End If

                        If ddl_edificio2.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_EDIFICIO2=" & ddl_edificio2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_EDIFICIO2= NULL ,"
                        End If

                        If ddl_edificio3.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_EDIFICIO3=" & ddl_edificio3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_EDIFICIO3= NULL ,"
                        End If

                        If ddl_edificio4.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_EDIFICIO4=" & ddl_edificio4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_EDIFICIO4= NULL ,"
                        End If

                        If ddl_edificio5.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_EDIFICIO5=" & ddl_edificio5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "PREF_EDIFICIO5= NULL ,"
                        End If




                        If ddl_pianodaCon.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_PIANI_DA_CON='" & ddl_pianodaCon.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & "PREF_PIANI_DA_CON= NULL ,"
                        End If



                        If ddl_pianoaCon.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_PIANI_A_CON='" & ddl_pianoaCon.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & "PREF_PIANI_A_CON= NULL ,"
                        End If


                        If ddl_pianodaSenza.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_PIANI_DA_SENZA='" & ddl_pianodaSenza.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & "PREF_PIANI_DA_SENZA= NULL ,"
                        End If



                        If ddl_pianoaSenza.SelectedValue <> "-1" Then
                            strSQL = strSQL & "PREF_PIANI_A_SENZA='" & ddl_pianoaSenza.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & "PREF_PIANI_A_SENZA= NULL ,"
                        End If











                        If txt_supMax.Text <> "" Then
                            strSQL = strSQL & "PREF_SUP_MAX=" & par.VirgoleInPunti(CDec(txt_supMax.Text)) & " ,"
                        Else
                            strSQL = strSQL & "PREF_SUP_MAX = NULL ,"
                        End If


                        If txt_supMin.Text <> "" Then
                            strSQL = strSQL & "PREF_SUP_MIN=" & par.VirgoleInPunti(CDec(txt_supMin.Text)) & " ,"
                        Else
                            strSQL = strSQL & "PREF_SUP_MIN = NULL ,"
                        End If

                        If txt_note.Text <> "" Then
                            strSQL = strSQL & "PREF_NOTE='" & par.PulisciStrSql(txt_note.Text) & "' "
                        Else
                            strSQL = strSQL & "PREF_NOTE = NULL "
                        End If


                        par.cmd.CommandText = "update DOMANDE_PREFERENZE SET PREF_CONDOMINIO='" & ChkZerUno(chk_condominio) & "', " _
                                            & " PREF_BARRIERE='" & ChkZerUno(chk_barr) & "', " _
                                            & " " & strSQL & " " _
                                            & " WHERE ID_DOMANDA=" & lIdDomanda


                        par.cmd.ExecuteNonQuery()



                    Else



                        '  ---INSERT


                        If ddl_localita1.SelectedValue <> "-1" Then
                            strSQL = " " & ddl_localita1.SelectedValue & " ,"
                        Else
                            strSQL = " NULL ,"
                        End If

                        If ddl_localita2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_zona1.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_quartiere1.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_indirizzo1.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo1.SelectedItem.Value) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo2.SelectedItem.Value) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo3.SelectedItem.Value) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo4.SelectedItem.Value) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo5.SelectedItem.Value) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If





                        If ddl_complesso1.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If


                        If ddl_complesso5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If





                        If ddl_edificio1.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio1.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio2.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio2.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio3.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio3.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio4.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio4.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio5.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio5.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_pianodaCon.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & ddl_pianodaCon.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_pianoaCon.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & ddl_pianoaCon.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If


                        If ddl_pianodaSenza.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & ddl_pianodaSenza.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_pianoaSenza.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & ddl_pianoaSenza.SelectedValue & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If







                        If txt_supMax.Text <> "" Then
                            strSQL = strSQL & " " & par.VirgoleInPunti(CDec(txt_supMax.Text)) & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If


                        If txt_supMin.Text <> "" Then
                            strSQL = strSQL & " " & par.VirgoleInPunti(CDec(txt_supMin.Text)) & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If txt_note.Text <> "" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(txt_note.Text) & "' "
                        Else
                            strSQL = strSQL & " NULL "
                        End If





                        par.cmd.CommandText = "Insert into DOMANDE_PREFERENZE (ID, ID_DOMANDA, PREF_LOCALITA1, PREF_LOCALITA2, PREF_LOCALITA3, PREF_LOCALITA4, PREF_LOCALITA5, " _
                                            & " PREF_ZONA1, PREF_ZONA2, PREF_ZONA3, PREF_ZONA4, PREF_ZONA5, " _
                                            & " PREF_QUART1, PREF_QUART2, PREF_QUART3, PREF_QUART4, PREF_QUART5, " _
                                            & " PREF_INDIRIZZO1, PREF_INDIRIZZO2, PREF_INDIRIZZO3, " _
                                            & " PREF_COMPLESSO1, PREF_COMPLESSO2, PREF_COMPLESSO3, PREF_COMPLESSO4, PREF_COMPLESSO5, " _
                                            & " PREF_EDIFICIO1, PREF_EDIFICIO2, PREF_EDIFICIO3, PREF_EDIFICIO4, PREF_EDIFICIO5, " _
                                            & " PREF_PIANI_DA_CON, PREF_PIANI_A_CON, PREF_PIANI_DA_SENZA, PREF_PIANI_A_SENZA, " _
                                            & " PREF_SUP_MAX, PREF_SUP_MIN, PREF_NOTE) Values " _
                                       & "(SEQ_DOMANDE_PREFERENZE.NEXTVAL," & lIdDomanda & ", " & strSQL & ")"
                        par.cmd.ExecuteNonQuery()






                    End If
                    myReaderJ.Close()

                    strSQL = ""

                    par.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_ESCLUSIONI WHERE ID_DOMANDA=" & lIdDomanda
                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then

                        If ddl_localita1ex.SelectedValue <> "-1" Then
                            strSQL = "ESCL_LOCALITA1=" & ddl_localita1ex.SelectedValue & " ,"
                        Else
                            strSQL = "ESCL_LOCALITA1= NULL ,"
                        End If

                        If ddl_localita2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_LOCALITA2=" & ddl_localita2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_LOCALITA2= NULL ,"
                        End If

                        If ddl_localita3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_LOCALITA3=" & ddl_localita3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_LOCALITA3= NULL ,"
                        End If

                        If ddl_localita4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_LOCALITA4=" & ddl_localita4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_LOCALITA4= NULL ,"
                        End If


                        If ddl_localita5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_LOCALITA5=" & ddl_localita5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_LOCALITA5= NULL ,"
                        End If



                        If ddl_zona1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_ZONA1=" & ddl_zona1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_ZONA1= NULL ,"
                        End If

                        If ddl_zona2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_ZONA2=" & ddl_zona2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_ZONA2= NULL ,"
                        End If

                        If ddl_zona3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_ZONA3=" & ddl_zona3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_ZONA3= NULL ,"
                        End If

                        If ddl_zona4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_ZONA4=" & ddl_zona4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_ZONA4= NULL ,"
                        End If

                        If ddl_zona5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_ZONA5=" & ddl_zona5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_ZONA5= NULL ,"
                        End If



                        If ddl_quartiere1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_QUART1=" & ddl_quartiere1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_QUART1= NULL ,"
                        End If

                        If ddl_quartiere2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_QUART2=" & ddl_quartiere2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_QUART2= NULL ,"
                        End If

                        If ddl_quartiere3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_QUART3=" & ddl_quartiere3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_QUART3= NULL ,"
                        End If

                        If ddl_quartiere4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_QUART4=" & ddl_quartiere4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_QUART4= NULL ,"
                        End If

                        If ddl_quartiere5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_QUART5=" & ddl_quartiere5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_QUART5= NULL ,"
                        End If



                        If ddl_indirizzo1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_INDIRIZZO1='" & par.PulisciStrSql(ddl_indirizzo1ex.SelectedItem.Value) & "' ,"
                        Else
                            strSQL = strSQL & "ESCL_INDIRIZZO1= NULL ,"
                        End If

                        If ddl_indirizzo2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_INDIRIZZO2='" & par.PulisciStrSql(ddl_indirizzo2ex.SelectedItem.Value) & "' ,"
                        Else
                            strSQL = strSQL & "ESCL_INDIRIZZO2= NULL ,"
                        End If

                        If ddl_indirizzo3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_INDIRIZZO3='" & par.PulisciStrSql(ddl_indirizzo3ex.SelectedItem.Value) & "' ,"
                        Else
                            strSQL = strSQL & "ESCL_INDIRIZZO3= NULL ,"
                        End If

                        If ddl_indirizzo4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_INDIRIZZO4='" & par.PulisciStrSql(ddl_indirizzo4ex.SelectedItem.Value) & "' ,"
                        Else
                            strSQL = strSQL & "ESCL_INDIRIZZO4= NULL ,"
                        End If


                        If ddl_indirizzo5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_INDIRIZZO5='" & par.PulisciStrSql(ddl_indirizzo5ex.SelectedItem.Value) & "' ,"
                        Else
                            strSQL = strSQL & "ESCL_INDIRIZZO5= NULL ,"
                        End If





                        If ddl_complesso1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_COMPLESSO1=" & ddl_complesso1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_COMPLESSO1= NULL ,"
                        End If

                        If ddl_complesso2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_COMPLESSO2=" & ddl_complesso2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_COMPLESSO2= NULL ,"
                        End If

                        If ddl_complesso3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_COMPLESSO3=" & ddl_complesso3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_COMPLESSO3= NULL ,"
                        End If

                        If ddl_complesso4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_COMPLESSO4=" & ddl_complesso4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_COMPLESSO4= NULL ,"
                        End If

                        If ddl_complesso5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_COMPLESSO5=" & ddl_complesso5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_COMPLESSO5= NULL ,"
                        End If




                        If ddl_edificio1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_EDIFICIO1=" & ddl_edificio1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_EDIFICIO1= NULL ,"
                        End If

                        If ddl_edificio2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_EDIFICIO2=" & ddl_edificio2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_EDIFICIO2= NULL ,"
                        End If

                        If ddl_edificio3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_EDIFICIO3=" & ddl_edificio3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_EDIFICIO3= NULL ,"
                        End If

                        If ddl_edificio4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_EDIFICIO4=" & ddl_edificio4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & "ESCL_EDIFICIO4= NULL ,"
                        End If

                        If ddl_edificio5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & "ESCL_EDIFICIO5=" & ddl_edificio5ex.SelectedValue & " "
                        Else
                            strSQL = strSQL & "ESCL_EDIFICIO5= NULL "
                        End If








                        par.cmd.CommandText = "update DOMANDE_PREFERENZE_ESCLUSIONI SET " & strSQL & " " _
                                            & " WHERE ID_DOMANDA=" & lIdDomanda


                        par.cmd.ExecuteNonQuery()

                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Operazione Completata! ');", True)

                    Else

                        '---- insert



                        If ddl_localita1ex.SelectedValue <> "-1" Then
                            strSQL = " " & ddl_localita1ex.SelectedValue & " ,"
                        Else
                            strSQL = " NULL ,"
                        End If

                        If ddl_localita2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_localita5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_localita5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_zona1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_zona5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_zona5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_quartiere1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_quartiere5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_quartiere5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If



                        If ddl_indirizzo1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo1ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo2ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo3ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo4ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_indirizzo5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " '" & par.PulisciStrSql(ddl_indirizzo5ex.SelectedItem.Text) & "' ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If







                        If ddl_complesso1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_complesso5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_complesso5ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If







                        If ddl_edificio1ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio1ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio2ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio2ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio3ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio3ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio4ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio4ex.SelectedValue & " ,"
                        Else
                            strSQL = strSQL & " NULL ,"
                        End If

                        If ddl_edificio5ex.SelectedValue <> "-1" Then
                            strSQL = strSQL & " " & ddl_edificio5ex.SelectedValue & " "
                        Else
                            strSQL = strSQL & " NULL "
                        End If







                        par.cmd.CommandText = "Insert into DOMANDE_PREFERENZE_ESCLUSIONI (ID, ID_DOMANDA, ESCL_LOCALITA1, ESCL_LOCALITA2, ESCL_LOCALITA3, ESCL_LOCALITA4, ESCL_LOCALITA5, " _
                                           & " ESCL_ZONA1, ESCL_ZONA2, ESCL_ZONA3, ESCL_ZONA4, ESCL_ZONA5, " _
                                           & " ESCL_QUART1, ESCL_QUART2, ESCL_QUART3, ESCL_QUART4, ESCL_QUART5, " _
                                           & " ESCL_INDIRIZZO1, ESCL_INDIRIZZO2, ESCL_INDIRIZZO3, ESCL_INDIRIZZO4, ESCL_INDIRIZZO5, " _
                                           & " ESCL_COMPLESSO1, ESCL_COMPLESSO2, ESCL_COMPLESSO3, ESCL_COMPLESSO4, ESCL_COMPLESSO5, " _
                                           & " ESCL_EDIFICIO1, ESCL_EDIFICIO2, ESCL_EDIFICIO3, ESCL_EDIFICIO4, ESCL_EDIFICIO5) Values " _
                                      & "(SEQ_DOMANDE_PREFERENZE_ESCL.NEXTVAL," & lIdDomanda & ", " & strSQL & ")"
                        par.cmd.ExecuteNonQuery()

                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Operazione Completata! ');", True)

                    End If
                    myReaderJ.Close()



            End Select


            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
            Select Case TIPO
                Case "ART.22 C.10"
                    par.cmd.CommandText = "SELECT  * FROM DOMANDE_PREFERENZE_VSA WHERE ID_DOMANDA=" & lIdDomanda & " FOR UPDATE NOWAIT"
                Case "BANDO CAMBI"
                    par.cmd.CommandText = "SELECT  * FROM DOMANDE_PREFERENZE_CAMBI WHERE ID_DOMANDA=" & lIdDomanda & " FOR UPDATE NOWAIT"
                Case Else
                    par.cmd.CommandText = "SELECT  * FROM DOMANDE_PREFERENZE WHERE ID_DOMANDA=" & lIdDomanda & " FOR UPDATE NOWAIT"
            End Select
            myreaderj = par.cmd.ExecuteReader()
            myreaderj.Close()




        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            HttpContext.Current.Session.Remove("TRANSAZIONE")
            HttpContext.Current.Session.Remove("CONNESSIONE")
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub




    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Protected Sub ddl_localita1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_localita1.SelectedIndexChanged

        Try

            FiltroLocalita(ddl_localita1, ddl_quartiere1, ddl_indirizzo1, ddl_zona1, ddl_complesso1, ddl_edificio1)

            If ddl_localita1.SelectedValue <> "-1" Then


                ddl_zona1.Enabled = True
                ddl_quartiere1.Enabled = True
                ddl_indirizzo1.Enabled = True
                ddl_complesso1.Enabled = True
                ddl_edificio1.Enabled = True


                ddl_localita2.Enabled = True



            Else


                CaricaComboBlocco(ddl_localita2, ddl_quartiere2, ddl_indirizzo2, ddl_zona2, ddl_complesso2, ddl_edificio2)

                ddl_localita2.SelectedValue = "-1"
                ddl_localita2.Items.FindByText("TUTTI").Selected = True

                ddl_quartiere2.SelectedValue = "-1"
                ddl_quartiere2.Items.FindByText("TUTTI").Selected = True

                ddl_indirizzo2.SelectedValue = "-1"
                ddl_indirizzo2.Items.FindByText("TUTTI").Selected = True

                ddl_zona2.SelectedValue = "-1"
                ddl_zona2.Items.FindByText("TUTTI").Selected = True

                ddl_complesso2.SelectedValue = "-1"
                ddl_complesso2.Items.FindByText("TUTTI").Selected = True

                ddl_edificio2.SelectedValue = "-1"
                ddl_edificio2.Items.FindByText("TUTTI").Selected = True




                ddl_zona1.Enabled = False
                ddl_quartiere1.Enabled = False
                ddl_indirizzo1.Enabled = False
                ddl_complesso1.Enabled = False
                ddl_edificio1.Enabled = False




                ddl_localita2.Enabled = False
                ddl_zona2.Enabled = False
                ddl_quartiere2.Enabled = False
                ddl_indirizzo2.Enabled = False
                ddl_complesso2.Enabled = False
                ddl_edificio2.Enabled = False



                ddl_localita3.SelectedValue = "-1"
                ddl_localita3.Items.FindByText("TUTTI").Selected = True

                ddl_quartiere3.SelectedValue = "-1"
                ddl_quartiere3.Items.FindByText("TUTTI").Selected = True

                ddl_indirizzo3.SelectedValue = "-1"
                ddl_indirizzo3.Items.FindByText("TUTTI").Selected = True

                ddl_zona3.SelectedValue = "-1"
                ddl_zona3.Items.FindByText("TUTTI").Selected = True

                ddl_complesso3.SelectedValue = "-1"
                ddl_complesso3.Items.FindByText("TUTTI").Selected = True

                ddl_edificio3.SelectedValue = "-1"
                ddl_edificio3.Items.FindByText("TUTTI").Selected = True

                ddl_localita3.Enabled = False
                ddl_zona3.Enabled = False
                ddl_quartiere3.Enabled = False
                ddl_indirizzo3.Enabled = False
                ddl_complesso3.Enabled = False
                ddl_edificio3.Enabled = False


                ddl_localita4.SelectedValue = "-1"
                ddl_localita4.Items.FindByText("TUTTI").Selected = True

                ddl_quartiere4.SelectedValue = "-1"
                ddl_quartiere4.Items.FindByText("TUTTI").Selected = True

                ddl_indirizzo4.SelectedValue = "-1"
                ddl_indirizzo4.Items.FindByText("TUTTI").Selected = True

                ddl_zona4.SelectedValue = "-1"
                ddl_zona4.Items.FindByText("TUTTI").Selected = True

                ddl_complesso4.SelectedValue = "-1"
                ddl_complesso4.Items.FindByText("TUTTI").Selected = True

                ddl_edificio4.SelectedValue = "-1"
                ddl_edificio4.Items.FindByText("TUTTI").Selected = True

                ddl_localita4.Enabled = False
                ddl_zona4.Enabled = False
                ddl_quartiere4.Enabled = False
                ddl_indirizzo4.Enabled = False
                ddl_complesso4.Enabled = False
                ddl_edificio4.Enabled = False




                ddl_localita5.SelectedValue = "-1"
                ddl_localita5.Items.FindByText("TUTTI").Selected = True

                ddl_quartiere5.SelectedValue = "-1"
                ddl_quartiere5.Items.FindByText("TUTTI").Selected = True

                ddl_indirizzo5.SelectedValue = "-1"
                ddl_indirizzo5.Items.FindByText("TUTTI").Selected = True

                ddl_zona5.SelectedValue = "-1"
                ddl_zona5.Items.FindByText("TUTTI").Selected = True

                ddl_complesso5.SelectedValue = "-1"
                ddl_complesso5.Items.FindByText("TUTTI").Selected = True

                ddl_edificio5.SelectedValue = "-1"
                ddl_edificio5.Items.FindByText("TUTTI").Selected = True

                ddl_localita5.Enabled = False
                ddl_zona5.Enabled = False
                ddl_quartiere5.Enabled = False
                ddl_indirizzo5.Enabled = False
                ddl_complesso5.Enabled = False
                ddl_edificio5.Enabled = False



            End If

        Catch ex As Exception
            ' chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try






    End Sub


    Protected Sub ddl_quartiere1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_quartiere1.SelectedIndexChanged

        FiltroQuartiere(ddl_localita1, ddl_quartiere1, ddl_indirizzo1, ddl_zona1, ddl_complesso1, ddl_edificio1)


        If ddl_localita1.SelectedValue <> "-1" Then


            ddl_localita2.Enabled = True
      

            ddl_zona1.Enabled = True
            ddl_quartiere1.Enabled = True
            ddl_indirizzo1.Enabled = True
            ddl_complesso1.Enabled = True
            ddl_edificio1.Enabled = True


        Else


            CaricaComboBlocco(ddl_localita2, ddl_quartiere2, ddl_indirizzo2, ddl_zona2, ddl_complesso2, ddl_edificio2)

            ddl_localita2.SelectedValue = "-1"
            ddl_localita2.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere2.SelectedValue = "-1"
            ddl_quartiere2.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo2.SelectedValue = "-1"
            ddl_indirizzo2.Items.FindByText("TUTTI").Selected = True

            ddl_zona2.SelectedValue = "-1"
            ddl_zona2.Items.FindByText("TUTTI").Selected = True

            ddl_complesso2.SelectedValue = "-1"
            ddl_complesso2.Items.FindByText("TUTTI").Selected = True

            ddl_edificio2.SelectedValue = "-1"
            ddl_edificio2.Items.FindByText("TUTTI").Selected = True


            ddl_localita2.Enabled = False
            ddl_zona2.Enabled = False
            ddl_quartiere2.Enabled = False
            ddl_indirizzo2.Enabled = False
            ddl_complesso2.Enabled = False
            ddl_edificio2.Enabled = False

            ddl_zona1.Enabled = False
            ddl_quartiere1.Enabled = False
            ddl_indirizzo1.Enabled = False
            ddl_complesso1.Enabled = False
            ddl_edificio1.Enabled = False

            ddl_localita3.SelectedValue = "-1"
            ddl_localita3.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere3.SelectedValue = "-1"
            ddl_quartiere3.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo3.SelectedValue = "-1"
            ddl_indirizzo3.Items.FindByText("TUTTI").Selected = True

            ddl_zona3.SelectedValue = "-1"
            ddl_zona3.Items.FindByText("TUTTI").Selected = True

            ddl_complesso3.SelectedValue = "-1"
            ddl_complesso3.Items.FindByText("TUTTI").Selected = True

            ddl_edificio3.SelectedValue = "-1"
            ddl_edificio3.Items.FindByText("TUTTI").Selected = True

            ddl_localita3.Enabled = False
            ddl_zona3.Enabled = False
            ddl_quartiere3.Enabled = False
            ddl_indirizzo3.Enabled = False
            ddl_complesso3.Enabled = False
            ddl_edificio3.Enabled = False


            ddl_localita4.SelectedValue = "-1"
            ddl_localita4.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere4.SelectedValue = "-1"
            ddl_quartiere4.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo4.SelectedValue = "-1"
            ddl_indirizzo4.Items.FindByText("TUTTI").Selected = True

            ddl_zona4.SelectedValue = "-1"
            ddl_zona4.Items.FindByText("TUTTI").Selected = True

            ddl_complesso4.SelectedValue = "-1"
            ddl_complesso4.Items.FindByText("TUTTI").Selected = True

            ddl_edificio4.SelectedValue = "-1"
            ddl_edificio4.Items.FindByText("TUTTI").Selected = True

            ddl_localita4.Enabled = False
            ddl_zona4.Enabled = False
            ddl_quartiere4.Enabled = False
            ddl_indirizzo4.Enabled = False
            ddl_complesso4.Enabled = False
            ddl_edificio4.Enabled = False




            ddl_localita5.SelectedValue = "-1"
            ddl_localita5.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere5.SelectedValue = "-1"
            ddl_quartiere5.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo5.SelectedValue = "-1"
            ddl_indirizzo5.Items.FindByText("TUTTI").Selected = True

            ddl_zona5.SelectedValue = "-1"
            ddl_zona5.Items.FindByText("TUTTI").Selected = True

            ddl_complesso5.SelectedValue = "-1"
            ddl_complesso5.Items.FindByText("TUTTI").Selected = True

            ddl_edificio5.SelectedValue = "-1"
            ddl_edificio5.Items.FindByText("TUTTI").Selected = True

            ddl_localita5.Enabled = False
            ddl_zona5.Enabled = False
            ddl_quartiere5.Enabled = False
            ddl_indirizzo5.Enabled = False
            ddl_complesso5.Enabled = False
            ddl_edificio5.Enabled = False












        End If




    End Sub




    Protected Sub ddl_localita2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_localita2.SelectedIndexChanged

        FiltroLocalita(ddl_localita2, ddl_quartiere2, ddl_indirizzo2, ddl_zona2, ddl_complesso2, ddl_edificio2)


        If ddl_localita2.SelectedItem.Text <> "TUTTI" Then



            ddl_zona2.Enabled = True
            ddl_quartiere2.Enabled = True
            ddl_indirizzo2.Enabled = True
            ddl_complesso2.Enabled = True
            ddl_edificio2.Enabled = True




            ddl_localita3.Enabled = True

        Else

            CaricaComboBlocco(ddl_localita3, ddl_quartiere3, ddl_indirizzo3, ddl_zona3, ddl_complesso3, ddl_edificio3)
            ddl_localita3.SelectedValue = "-1"
            ddl_localita3.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere3.SelectedValue = "-1"
            ddl_quartiere3.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo3.SelectedValue = "-1"
            ddl_indirizzo3.Items.FindByText("TUTTI").Selected = True

            ddl_zona3.SelectedValue = "-1"
            ddl_zona3.Items.FindByText("TUTTI").Selected = True

            ddl_complesso3.SelectedValue = "-1"
            ddl_complesso3.Items.FindByText("TUTTI").Selected = True

            ddl_edificio3.SelectedValue = "-1"
            ddl_edificio3.Items.FindByText("TUTTI").Selected = True




            ddl_zona2.Enabled = False
            ddl_quartiere2.Enabled = False
            ddl_indirizzo2.Enabled = False
            ddl_complesso2.Enabled = False
            ddl_edificio2.Enabled = False





            ddl_localita3.Enabled = False
            ddl_zona3.Enabled = False
            ddl_quartiere3.Enabled = False
            ddl_indirizzo3.Enabled = False
            ddl_complesso3.Enabled = False
            ddl_edificio3.Enabled = False


            ddl_localita4.SelectedValue = "-1"
            ddl_localita4.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere4.SelectedValue = "-1"
            ddl_quartiere4.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo4.SelectedValue = "-1"
            ddl_indirizzo4.Items.FindByText("TUTTI").Selected = True

            ddl_zona4.SelectedValue = "-1"
            ddl_zona4.Items.FindByText("TUTTI").Selected = True

            ddl_complesso4.SelectedValue = "-1"
            ddl_complesso4.Items.FindByText("TUTTI").Selected = True

            ddl_edificio4.SelectedValue = "-1"
            ddl_edificio4.Items.FindByText("TUTTI").Selected = True

            ddl_localita4.Enabled = False
            ddl_zona4.Enabled = False
            ddl_quartiere4.Enabled = False
            ddl_indirizzo4.Enabled = False
            ddl_complesso4.Enabled = False
            ddl_edificio4.Enabled = False




            ddl_localita5.SelectedValue = "-1"
            ddl_localita5.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere5.SelectedValue = "-1"
            ddl_quartiere5.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo5.SelectedValue = "-1"
            ddl_indirizzo5.Items.FindByText("TUTTI").Selected = True

            ddl_zona5.SelectedValue = "-1"
            ddl_zona5.Items.FindByText("TUTTI").Selected = True

            ddl_complesso5.SelectedValue = "-1"
            ddl_complesso5.Items.FindByText("TUTTI").Selected = True

            ddl_edificio5.SelectedValue = "-1"
            ddl_edificio5.Items.FindByText("TUTTI").Selected = True

            ddl_localita5.Enabled = False
            ddl_zona5.Enabled = False
            ddl_quartiere5.Enabled = False
            ddl_indirizzo5.Enabled = False
            ddl_complesso5.Enabled = False
            ddl_edificio5.Enabled = False

        End If






    End Sub

    Protected Sub ddl_quartiere2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_quartiere2.SelectedIndexChanged

        FiltroQuartiere(ddl_localita2, ddl_quartiere2, ddl_indirizzo2, ddl_zona2, ddl_complesso2, ddl_edificio2)


        If ddl_localita2.SelectedValue <> "-1" Then


            ddl_zona2.Enabled = True
            ddl_quartiere2.Enabled = True
            ddl_indirizzo2.Enabled = True
            ddl_complesso2.Enabled = True
            ddl_edificio2.Enabled = True





            ddl_localita3.Enabled = True




        Else

            CaricaComboBlocco(ddl_localita3, ddl_quartiere3, ddl_indirizzo3, ddl_zona3, ddl_complesso3, ddl_edificio3)
            ddl_localita3.SelectedValue = "-1"
            ddl_localita3.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere3.SelectedValue = "-1"
            ddl_quartiere3.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo3.SelectedValue = "-1"
            ddl_indirizzo3.Items.FindByText("TUTTI").Selected = True

            ddl_zona3.SelectedValue = "-1"
            ddl_zona3.Items.FindByText("TUTTI").Selected = True

            ddl_complesso3.SelectedValue = "-1"
            ddl_complesso3.Items.FindByText("TUTTI").Selected = True

            ddl_edificio3.SelectedValue = "-1"
            ddl_edificio3.Items.FindByText("TUTTI").Selected = True

            ddl_localita3.Enabled = False
            ddl_zona3.Enabled = False
            ddl_quartiere3.Enabled = False
            ddl_indirizzo3.Enabled = False
            ddl_complesso3.Enabled = False
            ddl_edificio3.Enabled = False



            ddl_zona2.Enabled = False
            ddl_quartiere2.Enabled = False
            ddl_indirizzo2.Enabled = False
            ddl_complesso2.Enabled = False
            ddl_edificio2.Enabled = False


            ddl_localita4.SelectedValue = "-1"
            ddl_localita4.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere4.SelectedValue = "-1"
            ddl_quartiere4.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo4.SelectedValue = "-1"
            ddl_indirizzo4.Items.FindByText("TUTTI").Selected = True

            ddl_zona4.SelectedValue = "-1"
            ddl_zona4.Items.FindByText("TUTTI").Selected = True

            ddl_complesso4.SelectedValue = "-1"
            ddl_complesso4.Items.FindByText("TUTTI").Selected = True

            ddl_edificio4.SelectedValue = "-1"
            ddl_edificio4.Items.FindByText("TUTTI").Selected = True

            ddl_localita4.Enabled = False
            ddl_zona4.Enabled = False
            ddl_quartiere4.Enabled = False
            ddl_indirizzo4.Enabled = False
            ddl_complesso4.Enabled = False
            ddl_edificio4.Enabled = False




            ddl_localita5.SelectedValue = "-1"
            ddl_localita5.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere5.SelectedValue = "-1"
            ddl_quartiere5.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo5.SelectedValue = "-1"
            ddl_indirizzo5.Items.FindByText("TUTTI").Selected = True

            ddl_zona5.SelectedValue = "-1"
            ddl_zona5.Items.FindByText("TUTTI").Selected = True

            ddl_complesso5.SelectedValue = "-1"
            ddl_complesso5.Items.FindByText("TUTTI").Selected = True

            ddl_edificio5.SelectedValue = "-1"
            ddl_edificio5.Items.FindByText("TUTTI").Selected = True

            ddl_localita5.Enabled = False
            ddl_zona5.Enabled = False
            ddl_quartiere5.Enabled = False
            ddl_indirizzo5.Enabled = False
            ddl_complesso5.Enabled = False
            ddl_edificio5.Enabled = False

        End If


    End Sub

    Protected Sub ddl_localita3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_localita3.SelectedIndexChanged

        FiltroLocalita(ddl_localita3, ddl_quartiere3, ddl_indirizzo3, ddl_zona3, ddl_complesso3, ddl_edificio3)


        If ddl_localita3.SelectedValue <> "-1" Then


            ddl_zona3.Enabled = True
            ddl_quartiere3.Enabled = True
            ddl_indirizzo3.Enabled = True
            ddl_complesso3.Enabled = True
            ddl_edificio3.Enabled = True

            ddl_localita4.Enabled = True



        Else


            CaricaComboBlocco(ddl_localita4, ddl_quartiere4, ddl_indirizzo4, ddl_zona4, ddl_complesso4, ddl_edificio4)
            ddl_localita4.SelectedValue = "-1"
            ddl_localita4.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere4.SelectedValue = "-1"
            ddl_quartiere4.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo4.SelectedValue = "-1"
            ddl_indirizzo4.Items.FindByText("TUTTI").Selected = True

            ddl_zona4.SelectedValue = "-1"
            ddl_zona4.Items.FindByText("TUTTI").Selected = True

            ddl_complesso4.SelectedValue = "-1"
            ddl_complesso4.Items.FindByText("TUTTI").Selected = True

            ddl_edificio4.SelectedValue = "-1"
            ddl_edificio4.Items.FindByText("TUTTI").Selected = True




            ddl_zona3.Enabled = False
            ddl_quartiere3.Enabled = False
            ddl_indirizzo3.Enabled = False
            ddl_complesso3.Enabled = False
            ddl_edificio3.Enabled = False


            ddl_localita4.Enabled = False
            ddl_zona4.Enabled = False
            ddl_quartiere4.Enabled = False
            ddl_indirizzo4.Enabled = False
            ddl_complesso4.Enabled = False
            ddl_edificio4.Enabled = False


            ddl_localita5.SelectedValue = "-1"
            ddl_localita5.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere5.SelectedValue = "-1"
            ddl_quartiere5.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo5.SelectedValue = "-1"
            ddl_indirizzo5.Items.FindByText("TUTTI").Selected = True

            ddl_zona5.SelectedValue = "-1"
            ddl_zona5.Items.FindByText("TUTTI").Selected = True

            ddl_complesso5.SelectedValue = "-1"
            ddl_complesso5.Items.FindByText("TUTTI").Selected = True

            ddl_edificio5.SelectedValue = "-1"
            ddl_edificio5.Items.FindByText("TUTTI").Selected = True

            ddl_localita5.Enabled = False
            ddl_zona5.Enabled = False
            ddl_quartiere5.Enabled = False
            ddl_indirizzo5.Enabled = False
            ddl_complesso5.Enabled = False
            ddl_edificio5.Enabled = False





        End If




    End Sub


    Protected Sub ddl_quartiere3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_quartiere3.SelectedIndexChanged


        FiltroQuartiere(ddl_localita3, ddl_quartiere3, ddl_indirizzo3, ddl_zona3, ddl_complesso3, ddl_edificio3)


        If ddl_localita3.SelectedValue <> "-1" Then


            ddl_zona3.Enabled = True
            ddl_quartiere3.Enabled = True
            ddl_indirizzo3.Enabled = True
            ddl_complesso3.Enabled = True
            ddl_edificio3.Enabled = True





            ddl_localita4.Enabled = True




        Else

            CaricaComboBlocco(ddl_localita4, ddl_quartiere4, ddl_indirizzo4, ddl_zona4, ddl_complesso4, ddl_edificio4)
            ddl_localita4.SelectedValue = "-1"
            ddl_localita4.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere4.SelectedValue = "-1"
            ddl_quartiere4.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo4.SelectedValue = "-1"
            ddl_indirizzo4.Items.FindByText("TUTTI").Selected = True

            ddl_zona4.SelectedValue = "-1"
            ddl_zona4.Items.FindByText("TUTTI").Selected = True

            ddl_complesso4.SelectedValue = "-1"
            ddl_complesso4.Items.FindByText("TUTTI").Selected = True

            ddl_edificio4.SelectedValue = "-1"
            ddl_edificio4.Items.FindByText("TUTTI").Selected = True

            ddl_localita4.Enabled = False
            ddl_zona4.Enabled = False
            ddl_quartiere4.Enabled = False
            ddl_indirizzo4.Enabled = False
            ddl_complesso4.Enabled = False
            ddl_edificio4.Enabled = False



            ddl_zona3.Enabled = False
            ddl_quartiere3.Enabled = False
            ddl_indirizzo3.Enabled = False
            ddl_complesso3.Enabled = False
            ddl_edificio3.Enabled = False



            ddl_localita5.SelectedValue = "-1"
            ddl_localita5.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere5.SelectedValue = "-1"
            ddl_quartiere5.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo5.SelectedValue = "-1"
            ddl_indirizzo5.Items.FindByText("TUTTI").Selected = True

            ddl_zona5.SelectedValue = "-1"
            ddl_zona5.Items.FindByText("TUTTI").Selected = True

            ddl_complesso5.SelectedValue = "-1"
            ddl_complesso5.Items.FindByText("TUTTI").Selected = True

            ddl_edificio5.SelectedValue = "-1"
            ddl_edificio5.Items.FindByText("TUTTI").Selected = True

            ddl_localita5.Enabled = False
            ddl_zona5.Enabled = False
            ddl_quartiere5.Enabled = False
            ddl_indirizzo5.Enabled = False
            ddl_complesso5.Enabled = False
            ddl_edificio5.Enabled = False

        End If



    End Sub




    Protected Sub ddl_localita4_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_localita4.SelectedIndexChanged

        FiltroLocalita(ddl_localita4, ddl_quartiere4, ddl_indirizzo4, ddl_zona4, ddl_complesso4, ddl_edificio4)


        If ddl_localita4.SelectedValue <> "-1" Then


            ddl_zona4.Enabled = True
            ddl_quartiere4.Enabled = True
            ddl_indirizzo4.Enabled = True
            ddl_complesso4.Enabled = True
            ddl_edificio4.Enabled = True

            ddl_localita5.Enabled = True



        Else


            CaricaComboBlocco(ddl_localita5, ddl_quartiere5, ddl_indirizzo5, ddl_zona5, ddl_complesso5, ddl_edificio5)
            ddl_localita5.SelectedValue = "-1"
            ddl_localita5.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere5.SelectedValue = "-1"
            ddl_quartiere5.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo5.SelectedValue = "-1"
            ddl_indirizzo5.Items.FindByText("TUTTI").Selected = True

            ddl_zona5.SelectedValue = "-1"
            ddl_zona5.Items.FindByText("TUTTI").Selected = True

            ddl_complesso5.SelectedValue = "-1"
            ddl_complesso5.Items.FindByText("TUTTI").Selected = True

            ddl_edificio5.SelectedValue = "-1"
            ddl_edificio5.Items.FindByText("TUTTI").Selected = True




            ddl_zona4.Enabled = False
            ddl_quartiere4.Enabled = False
            ddl_indirizzo4.Enabled = False
            ddl_complesso4.Enabled = False
            ddl_edificio4.Enabled = False


            ddl_localita5.Enabled = False
            ddl_zona5.Enabled = False
            ddl_quartiere5.Enabled = False
            ddl_indirizzo5.Enabled = False
            ddl_complesso5.Enabled = False
            ddl_edificio5.Enabled = False




        End If




    End Sub

    Protected Sub ddl_quartiere4_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_quartiere4.SelectedIndexChanged

        FiltroQuartiere(ddl_localita4, ddl_quartiere4, ddl_indirizzo4, ddl_zona4, ddl_complesso4, ddl_edificio4)


        If ddl_localita4.SelectedValue <> "-1" Then


            ddl_zona4.Enabled = True
            ddl_quartiere4.Enabled = True
            ddl_indirizzo4.Enabled = True
            ddl_complesso4.Enabled = True
            ddl_edificio4.Enabled = True





            ddl_localita5.Enabled = True




        Else

            CaricaComboBlocco(ddl_localita5, ddl_quartiere5, ddl_indirizzo5, ddl_zona5, ddl_complesso5, ddl_edificio5)
            ddl_localita5.SelectedValue = "-1"
            ddl_localita5.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere5.SelectedValue = "-1"
            ddl_quartiere5.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo5.SelectedValue = "-1"
            ddl_indirizzo5.Items.FindByText("TUTTI").Selected = True

            ddl_zona5.SelectedValue = "-1"
            ddl_zona5.Items.FindByText("TUTTI").Selected = True

            ddl_complesso5.SelectedValue = "-1"
            ddl_complesso5.Items.FindByText("TUTTI").Selected = True

            ddl_edificio5.SelectedValue = "-1"
            ddl_edificio5.Items.FindByText("TUTTI").Selected = True

            ddl_localita5.Enabled = False
            ddl_zona5.Enabled = False
            ddl_quartiere5.Enabled = False
            ddl_indirizzo5.Enabled = False
            ddl_complesso5.Enabled = False
            ddl_edificio5.Enabled = False



            ddl_zona4.Enabled = False
            ddl_quartiere4.Enabled = False
            ddl_indirizzo4.Enabled = False
            ddl_complesso4.Enabled = False
            ddl_edificio4.Enabled = False



        End If




    End Sub

    Protected Sub ddl_localita5_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_localita5.SelectedIndexChanged

        If ddl_localita5.SelectedValue <> "-1" Then


            ddl_zona5.Enabled = True
            ddl_quartiere5.Enabled = True
            ddl_indirizzo5.Enabled = True
            ddl_complesso5.Enabled = True
            ddl_edificio5.Enabled = True



        Else
            ddl_zona5.Enabled = False
            ddl_quartiere5.Enabled = False
            ddl_indirizzo5.Enabled = False
            ddl_complesso5.Enabled = False
            ddl_edificio5.Enabled = False

        End If


        FiltroLocalita(ddl_localita5, ddl_quartiere5, ddl_indirizzo5, ddl_zona5, ddl_complesso5, ddl_edificio5)


    End Sub


    Protected Sub ddl_quartiere5_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_quartiere5.SelectedIndexChanged

        FiltroQuartiere(ddl_localita5, ddl_quartiere5, ddl_indirizzo5, ddl_zona5, ddl_complesso5, ddl_edificio5)

    End Sub


    Protected Sub ddl_localita1ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_localita1ex.SelectedIndexChanged

        FiltroLocalita(ddl_localita1ex, ddl_quartiere1ex, ddl_indirizzo1ex, ddl_zona1ex, ddl_complesso1ex, ddl_edificio1ex)

        If ddl_localita1ex.SelectedValue <> "-1" Then




            ddl_zona1ex.Enabled = True
            ddl_quartiere1ex.Enabled = True
            ddl_indirizzo1ex.Enabled = True
            ddl_complesso1ex.Enabled = True
            ddl_edificio1ex.Enabled = True




            ddl_localita2ex.Enabled = True




        Else


            CaricaComboBlocco(ddl_localita2ex, ddl_quartiere2ex, ddl_indirizzo2ex, ddl_zona2ex, ddl_complesso2ex, ddl_edificio2ex)

            ddl_localita2ex.SelectedValue = "-1"
            ddl_localita2ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere2ex.SelectedValue = "-1"
            ddl_quartiere2ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo2ex.SelectedValue = "-1"
            ddl_indirizzo2ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona2ex.SelectedValue = "-1"
            ddl_zona2ex.Items.FindByText("TUTTI").Selected = True


            ddl_complesso2ex.SelectedValue = "-1"
            ddl_complesso2ex.Items.FindByText("TUTTI").Selected = True


            ddl_edificio2ex.SelectedValue = "-1"
            ddl_edificio2ex.Items.FindByText("TUTTI").Selected = True


            ddl_localita2ex.Enabled = False
            ddl_zona2ex.Enabled = False
            ddl_quartiere2ex.Enabled = False
            ddl_indirizzo2ex.Enabled = False
            ddl_complesso2ex.Enabled = False
            ddl_edificio2ex.Enabled = False



            ddl_zona1ex.Enabled = False
            ddl_quartiere1ex.Enabled = False
            ddl_indirizzo1ex.Enabled = False
            ddl_complesso1ex.Enabled = False
            ddl_edificio1ex.Enabled = False


            ddl_localita3ex.SelectedValue = "-1"
            ddl_localita3ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere3ex.SelectedValue = "-1"
            ddl_quartiere3ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo3ex.SelectedValue = "-1"
            ddl_indirizzo3ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona3ex.SelectedValue = "-1"
            ddl_zona3ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso3ex.SelectedValue = "-1"
            ddl_complesso3ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio3ex.SelectedValue = "-1"
            ddl_edificio3ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita3ex.Enabled = False
            ddl_zona3ex.Enabled = False
            ddl_quartiere3ex.Enabled = False
            ddl_indirizzo3ex.Enabled = False
            ddl_complesso3ex.Enabled = False
            ddl_edificio3ex.Enabled = False


            ddl_localita4ex.SelectedValue = "-1"
            ddl_localita4ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere4ex.SelectedValue = "-1"
            ddl_quartiere4ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo4ex.SelectedValue = "-1"
            ddl_indirizzo4ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona4ex.SelectedValue = "-1"
            ddl_zona4ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso4ex.SelectedValue = "-1"
            ddl_complesso4ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio4ex.SelectedValue = "-1"
            ddl_edificio4ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita4ex.Enabled = False
            ddl_zona4ex.Enabled = False
            ddl_quartiere4ex.Enabled = False
            ddl_indirizzo4ex.Enabled = False
            ddl_complesso4ex.Enabled = False
            ddl_edificio4ex.Enabled = False


            ddl_localita5ex.SelectedValue = "-1"
            ddl_localita5ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere5ex.SelectedValue = "-1"
            ddl_quartiere5ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo5ex.SelectedValue = "-1"
            ddl_indirizzo5ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona5ex.SelectedValue = "-1"
            ddl_zona5ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso5ex.SelectedValue = "-1"
            ddl_complesso5ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio5ex.SelectedValue = "-1"
            ddl_edificio5ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita5ex.Enabled = False
            ddl_zona5ex.Enabled = False
            ddl_quartiere5ex.Enabled = False
            ddl_indirizzo5ex.Enabled = False
            ddl_complesso5ex.Enabled = False
            ddl_edificio5ex.Enabled = False


        End If


    End Sub

    Protected Sub ddl_quartiere1ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_quartiere1ex.SelectedIndexChanged

        FiltroQuartiere(ddl_localita1ex, ddl_quartiere1ex, ddl_indirizzo1ex, ddl_zona1ex, ddl_complesso1ex, ddl_edificio1ex)



        If ddl_localita1ex.SelectedValue <> "-1" Then


            ddl_zona1ex.Enabled = True
            ddl_quartiere1ex.Enabled = True
            ddl_indirizzo1ex.Enabled = True
            ddl_complesso1ex.Enabled = True
            ddl_edificio1ex.Enabled = True



            ddl_localita2ex.Enabled = True
         
        Else


            CaricaComboBlocco(ddl_localita2ex, ddl_quartiere2ex, ddl_indirizzo2ex, ddl_zona2ex, ddl_complesso2ex, ddl_edificio2ex)

            ddl_localita2ex.SelectedValue = "-1"
            ddl_localita2ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere2ex.SelectedValue = "-1"
            ddl_quartiere2ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo2ex.SelectedValue = "-1"
            ddl_indirizzo2ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona2ex.SelectedValue = "-1"
            ddl_zona2ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso2ex.SelectedValue = "-1"
            ddl_complesso2ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio2ex.SelectedValue = "-1"
            ddl_edificio2ex.Items.FindByText("TUTTI").Selected = True


            ddl_localita2ex.Enabled = False
            ddl_zona2ex.Enabled = False
            ddl_quartiere2ex.Enabled = False
            ddl_indirizzo2ex.Enabled = False
            ddl_complesso2ex.Enabled = False
            ddl_edificio2ex.Enabled = False

            ddl_zona1ex.Enabled = False
            ddl_quartiere1ex.Enabled = False
            ddl_indirizzo1ex.Enabled = False
            ddl_complesso1ex.Enabled = False
            ddl_edificio1ex.Enabled = False

            ddl_localita3ex.SelectedValue = "-1"
            ddl_localita3ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere3ex.SelectedValue = "-1"
            ddl_quartiere3ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo3ex.SelectedValue = "-1"
            ddl_indirizzo3ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona3ex.SelectedValue = "-1"
            ddl_zona3ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso3ex.SelectedValue = "-1"
            ddl_complesso3ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio3ex.SelectedValue = "-1"
            ddl_edificio3ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita3ex.Enabled = False
            ddl_zona3ex.Enabled = False
            ddl_quartiere3ex.Enabled = False
            ddl_indirizzo3ex.Enabled = False
            ddl_complesso3ex.Enabled = False
            ddl_edificio3ex.Enabled = False


            ddl_localita4ex.SelectedValue = "-1"
            ddl_localita4ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere4ex.SelectedValue = "-1"
            ddl_quartiere4ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo4ex.SelectedValue = "-1"
            ddl_indirizzo4ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona4ex.SelectedValue = "-1"
            ddl_zona4ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso4ex.SelectedValue = "-1"
            ddl_complesso4ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio4ex.SelectedValue = "-1"
            ddl_edificio4ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita4ex.Enabled = False
            ddl_zona4ex.Enabled = False
            ddl_quartiere4ex.Enabled = False
            ddl_indirizzo4ex.Enabled = False
            ddl_complesso4ex.Enabled = False
            ddl_edificio4ex.Enabled = False



            ddl_localita5ex.SelectedValue = "-1"
            ddl_localita5ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere5ex.SelectedValue = "-1"
            ddl_quartiere5ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo5ex.SelectedValue = "-1"
            ddl_indirizzo5ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona5ex.SelectedValue = "-1"
            ddl_zona5ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso5ex.SelectedValue = "-1"
            ddl_complesso5ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio5ex.SelectedValue = "-1"
            ddl_edificio5ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita5ex.Enabled = False
            ddl_zona5ex.Enabled = False
            ddl_quartiere5ex.Enabled = False
            ddl_indirizzo5ex.Enabled = False
            ddl_complesso5ex.Enabled = False
            ddl_edificio5ex.Enabled = False







        End If

    End Sub

    Protected Sub ddl_localita2ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_localita2ex.SelectedIndexChanged

        FiltroLocalita(ddl_localita2ex, ddl_quartiere2ex, ddl_indirizzo2ex, ddl_zona2ex, ddl_complesso2ex, ddl_edificio2ex)

        If ddl_localita2ex.SelectedItem.Text <> "TUTTI" Then


            ddl_zona2ex.Enabled = True
            ddl_quartiere2ex.Enabled = True
            ddl_indirizzo2ex.Enabled = True
            ddl_complesso2ex.Enabled = True
            ddl_edificio2ex.Enabled = True



            ddl_localita3ex.Enabled = True
   


        Else

            CaricaComboBlocco(ddl_localita3ex, ddl_quartiere3ex, ddl_indirizzo3ex, ddl_zona3ex, ddl_complesso3ex, ddl_edificio3ex)
            ddl_localita3ex.SelectedValue = "-1"
            ddl_localita3ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere3ex.SelectedValue = "-1"
            ddl_quartiere3ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo3ex.SelectedValue = "-1"
            ddl_indirizzo3ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona3ex.SelectedValue = "-1"
            ddl_zona3ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso3ex.SelectedValue = "-1"
            ddl_complesso3ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio3ex.SelectedValue = "-1"
            ddl_edificio3ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita3ex.Enabled = False
            ddl_zona3ex.Enabled = False
            ddl_quartiere3ex.Enabled = False
            ddl_indirizzo3ex.Enabled = False
            ddl_complesso3ex.Enabled = False
            ddl_edificio3ex.Enabled = False

            ddl_zona2ex.Enabled = False
            ddl_quartiere2ex.Enabled = False
            ddl_indirizzo2ex.Enabled = False
            ddl_complesso2ex.Enabled = False
            ddl_edificio2ex.Enabled = False




            ddl_localita4ex.SelectedValue = "-1"
            ddl_localita4ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere4ex.SelectedValue = "-1"
            ddl_quartiere4ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo4ex.SelectedValue = "-1"
            ddl_indirizzo4ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona4ex.SelectedValue = "-1"
            ddl_zona4ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso4ex.SelectedValue = "-1"
            ddl_complesso4ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio4ex.SelectedValue = "-1"
            ddl_edificio4ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita4ex.Enabled = False
            ddl_zona4ex.Enabled = False
            ddl_quartiere4ex.Enabled = False
            ddl_indirizzo4ex.Enabled = False
            ddl_complesso4ex.Enabled = False
            ddl_edificio4ex.Enabled = False


            ddl_localita5ex.SelectedValue = "-1"
            ddl_localita5ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere5ex.SelectedValue = "-1"
            ddl_quartiere5ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo5ex.SelectedValue = "-1"
            ddl_indirizzo5ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona5ex.SelectedValue = "-1"
            ddl_zona5ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso5ex.SelectedValue = "-1"
            ddl_complesso5ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio5ex.SelectedValue = "-1"
            ddl_edificio5ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita5ex.Enabled = False
            ddl_zona5ex.Enabled = False
            ddl_quartiere5ex.Enabled = False
            ddl_indirizzo5ex.Enabled = False
            ddl_complesso5ex.Enabled = False
            ddl_edificio5ex.Enabled = False



        End If



    End Sub

    Protected Sub ddl_quartiere2ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_quartiere2ex.SelectedIndexChanged

        FiltroQuartiere(ddl_localita2ex, ddl_quartiere2ex, ddl_indirizzo2ex, ddl_zona2ex, ddl_complesso2ex, ddl_edificio2ex)

        If ddl_localita2ex.SelectedValue <> "-1" Then



            ddl_zona2ex.Enabled = True
            ddl_quartiere2ex.Enabled = True
            ddl_indirizzo2ex.Enabled = True
            ddl_complesso2ex.Enabled = True
            ddl_edificio2ex.Enabled = True


            ddl_localita3ex.Enabled = True
     

        Else

            CaricaComboBlocco(ddl_localita3ex, ddl_quartiere3ex, ddl_indirizzo3ex, ddl_zona3ex, ddl_complesso3ex, ddl_edificio3ex)
            ddl_localita3ex.SelectedValue = "-1"
            ddl_localita3ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere3ex.SelectedValue = "-1"
            ddl_quartiere3ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo3ex.SelectedValue = "-1"
            ddl_indirizzo3ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona3ex.SelectedValue = "-1"
            ddl_zona3ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso3ex.SelectedValue = "-1"
            ddl_complesso3ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio3ex.SelectedValue = "-1"
            ddl_edificio3ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita3ex.Enabled = False
            ddl_zona3ex.Enabled = False
            ddl_quartiere3ex.Enabled = False
            ddl_indirizzo3ex.Enabled = False
            ddl_complesso3ex.Enabled = False
            ddl_edificio3ex.Enabled = False

            ddl_zona2ex.Enabled = False
            ddl_quartiere2ex.Enabled = False
            ddl_indirizzo2ex.Enabled = False
            ddl_complesso2ex.Enabled = False
            ddl_edificio2ex.Enabled = False

            ddl_localita4ex.SelectedValue = "-1"
            ddl_localita4ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere4ex.SelectedValue = "-1"
            ddl_quartiere4ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo4ex.SelectedValue = "-1"
            ddl_indirizzo4ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona4ex.SelectedValue = "-1"
            ddl_zona4ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso4ex.SelectedValue = "-1"
            ddl_complesso4ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio4ex.SelectedValue = "-1"
            ddl_edificio4ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita4ex.Enabled = False
            ddl_zona4ex.Enabled = False
            ddl_quartiere4ex.Enabled = False
            ddl_indirizzo4ex.Enabled = False
            ddl_complesso4ex.Enabled = False
            ddl_edificio4ex.Enabled = False



            ddl_localita5ex.SelectedValue = "-1"
            ddl_localita5ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere5ex.SelectedValue = "-1"
            ddl_quartiere5ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo5ex.SelectedValue = "-1"
            ddl_indirizzo5ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona5ex.SelectedValue = "-1"
            ddl_zona5ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso5ex.SelectedValue = "-1"
            ddl_complesso5ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio5ex.SelectedValue = "-1"
            ddl_edificio5ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita5ex.Enabled = False
            ddl_zona5ex.Enabled = False
            ddl_quartiere5ex.Enabled = False
            ddl_indirizzo5ex.Enabled = False
            ddl_complesso5ex.Enabled = False
            ddl_edificio5ex.Enabled = False


        End If

    End Sub

    Protected Sub ddl_localita3ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_localita3ex.SelectedIndexChanged

        FiltroLocalita(ddl_localita3ex, ddl_quartiere3ex, ddl_indirizzo3ex, ddl_zona3ex, ddl_complesso3ex, ddl_edificio3ex)

        If ddl_localita3ex.SelectedValue <> "-1" Then

            ddl_zona3ex.Enabled = True
            ddl_quartiere3ex.Enabled = True
            ddl_indirizzo3ex.Enabled = True
            ddl_complesso3ex.Enabled = True
            ddl_edificio3ex.Enabled = True

            ddl_localita4ex.Enabled = True

        Else
          

            CaricaComboBlocco(ddl_localita4ex, ddl_quartiere4ex, ddl_indirizzo4ex, ddl_zona4ex, ddl_complesso4ex, ddl_edificio4ex)
            ddl_localita4ex.SelectedValue = "-1"
            ddl_localita4ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere4ex.SelectedValue = "-1"
            ddl_quartiere4ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo4ex.SelectedValue = "-1"
            ddl_indirizzo4ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona4ex.SelectedValue = "-1"
            ddl_zona4ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso4ex.SelectedValue = "-1"
            ddl_complesso4ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio4ex.SelectedValue = "-1"
            ddl_edificio4ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita4ex.Enabled = False
            ddl_zona4ex.Enabled = False
            ddl_quartiere4ex.Enabled = False
            ddl_indirizzo4ex.Enabled = False
            ddl_complesso4ex.Enabled = False
            ddl_edificio4ex.Enabled = False

            ddl_zona3ex.Enabled = False
            ddl_quartiere3ex.Enabled = False
            ddl_indirizzo3ex.Enabled = False
            ddl_complesso3ex.Enabled = False
            ddl_edificio3ex.Enabled = False





            ddl_localita5ex.SelectedValue = "-1"
            ddl_localita5ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere5ex.SelectedValue = "-1"
            ddl_quartiere5ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo5ex.SelectedValue = "-1"
            ddl_indirizzo5ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona5ex.SelectedValue = "-1"
            ddl_zona5ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso5ex.SelectedValue = "-1"
            ddl_complesso5ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio5ex.SelectedValue = "-1"
            ddl_edificio5ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita5ex.Enabled = False
            ddl_zona5ex.Enabled = False
            ddl_quartiere5ex.Enabled = False
            ddl_indirizzo5ex.Enabled = False
            ddl_complesso5ex.Enabled = False
            ddl_edificio5ex.Enabled = False





        End If



    End Sub



    Protected Sub ddl_quartiere3ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_quartiere3ex.SelectedIndexChanged


        FiltroQuartiere(ddl_localita3ex, ddl_quartiere3ex, ddl_indirizzo3ex, ddl_zona3ex, ddl_complesso3ex, ddl_edificio3ex)

        If ddl_localita3ex.SelectedValue <> "-1" Then



            ddl_zona3ex.Enabled = True
            ddl_quartiere3ex.Enabled = True
            ddl_indirizzo3ex.Enabled = True
            ddl_complesso3ex.Enabled = True
            ddl_edificio3ex.Enabled = True


            ddl_localita4ex.Enabled = True


        Else

            CaricaComboBlocco(ddl_localita4ex, ddl_quartiere4ex, ddl_indirizzo4ex, ddl_zona4ex, ddl_complesso4ex, ddl_edificio4ex)
            ddl_localita4ex.SelectedValue = "-1"
            ddl_localita4ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere4ex.SelectedValue = "-1"
            ddl_quartiere4ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo4ex.SelectedValue = "-1"
            ddl_indirizzo4ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona4ex.SelectedValue = "-1"
            ddl_zona4ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso4ex.SelectedValue = "-1"
            ddl_complesso4ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio4ex.SelectedValue = "-1"
            ddl_edificio4ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita4ex.Enabled = False
            ddl_zona4ex.Enabled = False
            ddl_quartiere4ex.Enabled = False
            ddl_indirizzo4ex.Enabled = False
            ddl_complesso4ex.Enabled = False
            ddl_edificio4ex.Enabled = False

            ddl_zona3ex.Enabled = False
            ddl_quartiere3ex.Enabled = False
            ddl_indirizzo3ex.Enabled = False
            ddl_complesso3ex.Enabled = False
            ddl_edificio3ex.Enabled = False

           

            ddl_localita5ex.SelectedValue = "-1"
            ddl_localita5ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere5ex.SelectedValue = "-1"
            ddl_quartiere5ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo5ex.SelectedValue = "-1"
            ddl_indirizzo5ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona5ex.SelectedValue = "-1"
            ddl_zona5ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso5ex.SelectedValue = "-1"
            ddl_complesso5ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio5ex.SelectedValue = "-1"
            ddl_edificio5ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita5ex.Enabled = False
            ddl_zona5ex.Enabled = False
            ddl_quartiere5ex.Enabled = False
            ddl_indirizzo5ex.Enabled = False
            ddl_complesso5ex.Enabled = False
            ddl_edificio5ex.Enabled = False


        End If

    End Sub




    Protected Sub ddl_localita4ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_localita4ex.SelectedIndexChanged

        FiltroLocalita(ddl_localita4ex, ddl_quartiere4ex, ddl_indirizzo4ex, ddl_zona4ex, ddl_complesso4ex, ddl_edificio4ex)

        If ddl_localita4ex.SelectedValue <> "-1" Then

            ddl_zona4ex.Enabled = True
            ddl_quartiere4ex.Enabled = True
            ddl_indirizzo4ex.Enabled = True
            ddl_complesso4ex.Enabled = True
            ddl_edificio4ex.Enabled = True

            ddl_localita5ex.Enabled = True

        Else


            CaricaComboBlocco(ddl_localita5ex, ddl_quartiere5ex, ddl_indirizzo5ex, ddl_zona5ex, ddl_complesso5ex, ddl_edificio5ex)
            ddl_localita5ex.SelectedValue = "-1"
            ddl_localita5ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere5ex.SelectedValue = "-1"
            ddl_quartiere5ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo5ex.SelectedValue = "-1"
            ddl_indirizzo5ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona5ex.SelectedValue = "-1"
            ddl_zona5ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso5ex.SelectedValue = "-1"
            ddl_complesso5ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio5ex.SelectedValue = "-1"
            ddl_edificio5ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita5ex.Enabled = False
            ddl_zona5ex.Enabled = False
            ddl_quartiere5ex.Enabled = False
            ddl_indirizzo5ex.Enabled = False
            ddl_complesso5ex.Enabled = False
            ddl_edificio5ex.Enabled = False

            ddl_zona4ex.Enabled = False
            ddl_quartiere4ex.Enabled = False
            ddl_indirizzo4ex.Enabled = False
            ddl_complesso4ex.Enabled = False
            ddl_edificio4ex.Enabled = False


        End If








    End Sub

    Protected Sub ddl_quartiere4ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_quartiere4ex.SelectedIndexChanged

        FiltroQuartiere(ddl_localita4ex, ddl_quartiere4ex, ddl_indirizzo4ex, ddl_zona4ex, ddl_complesso4ex, ddl_edificio4ex)

        If ddl_localita4ex.SelectedValue <> "-1" Then



            ddl_zona4ex.Enabled = True
            ddl_quartiere4ex.Enabled = True
            ddl_indirizzo4ex.Enabled = True
            ddl_complesso4ex.Enabled = True
            ddl_edificio4ex.Enabled = True


            ddl_localita5ex.Enabled = True


        Else

            CaricaComboBlocco(ddl_localita5ex, ddl_quartiere5ex, ddl_indirizzo5ex, ddl_zona5ex, ddl_complesso5ex, ddl_edificio5ex)
            ddl_localita5ex.SelectedValue = "-1"
            ddl_localita5ex.Items.FindByText("TUTTI").Selected = True

            ddl_quartiere5ex.SelectedValue = "-1"
            ddl_quartiere5ex.Items.FindByText("TUTTI").Selected = True

            ddl_indirizzo5ex.SelectedValue = "-1"
            ddl_indirizzo5ex.Items.FindByText("TUTTI").Selected = True

            ddl_zona5ex.SelectedValue = "-1"
            ddl_zona5ex.Items.FindByText("TUTTI").Selected = True

            ddl_complesso5ex.SelectedValue = "-1"
            ddl_complesso5ex.Items.FindByText("TUTTI").Selected = True

            ddl_edificio5ex.SelectedValue = "-1"
            ddl_edificio5ex.Items.FindByText("TUTTI").Selected = True

            ddl_localita5ex.Enabled = False
            ddl_zona5ex.Enabled = False
            ddl_quartiere5ex.Enabled = False
            ddl_indirizzo5ex.Enabled = False
            ddl_complesso5ex.Enabled = False
            ddl_edificio5ex.Enabled = False

            ddl_zona4ex.Enabled = False
            ddl_quartiere4ex.Enabled = False
            ddl_indirizzo4ex.Enabled = False
            ddl_complesso4ex.Enabled = False
            ddl_edificio4ex.Enabled = False




        End If

    End Sub


    Protected Sub ddl_localita5ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_localita5ex.SelectedIndexChanged

        If ddl_localita5ex.SelectedValue <> "-1" Then

            ddl_zona5ex.Enabled = True
            ddl_quartiere5ex.Enabled = True
            ddl_indirizzo5ex.Enabled = True
            ddl_complesso5ex.Enabled = True
            ddl_edificio5ex.Enabled = True


        Else
            ddl_zona5ex.Enabled = False
            ddl_quartiere5ex.Enabled = False
            ddl_indirizzo5ex.Enabled = False
            ddl_complesso5ex.Enabled = False
            ddl_edificio5ex.Enabled = False
        End If

        FiltroLocalita(ddl_localita5ex, ddl_quartiere5ex, ddl_indirizzo5ex, ddl_zona5ex, ddl_complesso5ex, ddl_edificio5ex)

    End Sub

    Protected Sub ddl_quartiere5ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_quartiere5ex.SelectedIndexChanged

        FiltroQuartiere(ddl_localita5ex, ddl_quartiere5ex, ddl_indirizzo5ex, ddl_zona5ex, ddl_complesso5ex, ddl_edificio5ex)

    End Sub

    Private Function ddlConfronto(ByVal ddl1 As DropDownList, ByVal ddl2 As DropDownList, ByVal ddl1ex As DropDownList, ByVal ddl2ex As DropDownList) As Integer
        ddlConfronto = 0


        If ddl1.SelectedValue.ToString = "" Then


            ddl1.SelectedValue = -1

        End If




        If ddl2.SelectedValue.ToString = "" Then


            ddl2.SelectedValue = -1

        End If



        If ddl1ex.SelectedValue.ToString = "" Then


            ddl1ex.SelectedValue = -1

        End If




        If ddl2ex.SelectedValue.ToString = "" Then


            ddl2ex.SelectedValue = -1

        End If




        If (ddl1.SelectedValue = -1 And ddl1ex.SelectedValue = -1) And (ddl2.SelectedValue = -1 And ddl2ex.SelectedValue = -1) Then

            ddlConfronto = 0

            Exit Function
        End If




        If ddl1.SelectedValue = ddl1ex.SelectedValue Then

            If ddl2.SelectedValue = ddl2ex.SelectedValue Then

                ddlConfronto = 1

            End If

        End If

        Return ddlConfronto
    End Function


    Private Function ddlConfrontoPiani(ByVal ddl1 As DropDownList, ByVal ddl2 As DropDownList) As Integer
        ddlConfrontoPiani = 0

        If (ddl1.SelectedValue = -1 And ddl2.SelectedValue = -1) Then
            ddlConfrontoPiani = 0
            Exit Function

        End If


        If ddl1.SelectedValue = ddl2.SelectedValue Then

            ddlConfrontoPiani = 1

        End If
        Return ddlConfrontoPiani
    End Function



    Private Function ddlLocalitaVuota(ByVal ddl1 As DropDownList, ByVal ddl2 As DropDownList, ByVal ddl3 As DropDownList, ByVal ddl4 As DropDownList, ByVal ddl5 As DropDownList, ByVal ddl6 As DropDownList) As Integer
        ddlLocalitaVuota = 0




        If ddl1.SelectedValue.ToString = "" Then
            ddl1.SelectedValue = -1
        End If

        If ddl2.SelectedValue.ToString = "" Then
            ddl2.SelectedValue = -1
        End If
        If ddl3.SelectedValue.ToString = "" Then
            ddl3.SelectedValue = -1
        End If

        If ddl4.SelectedValue.ToString = "" Then
            ddl4.SelectedValue = -1
        End If
        If ddl5.SelectedValue.ToString = "" Then
            ddl5.SelectedValue = -1
        End If

        If ddl6.SelectedValue.ToString = "" Then
            ddl6.SelectedValue = -1
        End If





        If (ddl1.SelectedValue = -1) And (ddl2.SelectedValue = -1) And (ddl3.SelectedValue = "-1") And (ddl4.SelectedValue = -1) And (ddl5.SelectedValue = -1) And (ddl6.SelectedValue = -1) Then

            ddlLocalitaVuota = 0
            Exit Function

        End If

        If (ddl1.SelectedValue = -1) Then

            ddlLocalitaVuota = 1

        Else
            ddlLocalitaVuota = 0


        End If



        Return ddlLocalitaVuota
    End Function



    Private Function ChkZerUno(ByVal chk As CheckBox) As Integer
        ChkZerUno = 0

        If chk.Checked = True Then
            ChkZerUno = 1
        Else

            ChkZerUno = 0
        End If

        Return ChkZerUno
    End Function


    Private Sub FiltroLocalita(ByVal ddl1 As DropDownList, ByVal ddl2 As DropDownList, ByVal ddl3 As DropDownList, ByVal ddl4 As DropDownList, ByVal ddl5 As DropDownList, ByVal ddl6 As DropDownList)

        Dim stringaSQL As String = ""
        Dim Sstringa As String = ""



        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            If ddl1.SelectedValue <> "-1" Then


                If ddl2.SelectedValue.ToString = "" Or ddl2.SelectedValue = "-1" Then

                    stringaSQL = " "
                Else

                    '  stringaSQL = " and tab_quartieri.id=" & ddl2.SelectedItem.Value

                End If

                Dim ds As New Data.DataTable

                Sstringa = "SELECT distinct tab_quartieri.nome as nomeQuartiere, tab_quartieri.id as idQuartiere,siscom_mi.indirizzi.descrizione as desc_indirizzi,complessi_immobiliari.id as idComplesso,complessi_immobiliari.denominazione as nomeComplesso,edifici.id as idEdificio,edifici.denominazione as nomeEdificio " _
                           & "  FROM  sepa.comuni_nazioni, siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari, siscom_mi.edifici,siscom_mi.indirizzi " _
                           & " WHERE comuni_nazioni.cod = edifici.cod_comune " _
                           & " and complessi_immobiliari.id = edifici.id_complesso " _
                           & " And edifici.id_indirizzo_principale = siscom_mi.indirizzi.ID " _
                           & " and indirizzi.ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) " _
                           & " and comuni_nazioni.ID=" & ddl1.SelectedValue.ToString & " " _
                           & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                           & " " & stringaSQL & " " _
                           & " ORDER BY nomeQuartiere,desc_indirizzi,nomeComplesso,nomeEdificio ASC"

                Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                da1.Fill(ds)
                da1.Dispose()
                ddl2.Items.Clear()
                ddl3.Items.Clear()
                ddl5.Items.Clear()
                ddl6.Items.Clear()

                Dim k As Integer = 0
                If ds.Rows.Count > 1 Then
                    Do While k < ds.Rows.Count - 1

                        If k = 0 Then
                            ddl2.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeQuartiere"), ""), par.IfNull(ds.Rows(k).Item("idQuartiere"), "")))
                            ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "")))
                            ddl5.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeComplesso"), ""), par.IfNull(ds.Rows(k).Item("idComplesso"), "")))
                            ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k).Item("idEdificio"), "")))
                        End If


                        If par.IfNull(ds.Rows(k).Item("idQuartiere"), 0) <> par.IfNull(ds.Rows(k + 1).Item("idQuartiere"), 0) Then
                            ddl2.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("nomeQuartiere"), ""), par.IfNull(ds.Rows(k + 1).Item("idQuartiere"), "")))
                        End If

                        If par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "") <> par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), "") Then
                            ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), "")))
                        End If

                        If par.IfNull(ds.Rows(k).Item("idComplesso"), 0) <> par.IfNull(ds.Rows(k + 1).Item("idComplesso"), 0) Then
                            ddl5.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("nomeComplesso"), ""), par.IfNull(ds.Rows(k + 1).Item("idComplesso"), "")))
                        End If

                        If par.IfNull(ds.Rows(k).Item("idEdificio"), 0) <> par.IfNull(ds.Rows(k + 1).Item("idEdificio"), 0) Then
                            ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k + 1).Item("idEdificio"), "")))
                        End If


                        k = k + 1
                    Loop
                Else
                    ddl2.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeQuartiere"), ""), par.IfNull(ds.Rows(k).Item("idQuartiere"), "")))
                    ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "")))
                    ddl5.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeComplesso"), ""), par.IfNull(ds.Rows(k).Item("idComplesso"), "")))
                    ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k).Item("idEdificio"), "")))
                End If


                ddl2.Items.Add(New ListItem("TUTTI", "-1"))
                ddl2.Items.FindByText("TUTTI").Selected = True

                ddl3.Items.Add(New ListItem("TUTTI", "-1"))
                ddl3.Items.FindByText("TUTTI").Selected = True

                ddl5.Items.Add(New ListItem("TUTTI", "-1"))
                ddl5.Items.FindByText("TUTTI").Selected = True

                ddl6.Items.Add(New ListItem("TUTTI", "-1"))
                ddl6.Items.FindByText("TUTTI").Selected = True

                ds = New Data.DataTable
                da1.Dispose()

                Sstringa = "SELECT DISTINCT zona_aler.cod as id_zona, zona_aler.zona as desc_zona" _
                                   & " FROM siscom_mi.indirizzi, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri, sepa.comuni_nazioni, siscom_mi.edifici, zona_aler " _
                                   & " WHERE edifici.id_indirizzo_principale = siscom_mi.indirizzi.ID " _
                                   & " and comuni_nazioni.cod = edifici.cod_comune " _
                                   & " and comuni_nazioni.id= " & ddl1.SelectedValue.ToString & " " _
                                   & " and complessi_immobiliari.id (+)= edifici.id_complesso " _
                                   & " and edifici.id_zona = zona_aler.cod (+)" _
                                   & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                                   & " " & stringaSQL & " " _
                                   & " order by zona_aler.zona "

                da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                da1.Fill(ds)

                ddl4.Items.Clear()


                For Each row As Data.DataRow In ds.Rows

                    ddl4.Items.Add(New ListItem(par.IfNull(row.Item("desc_zona"), ""), par.IfNull(row.Item("id_zona"), "")))
                Next


                ds = New Data.DataTable
                da1.Dispose()


                ddl4.Items.Add(New ListItem("TUTTI", "-1"))
                ddl4.Items.FindByText("TUTTI").Selected = True

            Else

                'ddl1.Items.Clear()
                ddl2.Items.Clear()
                ddl3.Items.Clear()
                ddl4.Items.Clear()
                ddl5.Items.Clear()
                ddl6.Items.Clear()


                CaricaComboBlocco(ddl1, ddl2, ddl3, ddl4, ddl5, ddl6)






            End If

            ddl2.Items.Remove("")
            ddl3.Items.Remove("")
            ddl4.Items.Remove("")



        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>location.replace('../Errore.aspx');</script>")

        End Try


    End Sub



    Private Sub CaricaComboBlocco(ByVal ddl1 As DropDownList, ByVal ddl2 As DropDownList, ByVal ddl3 As DropDownList, ByVal ddl4 As DropDownList, ByVal ddl5 As DropDownList, ByVal ddl6 As DropDownList)


        Try
            Dim Sstringa As String = ""




            Dim ds As New Data.DataTable


            Sstringa = "SELECT distinct tab_quartieri.nome as nomeQuartiere, tab_quartieri.id as idQuartiere,siscom_mi.indirizzi.descrizione as desc_indirizzi,complessi_immobiliari.id as idComplesso,complessi_immobiliari.denominazione as nomeComplesso,edifici.id as idEdificio,edifici.denominazione as nomeEdificio " _
                        & "  FROM  sepa.comuni_nazioni, siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari, siscom_mi.edifici,siscom_mi.indirizzi " _
                        & " WHERE comuni_nazioni.cod = edifici.cod_comune " _
                        & " and complessi_immobiliari.id = edifici.id_complesso " _
                        & " And edifici.id_indirizzo_principale = siscom_mi.indirizzi.ID " _
                        & " and indirizzi.ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) " _
                        & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                        & " ORDER BY nomeQuartiere,desc_indirizzi,nomeComplesso,nomeEdificio ASC"

            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
            da1.Fill(ds)
            da1.Dispose()

            ddl2.Items.Clear()
            ddl3.Items.Clear()
            ddl5.Items.Clear()
            ddl6.Items.Clear()

            Dim k As Integer = 0
            If ds.Rows.Count > 1 Then
                Do While k < ds.Rows.Count - 1

                    If k = 0 Then

                        ddl2.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeQuartiere"), ""), par.IfNull(ds.Rows(k).Item("idQuartiere"), "")))
                        ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "")))
                        ddl5.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeComplesso"), ""), par.IfNull(ds.Rows(k).Item("idComplesso"), "")))
                        ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k).Item("idEdificio"), "")))
                    End If



                    If par.IfNull(ds.Rows(k).Item("idQuartiere"), 0) <> par.IfNull(ds.Rows(k + 1).Item("idQuartiere"), 0) Then
                        ddl2.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("nomeQuartiere"), ""), par.IfNull(ds.Rows(k + 1).Item("idQuartiere"), "")))
                    End If

                    If par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "") <> par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), "") Then
                        ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), "")))
                    End If

                    If par.IfNull(ds.Rows(k).Item("idComplesso"), 0) <> par.IfNull(ds.Rows(k + 1).Item("idComplesso"), 0) Then
                        ddl5.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("nomeComplesso"), ""), par.IfNull(ds.Rows(k + 1).Item("idComplesso"), "")))
                    End If

                    If par.IfNull(ds.Rows(k).Item("idEdificio"), 0) <> par.IfNull(ds.Rows(k + 1).Item("idEdificio"), 0) Then
                        ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k + 1).Item("idEdificio"), "")))
                    End If


                    k = k + 1
                Loop
            Else

                ddl2.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeQuartiere"), ""), par.IfNull(ds.Rows(k).Item("idQuartiere"), "")))
                ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "")))
                ddl5.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeComplesso"), ""), par.IfNull(ds.Rows(k).Item("idComplesso"), "")))
                ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k).Item("idEdificio"), "")))
            End If


            '  ddl1.Items.Add(New ListItem("TUTTI", "-1"))
            ' ddl1.Items.FindByValue("-1").Selected = True

            ddl2.Items.Add(New ListItem("TUTTI", "-1"))
            ddl2.Items.FindByText("TUTTI").Selected = True

            ddl3.Items.Add(New ListItem("TUTTI", "-1"))
            ddl3.Items.FindByText("TUTTI").Selected = True

            ddl5.Items.Add(New ListItem("TUTTI", "-1"))
            ddl5.Items.FindByText("TUTTI").Selected = True

            ddl6.Items.Add(New ListItem("TUTTI", "-1"))
            ddl6.Items.FindByText("TUTTI").Selected = True
















            ds = New Data.DataTable
            da1.Dispose()


            Sstringa = "SELECT * from zona_aler order by zona asc"
            da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
            da1.Fill(ds)
            da1.Dispose()
            ddl4.Items.Clear()

            For Each row As Data.DataRow In ds.Rows
                ddl4.Items.Add(New ListItem(par.IfNull(row.Item("ZONA"), ""), par.IfNull(row.Item("COD"), "")))
            Next
            ddl4.Items.Add(New ListItem("TUTTI", "-1"))
            ddl4.Items.FindByText("TUTTI").Selected = True











        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub



    Private Sub FiltroQuartiere(ByVal ddl1 As DropDownList, ByVal ddl2 As DropDownList, ByVal ddl3 As DropDownList, ByVal ddl4 As DropDownList, ByVal ddl5 As DropDownList, ByVal ddl6 As DropDownList)


        Dim ds As New Data.DataTable
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Dim Sstringa As String = ""
            Dim stringaSQL As String = ""


            If ddl2.SelectedItem.Text = "NON DEFINIBILE" Then

                Exit Sub


            End If




            If ddl1.SelectedValue <> "-1" Then

                If ddl2.SelectedValue.ToString = "" Or ddl2.SelectedValue = "-1" Then

                    stringaSQL = " "

                Else
                    stringaSQL = " and tab_quartieri.id=" & ddl2.SelectedItem.Value


                End If



                ds = New Data.DataTable

                'Sstringa = "SELECT DISTINCT (comuni_nazioni.ID) as id_comune, comuni_nazioni.nome as nome_comune" _
                '                  & " FROM siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri, sepa.comuni_nazioni, siscom_mi.edifici, zona_aler " _
                '                  & " where comuni_nazioni.cod = edifici.cod_comune " _
                '                    & " and comuni_nazioni.id= " & ddl1.SelectedValue.ToString & " " _
                '                  & " and complessi_immobiliari.id (+)= edifici.id_complesso " _
                '                  & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                '                  & " AND edifici.id_zona= zona_aler.cod (+)  " _
                '                  & " " & stringaSQL & " " _
                '                  & " order by nome_comune asc "
                'Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                'da1.Fill(ds)
                'ddl1.Items.Clear()


                'For Each row As Data.DataRow In ds.Rows
                '    ddl1.Items.Add(New ListItem(par.IfNull(row.Item("nome_comune"), ""), par.IfNull(row.Item("id_comune"), "")))

                'Next
                'ddl1.Items.Add(New ListItem("TUTTI", "-1"))





                Sstringa = "SELECT distinct siscom_mi.indirizzi.descrizione as desc_indirizzi,complessi_immobiliari.id as idComplesso,complessi_immobiliari.denominazione as nomeComplesso,edifici.id as idEdificio,edifici.denominazione as nomeEdificio " _
                         & "  FROM  sepa.comuni_nazioni, siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari, siscom_mi.edifici,siscom_mi.indirizzi " _
                         & " WHERE comuni_nazioni.cod = edifici.cod_comune " _
                         & " and complessi_immobiliari.id = edifici.id_complesso " _
                         & " And edifici.id_indirizzo_principale = siscom_mi.indirizzi.ID " _
                         & " and indirizzi.ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) " _
                         & " and comuni_nazioni.ID=" & ddl1.SelectedValue.ToString & " " _
                         & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                         & " " & stringaSQL & " " _
                         & " ORDER BY desc_indirizzi,nomeComplesso,nomeEdificio ASC"

                Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                da1.Fill(ds)

                '  ddl1.Items.Clear()
                ddl3.Items.Clear()
                ddl5.Items.Clear()
                ddl6.Items.Clear()

                Dim k As Integer = 0
                If ds.Rows.Count > 1 Then
                    Do While k < ds.Rows.Count - 1

                        If k = 0 Then

                            ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "")))
                            ddl5.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeComplesso"), ""), par.IfNull(ds.Rows(k).Item("idComplesso"), "")))
                            ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k).Item("idEdificio"), "")))
                        End If




                        If par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "") <> par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), "") Then
                            ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), "")))
                        End If

                        If par.IfNull(ds.Rows(k).Item("idComplesso"), 0) <> par.IfNull(ds.Rows(k + 1).Item("idComplesso"), 0) Then
                            ddl5.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("nomeComplesso"), ""), par.IfNull(ds.Rows(k + 1).Item("idComplesso"), "")))
                        End If

                        If par.IfNull(ds.Rows(k).Item("idEdificio"), 0) <> par.IfNull(ds.Rows(k + 1).Item("idEdificio"), 0) Then
                            ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k + 1).Item("idEdificio"), "")))
                        End If


                        k = k + 1
                    Loop
                Else

                    ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "")))
                    ddl5.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeComplesso"), ""), par.IfNull(ds.Rows(k).Item("idComplesso"), "")))
                    ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k).Item("idEdificio"), "")))
                End If


                '  ddl1.Items.Add(New ListItem("TUTTI", "-1"))
                ' ddl1.Items.FindByText("TUTTI").Selected = True

                ddl3.Items.Add(New ListItem("TUTTI", "-1"))
                ddl3.Items.FindByText("TUTTI").Selected = True

                ddl5.Items.Add(New ListItem("TUTTI", "-1"))
                ddl5.Items.FindByText("TUTTI").Selected = True

                ddl6.Items.Add(New ListItem("TUTTI", "-1"))
                ddl6.Items.FindByText("TUTTI").Selected = True


                ds = New Data.DataTable
                da1.Dispose()






                If stringaSQL <> " " Then

                    Sstringa = "SELECT DISTINCT zona_aler.zona as desc_zona, zona_aler.cod as id_zona" _
                                    & " FROM siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri, sepa.comuni_nazioni, siscom_mi.edifici, zona_aler " _
                                    & " where comuni_nazioni.cod = edifici.cod_comune " _
                                    & " and complessi_immobiliari.id (+)= edifici.id_complesso " _
                                    & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                                    & " AND edifici.id_zona= zona_aler.cod (+)  " _
                                    & " " & stringaSQL & " " _
                                    & " order by desc_zona asc "
                    da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                    da1.Fill(ds)

                    ddl4.Items.Clear()

                    For Each row As Data.DataRow In ds.Rows

                        ddl4.Items.Add(New ListItem(par.IfNull(row.Item("desc_zona"), ""), par.IfNull(row.Item("id_zona"), "")))
                    Next

                    ddl4.Items.Add(New ListItem("TUTTI", "-1"))
                    ddl4.Items.FindByText("TUTTI").Selected = True






                    ds = New Data.DataTable
                    da1.Dispose()









                Else



                    'ddl1.Items.FindByText("TUTTI").Selected = True


                    ds = New Data.DataTable
                    da1.Dispose()


                    Sstringa = "SELECT * from zona_aler order by zona asc"
                    da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                    da1.Fill(ds)
                    ddl4.Items.Clear()

                    For Each row As Data.DataRow In ds.Rows
                        ddl4.Items.Add(New ListItem(par.IfNull(row.Item("ZONA"), ""), par.IfNull(row.Item("COD"), "")))
                    Next
                    ddl4.Items.Add(New ListItem("TUTTI", "-1"))
                    ddl4.Items.FindByText("TUTTI").Selected = True




                    ds = New Data.DataTable
                    da1.Dispose()

                    Sstringa = "SELECT DISTINCT tab_quartieri.id as id_quart, tab_quartieri.nome as nome_quart" _
                                   & " FROM siscom_mi.indirizzi, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri, sepa.comuni_nazioni, siscom_mi.edifici, zona_aler " _
                                   & " WHERE indirizzi.ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) " _
                                   & " and comuni_nazioni.cod = edifici.cod_comune " _
                                   & " and comuni_nazioni.id= " & ddl1.SelectedValue.ToString & " " _
                                   & " and complessi_immobiliari.id (+)= edifici.id_complesso " _
                                   & " and edifici.id_zona = zona_aler.cod (+)" _
                                   & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                                   & " and edifici.id_indirizzo_principale = siscom_mi.indirizzi.ID " _
                                   & " order by tab_quartieri.nome "

                    da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                    da1.Fill(ds)

                    ddl2.Items.Clear()


                    For Each row As Data.DataRow In ds.Rows

                        ddl2.Items.Add(New ListItem(par.IfNull(row.Item("nome_quart"), ""), par.IfNull(row.Item("id_quart"), "")))
                    Next


                    ddl2.Items.Add(New ListItem("TUTTI", "-1"))
                    ddl2.Items.FindByText("TUTTI").Selected = True






                End If



                'ds = New Data.DataTable
                'da1.Dispose()




                'Sstringa = "SELECT DISTINCT siscom_mi.indirizzi.descrizione " _
                '                   & " FROM siscom_mi.indirizzi, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri, sepa.comuni_nazioni, siscom_mi.edifici " _
                '                    & " WHERE indirizzi.ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) " _
                '                   & " and comuni_nazioni.cod = edifici.cod_comune " _
                '                   & " and comuni_nazioni.id= " & ddl1.SelectedValue.ToString & " " _
                '                   & " and complessi_immobiliari.id (+)= edifici.id_complesso " _
                '                   & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                '                   & " and edifici.id_indirizzo_principale = siscom_mi.indirizzi.ID " _
                '                   & " " & stringaSQL & " " _
                '                   & " order by indirizzi.descrizione "

                'da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                'da1.Fill(ds)
                'ddl3.Items.Clear()

                'For Each row As Data.DataRow In ds.Rows
                '    ddl3.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("DESCRIZIONE"), "")))
                'Next
                'ddl3.Items.Add(New ListItem("TUTTI", "-1"))
                'ddl3.Items.FindByText("TUTTI").Selected = True






                'ds = New Data.DataTable
                'da1.Dispose()




                'Sstringa = "SELECT DISTINCT complessi_immobiliari.id as id_comp, complessi_immobiliari.denominazione as den_comp" _
                '                   & " FROM siscom_mi.indirizzi, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri, sepa.comuni_nazioni, siscom_mi.edifici, zona_aler " _
                '                   & " WHERE indirizzi.ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) " _
                '                   & " and comuni_nazioni.cod = edifici.cod_comune " _
                '                   & " and comuni_nazioni.id= " & ddl1.SelectedValue.ToString & " " _
                '                   & " and complessi_immobiliari.id (+)= edifici.id_complesso " _
                '                   & " and edifici.id_zona = zona_aler.cod (+)" _
                '                   & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                '                   & " and edifici.id_indirizzo_principale = siscom_mi.indirizzi.ID " _
                '                   & " " & stringaSQL & " " _
                '                   & " order by complessi_immobiliari.denominazione "

                'da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                'da1.Fill(ds)

                'ddl5.Items.Clear()


                'For Each row As Data.DataRow In ds.Rows

                '    ddl5.Items.Add(New ListItem(par.IfNull(row.Item("den_comp"), ""), par.IfNull(row.Item("id_comp"), "")))
                'Next


                'ddl5.Items.Add(New ListItem("TUTTI", "-1"))
                'ddl5.Items.FindByText("TUTTI").Selected = True






                'ds = New Data.DataTable
                'da1.Dispose()


                'Sstringa = "SELECT DISTINCT edifici.id as id_edif, edifici.denominazione as den_edif" _
                '                   & " FROM siscom_mi.indirizzi, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri, sepa.comuni_nazioni, siscom_mi.edifici, zona_aler " _
                '                   & " WHERE indirizzi.ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) " _
                '                   & " and comuni_nazioni.cod = edifici.cod_comune " _
                '                   & " and comuni_nazioni.id= " & ddl1.SelectedValue.ToString & " " _
                '                   & " and complessi_immobiliari.id (+)= edifici.id_complesso " _
                '                   & " and edifici.id_zona = zona_aler.cod (+)" _
                '                   & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                '                   & " and edifici.id_indirizzo_principale = siscom_mi.indirizzi.ID " _
                '                   & " " & stringaSQL & " " _
                '                   & " order by edifici.denominazione "

                'da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                'da1.Fill(ds)

                'ddl6.Items.Clear()


                'For Each row As Data.DataRow In ds.Rows

                '    ddl6.Items.Add(New ListItem(par.IfNull(row.Item("den_edif"), ""), par.IfNull(row.Item("id_edif"), "")))
                'Next


                'ddl6.Items.Add(New ListItem("TUTTI", "-1"))
                'ddl6.Items.FindByText("TUTTI").Selected = True









                'Else




                '    If ddl2.SelectedValue <> "-1" And ddl2.SelectedValue <> "" Then

                '        ds = New Data.DataTable

                '        Sstringa = "SELECT DISTINCT (comuni_nazioni.ID) as id_comune, comuni_nazioni.nome as nome_comune " _
                '                          & " FROM siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri, sepa.comuni_nazioni, siscom_mi.edifici, zona_aler " _
                '                          & " where comuni_nazioni.cod = edifici.cod_comune " _
                '                          & " and complessi_immobiliari.id (+)= edifici.id_complesso " _
                '                          & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                '                            & " AND edifici.id_zona= zona_aler.cod (+)  " _
                '                          & " and tab_quartieri.id=" & ddl2.SelectedItem.Value & " " _
                '                          & " order by comuni_nazioni.nome asc "
                '        Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                '        da1.Fill(ds)
                '        ddl1.Items.Clear()

                '        'ddl_indirizzo1.Items.Add(New ListItem("- - - ", "-1"))
                '        For Each row As Data.DataRow In ds.Rows
                '            ddl1.Items.Add(New ListItem(par.IfNull(row.Item("nome_comune"), ""), par.IfNull(row.Item("id_comune"), "")))


                '        Next
                '        ddl1.Items.Add(New ListItem("TUTTI", "-1"))








                '        ds = New Data.DataTable
                '        da1.Dispose()

                '        ds = New Data.DataTable

                '        Sstringa = "SELECT DISTINCT zona_aler.cod as id_zona, zona_aler.zona as descr_zona " _
                '                          & " FROM siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri, sepa.comuni_nazioni, siscom_mi.edifici, zona_aler " _
                '                          & " where comuni_nazioni.cod = edifici.cod_comune " _
                '                          & " and complessi_immobiliari.id (+)= edifici.id_complesso " _
                '                          & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                '                            & " AND edifici.id_zona= zona_aler.cod (+)  " _
                '                          & " and tab_quartieri.id=" & ddl2.SelectedItem.Value & " " _
                '                          & " order by zona_aler.zona asc "
                '        da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                '        da1.Fill(ds)

                '        ddl4.Items.Clear()

                '        For Each row As Data.DataRow In ds.Rows

                '            ddl4.Items.Add(New ListItem(par.IfNull(row.Item("descr_zona"), ""), par.IfNull(row.Item("id_zona"), "")))

                '        Next

                '        ddl4.Items.Add(New ListItem("TUTTI", "-1"))




                '        ds = New Data.DataTable
                '        da1.Dispose()




                '        Sstringa = "SELECT DISTINCT complessi_immobiliari.id as id_comp, complessi_immobiliari.denominazione as den_comp" _
                '                           & " FROM siscom_mi.indirizzi, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri, sepa.comuni_nazioni, siscom_mi.edifici, zona_aler " _
                '                            & " WHERE indirizzi.ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) " _
                '                           & " and comuni_nazioni.cod = edifici.cod_comune " _
                '                           & " and complessi_immobiliari.id (+)= edifici.id_complesso " _
                '                           & " and edifici.id_zona = zona_aler.cod (+)" _
                '                           & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                '                            & " and tab_quartieri.id=" & ddl2.SelectedItem.Value & " " _
                '                           & " order by complessi_immobiliari.denominazione "

                '        da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                '        da1.Fill(ds)

                '        ddl5.Items.Clear()


                '        For Each row As Data.DataRow In ds.Rows

                '            ddl5.Items.Add(New ListItem(par.IfNull(row.Item("den_comp"), ""), par.IfNull(row.Item("id_comp"), "")))
                '        Next


                '        ddl5.Items.Add(New ListItem("TUTTI", "-1"))
                '        ddl5.Items.FindByText("TUTTI").Selected = True





                '        ds = New Data.DataTable
                '        da1.Dispose()


                '        Sstringa = "SELECT DISTINCT edifici.id as id_edif, edifici.denominazione as den_edif" _
                '                           & " FROM siscom_mi.indirizzi, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri, sepa.comuni_nazioni, siscom_mi.edifici, zona_aler " _
                '                            & " WHERE indirizzi.ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) " _
                '                           & " and comuni_nazioni.cod = edifici.cod_comune " _
                '                           & " and complessi_immobiliari.id (+)= edifici.id_complesso " _
                '                           & " and edifici.id_zona = zona_aler.cod (+)" _
                '                           & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                '                            & " and tab_quartieri.id=" & ddl2.SelectedItem.Value & " " _
                '                            & " and edifici.id_indirizzo_principale = siscom_mi.indirizzi.ID " _
                '                           & " order by edifici.denominazione "

                '        da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                '        da1.Fill(ds)

                '        ddl6.Items.Clear()


                '        For Each row As Data.DataRow In ds.Rows

                '            ddl6.Items.Add(New ListItem(par.IfNull(row.Item("den_edif"), ""), par.IfNull(row.Item("id_edif"), "")))
                '        Next


                '        ddl6.Items.Add(New ListItem("TUTTI", "-1"))
                '        ddl6.Items.FindByText("TUTTI").Selected = True



                '        Sstringa = "SELECT DISTINCT siscom_mi.indirizzi.descrizione " _
                '                           & " FROM siscom_mi.indirizzi, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri, sepa.comuni_nazioni, siscom_mi.edifici " _
                '                            & " WHERE indirizzi.ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) " _
                '                           & " and comuni_nazioni.cod = edifici.cod_comune " _
                '                           & " and complessi_immobiliari.id (+)= edifici.id_complesso " _
                '                           & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                '                           & " and tab_quartieri.id=" & ddl2.SelectedItem.Value & " " _
                '                           & " and edifici.id_indirizzo_principale = siscom_mi.indirizzi.ID " _
                '                           & " order by indirizzi.descrizione "

                '        da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                '        da1.Fill(ds)
                '        ddl3.Items.Clear()

                '        For Each row As Data.DataRow In ds.Rows
                '            ddl3.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("DESCRIZIONE"), "")))
                '        Next
                '        ddl3.Items.Add(New ListItem("TUTTI", "-1"))
                '        ddl3.Items.FindByText("TUTTI").Selected = True


            Else
                ddl3.Items.Clear()
                ddl1.Items.Clear()
                ddl2.Items.Clear()
                ddl4.Items.Clear()
                ddl5.Items.Clear()
                ddl6.Items.Clear()
                CaricaComboBlocco(ddl1, ddl2, ddl3, ddl4, ddl5, ddl6)

            End If

            '   End If


            ddl1.Items.Remove("")
            ddl3.Items.Remove("")
            ddl4.Items.Remove("")



        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub FiltroComplesso(ByVal ddl1 As DropDownList, ByVal ddl2 As DropDownList, ByVal ddl3 As DropDownList, ByVal ddl4 As DropDownList, ByVal ddl5 As DropDownList, ByVal ddl6 As DropDownList)


        Dim ds As New Data.DataTable
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Dim Sstringa As String = ""
            Dim stringaSQL As String = ""
            Dim pieno As Boolean = False

            'ddl1=localita
            'ddl2=quartiere
            'ddl3=indirizzo
            'ddl4=zona
            'ddl5=complesso
            'ddl6=edificio



            If ddl5.SelectedValue <> "-1" Then


                If ddl5.SelectedValue.ToString <> "" And ddl5.SelectedValue <> "-1" Then


                    stringaSQL = " and complessi_immobiliari.id=" & ddl5.SelectedItem.Value


                End If




                Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)



                If stringaSQL <> " " Then

                    Sstringa = "SELECT DISTINCT zona_aler.zona as desc_zona, zona_aler.cod as id_zona" _
                                    & " FROM siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri, sepa.comuni_nazioni, siscom_mi.edifici, zona_aler " _
                                    & " where comuni_nazioni.cod = edifici.cod_comune " _
                                    & " and complessi_immobiliari.id (+)= edifici.id_complesso " _
                                    & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                                    & " AND edifici.id_zona= zona_aler.cod (+)  " _
                                    & " " & stringaSQL & " " _
                                    & " order by desc_zona asc "
                    da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                    da1.Fill(ds)

                    ddl4.Items.Clear()

                    For Each row As Data.DataRow In ds.Rows

                        ddl4.Items.Add(New ListItem(par.IfNull(row.Item("desc_zona"), ""), par.IfNull(row.Item("id_zona"), "")))
                    Next

                    ddl4.Items.Add(New ListItem("TUTTI", "-1"))
                    ddl4.Items.FindByText("TUTTI").Selected = True






                Else



                    ' ddl1.Items.FindByText("TUTTI").Selected = True


                    ds = New Data.DataTable



                    Sstringa = "SELECT * from zona_aler order by zona asc"
                    da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                    da1.Fill(ds)
                    ddl4.Items.Clear()

                    For Each row As Data.DataRow In ds.Rows
                        ddl4.Items.Add(New ListItem(par.IfNull(row.Item("ZONA"), ""), par.IfNull(row.Item("COD"), "")))
                    Next
                    ddl4.Items.Add(New ListItem("TUTTI", "-1"))
                    ddl4.Items.FindByText("TUTTI").Selected = True



                End If



                ds = New Data.DataTable
                da1.Dispose()








                ds = New Data.DataTable



                Sstringa = "SELECT distinct tab_quartieri.nome as nomeQuartiere, tab_quartieri.id as idQuartiere, siscom_mi.indirizzi.descrizione as desc_indirizzi,edifici.id as idEdificio,edifici.denominazione as nomeEdificio " _
                                         & "  FROM  sepa.comuni_nazioni, siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari, siscom_mi.edifici,siscom_mi.indirizzi " _
                                         & " WHERE comuni_nazioni.cod = edifici.cod_comune " _
                                         & " and complessi_immobiliari.id = edifici.id_complesso " _
                                         & " And edifici.id_indirizzo_principale = siscom_mi.indirizzi.ID " _
                                         & " and indirizzi.ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) " _
                                         & " and comuni_nazioni.ID=" & ddl1.SelectedValue.ToString & " " _
                                         & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                                         & " " & stringaSQL & " " _
                                         & " ORDER BY desc_indirizzi,nomeEdificio,nomeQuartiere ASC"

                da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                da1.Fill(ds)

                ddl2.Items.Clear()
                ddl3.Items.Clear()
                ddl6.Items.Clear()

                Dim k As Integer = 0
                If ds.Rows.Count > 1 Then
                    Do While k < ds.Rows.Count - 1

                        If k = 0 Then
                            ddl2.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeQuartiere"), ""), par.IfNull(ds.Rows(k).Item("idQuartiere"), "")))
                            ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "")))

                            ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k).Item("idEdificio"), "")))
                        End If


                        If par.IfNull(ds.Rows(k).Item("idQuartiere"), 0) <> par.IfNull(ds.Rows(k + 1).Item("idQuartiere"), 0) Then
                            ddl2.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("nomeQuartiere"), ""), par.IfNull(ds.Rows(k + 1).Item("idQuartiere"), "")))
                        End If

                        If par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "") <> par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), "") Then
                            ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), "")))
                        End If



                        If par.IfNull(ds.Rows(k).Item("idEdificio"), 0) <> par.IfNull(ds.Rows(k + 1).Item("idEdificio"), 0) Then
                            ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k + 1).Item("idEdificio"), "")))
                        End If


                        k = k + 1
                    Loop
                Else
                    ddl2.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeQuartiere"), ""), par.IfNull(ds.Rows(k).Item("idQuartiere"), "")))
                    ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "")))

                    ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k).Item("idEdificio"), "")))
                End If


                ddl2.Items.Add(New ListItem("TUTTI", "-1"))
                '   ddl2.Items.FindByText("TUTTI").Selected = True

                ddl3.Items.Add(New ListItem("TUTTI", "-1"))
                ddl3.Items.FindByText("TUTTI").Selected = True


                ddl6.Items.Add(New ListItem("TUTTI", "-1"))
                ddl6.Items.FindByText("TUTTI").Selected = True



            Else





                ds = New Data.DataTable



                Sstringa = "SELECT distinct siscom_mi.indirizzi.descrizione as desc_indirizzi,complessi_immobiliari.id as idComplesso,complessi_immobiliari.denominazione as nomeComplesso,edifici.id as idEdificio,edifici.denominazione as nomeEdificio " _
                                         & "  FROM  sepa.comuni_nazioni, siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari, siscom_mi.edifici,siscom_mi.indirizzi " _
                                         & " WHERE comuni_nazioni.cod = edifici.cod_comune " _
                                         & " and complessi_immobiliari.id = edifici.id_complesso " _
                                         & " And edifici.id_indirizzo_principale = siscom_mi.indirizzi.ID " _
                                         & " and indirizzi.ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) " _
                                         & " and comuni_nazioni.ID=" & ddl1.SelectedValue.ToString & " " _
                                         & " and tab_quartieri.ID=" & ddl2.SelectedValue.ToString & " " _
                                         & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                                         & " " & stringaSQL & " " _
                                         & " ORDER BY desc_indirizzi,nomeEdificio,nomeComplesso ASC"

                Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                da1.Fill(ds)

                ddl5.Items.Clear()
                ddl3.Items.Clear()
                ddl6.Items.Clear()

                Dim k As Integer = 0
                If ds.Rows.Count > 1 Then
                    Do While k < ds.Rows.Count - 1

                        If k = 0 Then

                            ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "")))
                            ddl5.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeComplesso"), ""), par.IfNull(ds.Rows(k).Item("idComplesso"), "")))
                            ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k).Item("idEdificio"), "")))
                        End If




                        If par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "") <> par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), "") Then
                            ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), "")))
                        End If

                        If par.IfNull(ds.Rows(k).Item("idComplesso"), 0) <> par.IfNull(ds.Rows(k + 1).Item("idComplesso"), 0) Then
                            ddl5.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("nomeComplesso"), ""), par.IfNull(ds.Rows(k + 1).Item("idComplesso"), "")))
                        End If

                        If par.IfNull(ds.Rows(k).Item("idEdificio"), 0) <> par.IfNull(ds.Rows(k + 1).Item("idEdificio"), 0) Then
                            ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k + 1).Item("idEdificio"), "")))
                        End If


                        k = k + 1
                    Loop
                Else

                    ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "")))
                    ddl5.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeComplesso"), ""), par.IfNull(ds.Rows(k).Item("idComplesso"), "")))
                    ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k).Item("idEdificio"), "")))
                End If


                ddl5.Items.Add(New ListItem("TUTTI", "-1"))
                ddl5.Items.FindByText("TUTTI").Selected = True

                ddl3.Items.Add(New ListItem("TUTTI", "-1"))
                ddl3.Items.FindByText("TUTTI").Selected = True


                ddl6.Items.Add(New ListItem("TUTTI", "-1"))
                ddl6.Items.FindByText("TUTTI").Selected = True







            End If

            ddl1.Items.Remove("")
            ddl3.Items.Remove("")
            ddl4.Items.Remove("")








        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub





    Private Sub FiltroEdificio(ByVal ddl1 As DropDownList, ByVal ddl2 As DropDownList, ByVal ddl3 As DropDownList, ByVal ddl4 As DropDownList, ByVal ddl5 As DropDownList, ByVal ddl6 As DropDownList)


        Dim ds As New Data.DataTable
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Dim Sstringa As String = ""
            Dim stringaSQL As String = ""
            Dim pieno As Boolean = False

            'ddl1=localita
            'ddl2=quartiere
            'ddl3=indirizzo
            'ddl4=zona
            'ddl5=complesso
            'ddl6=edificio






            If ddl6.SelectedValue <> "-1" Then

                If ddl6.SelectedValue.ToString <> "" Or ddl6.SelectedValue <> "-1" Then


                    stringaSQL = stringaSQL & " and edifici.id=" & ddl6.SelectedItem.Value


                End If




                Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)










                If stringaSQL <> " " Then

                    Sstringa = "SELECT DISTINCT zona_aler.zona as desc_zona, zona_aler.cod as id_zona" _
                                    & " FROM siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri, sepa.comuni_nazioni, siscom_mi.edifici, zona_aler " _
                                    & " where comuni_nazioni.cod = edifici.cod_comune " _
                                    & " and complessi_immobiliari.id (+)= edifici.id_complesso " _
                                    & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                                    & " AND edifici.id_zona= zona_aler.cod (+)  " _
                                    & " " & stringaSQL & " " _
                                    & " order by desc_zona asc "
                    da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                    da1.Fill(ds)

                    ddl4.Items.Clear()

                    For Each row As Data.DataRow In ds.Rows

                        ddl4.Items.Add(New ListItem(par.IfNull(row.Item("desc_zona"), ""), par.IfNull(row.Item("id_zona"), "")))
                    Next

                    ddl4.Items.Add(New ListItem("TUTTI", "-1"))
                    ddl4.Items.FindByText("TUTTI").Selected = True






                Else



                    ' ddl1.Items.FindByText("TUTTI").Selected = True


                    ds = New Data.DataTable



                    Sstringa = "SELECT * from zona_aler order by zona asc"
                    da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                    da1.Fill(ds)
                    ddl4.Items.Clear()

                    For Each row As Data.DataRow In ds.Rows
                        ddl4.Items.Add(New ListItem(par.IfNull(row.Item("ZONA"), ""), par.IfNull(row.Item("COD"), "")))
                    Next
                    ddl4.Items.Add(New ListItem("TUTTI", "-1"))
                    ddl4.Items.FindByText("TUTTI").Selected = True



                End If
                ds = New Data.DataTable








                Sstringa = "SELECT distinct tab_quartieri.nome as nomeQuartiere, tab_quartieri.id as idQuartiere,siscom_mi.indirizzi.descrizione as desc_indirizzi,complessi_immobiliari.id as idComplesso,complessi_immobiliari.denominazione as nomeComplesso" _
                                         & "  FROM  sepa.comuni_nazioni, siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari, siscom_mi.edifici,siscom_mi.indirizzi " _
                                         & " WHERE comuni_nazioni.cod = edifici.cod_comune " _
                                         & " and complessi_immobiliari.id = edifici.id_complesso " _
                                         & " And edifici.id_indirizzo_principale = siscom_mi.indirizzi.ID " _
                                         & " and indirizzi.ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) " _
                                         & " and comuni_nazioni.ID=" & ddl1.SelectedValue.ToString & " " _
                                         & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                                         & " " & stringaSQL & " " _
                                         & " ORDER BY nomeQuartiere,desc_indirizzi,nomeComplesso ASC"

                da1 = New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                da1.Fill(ds)
                da1.Dispose()
                ddl2.Items.Clear()
                ddl3.Items.Clear()
                ddl5.Items.Clear()


                Dim k As Integer = 0
                If ds.Rows.Count > 1 Then
                    Do While k < ds.Rows.Count - 1

                        If k = 0 Then

                            ddl2.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeQuartiere"), ""), par.IfNull(ds.Rows(k).Item("idQuartiere"), "")))
                            ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "")))
                            ddl5.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeComplesso"), ""), par.IfNull(ds.Rows(k).Item("idComplesso"), "")))

                        End If



                        If par.IfNull(ds.Rows(k).Item("idQuartiere"), 0) <> par.IfNull(ds.Rows(k + 1).Item("idQuartiere"), 0) Then
                            ddl2.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("nomeQuartiere"), ""), par.IfNull(ds.Rows(k + 1).Item("idQuartiere"), "")))
                        End If

                        If par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "") <> par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), "") Then
                            ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), "")))
                        End If

                        If par.IfNull(ds.Rows(k).Item("idComplesso"), 0) <> par.IfNull(ds.Rows(k + 1).Item("idComplesso"), 0) Then
                            ddl5.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("nomeComplesso"), ""), par.IfNull(ds.Rows(k + 1).Item("idComplesso"), "")))
                        End If




                        k = k + 1
                    Loop
                Else

                    ddl2.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeQuartiere"), ""), par.IfNull(ds.Rows(k).Item("idQuartiere"), "")))
                    ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "")))
                    ddl5.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeComplesso"), ""), par.IfNull(ds.Rows(k).Item("idComplesso"), "")))

                End If


                ddl2.Items.Add(New ListItem("TUTTI", "-1"))
                ' ddl2.Items.FindByText("TUTTI").Selected = True

                ddl3.Items.Add(New ListItem("TUTTI", "-1"))
                ddl3.Items.FindByText("TUTTI").Selected = True

                ddl5.Items.Add(New ListItem("TUTTI", "-1"))
                ' ddl5.Items.FindByText("TUTTI").Selected = True





            Else






                ds = New Data.DataTable



                Sstringa = "SELECT distinct siscom_mi.indirizzi.descrizione as desc_indirizzi, edifici.id as idEdificio, edifici.denominazione as nomeEdificio " _
                                         & "  FROM  sepa.comuni_nazioni, siscom_mi.tab_quartieri, siscom_mi.complessi_immobiliari, siscom_mi.edifici,siscom_mi.indirizzi " _
                                         & " WHERE comuni_nazioni.cod = edifici.cod_comune " _
                                         & " and complessi_immobiliari.id = edifici.id_complesso " _
                                         & " And edifici.id_indirizzo_principale = siscom_mi.indirizzi.ID " _
                                         & " and indirizzi.ID IN (SELECT DISTINCT id_indirizzo_principale FROM siscom_mi.edifici, siscom_mi.complessi_immobiliari WHERE edifici.id_complesso = complessi_immobiliari.ID AND edifici.ID <> 1) " _
                                         & " and comuni_nazioni.ID=" & ddl1.SelectedValue.ToString & " " _
                                         & " and tab_quartieri.ID=" & ddl2.SelectedValue.ToString & " " _
                                         & " and complessi_immobiliari.ID=" & ddl5.SelectedValue.ToString & " " _
                                         & " and tab_quartieri.id (+)= complessi_immobiliari.id_quartiere " _
                                         & " " & stringaSQL & " " _
                                         & " ORDER BY desc_indirizzi,nomeEdificio ASC"

                Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(Sstringa, par.OracleConn)
                da1.Fill(ds)


                ddl3.Items.Clear()
                ddl6.Items.Clear()

                Dim k As Integer = 0
                If ds.Rows.Count > 1 Then
                    Do While k < ds.Rows.Count - 1

                        If k = 0 Then

                            ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "")))

                            ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k).Item("idEdificio"), "")))
                        End If




                        If par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "") <> par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), "") Then
                            ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k + 1).Item("desc_indirizzi"), "")))
                        End If



                        If par.IfNull(ds.Rows(k).Item("idEdificio"), 0) <> par.IfNull(ds.Rows(k + 1).Item("idEdificio"), 0) Then
                            ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k + 1).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k + 1).Item("idEdificio"), "")))
                        End If


                        k = k + 1
                    Loop
                Else

                    ddl3.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("desc_indirizzi"), ""), par.IfNull(ds.Rows(k).Item("desc_indirizzi"), "")))

                    ddl6.Items.Add(New ListItem(par.IfNull(ds.Rows(k).Item("nomeEdificio"), ""), par.IfNull(ds.Rows(k).Item("idEdificio"), "")))
                End If



                ddl3.Items.Add(New ListItem("TUTTI", "-1"))
                ddl3.Items.FindByText("TUTTI").Selected = True


                ddl6.Items.Add(New ListItem("TUTTI", "-1"))
                ddl6.Items.FindByText("TUTTI").Selected = True











            End If



            ddl1.Items.Remove("")
            ddl3.Items.Remove("")
            ddl4.Items.Remove("")






































        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub






    Private Sub FrmSolaLettura(ByVal obj As Control)
        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                FrmSolaLettura(CTRL)
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Enabled = False
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Enabled = False
            End If
        Next
    End Sub

    Protected Sub ddl_complesso1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_complesso1.SelectedIndexChanged
        FiltroComplesso(ddl_localita1, ddl_quartiere1, ddl_indirizzo1, ddl_zona1, ddl_complesso1, ddl_edificio1)
    End Sub


    Protected Sub ddl_complesso2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_complesso2.SelectedIndexChanged
        FiltroComplesso(ddl_localita2, ddl_quartiere2, ddl_indirizzo2, ddl_zona2, ddl_complesso2, ddl_edificio2)
    End Sub


    Protected Sub ddl_complesso3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_complesso3.SelectedIndexChanged
        FiltroComplesso(ddl_localita3, ddl_quartiere3, ddl_indirizzo3, ddl_zona3, ddl_complesso3, ddl_edificio3)
    End Sub

    Protected Sub ddl_complesso4_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_complesso4.SelectedIndexChanged
        FiltroComplesso(ddl_localita4, ddl_quartiere4, ddl_indirizzo4, ddl_zona4, ddl_complesso4, ddl_edificio4)
    End Sub

    Protected Sub ddl_complesso5_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_complesso5.SelectedIndexChanged
        FiltroComplesso(ddl_localita5, ddl_quartiere5, ddl_indirizzo5, ddl_zona5, ddl_complesso5, ddl_edificio5)
    End Sub


    Protected Sub ddl_complesso1ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_complesso1ex.SelectedIndexChanged
        FiltroComplesso(ddl_localita1ex, ddl_quartiere1ex, ddl_indirizzo1ex, ddl_zona1ex, ddl_complesso1ex, ddl_edificio1ex)
    End Sub


    Protected Sub ddl_complesso2ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_complesso2ex.SelectedIndexChanged
        FiltroComplesso(ddl_localita2ex, ddl_quartiere2ex, ddl_indirizzo2ex, ddl_zona2ex, ddl_complesso2ex, ddl_edificio2ex)
    End Sub


    Protected Sub ddl_complesso3ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_complesso3ex.SelectedIndexChanged
        FiltroComplesso(ddl_localita3ex, ddl_quartiere3ex, ddl_indirizzo3ex, ddl_zona3ex, ddl_complesso3ex, ddl_edificio3ex)
    End Sub

    Protected Sub ddl_complesso4ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_complesso4ex.SelectedIndexChanged
        FiltroComplesso(ddl_localita4ex, ddl_quartiere4ex, ddl_indirizzo4ex, ddl_zona4ex, ddl_complesso4ex, ddl_edificio4ex)
    End Sub

    Protected Sub ddl_complesso5ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_complesso5ex.SelectedIndexChanged
        FiltroComplesso(ddl_localita5ex, ddl_quartiere5ex, ddl_indirizzo5ex, ddl_zona5ex, ddl_complesso5ex, ddl_edificio5ex)
    End Sub


    Protected Sub ddl_edificio1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_edificio1.SelectedIndexChanged
        FiltroEdificio(ddl_localita1, ddl_quartiere1, ddl_indirizzo1, ddl_zona1, ddl_complesso1, ddl_edificio1)
    End Sub


    Protected Sub ddl_edificio2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_edificio2.SelectedIndexChanged
        FiltroEdificio(ddl_localita2, ddl_quartiere2, ddl_indirizzo2, ddl_zona2, ddl_complesso2, ddl_edificio2)
    End Sub


    Protected Sub ddl_edificio3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_edificio3.SelectedIndexChanged
        FiltroEdificio(ddl_localita3, ddl_quartiere3, ddl_indirizzo3, ddl_zona3, ddl_complesso3, ddl_edificio3)
    End Sub

    Protected Sub ddl_edificio4_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_edificio4.SelectedIndexChanged
        FiltroEdificio(ddl_localita4, ddl_quartiere4, ddl_indirizzo4, ddl_zona4, ddl_complesso4, ddl_edificio4)
    End Sub

    Protected Sub ddl_edificio5_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_edificio5.SelectedIndexChanged
        FiltroEdificio(ddl_localita5, ddl_quartiere5, ddl_indirizzo5, ddl_zona5, ddl_complesso5, ddl_edificio5)
    End Sub


    Protected Sub ddl_edificio1ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_edificio1ex.SelectedIndexChanged
        FiltroEdificio(ddl_localita1ex, ddl_quartiere1ex, ddl_indirizzo1ex, ddl_zona1ex, ddl_complesso1ex, ddl_edificio1ex)
    End Sub


    Protected Sub ddl_edificio2ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_edificio2ex.SelectedIndexChanged
        FiltroEdificio(ddl_localita2ex, ddl_quartiere2ex, ddl_indirizzo2ex, ddl_zona2ex, ddl_complesso2ex, ddl_edificio2ex)
    End Sub


    Protected Sub ddl_edificio3ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_edificio3ex.SelectedIndexChanged
        FiltroEdificio(ddl_localita3ex, ddl_quartiere3ex, ddl_indirizzo3ex, ddl_zona3ex, ddl_complesso3ex, ddl_edificio3ex)
    End Sub

    Protected Sub ddl_edificio4ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_edificio4ex.SelectedIndexChanged
        FiltroEdificio(ddl_localita4ex, ddl_quartiere4ex, ddl_indirizzo4ex, ddl_zona4ex, ddl_complesso4ex, ddl_edificio4ex)
    End Sub

    Protected Sub ddl_edificio5ex_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_edificio5ex.SelectedIndexChanged
        FiltroEdificio(ddl_localita5ex, ddl_quartiere5ex, ddl_indirizzo5ex, ddl_zona5ex, ddl_complesso5ex, ddl_edificio5ex)
    End Sub

    Protected Sub btnAvanti1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAvanti1.Click

        avanti()
    End Sub

    Protected Sub btnAvanti2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAvanti2.Click
        avanti()
    End Sub

    Protected Sub btnIndietro3_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnIndietro3.Click
        indietro()
    End Sub

    Protected Sub btnIndietro2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnIndietro2.Click
        indietro()
    End Sub

    Protected Sub avanti()

        MultiView1.ActiveViewIndex = MultiView1.ActiveViewIndex + 1
        H1.Value = 1

    End Sub

    Protected Sub indietro()

        MultiView1.ActiveViewIndex = MultiView1.ActiveViewIndex - 1
        H1.Value = 1

    End Sub

    Protected Sub btnEsci1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnEsci1.Click


        Try

            H1.Value = 0
            If Uscita.Value = 1 Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                par.myTrans.Rollback()
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                HttpContext.Current.Session.Remove("TRANSAZIONE")
                HttpContext.Current.Session.Remove("CONNESSIONE")
                Session.Item("LAVORAZIONE") = "0"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, Me.Page.ClientID, "self.close();", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnEsci2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnEsci2.Click
        Try


            H1.Value = 0

            If Uscita.Value = 1 Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                par.myTrans.Rollback()
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                HttpContext.Current.Session.Remove("TRANSAZIONE")
                HttpContext.Current.Session.Remove("CONNESSIONE")
                Session.Item("LAVORAZIONE") = "0"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, Me.Page.ClientID, "self.close();", True)

            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub EsciNas_Click(sender As Object, e As System.EventArgs) Handles EsciNas.Click

        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans
        par.myTrans.Rollback()
        par.OracleConn.Close()
        par.OracleConn.Dispose()
        HttpContext.Current.Session.Remove("TRANSAZIONE")
        HttpContext.Current.Session.Remove("CONNESSIONE")
        Session.Item("LAVORAZIONE") = "0"

    End Sub

    Protected Sub btn_annulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_annulla.Click

        Try


            H1.Value = 0

            If Uscita.Value = 1 Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                par.myTrans.Rollback()
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                HttpContext.Current.Session.Remove("TRANSAZIONE")
                HttpContext.Current.Session.Remove("CONNESSIONE")
                Session.Item("LAVORAZIONE") = "0"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, Me.Page.ClientID, "self.close();", True)

            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddl_pianodaCon_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_pianodaCon.SelectedIndexChanged
        If ddl_pianodaCon.SelectedItem.Text <> "TUTTI" Then
            ddl_pianoaCon.Enabled = True
        Else
            ddl_pianoaCon.Enabled = False
            ddl_pianoaCon.SelectedValue = "-1"
        End If

    End Sub

    Protected Sub ddl_pianodaSenza_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_pianodaSenza.SelectedIndexChanged

        If ddl_pianodaSenza.SelectedItem.Text <> "TUTTI" Then
            ddl_pianoaSenza.Enabled = True
        Else
            ddl_pianoaSenza.Enabled = False
            ddl_pianoaSenza.SelectedValue = "-1"
        End If

    End Sub

    Protected Sub ddl_pianoaCon_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_pianoaCon.SelectedIndexChanged
        If controlloRangePiani(ddl_pianodaCon.SelectedItem.Text, ddl_pianoaCon.SelectedItem.Text) = 1 Then
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "alert('Il range di preferenza dei piani non è valido! L\'opzione \'da\' deve essere minore dell\'opzione \'a\'');", True)
        End If
    End Sub

    Protected Sub ddl_pianoaSenza_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_pianoaSenza.SelectedIndexChanged
        If controlloRangePiani(ddl_pianodaSenza.SelectedItem.Text, ddl_pianoaSenza.SelectedItem.Text) = 1 Then
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "alert('Il range di preferenza dei piani non è valido! L\'opzione \'da\' deve essere minore dell\'opzione \'a\'');", True)
        End If
    End Sub


    Protected Function controlloRangePiani(ByVal campo1 As String, ByVal campo2 As String) As Integer
        controlloRangePiani = 0
        Try

            If campo1 <> "TUTTI" And campo2 <> "TUTTI" Then
                Dim i As Integer = campo1.IndexOf("(")
                campo1 = Right(campo1, Len(campo1) - i - 1).Replace(")", "")



                i = campo2.IndexOf("(")
                campo2 = Right(campo2, Len(campo2) - i - 1).Replace(")", "")


                If CDbl(campo2) < CDbl(campo1) Then

                    controlloRangePiani = 1

                End If

            End If
        Catch ex As Exception
            'AGGIUNGERE ESCI
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            HttpContext.Current.Session.Remove("TRANSAZIONE")
            HttpContext.Current.Session.Remove("CONNESSIONE")
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return controlloRangePiani
    End Function


End Class
