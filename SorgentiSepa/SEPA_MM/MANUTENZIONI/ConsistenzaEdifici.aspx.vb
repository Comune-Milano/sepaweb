
Partial Class MANUTENZIONI_ConsistenzaEdifici
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String
    Private Property vobj() As String
        Get
            If Not (ViewState("par_vobj") Is Nothing) Then
                Return CStr(ViewState("par_vobj"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vobj") = value
        End Set

    End Property
    Private Property vSalvato() As Boolean
        Get
            If Not (ViewState("par_vSalvato") Is Nothing) Then
                Return CBool(ViewState("par_vSalvato"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_vobj") = value
        End Set

    End Property


    Private Property vId() As Long
        Get
            If Not (ViewState("par_id") Is Nothing) Then
                Return CLng(ViewState("par_id"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_id") = value
        End Set

    End Property
    Private Property vTipo() As String
        Get
            If Not (ViewState("par_Tipo") Is Nothing) Then
                Return CStr(ViewState("par_Tipo"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Tipo") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If
        Try

            If Not IsPostBack Then
                If Request.QueryString("ID") <> 0 Then
                    vId = Request.QueryString("ID")
                    vTipo = Request.QueryString("TIPO")
                    Session.Add("ID", vId)
                    Session.Add("TIPO", vTipo)
                Else
                    vId = Session.Item("ID")
                    vTipo = Session.Item("TIPO")
                End If

                If vId = 0 Or vTipo = "" Then
                    vId = Session.Item("ID")
                    vTipo = Session.Item("TIPO")
                End If
                'sStringaSql = Session.Item("PED")
                'sStringaSql = sStringaSql.ToUpper



                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                par.OracleConn.Open()
                par.SettaCommand(par)
                HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                'If par.OracleConn.State = Data.ConnectionState.Open Then
                '    Exit Sub
                'Else

                '    par.OracleConn.Open()
                '    par.SettaCommand(par)
                'End If



                Me.DrlSchede.Items.Add(New ListItem("- - - - - - - - - - - - - - - - - - ", -1))
                Me.DrlSchede.Items.Add(New ListItem("Sc. A-RILIEVO STRUTTURE", 0))
                Me.DrlSchede.Items.Add(New ListItem("Sc. B-SCHEDA RILIEVO CHIUSURE", 1))
                Me.DrlSchede.Items.Add(New ListItem("Sc. C-SCHEDA RILIEVO PARTIZIONI INTERNE", 2))
                Me.DrlSchede.Items.Add(New ListItem("Sc. D-SCHEDA RILIEVO PAVIMENTAZIONI INTERNE", 3))
                Me.DrlSchede.Items.Add(New ListItem("Sc. E-SCHEDA RILIEVO PROTEZIONE E DELIMITAZIONI", 4))
                Me.DrlSchede.Items.Add(New ListItem("Sc. F-SCHEDA RILIEVO ATTREZZATURE E SPAZI INTERNI", 5))
                Me.DrlSchede.Items.Add(New ListItem("Sc. G-SCHEDA RILIEVO ATTREZZATURE ED ARREDI ESTERNI", 6))
                Me.DrlSchede.Items.Add(New ListItem("Sc. H-SCHEDA RILIEVO IMPIANTI FISSI DI TRASPORTO", 7))
                Me.DrlSchede.Items.Add(New ListItem("Sc. I-SCHEDA RILIEVO IMPIANTI  RISCALDAMENTO E PRODUZIONE H2O CENTRALIZZATA", 8))
                Me.DrlSchede.Items.Add(New ListItem("Sc. L-SCHEDA RILIEVO IMPIANTI IDRICO SANITARI", 9))
                Me.DrlSchede.Items.Add(New ListItem("Sc. M-SCHEDA RILIEVO IMPIANTI ANTINCENDIO", 10))
                Me.DrlSchede.Items.Add(New ListItem("Sc. N-SCHEDA RILIEVO RETE SCARICO / FOGNARIA", 11))
                Me.DrlSchede.Items.Add(New ListItem("Sc. O-SCHEDA RILIEVO IMPIANTI SMALTIMENTO AERIFORMI ", 12))
                Me.DrlSchede.Items.Add(New ListItem("Sc. P-SCHEDA RILIEVO INPIANTO DI DISTRIBUZIONE GAS ", 13))
                Me.DrlSchede.Items.Add(New ListItem("Sc. Q-SCHEDA RILIEVO IMPIANTI ELETTRICI", 14))
                Me.DrlSchede.Items.Add(New ListItem("Sc. R-SCHEDA RILIEVO IMPIANTI TELEVISIVI", 15))
                Me.DrlSchede.Items.Add(New ListItem("Sc. S-SCHEDA RILIEVO IMPIANTI CITOFONI", 16))
                Me.DrlSchede.Items.Add(New ListItem("Sc. T-SCHEDA RILIEVO IMPIANTI DI TELECOMUNICAZIONE", 17))
                Me.DrlSchede.Enabled = False

                If vId <> 0 And vTipo = "EDIF" Then


                    par.cmd.CommandText = "select edifici.cod_edificio, edifici.denominazione as DenEdificio, indirizzi.descrizione as Indirizzo, indirizzi.civico, indirizzi.cap, indirizzi.localita, edifici.data_costruzione from SISCOM_MI.edifici, SISCOM_MI.indirizzi where edifici.id = " & vId & " and edifici.id_indirizzo_principale = indirizzi.id "
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        LblCodEdifi.Text = par.IfNull(myReader1("cod_edificio"), "n.d")
                        LblDescIndirizzo.Text = par.IfNull(myReader1("DENEDIFICIO"), "n.d")
                        LblCivico.Text = par.IfNull(myReader1("CIVICO"), "n.d")
                        LblCap.Text = par.IfNull(myReader1("CAP"), "n.d")
                        LblLocali.Text = par.IfNull(myReader1("LOCALITA"), "n.d")
                        lblAnno.Text = par.IfNull(par.FormattaData(myReader1("DATA_COSTRUZIONE")), "00000000")
                        'lblfoglio.Text = par.IfNull(myReader1("FOGLIO"), "nd")
                        'lblmap.Text = par.IfNull(myReader1("NUMERO"), "nd")
                        vobj = LblCodEdifi.Text
                    End If
                    myReader1.Close()
                    '************RIEMPIO LE COMBO CON I DATI DA PRENDERE DAL DB ******************

                    par.cmd.CommandText = "Select * from SISCOM_MI.destinazioni_principali"
                    myReader1 = par.cmd.ExecuteReader()
                    cmbDestPrincipale.Items.Add(New ListItem(" ", -1))
                    While myReader1.Read
                        cmbDestPrincipale.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
                    End While
                    myReader1.Close()

                    par.cmd.CommandText = "Select * from SISCOM_MI.tipologie_strutturali"
                    myReader1 = par.cmd.ExecuteReader()
                    cmbTipolStrutt.Items.Add(New ListItem(" ", -1))
                    While myReader1.Read
                        cmbTipolStrutt.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
                    End While
                    myReader1.Close()
                    Me.cmbTipolEdil1.Items.Add("  ")
                    Me.cmbTipolEdil1.Items.Add("ISOLATO")
                    Me.cmbTipolEdil1.Items.Add("CONTIGUO")
                    Me.cmbTipolEdil1.Items.Add("ALTRO")

                    Me.cmbTipoEdilizia2.Items.Add("  ")
                    Me.cmbTipoEdilizia2.Items.Add("TORRE")
                    Me.cmbTipoEdilizia2.Items.Add("LINEA")
                    Me.cmbTipoEdilizia2.Items.Add("BLOCCO")
                    Me.cmbTipoEdilizia2.Items.Add("CASA ISOLATA SU LOTTO")
                    Me.cmbTipoEdilizia2.Items.Add("CASA A SCHIERA")
                    Me.cmbTipoEdilizia2.Items.Add("CORTE")
                    '*********PEPPE MODIFY 22/04/2009
                    Me.cmbTipoEdilizia2.Items.Add("BALLATOIO")
                    Me.cmbTipoEdilizia2.Items.Add("PALAZZINA")

                    Me.cmbTipoEdilizia3.Items.Add("  ")
                    Me.cmbTipoEdilizia3.Items.Add("BIFAMILIARE")
                    Me.cmbTipoEdilizia3.Items.Add("UNIFAMILIARE")
                    '*********PEPPE MODIFY 22/04/2009
                    Me.cmbTipoEdilizia3.Items.Add("PLURIFAMILIARE")


                    'par.cmd.CommandText = "Select * from SISCOM_MI.tipologie_edili"
                    'myReader1 = par.cmd.ExecuteReader()
                    'cmbTipolEdil.Items.Add(New ListItem(" ", -1))
                    'While myReader1.Read
                    '    cmbTipolEdil.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
                    'End While
                    'myReader1.Close()
                    par.cmd.CommandText = "Select * from SISCOM_MI.stato_ce"
                    myReader1 = par.cmd.ExecuteReader()
                    cmbStatoFisico.Items.Add(New ListItem(" ", -1))
                    While myReader1.Read
                        cmbStatoFisico.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
                    End While
                    myReader1.Close()

                    'par.OracleConn.Close()

                ElseIf vId <> 0 And vTipo = "COMP" Then
                    par.cmd.CommandText = "select Complessi_immobiliari.cod_complesso, complessi_immobiliari.denominazione as DenComplesso, indirizzi.descrizione as Indirizzo, indirizzi.civico, indirizzi.cap, indirizzi.localita from SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where complessi_immobiliari.id = " & vId & " and complessi_immobiliari.id_indirizzo_riferimento = indirizzi.id"
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        LblCodEdifi.Text = par.IfNull(myReader1("cod_complesso"), " ")
                        LblDescIndirizzo.Text = par.IfNull(myReader1("INDIRIZZO"), " ")
                        LblCivico.Text = par.IfNull(myReader1("CIVICO"), " ")
                        LblCap.Text = par.IfNull(myReader1("CAP"), " ")
                        LblLocali.Text = par.IfNull(myReader1("LOCALITA"), " ")
                        lblAnno.Text = "nd"
                        lblfoglio.Text = "nd"
                        lblmap.Text = "nd"
                        vobj = LblCodEdifi.Text
                    End If
                    myReader1.Close()
                    ''''************RIEMPIO LE COMBO CON I DATI DA PRENDERE DAL DB ******************

                    par.cmd.CommandText = "Select * from SISCOM_MI.destinazioni_principali"
                    myReader1 = par.cmd.ExecuteReader()
                    cmbDestPrincipale.Items.Add(New ListItem(" ", -1))
                    While myReader1.Read
                        cmbDestPrincipale.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
                    End While
                    myReader1.Close()

                    par.cmd.CommandText = "Select * from SISCOM_MI.tipologie_strutturali"
                    myReader1 = par.cmd.ExecuteReader()
                    cmbTipolStrutt.Items.Add(New ListItem(" ", -1))
                    While myReader1.Read
                        cmbTipolStrutt.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
                    End While
                    myReader1.Close()
                    Me.cmbTipolEdil1.Items.Add("  ")
                    Me.cmbTipolEdil1.Items.Add("ISOLATO")
                    Me.cmbTipolEdil1.Items.Add("CONTIGUO")
                    Me.cmbTipolEdil1.Items.Add("ALTRO")

                    Me.cmbTipoEdilizia2.Items.Add("  ")
                    Me.cmbTipoEdilizia2.Items.Add("TORRE")
                    Me.cmbTipoEdilizia2.Items.Add("LINEA")
                    Me.cmbTipoEdilizia2.Items.Add("BLOCCO")
                    Me.cmbTipoEdilizia2.Items.Add("CASA ISOLATA SU LOTTO")
                    Me.cmbTipoEdilizia2.Items.Add("CASA A SCHIERA")
                    Me.cmbTipoEdilizia2.Items.Add("CORTE")
                    '*********PEPPE MODIFY 22/04/2009
                    Me.cmbTipoEdilizia2.Items.Add("BALLATOIO")
                    Me.cmbTipoEdilizia2.Items.Add("PALAZZINA")

                    Me.cmbTipoEdilizia3.Items.Add("  ")
                    Me.cmbTipoEdilizia3.Items.Add("BIFAMILIARE")
                    Me.cmbTipoEdilizia3.Items.Add("UNIFAMILIARE")
                    '*********PEPPE MODIFY 22/04/2009
                    Me.cmbTipoEdilizia3.Items.Add("PLURIFAMILIARE")


                    'par.cmd.CommandText = "Select * from SISCOM_MI.tipologie_strutturali"
                    'myReader1 = par.cmd.ExecuteReader()
                    'cmbTipolEdil1.Items.Add(New ListItem(" ", -1))
                    'While myReader1.Read
                    '    cmbTipolEdil1.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
                    'End While
                    'myReader1.Close()
                    par.cmd.CommandText = "Select * from SISCOM_MI.stato_ce"
                    myReader1 = par.cmd.ExecuteReader()
                    cmbStatoFisico.Items.Add(New ListItem(" ", -1))
                    While myReader1.Read
                        cmbStatoFisico.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
                    End While
                    myReader1.Close()

                    '*******Sotto transazione deve essere tolta********
                    'par.OracleConn.Close()

                End If
                ApriEsistente()

                Session.Add("LAVORAZIONE", "1")
            End If
        Catch ex As Exception
            Session.Add("LAVORAZIONE", "0")
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
        End Try

        BindGridConsistenza()
        BindGridServComuni()
        BindGridImpComun()
        BindGridlocali()
        txtindietro.Text = txtindietro.Text - 1
    End Sub


    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnAdd.Click
        Response.Write("<script>window.open('Consistenza_CE.aspx','VARIAZIONI', 'resizable=no, width=300, height=180');</script>")
        'Me.txtindietro.Text = txtindietro.Text - 1
    End Sub
    Private Sub BindGridConsistenza()

        Try


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            'par.OracleConn.Open()
            Dim StringaSql As String = ""

            If vTipo = "EDIF" Then
                StringaSql = "select rownum, ID_EDIFICIO as ID_EDICOMP, consistenza_ce.id_tipologia, descrizione as TipoCons, quant from SISCOM_MI.consistenza_ce, SISCOM_MI.tipologia_consistenza_ce where consistenza_ce.id_edificio=" & vId & " and consistenza_ce.id_tipologia = tipologia_consistenza_ce.id"
            ElseIf vTipo = "COMP" Then
                StringaSql = "select rownum, ID_COMPLESSO as ID_EDICOMP, consistenza_ce.id_tipologia, descrizione as TipoCons, quant from SISCOM_MI.consistenza_ce, SISCOM_MI.tipologia_consistenza_ce where consistenza_ce.id_complesso=" & vId & " and consistenza_ce.id_tipologia = tipologia_consistenza_ce.id"
            End If
            par.cmd.CommandText = StringaSql
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")
            'DatGridConsistenza.CurrentPageIndex = 0

            DatGridConsistenza.DataSource = ds

            DatGridConsistenza.DataBind()
            '*********IN CASO DI TRANSAZIONE DEVE ESSERE TOLTA
            'par.OracleConn.Close()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
        End Try

    End Sub
    Private Sub BindGridServComuni()
        'par.OracleConn.Open()
        Try


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Dim StringaSql As String = ""

            If vTipo = "EDIF" Then
                StringaSql = "select rownum, ID_EDIFICIO as ID_EDICOMP, servizi_comuni_ce.id_tipologia, descrizione as TipoServ, quant from SISCOM_MI.servizi_comuni_ce, SISCOM_MI.tipologia_servizi_comuni_ce where servizi_comuni_ce.id_edificio=" & vId & " and servizi_comuni_ce.id_tipologia = tipologia_servizi_comuni_ce.id"
            ElseIf vTipo = "COMP" Then
                StringaSql = "select rownum, ID_COMPLESSO as ID_EDICOMP, servizi_comuni_ce.id_tipologia, descrizione as TipoServ, quant from SISCOM_MI.servizi_comuni_ce, SISCOM_MI.tipologia_servizi_comuni_ce where servizi_comuni_ce.id_complesso=" & vId & " and servizi_comuni_ce.id_tipologia = tipologia_servizi_comuni_ce.id"
            End If

            par.cmd.CommandText = StringaSql
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")

            DataGridServComuni.DataSource = ds
            DataGridServComuni.DataBind()

            'par.OracleConn.Close()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try
    End Sub
    Private Sub BindGridImpComun()
        Try

            'par.OracleConn.Open()
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Dim StringaSql As String = ""

            If vTipo = "EDIF" Then
                StringaSql = "select rownum, ID_EDIFICIO as ID_EDICOMP, imp_comuni_ce.id_tipologia, descrizione as TipoImp, quant from SISCOM_MI.imp_comuni_ce, SISCOM_MI.tipologia_imp_comuni_ce where imp_comuni_ce.id_edificio=" & vId & " and imp_comuni_ce.id_tipologia = tipologia_imp_comuni_ce.id"
            ElseIf vTipo = "COMP" Then
                StringaSql = "select rownum, ID_COMPLESSO as ID_EDICOMP, imp_comuni_ce.id_tipologia, descrizione as TipoImp, quant from SISCOM_MI.imp_comuni_ce, SISCOM_MI.tipologia_imp_comuni_ce where imp_comuni_ce.id_complesso=" & vId & " and imp_comuni_ce.id_tipologia = tipologia_imp_comuni_ce.id"
            End If

            par.cmd.CommandText = StringaSql
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")

            DatGridImpComuni.DataSource = ds
            DatGridImpComuni.DataBind()

            'par.OracleConn.Close()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try
    End Sub
    Private Sub BindGridlocali()

        'par.OracleConn.Open()

        Try


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Dim StringaSql As String = ""

            If vTipo = "EDIF" Then
                StringaSql = "SELECT ROWNUM, id_edificio as ID,ID_LOCALE , descrizione FROM SISCOM_MI.LOCALI, SISCOM_MI.LOCALI_NO_ACCESSIBILI WHERE LOCALI_NO_ACCESSIBILI.id_EDIFICIO = " & vId & " AND LOCALI_NO_ACCESSIBILI.ID_LOCALE = LOCALI.ID"
            ElseIf vTipo = "COMP" Then
                StringaSql = "SELECT ROWNUM, ID_COMPLESSO as ID,ID_LOCALE , descrizione FROM SISCOM_MI.LOCALI, SISCOM_MI.LOCALI_NO_ACCESSIBILI WHERE LOCALI_NO_ACCESSIBILI.ID_COMPLESSO = " & vId & " AND LOCALI_NO_ACCESSIBILI.ID_LOCALE = LOCALI.ID"
            End If

            par.cmd.CommandText = StringaSql
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")

            DataGridLocaliIrrilevati.DataSource = ds
            DataGridLocaliIrrilevati.DataBind()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try
        'par.OracleConn.Close()
    End Sub

    Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click
        Response.Write("<script>window.open('ServiziComuni_CE.aspx','VARIAZIONI', 'resizable=no, width=300, height=180');</script>")
        'Me.txtindietro.Text = txtindietro.Text - 1

    End Sub

    Protected Sub ImageButton7_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton7.Click
        Response.Write("<script>window.open('ImpComuni_CE.aspx','VARIAZIONI', 'resizable=no, width=300, height=180');</script>")
        'Me.txtindietro.Text = txtindietro.Text - 1
    End Sub
    '**************DATA GRID CONSISTENZA ****************************************
    Protected Sub DatGridConsistenza_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DatGridConsistenza.EditCommand
        LBLDESCRIZIONE.Text = e.Item.Cells(2).Text
        LBLID.Text = e.Item.Cells(1).Text
        Label6.Text = "Hai selezionato la riga N°: " & e.Item.Cells(0).Text
    End Sub

    Protected Sub DatGridConsistenza_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DatGridConsistenza.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(0).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(1).Text & "';document.getElementById('txtdesc').value='" & e.Item.Cells(2).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(0).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(1).Text & "';document.getElementById('txtdesc').value='" & e.Item.Cells(2).Text & "'")

        End If

    End Sub

    Protected Sub DatGridConsistenza_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DatGridConsistenza.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DatGridConsistenza.CurrentPageIndex = e.NewPageIndex
            BindGridConsistenza()
        End If
    End Sub



    '***********************************DATA GRID SERVIZI************************************
    Protected Sub DataGridServComuni_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGridServComuni.EditCommand
        LBLDESCRIZIONE.Text = e.Item.Cells(2).Text
        LblIdserv.Text = e.Item.Cells(1).Text
        LblselServ.Text = "Hai selezionato la riga N°: " & e.Item.Cells(0).Text

    End Sub

    Protected Sub DataGridServComuni_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridServComuni.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmiaserv').value='Hai selezionato la riga n. " & e.Item.Cells(0).Text & "';document.getElementById('txtidserv').value='" & e.Item.Cells(1).Text & "';document.getElementById('txtdescserv').value='" & e.Item.Cells(2).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmiaserv').value='Hai selezionato la riga n. " & e.Item.Cells(0).Text & "';document.getElementById('txtidserv').value='" & e.Item.Cells(1).Text & "';document.getElementById('txtdescserv').value='" & e.Item.Cells(2).Text & "'")

        End If

    End Sub

    Protected Sub DataGridServComuni_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridServComuni.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridServComuni.CurrentPageIndex = e.NewPageIndex
            BindGridServComuni()
        End If
    End Sub



    '*****************************DATA GRID IMP_COMUNI ******************************************


    Protected Sub DatGridImpComuni_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DatGridImpComuni.EditCommand
        LBLDESCRIZIONE.Text = e.Item.Cells(2).Text
        LblIDImpComuni.Text = e.Item.Cells(0).Text
        LblselImpCom.Text = "Hai selezionato la riga N°: " & e.Item.Cells(1).Text

    End Sub

    Protected Sub DatGridImpComuni_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DatGridImpComuni.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmiaimp').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('txtidimp').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtdescimp').value='" & e.Item.Cells(2).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmiaimp').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('txtidimp').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtdescimp').value='" & e.Item.Cells(2).Text & "'")

        End If

    End Sub

    Protected Sub DatGridImpComuni_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DatGridImpComuni.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DatGridImpComuni.CurrentPageIndex = e.NewPageIndex
            BindGridImpComun()
        End If

    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        salva()
    End Sub
    Private Sub salva()
        'par.OracleConn.Open()
        'par.SettaCommand(par)
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        Dim id_dest_princ As String
        Dim id_tipol_struttura As String

        id_dest_princ = Me.cmbDestPrincipale.SelectedValue.ToString
        id_tipol_struttura = Me.cmbTipolStrutt.SelectedValue.ToString
        If id_dest_princ = "-1" Then
            id_dest_princ = "NULL"

        End If
        If id_tipol_struttura = "-1" Then
            id_tipol_struttura = "NULL"
        End If
        Try
            If Me.cmbDestPrincipale.SelectedValue <> -1 Then

                If btnrilievo.Visible = False Then


                    If vTipo = "EDIF" Then

                        par.cmd.CommandText = "insert into SISCOM_MI.sk_consistenza_ce (id_operatore,id_edificio, data_inserimento, id_destinazione_principale, Id_tipologia_struttura, tipologia_edilizia_1, tipologia_edilizia_2, tipologia_edilizia_3, note, ID_MANCATO_RILIEVO)" _
                        & "values(" & Session("ID_OPERATORE") & "," & vId & ", " & Format(Now, "yyyyMMdd") & ", " & id_dest_princ & ", " & id_tipol_struttura & ", '" & Me.cmbTipolEdil1.SelectedItem.ToString & "', '" & Me.cmbTipoEdilizia2.SelectedItem.ToString & "', '" & Me.cmbTipoEdilizia3.SelectedItem.ToString & "', '" & par.PulisciStrSql(Me.txtNote.Text) & "', " & RitornaNullSeMenoUno(Me.cmbStatoFisico.SelectedValue) & ")"
                        par.cmd.ExecuteNonQuery()
                        Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")

                    ElseIf vTipo = "COMP" Then
                        par.cmd.CommandText = "insert into SISCOM_MI.sk_consistenza_ce (id_operatore,id_complesso, data_inserimento, id_destinazione_principale, Id_tipologia_struttura, tipologia_edilizia_1, tipologia_edilizia_2, tipologia_edilizia_3, note, ID_MANCATO_RILIEVO)" _
                        & "values(" & Session("ID_OPERATORE") & "," & vId & ", " & Format(Now, "yyyyMMdd") & ", " & id_dest_princ & ", " & id_tipol_struttura & ", '" & Me.cmbTipolEdil1.SelectedItem.ToString & "', '" & Me.cmbTipoEdilizia2.SelectedItem.ToString & "', '" & Me.cmbTipoEdilizia3.SelectedItem.ToString & "', '" & par.PulisciStrSql(Me.txtNote.Text) & "'," & RitornaNullSeMenoUno(Me.cmbStatoFisico.SelectedValue) & ")"
                        par.cmd.ExecuteNonQuery()
                        Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")

                    End If
                ElseIf btnrilievo.Visible = True Then

                    If vTipo = "EDIF" Then
                        ''***ESEGUO PRIMA LA DELETE e POI LA STESSA INSERT
                        'par.cmd.CommandText = "delete SISCOM_MI.sk_consistenza_ce where id_edificio =" & vId
                        'par.cmd.ExecuteNonQuery()

                        'par.cmd.CommandText = "insert into SISCOM_MI.sk_consistenza_ce (id_operatore,id_edificio, data_inserimento, id_destinazione_principale, Id_tipologia_struttura, tipologia_edilizia_1, tipologia_edilizia_2, tipologia_edilizia_3, note, ID_MANCATO_RILIEVO)" _
                        '& "values(" & Session("ID_OPERATORE") & "," & vId & ", " & Format(Now, "yyyyMMdd") & ", " & id_dest_princ & ", " & id_tipol_struttura & ", '" & Me.cmbTipolEdil1.SelectedItem.ToString & "', '" & Me.cmbTipoEdilizia2.SelectedItem.ToString & "', '" & Me.cmbTipoEdilizia3.SelectedItem.ToString & "', '" & par.PulisciStrSql(Me.txtNote.Text) & "', " & RitornaNullSeMenoUno(Me.cmbStatoFisico.SelectedValue) & ")"
                        'par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "UPDATE SISCOM_MI.sk_consistenza_ce SET ID_OPERATORE=" & Session("ID_OPERATORE") & "" _
                        & " ,DATA_INSERIMENTO =" & Format(Now, "yyyyMMdd") & ", id_destinazione_principale=" & id_dest_princ & ", Id_tipologia_struttura = " & id_tipol_struttura & ", tipologia_edilizia_1 = '" & Me.cmbTipolEdil1.SelectedItem.ToString & "'" _
                        & ", tipologia_edilizia_2 = '" & Me.cmbTipoEdilizia2.SelectedItem.ToString & "', tipologia_edilizia_3 = '" & Me.cmbTipoEdilizia3.SelectedItem.ToString & "', note = '" & par.PulisciStrSql(Me.txtNote.Text) & "', ID_MANCATO_RILIEVO = " & RitornaNullSeMenoUno(Me.cmbStatoFisico.SelectedValue) & "" _
                        & " WHERE ID_EDIFICIO = " & vId & ""
                        par.cmd.ExecuteNonQuery()

                        Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")

                    ElseIf vTipo = "COMP" Then
                        '***ESEGUO PRIMA LA DELETE e POI LA STESSA INSERT

                        'par.cmd.CommandText = "delete SISCOM_MI.sk_consistenza_ce where id_complesso =" & vId
                        'par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "UPDATE SISCOM_MI.sk_consistenza_ce SET ID_OPERATORE=" & Session("ID_OPERATORE") & "" _
                        & " ,DATA_INSERIMENTO =" & Format(Now, "yyyyMMdd") & ", id_destinazione_principale=" & id_dest_princ & ", Id_tipologia_struttura = " & id_tipol_struttura & ", tipologia_edilizia_1 = '" & Me.cmbTipolEdil1.SelectedItem.ToString & "'" _
                        & ", tipologia_edilizia_2 = '" & Me.cmbTipoEdilizia2.SelectedItem.ToString & "', tipologia_edilizia_3 = '" & Me.cmbTipoEdilizia3.SelectedItem.ToString & "', note = '" & par.PulisciStrSql(Me.txtNote.Text) & "', ID_MANCATO_RILIEVO = " & RitornaNullSeMenoUno(Me.cmbStatoFisico.SelectedValue) & "" _
                        & " WHERE ID_COMPLESSO = " & vId & ""
                        par.cmd.ExecuteNonQuery()
                        Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")

                    End If

                End If
                Me.btnrilievo.Visible = True
                Me.DrlSchede.Enabled = True
            Else
                Response.Write("<script>alert('Riempire tutti i campi!')</script>")

            End If

            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            vSalvato = True

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
        End Try
        'par.OracleConn.Close()

    End Sub


    Protected Sub BtnDeleteConsistenza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnDeleteConsistenza.Click
        Try

            If Me.txtid.Text <> "" Then
                If vTipo = "EDIF" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans


                    par.cmd.CommandText = "delete from SISCOM_MI.consistenza_ce where id_EDIFICIO = " & vId & " and ID_TIPOLOGIA = '" & txtid.Text & "'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    'par.OracleConn.Close()
                    txtmiaserv.Text = ""



                ElseIf vTipo = "COMP" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    'par.SettaCommand(par)

                    par.cmd.CommandText = "delete from SISCOM_MI.consistenza_ce where ID_COMPLESSO = " & vId & " and ID_TIPOLOGIA = " & txtid.Text
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    'par.OracleConn.Close()
                    'BindGridConsistenza()
                    txtmia.Text = ""


                End If
                DatGridConsistenza.CurrentPageIndex = 0
                BindGridConsistenza()


            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try

    End Sub

    Protected Sub BtnDeleteServizi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnDeleteServizi.Click
        Try
            If txtidserv.Text <> "" Then
                If vTipo = "EDIF" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "delete from SISCOM_MI.SERVIZI_COMUNI_CE where id_EDIFICIO = " & vId & " and ID_TIPOLOGIA = '" & txtidserv.Text & "'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    'par.OracleConn.Close()
                    'BindGridServComuni()
                    txtmiaserv.Text = ""



                ElseIf vTipo = "COMP" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "delete from SISCOM_MI.SERVIZI_COMUNI_CE where id_complesso = " & vId & " and ID_TIPOLOGIA = '" & txtidserv.Text & "'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    'par.OracleConn.Close()
                    'BindGridServComuni()
                    txtmiaserv.Text = ""

                End If
                DataGridServComuni.CurrentPageIndex = 0
                BindGridServComuni()

            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try

    End Sub

    Protected Sub btnDeleteImpComuni_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDeleteImpComuni.Click
        Try

            If txtidimp.Text <> "" Then
                If vTipo = "EDIF" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "delete from SISCOM_MI.imp_comuni_ce where id_EDIFICIO = " & vId & " and ID_TIPOLOGIA = '" & txtidimp.Text & "'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    'par.OracleConn.Close()
                    txtmiaimp.Text = ""



                ElseIf vTipo = "COMP" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "delete from SISCOM_MI.imp_comuni_ce where id_complesso = " & vId & " and ID_TIPOLOGIA = '" & txtidimp.Text & "'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    'par.OracleConn.Close()
                    txtmiaimp.Text = ""
                End If
                DatGridImpComuni.CurrentPageIndex = 0
                BindGridImpComun()

            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try

    End Sub
    Private Sub ApriEsistente()
        'par.OracleConn.Open()
        'par.SettaCommand(par)
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

            If vTipo = "EDIF" Then

                par.cmd.CommandText = "Select * from SISCOM_MI.SK_CONSISTENZA_CE where ID_EDIFICIO = " & vId
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.btnrilievo.Visible = True
                    Me.DrlSchede.Enabled = True
                    Me.cmbDestPrincipale.SelectedValue = par.IfNull(myReader1("id_destinazione_principale"), -1)
                    Me.cmbTipolStrutt.SelectedValue = par.IfNull(myReader1("id_tipologia_struttura"), -1)
                    Me.cmbTipolEdil1.SelectedValue = myReader1("tipologia_edilizia_1")
                    Me.cmbTipoEdilizia2.SelectedValue = myReader1("tipologia_edilizia_2")
                    Me.cmbTipoEdilizia3.SelectedValue = myReader1("tipologia_edilizia_3")
                    Me.txtNote.Text = par.IfNull(myReader1("NOTE"), " ")
                    Me.cmbStatoFisico.SelectedValue = par.IfNull(myReader1("ID_MANCATO_RILIEVO"), -1)
                    Me.btnrilievo.Visible = True
                    Me.DrlSchede.Enabled = True
                End If


            ElseIf vTipo = "COMP" Then
                par.cmd.CommandText = "Select * from SISCOM_MI.SK_CONSISTENZA_CE where ID_COMPLESSO = " & vId
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.btnrilievo.Visible = True
                    Me.DrlSchede.Enabled = True
                    Me.cmbDestPrincipale.SelectedValue = par.IfNull(myReader1("id_destinazione_principale"), -1)
                    Me.cmbTipolStrutt.SelectedValue = par.IfNull(myReader1("id_tipologia_struttura"), -1)
                    Me.cmbTipolEdil1.SelectedValue = myReader1("tipologia_edilizia_1")
                    Me.cmbTipoEdilizia2.SelectedValue = myReader1("tipologia_edilizia_2")
                    Me.cmbTipoEdilizia3.SelectedValue = myReader1("tipologia_edilizia_3")
                    Me.txtNote.Text = par.IfNull(myReader1("NOTE"), " ")
                    Me.cmbStatoFisico.SelectedValue = par.IfNull(myReader1("ID_MANCATO_RILIEVO"), -1)
                    Me.btnrilievo.Visible = True
                    Me.DrlSchede.Enabled = True
                End If


            End If
            'par.OracleConn.Close()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try

    End Sub

    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        Session.Add("LAVORAZIONE", "0")
        par.myTrans.Rollback()
        par.OracleConn.Close()
        HttpContext.Current.Session.Remove("TRANSAZIONE")
        HttpContext.Current.Session.Remove("CONNESSIONE")
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Page.Dispose()
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")

    End Sub

    Private Function QualeAprire()

        Select Case Me.DrlSchede.SelectedValue
            Case 0
                Response.Write("<script>window.open('ScA.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 1

                Response.Write("<script>window.open('ScB.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 2
                Response.Write("<script>window.open('ScC.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 3
                Response.Write("<script>window.open('ScD.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 4
                Response.Write("<script>window.open('ScE.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 5
                Response.Write("<script>window.open('ScF.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 6
                Response.Write("<script>window.open('ScG.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 7
                Response.Write("<script>window.open('ScH.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 8
                Response.Write("<script>window.open('ScI.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 9
                Response.Write("<script>window.open('ScL.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 10
                Response.Write("<script>window.open('ScM.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 11
                Response.Write("<script>window.open('ScN.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 12
                Response.Write("<script>window.open('ScO.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 13
                Response.Write("<script>window.open('ScP.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 14
                Response.Write("<script>window.open('ScQ.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 15
                Response.Write("<script>window.open('ScR.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 16
                Response.Write("<script>window.open('ScS.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

            Case 17
                Response.Write("<script>window.open('ScT.aspx?ID=" & vId & "','','scrollbars=yes, resizable=yes, width='+screen.width+', height='+screen.width+'');</script>")

        End Select
    End Function

    Protected Sub btnrilievo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnrilievo.Click
        QualeAprire()
    End Sub

    Protected Sub addriliev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles addriliev.Click
        Response.Write("<script>window.open('Locali.aspx','RILIEVI', 'resizable=no, width=300, height=180');</script>")
        Me.txtindietro.Text = txtindietro.Text - 1

    End Sub
    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Try
            Dim a As String
            a = ""
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = valorepass
            End If
            Return a
        Catch ex As Exception
        End Try
    End Function

    Protected Sub DataGridLocaliIrrilevati_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridLocaliIrrilevati.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtidlocale').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtidlocale').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub

    Protected Sub delriliev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles delriliev.Click

        Try

            If txtidlocale.Text <> "" Then
                If vTipo = "EDIF" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "delete from SISCOM_MI.LOCALI_NO_ACCESSIBILI where id_EDIFICIO = " & vId & " and ID_LOCALE = '" & txtidlocale.Text & "'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    'par.OracleConn.Close()
                    'BindGridlocali()
                    txtmiaserv.Text = ""


                ElseIf vTipo = "COMP" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "delete from SISCOM_MI.LOCALI_NO_ACCESSIBILI where ID_COMPLESSO = " & vId & " and ID_LOCALE = '" & txtidlocale.Text & "'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    'par.OracleConn.Close()
                    'BindGridlocali()
                    txtmiaserv.Text = ""

                End If
                DataGridLocaliIrrilevati.CurrentPageIndex = 0
                BindGridlocali()

            Else
                Response.Write("<script>alert('Nessun elemento selezionato!')</script>")
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
        End Try

    End Sub

    Protected Sub DataGridLocaliIrrilevati_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridLocaliIrrilevati.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridLocaliIrrilevati.CurrentPageIndex = e.NewPageIndex
            BindGridlocali()
        End If
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Session.Add("LAVORAZIONE", "0")
        par.myTrans.Rollback()
        par.OracleConn.Close()
        HttpContext.Current.Session.Remove("TRANSAZIONE")
        HttpContext.Current.Session.Remove("CONNESSIONE")
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Page.Dispose()
        Response.Write("<script>history.go(" & txtindietro.Text & ");</script>")
    End Sub
End Class
