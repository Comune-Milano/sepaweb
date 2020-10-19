
Partial Class PED_DatiUnita
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreID As Long
    Dim IdComplesso As Long
    Dim IdEdificio As Long

    Protected Sub btnChiudi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudi.Click

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim MioColore As String
        Dim scriptblock As String

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        btnChiudi.Attributes.Add("onclick", "javascript:window.close();")


        sValoreID = Request.QueryString("ID")
        IdUnita = sValoreID

        scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "function Stampabile() {window.open('Stampabile.aspx?ID=" & IdUnita & "','File','');}" _
                    & "</script>"
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript545")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript545", scriptblock)
        End If

        Try
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT stato_censimento.descrizione as ""censimento"",tipo_disponibilita.descrizione as ""disp"", unita_immobiliari.id_unita_principale,tipologia_unita_immobiliari.descrizione as ""DTipoI"",tipologia_provenienza.descrizione as ""dProv"",tipo_complesso_immobiliare.descrizione as ""CodCom"",complessi_immobiliari.id as ""IdC"",complessi_immobiliari.denominazione as ""DComp"",edifici.id as ""IdE"",edifici.denominazione,TIPO_LIVELLO_PIANO.descrizione as ""DPiano"",comuni.comu_descr,indirizzi.descrizione as ""Dvia"",indirizzi.civico,indirizzi.cap,unita_immobiliari.*,identificativi_catastali.foglio,identificativi_catastali.numero,identificativi_catastali.sub,identificativi_catastali.SUPERFICIE_MQ FROM SISCOM_MI.tipologia_provenienza,SISCOM_MI.tipo_complesso_immobiliare,SISCOM_MI.complessi_immobiliari,SISCOM_MI.edifici,siscom_mi.TIPO_LIVELLO_PIANO,SISCOM_MI.indirizzi,SISCOM_MI.identificativi_catastali,SISCOM_MI.unita_immobiliari,comuni,SISCOM_MI.tipologia_unita_immobiliari,SISCOM_MI.tipo_disponibilita,SISCOM_MI.stato_censimento WHERE unita_immobiliari.cod_stato_censimento=stato_censimento.cod (+) and unita_immobiliari.cod_tipo_disponibilita=tipo_disponibilita.cod (+) and unita_immobiliari.cod_tipologia=tipologia_unita_immobiliari.cod (+) and complessi_immobiliari.cod_tipologia_provenienza=tipologia_provenienza.cod (+) and complessi_immobiliari.cod_tipo_complesso=tipo_complesso_immobiliare.cod (+) and indirizzi.cod_comune=comuni.comu_cod (+) and  identificativi_catastali.id_indirizzo_riferimento=indirizzi.id (+) and edifici.id_complesso=complessi_immobiliari.id (+) and unita_immobiliari.id_edificio=edifici.id  and UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD(+) and unita_immobiliari.id_catastale=identificativi_catastali.id (+) and unita_immobiliari.ID=" & sValoreID
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                lblUI.Text = par.IfNull(myReader1("cod_unita_immobiliare"), "")
                lbFoglio.Text = par.IfNull(myReader1("foglio"), "")
                lblParticella.Text = par.IfNull(myReader1("numero"), "")
                lblSub.Text = par.IfNull(myReader1("sub"), "")

                lblIndirizzo.Text = par.IfNull(myReader1("DVia"), "") & ", " & par.IfNull(myReader1("civico"), "")
                lblCAPCitta.Text = par.IfNull(myReader1("cap"), "") & " " & par.IfNull(myReader1("comu_descr"), "")

                'txtGoogle.Value = lblIndirizzo.Text & " " & lblCAPCitta.Text
                Image1.Attributes.Add("onclick", "javascript:window.open('http://www.google.it/maps?q=" & par.VaroleDaPassare(lblIndirizzo.Text & " " & lblCAPCitta.Text) & "','','');")

                lblScala.Text = par.IfNull(myReader1("scala"), "")
                lblPiano.Text = par.IfNull(myReader1("DPiano"), "")
                lblEdificio.Text = par.IfNull(myReader1("Denominazione"), "")
                lblComplesso.Text = par.IfNull(myReader1("DComp"), "")
                lblTipoComplesso.Text = par.IfNull(myReader1("CodCom"), "")
                lblProv.Text = par.IfNull(myReader1("dProv"), "")
                lblTipoImm.Text = par.IfNull(myReader1("DTipoI"), "")
                IdComplesso = par.IfNull(myReader1("IdC"), -1)
                IdEdificio = par.IfNull(myReader1("IdE"), -1)

                lblDisp.Text = par.IfNull(myReader1("disp"), "")
                lblCensimento.Text = par.IfNull(myReader1("censimento"), "")

                Select Case Mid(sValoreID, 1, 1)
                    Case "1"
                        lblGestore.Text = "GEFI"
                    Case "2"
                        lblGestore.Text = "PIRELLI"
                    Case "3"
                        lblGestore.Text = "ROMEO"
                End Select

                par.cmd.CommandText = "SELECT tipologia_rapp_contrattuale.descrizione FROM SISCOM_MI.tipologia_rapp_contrattuale,SISCOM_MI.unita_immobiliari,SISCOM_MI.unita_contrattuale,SISCOM_MI.rapporti_utenza WHERE rapporti_utenza.cod_tipologia_rapp_contr=tipologia_rapp_contrattuale.cod and unita_contrattuale.id_contratto=rapporti_utenza.id and unita_immobiliari.id=unita_contrattuale.id_unita and unita_immobiliari.id=" & sValoreID
                Dim myReader1011 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1011.Read() Then
                    lblOccupante.Text = par.IfNull(myReader1011("descrizione"), "") & " "
                End If

                'lblOccupante.Text = lblOccupante.Text & 
                Label1.Text = "<a href='DatiContratto.aspx?ID=" & sValoreID & "&UI=" & lblUI.Text & "' target='_blank'>Clicca qui per visualizzare i dati Contrattuali rilevati dal PED</a>"
                lblMillesimi.Text = "<a href='DatiMillesimi.aspx?ID=" & sValoreID & "&UI=" & lblUI.Text & "' target='_blank'>Tabelle Millesimali</a>"
                lblPertinenze.Text = ""
                If par.IfNull(myReader1("id_unita_principale"), -1) <> -1 Then
                    lblPrincipale.Text = "<a href='DatiUnita.aspx?ID=" & par.IfNull(myReader1("id_unita_principale"), -1) & "' target='_blank'>Visualizza</a>"
                Else
                    par.cmd.CommandText = "SELECT cod_unita_immobiliare,id FROM SISCOM_MI.unita_immobiliari WHERE ID_UNITA_principale=" & sValoreID
                    Dim myReader101 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader101.Read()
                        lblPertinenze.Text = lblPertinenze.Text & "<a href='DatiUnita.aspx?ID=" & par.IfNull(myReader101("id"), -1) & "' target='_blank'>" & par.IfNull(myReader101("cod_unita_immobiliare"), -1) & "</a>;"
                    Loop
                    myReader101.Close()
                End If


                par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.DIMENSIONI WHERE DIMENSIONI.ID_UNITA_IMMOBILIARE=" & sValoreID & " AND DIMENSIONI.COD_TIPOLOGIA='SUP_CONV'"
                Dim myReader10 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader10.Read Then
                    lblSup.Text = par.IfNull(myReader10("VALORE"), "")
                    If lblSup.Text <> "" Then
                        lblSup.Text = lblSup.Text & " <a href='DatiMisure.aspx?ID=" & sValoreID & "&UI=" & lblUI.Text & "' target='_blank'>Tutte le misure</a>"
                    End If
                End If
                myReader10.Close()

                par.cmd.CommandText = "SELECT utenza_dichiarazioni.id FROM sepa.utenza_dichiarazioni,SEPA.TRANSCODIFICA_GESTORI WHERE utenza_dichiarazioni.posizione=TRANSCODIFICA_GESTORI.posizione and TRANSCODIFICA_GESTORI.UI_PED='" & lblUI.Text & "'"
                Dim myReader12 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader12.Read Then
                    'lblEdificio.Text = lblEdificio.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "<a href='javascript:ApriANAUT(" & par.IfNull(myReader12("id"), -1) & ");'>Clicca qui per visualizzare i dati dell'ANAGRAFE UTENZA 2007</a>"
                    Label2.Text = "<a href='javascript:ApriANAUT(" & par.IfNull(myReader12("id"), -1) & ");'>Clicca qui per visualizzare i dati dell'ANAGRAFE UTENZA</a>"


                End If
                myReader12.Close()

            End If




            myReader1.Close()


            FotoComplesso = "<table border='0' cellpadding='1' cellspacing='1' style='BACKGROUND-COLOR: white;width:650px'>"
            FotoComplesso = FotoComplesso & "<tr>" & vbCrLf
            FotoComplesso = FotoComplesso & "<td bgcolor=" & Chr(34) & "darkgray" & Chr(34) & " style='width:450px'><font face='Arial' size='1'>DESCRIZIONE</font></td>" & vbCrLf
            FotoComplesso = FotoComplesso & "<td bgcolor=" & Chr(34) & "darkgray" & Chr(34) & " style='width:200px'><font face='Arial' size='1'>FOTO</font></td>" & vbCrLf

            par.cmd.CommandText = "select * from SISCOM_MI.riprese_fotografiche WHERE id_complesso=" & IdComplesso
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            MioColore = "#e3e1e1"
            Do While myReader2.Read()
                FotoComplesso = FotoComplesso & "<tr>" & vbCrLf
                FotoComplesso = FotoComplesso & "<td bgcolor=" & Chr(34) & MioColore & Chr(34) & " style='width:450px'><font face='Arial' size='1'>" & par.IfNull(myReader2("descrizione"), "") & "</font></td>"
                FotoComplesso = FotoComplesso & "<td bgcolor=" & Chr(34) & MioColore & Chr(34) & " style='width:200px'><p align='center'><font face='Arial' size='1'><a href='VisFoto.aspx?FILE=" & par.IfNull(myReader2("nome_file"), "") & "' target='_blank'><img border='0' src='../immagini/VisFoto.gif'>Visualizza</a></font></td>"
                FotoComplesso = FotoComplesso & "</tr>" & vbCrLf
                If MioColore = "#e3e1e1" Then
                    MioColore = "#cccacb"
                Else
                    MioColore = "#e3e1e1"
                End If
            Loop
            myReader2.Close()

            FotoComplesso = FotoComplesso & "</table>"


            FotoEdificio = "<table border='0' cellpadding='1' cellspacing='1' style='BACKGROUND-COLOR: white;width:650px'>"
            FotoEdificio = FotoEdificio & "<tr>" & vbCrLf
            FotoEdificio = FotoEdificio & "<td bgcolor=" & Chr(34) & "darkgray" & Chr(34) & " style='width:450px'><font face='Arial' size='1'>DESCRIZIONE</font></td>" & vbCrLf
            FotoEdificio = FotoEdificio & "<td bgcolor=" & Chr(34) & "darkgray" & Chr(34) & " style='width:200px'><font face='Arial' size='1'>FOTO</font></td>" & vbCrLf

            par.cmd.CommandText = "select * from SISCOM_MI.riprese_fotografiche WHERE id_edificio=" & IdEdificio
            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            MioColore = "#e3e1e1"
            Do While myReader3.Read()
                FotoEdificio = FotoEdificio & "<tr>" & vbCrLf
                FotoEdificio = FotoEdificio & "<td bgcolor=" & Chr(34) & MioColore & Chr(34) & " style='width:450px'><font face='Arial' size='1'>" & par.IfNull(myReader3("descrizione"), "") & "</font></td>"
                FotoEdificio = FotoEdificio & "<td bgcolor=" & Chr(34) & MioColore & Chr(34) & " style='width:200px'><p align='center'><font face='Arial' size='1'><a href='VisFoto.aspx?FILE=" & par.IfNull(myReader3("nome_file"), "") & "' target='_blank'><img border='0' src='../immagini/VisFoto.gif'> Visualizza</a></font></td>"
                FotoEdificio = FotoEdificio & "</tr>" & vbCrLf
                If MioColore = "#e3e1e1" Then
                    MioColore = "#cccacb"
                Else
                    MioColore = "#e3e1e1"
                End If
            Loop
            myReader3.Close()

            FotoEdificio = FotoEdificio & "</table>"


            FotoDWG = "<table border='0' cellpadding='1' cellspacing='1' style='BACKGROUND-COLOR: white;width:650px'>"
            FotoDWG = FotoDWG & "<tr>" & vbCrLf
            FotoDWG = FotoDWG & "<td bgcolor=" & Chr(34) & "darkgray" & Chr(34) & " style='width:450px'><font face='Arial' size='1'>DESCRIZIONE</font></td>" & vbCrLf
            FotoDWG = FotoDWG & "<td bgcolor=" & Chr(34) & "darkgray" & Chr(34) & " style='width:200px'><font face='Arial' size='1'>VISUALIZZA</font></td>" & vbCrLf
            FotoDWG = FotoDWG & "</table>"
            FotoDWG = FotoDWG & "<p align='center'><img border='0' src='../Immagini/images.jpg'></p>"

            par.OracleConn.Close()
        Catch ex As Exception
            Response.Write("ERRORE " & ex.Message)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
        End Try

    End Sub

    Public Sub TabFotoEdificio()
        If FotoEdificio <> "" Then
            Response.Write("<div class=" & Chr(34) & "tabbertab" & Chr(34) & " style=" & Chr(34) & "BACKGROUND-COLOR: white;width:700px;height:100px;overflow-x: scroll;overflow-y: scroll" & Chr(34) & ">")
            Response.Write("<h2>Foto Edificio</h2>")
            Response.Write(FotoEdificio)
            Response.Write("</div>")
        End If
    End Sub

    Public Sub TabFotoComplesso()
        If FotoComplesso <> "" Then
            Response.Write("<div class=" & Chr(34) & "tabbertab" & Chr(34) & " style=" & Chr(34) & "BACKGROUND-COLOR: white;width:700px;height:100px;overflow-x: scroll;overflow-y: scroll" & Chr(34) & ">")
            Response.Write("<h2>Foto Complesso</h2>")
            Response.Write(FotoComplesso)
            Response.Write("</div>")
        End If
    End Sub

    Public Sub TabFotoDWG()
        If FotoDWG <> "" Then
            Response.Write("<div class=" & Chr(34) & "tabbertab" & Chr(34) & " style=" & Chr(34) & "BACKGROUND-COLOR: white;width:700px;height:100px;overflow-x: scroll;overflow-y: scroll" & Chr(34) & ">")
            Response.Write("<h2>Planimetrie</h2>")
            Response.Write(FotoDWG)
            Response.Write("</div>")
        End If
    End Sub

    Public Sub ScriviIdUnita()

        Response.Write(IdUnita)

    End Sub

    Public Property FotoEdificio() As String
        Get
            If Not (ViewState("par_FotoEdificio") Is Nothing) Then
                Return CStr(ViewState("par_FotoEdificio"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_FotoEdificio") = value
        End Set
    End Property

    Public Property IdUnita() As String
        Get
            If Not (ViewState("par_IdUnita") Is Nothing) Then
                Return CStr(ViewState("par_IdUnita"))
            Else
                Return "-1"
            End If
        End Get

        Set(ByVal value As string)
            ViewState("par_IdUnita") = value
        End Set
    End Property

    Public Property FotoComplesso() As String
        Get
            If Not (ViewState("par_FotoComplesso") Is Nothing) Then
                Return CStr(ViewState("par_FotoComplesso"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_FotoComplesso") = value
        End Set
    End Property

    Public Property FotoDWG() As String
        Get
            If Not (ViewState("par_FotoDWG") Is Nothing) Then
                Return CStr(ViewState("par_FotoDWG"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_FotoDWG") = value
        End Set
    End Property

    Public Property Fabbricati() As String
        Get
            If Not (ViewState("par_Fabbricati") Is Nothing) Then
                Return CStr(ViewState("par_Fabbricati"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Fabbricati") = value
        End Set
    End Property


End Class
