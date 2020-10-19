
Partial Class ASS_ProcessoDecisionale
    Inherits PageSetIdMode
    Dim par As New CM.Global()
    Dim diffida2 As Integer = 0
    Dim strRadioButton As String = ""

    Protected Sub btnHome_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()
            idDomanda.Value = Request.QueryString("ID")
            tipoDomanda.Value = Request.QueryString("TIPO")
            tipoDomanda2.Value = Request.QueryString("TIPO")
            numOfferta.Value = Request.QueryString("OF")

            caricaMotivi()
            ControllaSeEsisteDichAgg()

            Select Case tipoDomanda.Value
                Case "1"
                    VisualizzaDomanda()
                    CaricaAlloggiProposti()
                    CaricaAlloggiAssegnati()
                Case "2"
                    VisualizzaDomandaCambi()
                    CaricaAlloggiProposti()
                    CaricaAlloggiAssegnati()
                Case "3"
                    VisualizzaDomandaEmerg()
                    CaricaAlloggiProposti()
                    CaricaAlloggiAssegnati()
            End Select
            VerificaInfoProvv()

        End If
    End Sub

    Public Property nome() As String
        Get
            If Not (ViewState("par_nome") Is Nothing) Then
                Return CStr(ViewState("par_nome"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_nome") = value
        End Set
    End Property

    Public Property cognome() As String
        Get
            If Not (ViewState("par_cognome") Is Nothing) Then
                Return CStr(ViewState("par_cognome"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_cognome") = value
        End Set
    End Property

    Public Property dataPG() As String
        Get
            If Not (ViewState("par_dataPG") Is Nothing) Then
                Return CStr(ViewState("par_dataPG"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataPG") = value
        End Set
    End Property

    Public Property idBando() As Integer
        Get
            If Not (ViewState("par_idBando") Is Nothing) Then
                Return CInt(ViewState("par_idBando"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_idBando") = value
        End Set
    End Property

    Public Property tipoDeroga() As Integer
        Get
            If Not (ViewState("par_tipoDeroga") Is Nothing) Then
                Return CInt(ViewState("par_tipoDeroga"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_tipoDeroga") = value
        End Set
    End Property

    Public Property codFisc() As String
        Get
            If Not (ViewState("par_codFisc") Is Nothing) Then
                Return CStr(ViewState("par_codFisc"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_codFisc") = value
        End Set
    End Property

    Public Property dataDisp() As String
        Get
            If Not (ViewState("par_dataDisp") Is Nothing) Then
                Return CStr(ViewState("par_dataDisp"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataDisp") = value
        End Set
    End Property

    Public Property pgDom() As String
        Get
            If Not (ViewState("par_pgDom") Is Nothing) Then
                Return CStr(ViewState("par_pgDom"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_pgDom") = value
        End Set
    End Property

    Public Property idUnita() As Long
        Get
            If Not (ViewState("par_idUnita") Is Nothing) Then
                Return CLng(ViewState("par_idUnita"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idUnita") = value
        End Set
    End Property

    Private Sub VisualizzaDomandaEmerg()
        Dim scriptblock As String = ""

        Try
            Dim CF As String = ""
            Dim lIdUnita As Long = 0

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim idDImport As Long = 0
            'par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_vsa WHERE ID=" & idDomanda.Value & " FOR UPDATE NOWAIT"
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader.Read() Then
            'myReader.Close()
            par.cmd.CommandText = "SELECT id_d_import,id_motivo_domanda,DICHIARAZIONI_vsa.PG AS ""PGDIC"",trunc(DOMANDE_BANDO_vsa.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_vsa.reddito_isee,2) as ""reddito_isee"",domande_bando_vsa.carta_i,DOMANDE_BANDO_vsa.DATA_PG,DOMANDE_BANDO_vsa.id_bando,DOMANDE_BANDO_VSA.FL_INVITO,DOMANDE_BANDO_vsa.PG,DOMANDE_BANDO_vsa.ID_DICHIARAZIONE,COMP_NUCLEO_vsa.COGNOME,COMP_NUCLEO_vsa.NOME,COMP_NUCLEO_vsa.COD_FISCALE,DOMANDE_BANDO_vsa.tipo_pratica,domande_bando_vsa.FL_ASS_ESTERNA,DOMANDE_BANDO_vsa.TIPO_ALLOGGIO FROM DOMANDE_BANDO_vsa,COMP_NUCLEO_vsa,dichiarazioni_vsa WHERE dichiarazioni_vsa.id=domande_bando_vsa.id_dichiarazione and DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=COMP_NUCLEO_vsa.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_vsa.PROGR=DOMANDE_BANDO_vsa.PROGR_COMPONENTE AND DOMANDE_BANDO_vsa.ID=" & idDomanda.Value
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    If par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "0" Or par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "2" Then
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('ATTENZIONE... Questa domanda non è stata data in gestione ad ente esterno per assegnazione e non può essere modificata!!');" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                        End If
                    End If
                    idDichiarazione.Value = par.IfNull(myReader1("ID_DICHIARAZIONE"), "-1")
                    idDImport = par.IfNull(myReader1("id_d_import"), 0)
                    codFisc = par.IfNull(myReader1("COD_FISCALE"), "")
                    dataPG = par.IfNull(myReader1("DATA_PG"), "")

                    lblPGDom.Text = par.IfNull(myReader1("PG"), "")
                    lblPGDich.Text = par.IfNull(myReader1("PGDIC"), "")
                    lblNumDoc.Text = par.IfNull(myReader1("carta_i"), "")

                    If par.IfNull(myReader1("id_motivo_domanda"), 0) = 11 Then
                        lblPGDomNew.Text = par.IfNull(myReader1("PG"), "")
                        lblPGDichNew.Text = par.IfNull(myReader1("PGDIC"), "")
                        lblPGDichNew.Text = "<a href=""javascript:window.open('../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & idDichiarazione.Value & "&LE=1&US=1','','top=250,left=650,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');void(0);"">" & lblPGDichNew.Text & "</a>"
                    End If

                    'If Len(par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")) > 25 Then
                    '    lblNominativo.Text = Mid(par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), ""), 1, 23) & "..."
                    '    lblNominativo.ToolTip = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    'Else
                    cognome = par.IfNull(myReader1("COGNOME"), "")
                    nome = par.IfNull(myReader1("NOME"), "")
                    lblNominativo.Text = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    'End If

                    'lblPosizGr.Text = par.IfNull(myReader1("POSIZIONE"), "0")
                    idBando = par.IfNull(myReader1("ID_BANDO"), "")
                    'lblNumOfferta.Text = Request.QueryString("OF")
                    lblNumOfferta.Text = numOfferta.Value

                    lblIsee.Text = par.IfNull(myReader1("REDDITO_ISEE"), "")

                    lblIsbarc.Text = par.IfNull(myReader1("isbarc_r"), "0")
                    'lblTipoPratica.Text = par.IfNull(myReader1("TIPO_PRATICA"), "0")
                    'Label20.Text = par.IfNull(myReader1("POSIZIONE"), "0")
                    'Label18.Text = par.IfNull(myReader1("GRAD"), "0")

                    'Label23.Text = par.IfNull(myReader1("COGNOME"), "")
                    'Label24.Text = par.IfNull(myReader1("NOME"), "")
                    'Label25.Text = CF


                    If par.IfNull(myReader1("TIPO_ALLOGGIO"), "0") = "0" Then
                        lblTipoAll.Text = "A"
                    Else
                        lblTipoAll.Text = "B"
                    End If

                    tipoAll.Value = par.IfNull(myReader1("TIPO_ALLOGGIO"), "0")

                    'lblPGDich.Attributes.Add("onclick", "javascript:window.open('../max.aspx?ID=" & ID_Dichiarazione & "&LE=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")
                    'lblPGDom.Attributes.Add("onclick", "javascript:window.open('../domanda.aspx?ID=" & idDomanda.Value & "&ID1=-1&PROGR=-1&LE=1&APP=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")

                    pgDom = lblPGDom.Text
                    'lblPGDom.Text = "<a href=""javascript:window.open('../VSA/domanda.aspx?ID=" & idDomanda.Value & "&ID1=-1&PROGR=-1&LE=1&APP=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');void(0);"">" & lblPGDom.Text & "</a>"
                    lblPGDom.Text = "<a href=""javascript:window.open('../VSA/NuovaDomandaVSA/domandaNuova.aspx?ID=" & idDomanda.Value & "&ID1=-1&PROGR=-1&LE=1&APP=1&US=1','');void(0);"">" & lblPGDom.Text & "</a>"
                    lblPGDich.Text = "<a href=""javascript:window.open('../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & idDichiarazione.Value & "&LE=1&US=1','','top=250,left=650,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');void(0);"">" & lblPGDich.Text & "</a>"

                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM DOMANDE_OFFERTE_SCAD WHERE ID_DOMANDA=" & idDomanda.Value & " AND ID=" & numOfferta.Value
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblScadOfferta.Text = par.FormattaData(par.IfNull(myReader1("DATA_SCADENZA"), ""))
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT count(COMP_NUCLEO_vsa.ID) FROM COMP_NUCLEO_vsa,dichiarazioni_vsa,domande_bando_vsa WHERE DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND DOMANDE_BANDO_vsa.ID=" & idDomanda.Value
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblNumComp.Text = par.IfNull(myReader1(0), "0")
                End If
                myReader1.Close()


                par.cmd.CommandText = "SELECT DOMANDE_BANDO.id,DOMANDE_BANDO.pg,BANDI_GRADUATORIA_DEF.posizione FROM BANDI_GRADUATORIA_DEF,DOMANDE_BANDO WHERE DOMANDE_BANDO.ID=BANDI_GRADUATORIA_DEF.ID_DOMANDA (+) and id_dichiarazione=" & idDImport
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblPGDom.Text = par.IfNull(myReader1("PG"), "")
                    lblPGDom.Text = "<a href=""javascript:window.open('../domanda.aspx?ID=" & par.IfNull(myReader1("ID"), "0") & "&ID1=-1&PROGR=-1&LE=1&APP=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');void(0);"">" & lblPGDom.Text & "</a>"
                    lblPosizGr.Text = par.IfNull(myReader1("POSIZIONE"), "0")
                Else
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_cambi.id,DOMANDE_BANDO_cambi.pg,BANDI_GRADUATORIA_DEF_cambi.posizione FROM BANDI_GRADUATORIA_DEF_cambi,DOMANDE_BANDO_cambi WHERE DOMANDE_BANDO_cambi.ID=BANDI_GRADUATORIA_DEF_cambi.ID_DOMANDA (+) AND id_dichiarazione=" & idDImport
                    Dim myReader1B As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1B.Read Then
                        lblPGDom.Text = par.IfNull(myReader1B("PG"), "")
                        lblPGDom.Text = "<a href=""javascript:window.open('../CAMBI/domanda.aspx?ID=" & par.IfNull(myReader1B("ID"), "0") & "&ID1=-1&PROGR=-1&LE=1&APP=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');void(0);"">" & lblPGDom.Text & "</a>"
                        lblPosizGr.Text = par.IfNull(myReader1B("POSIZIONE"), "0")
                    End If
                    myReader1B.Close()
                End If
                myReader1.Close()


                par.cmd.CommandText = "select * from dichiarazioni where id in (select id_dichiarazione from domande_bando where id_dichiarazione=" & idDImport & ")"
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblPGDich.Text = par.IfNull(myReader1("PG"), "")
                    lblPGDich.Text = "<a href=""javascript:window.open('../max.aspx?ID=" & par.IfNull(myReader1("ID"), "0") & "&LE=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');void(0);"">" & lblPGDich.Text & "</a>"

                Else
                    par.cmd.CommandText = "select * from dichiarazioni_Cambi where id in (select id_dichiarazione from domande_bando_cambi where id_dichiarazione=" & idDImport & ")"
                    Dim myReader1B As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1B.Read Then
                        lblPGDich.Text = par.IfNull(myReader1B("PG"), "")
                        lblPGDich.Text = "<a href=""javascript:window.open('../CAMBI/max.aspx?ID=" & par.IfNull(myReader1B("ID"), "0") & "&LE=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');void(0);"">" & lblPGDich.Text & "</a>"
                    End If
                    myReader1B.Close()
                End If
                myReader1.Close()

                If lblPGDich.Text = lblPGDichNew.Text Then
                    rigaNuova.Visible = False
                End If

                'RICAVO POSIZIONE IN GRADUATORIA
                par.cmd.CommandText = "SELECT domande_bando_vsa.ID as id_domanda,DOMANDE_BANDO_VSA.PG, COGNOME,NOME,SUM (punteggio) AS punteggio,DATA_PRESENTAZIONE AS DATA_PRES,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO" _
                    & " FROM tab_punti_emergenze, domande_bando_vsa_punti_em, domande_bando_vsa,COMP_NUCLEO_VSA,DICHIARAZIONI_VSA,SISCOM_MI.TIPO_LIVELLO_PIANO,DOMANDE_VSA_ALLOGGIO " _
                    & " WHERE domande_bando_vsa_punti_em.id_punteggio = tab_punti_emergenze.ID" _
                    & " AND domande_bando_vsa_punti_em.id_domanda = domande_bando_vsa.ID" _
                    & " AND comp_nucleo_vsa.id_dichiarazione = dichiarazioni_vsa.ID " _
                    & " AND domande_bando_vsa.id_dichiarazione = dichiarazioni_vsa.ID AND COMP_NUCLEO_VSA.PROGR=0" _
                    & " AND (domande_bando_vsa.id_stato = '8' OR domande_bando_vsa.id_stato = '9')" _
                    & " AND domande_bando_vsa.id_motivo_domanda = 4 AND TIPO_LIVELLO_PIANO.COD=DOMANDE_VSA_ALLOGGIO.PIANO AND DOMANDE_VSA_ALLOGGIO.ID_DOMANDA=DOMANDE_BANDO_VSA.ID " _
                    & " GROUP BY domande_bando_vsa.ID,domande_bando_vsa.pg,DATA_PRESENTAZIONE,NOME,COGNOME,TIPO_LIVELLO_PIANO.DESCRIZIONE" _
                    & " ORDER BY punteggio DESC,DATA_PRESENTAZIONE ASC"
                Dim daE As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dtE As New Data.DataTable
                daE = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                daE.Fill(dtE)
                daE.Dispose()

                Dim posizione As Integer = 1
                For Each row1 As Data.DataRow In dtE.Rows
                    If row1.Item("ID_DOMANDA") = idDomanda.Value Then
                        lblPosizGr.Text = posizione
                        Exit For
                    End If
                    posizione = posizione + 1
                Next

                'Else
                '    myReader.Close()
                'End If


                par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Pratica aperta da un altro utente. Non è possibile procedere in questo momento!');history.go(-1);" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            Else
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('" & EX1.ToString & "');history.go(-1);" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "alert('" & ex.Message & "');history.go(-1);" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
            End If
        End Try

    End Sub

    Private Sub VisualizzaDomandaCambi()
        Dim scriptblock As String = ""

        Try
            Dim CF As String = ""
            Dim lIdUnita As Long = 0

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            'par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_CAMBI WHERE ID=" & idDomanda.Value & " FOR UPDATE NOWAIT"
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader.Read() Then
            '    myReader.Close()
            par.cmd.CommandText = "SELECT DICHIARAZIONI_CAMBI.PG AS ""PGDIC"",trunc(DOMANDE_BANDO_cambi.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_cambi.reddito_isee,2) as ""reddito_isee"",domande_bando_cambi.carta_i,DOMANDE_BANDO_cambi.DATA_PG,DOMANDE_BANDO_cambi.id_bando,DOMANDE_BANDO_cambi.PG,DOMANDE_BANDO_CAMBI.fl_invito,DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE,COMP_NUCLEO_cambi.COGNOME,COMP_NUCLEO_cambi.NOME,COMP_NUCLEO_CAMBI.COD_FISCALE,DOMANDE_BANDO_cambi.tipo_pratica,domande_bando_cambi.FL_ASS_ESTERNA,DOMANDE_BANDO_cambi.TIPO_ALLOGGIO,BANDI_GRADUATORIA_DEF_cambi.POSIZIONE,trunc(BANDI_GRADUATORIA_DEF_cambi.ISBARC_R,4) AS ""GRAD"" FROM BANDI_GRADUATORIA_DEF_cambi,DOMANDE_BANDO_cambi,COMP_NUCLEO_cambi,dichiarazioni_cambi WHERE dichiarazioni_cambi.id=domande_bando_cambi.id_dichiarazione and DOMANDE_BANDO_cambi.ID=BANDI_GRADUATORIA_DEF_cambi.ID_DOMANDA (+) AND DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=COMP_NUCLEO_cambi.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_cambi.PROGR=DOMANDE_BANDO_cambi.PROGR_COMPONENTE AND DOMANDE_BANDO_cambi.ID=" & idDomanda.Value
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    If par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "0" Or par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "2" Then
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('ATTENZIONE... Questa domanda non è stata data in gestione ad ente esterno per assegnazione e non può essere modificata!!');" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                        End If
                    End If

                    codFisc = par.IfNull(myReader1("COD_FISCALE"), "")
                    idDichiarazione.Value = par.IfNull(myReader1("ID_DICHIARAZIONE"), 0)
                    dataPG = par.IfNull(myReader1("DATA_PG"), "")
                    'lblPG.Text = par.IfNull(myReader1("PG"), "")
                    lblPGDom.Text = par.IfNull(myReader1("PG"), "")
                    lblPGDich.Text = par.IfNull(myReader1("PGDIC"), "")
                    lblNumDoc.Text = par.IfNull(myReader1("carta_i"), "")

                    'If Len(par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")) > 25 Then
                    '    lblNominativo.Text = Mid(par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), ""), 1, 23) & "..."
                    '    lblNominativo.ToolTip = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    'Else
                    cognome = par.IfNull(myReader1("COGNOME"), "")
                    nome = par.IfNull(myReader1("NOME"), "")
                    lblNominativo.Text = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    'End If

                    idBando = par.IfNull(myReader1("ID_BANDO"), "")
                    lblPosizGr.Text = par.IfNull(myReader1("POSIZIONE"), "0")
                    'lblNumOfferta.Text = Request.QueryString("OF")
                    lblNumOfferta.Text = numOfferta.Value

                    lblIsee.Text = par.IfNull(myReader1("REDDITO_ISEE"), "")

                    lblIsbarc.Text = par.IfNull(myReader1("isbarc_r"), "0")
                    'lblTipoPratica.Text = par.IfNull(myReader1("TIPO_PRATICA"), "0")
                    'Label20.Text = par.IfNull(myReader1("POSIZIONE"), "0")
                    'Label18.Text = par.IfNull(myReader1("GRAD"), "0")

                    'Label23.Text = par.IfNull(myReader1("COGNOME"), "")
                    'Label24.Text = par.IfNull(myReader1("NOME"), "")
                    'Label25.Text = CF
                    'Label26.Text = par.IfNull(myReader1("ID_DICHIARAZIONE"), "-1")

                    If par.IfNull(myReader1("TIPO_ALLOGGIO"), "0") = "0" Then
                        lblTipoAll.Text = "A"
                    Else
                        lblTipoAll.Text = "B"
                    End If

                    tipoAll.Value = par.IfNull(myReader1("TIPO_ALLOGGIO"), "0")

                    'lblPGDich.Attributes.Add("onclick", "javascript:window.open('../max.aspx?ID=" & ID_Dichiarazione & "&LE=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")
                    'lblPGDom.Attributes.Add("onclick", "javascript:window.open('../domanda.aspx?ID=" & idDomanda.Value & "&ID1=-1&PROGR=-1&LE=1&APP=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")

                    pgDom = lblPGDom.Text
                    lblPGDom.Text = "<a href=""javascript:window.open('../CAMBI/domanda.aspx?ID=" & idDomanda.Value & "&ID1=-1&PROGR=-1&LE=1&APP=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');void(0);"">" & lblPGDom.Text & "</a>"
                    lblPGDich.Text = "<a href=""javascript:window.open('../CAMBI/max.aspx?ID=" & idDichiarazione.Value & "&LE=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');void(0);"">" & lblPGDich.Text & "</a>"

                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM DOMANDE_OFFERTE_SCAD WHERE ID_DOMANDA=" & idDomanda.Value & " AND ID=" & numOfferta.Value
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblScadOfferta.Text = par.FormattaData(par.IfNull(myReader1("DATA_SCADENZA"), ""))
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT count(COMP_NUCLEO_cambi.ID) FROM COMP_NUCLEO_cambi,dichiarazioni_cambi,domande_bando_cambi WHERE DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND COMP_NUCLEO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND DOMANDE_BANDO_cambi.ID=" & idDomanda.Value
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblNumComp.Text = par.IfNull(myReader1(0), "0")
                End If
                myReader1.Close()

            'Else
            '    myReader.Close()
            'End If


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Pratica aperta da un altro utente. Non è possibile procedere in questo momento!');history.go(-1);" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            Else
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('" & EX1.ToString & "');history.go(-1);" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "alert('" & ex.Message & "');history.go(-1);" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
            End If
        End Try

    End Sub

    Private Sub VisualizzaDomanda()
        Dim scriptblock As String = ""

        Try
            Dim CF As String = ""
            Dim lIdUnita As Long = 0

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            'par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO WHERE ID = " & idDomanda.Value & " AND FL_INVITO=1"
            'Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader0.Read Then
            '    btnInvita.Enabled = False
            '    btnInvita.BackColor = Drawing.Color.Gainsboro
            '    invito.Value = "1"
            'End If
            'myReader0.Close()

            'par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO WHERE ID=" & idDomanda.Value & " FOR UPDATE NOWAIT"
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader.Read() Then
            '    myReader.Close()
            par.cmd.CommandText = "SELECT DICHIARAZIONI.PG AS ""PGDIC"", trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",domande_bando.carta_i,DOMANDE_BANDO.DATA_PG,domande_bando.fl_invito,DOMANDE_BANDO.id_bando,DOMANDE_BANDO.PG,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,COMP_NUCLEO.COD_FISCALE,DOMANDE_BANDO.tipo_pratica,domande_bando.FL_ASS_ESTERNA,DOMANDE_BANDO.TIPO_ALLOGGIO,BANDI_GRADUATORIA_DEF.tipo as TIPOERP,BANDI_GRADUATORIA_DEF.POSIZIONE,trunc(BANDI_GRADUATORIA_DEF.ISBARC_R,4) AS ""GRAD"",DOMANDE_BANDO.ID_DICHIARAZIONE FROM BANDI_GRADUATORIA_DEF,DOMANDE_BANDO,COMP_NUCLEO,DICHIARAZIONI WHERE DICHIARAZIONI.ID=DOMANDE_BANDO.ID_DICHIARAZIONE AND DOMANDE_BANDO.ID=BANDI_GRADUATORIA_DEF.ID_DOMANDA (+) AND DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE (+) AND COMP_NUCLEO.PROGR=DOMANDE_BANDO.PROGR_COMPONENTE AND DOMANDE_BANDO.ID=" & idDomanda.Value
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    If par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "0" Or par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "2" Then
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('ATTENZIONE... Questa domanda non è stata data in gestione ad ente esterno per assegnazione e non può essere modificata!!');" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                        End If
                    End If

                    codFisc = par.IfNull(myReader1("COD_FISCALE"), "")
                    idDichiarazione.Value = par.IfNull(myReader1("ID_DICHIARAZIONE"), 0)
                    dataPG = par.IfNull(myReader1("DATA_PG"), "")
                    'lblPG.Text = par.IfNull(myReader1("PG"), "")
                    lblPGDom.Text = par.IfNull(myReader1("PG"), "")
                    lblPGDich.Text = par.IfNull(myReader1("PGDIC"), "")
                    lblNumDoc.Text = par.IfNull(myReader1("carta_i"), "")
                    tipoDeroga = par.IfNull(myReader1("TIPOERP"), "")

                    If par.IfNull(myReader1("FL_INVITO"), "") = "1" Then
                        'btnInvita.Enabled = False
                        'btnInvita.BackColor = Drawing.Color.Gainsboro
                        invito.Value = "1"
                    End If

                    'If Len(par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")) > 25 Then
                    '    lblNominativo.Text = Mid(par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), ""), 1, 23) & "..."
                    '    lblNominativo.ToolTip = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    'Else
                    cognome = par.IfNull(myReader1("COGNOME"), "")
                    nome = par.IfNull(myReader1("NOME"), "")
                    lblNominativo.Text = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    'End If

                    idBando = par.IfNull(myReader1("ID_BANDO"), "")
                    lblPosizGr.Text = par.IfNull(myReader1("POSIZIONE"), "0")
                    'lblNumOfferta.Text = Request.QueryString("OF")
                    lblNumOfferta.Text = numOfferta.Value

                    lblIsee.Text = par.IfNull(myReader1("REDDITO_ISEE"), "")

                    lblIsbarc.Text = par.IfNull(myReader1("isbarc_r"), "0")
                    'lblTipoPratica.Text = par.IfNull(myReader1("TIPO_PRATICA"), "0")
                    'Label20.Text = par.IfNull(myReader1("POSIZIONE"), "0")
                    'Label18.Text = par.IfNull(myReader1("GRAD"), "0")

                    'Label23.Text = par.IfNull(myReader1("COGNOME"), "")
                    'Label24.Text = par.IfNull(myReader1("NOME"), "")
                    'Label25.Text = CF
                    'Label26.Text = par.IfNull(myReader1("ID_DICHIARAZIONE"), "-1")

                    If par.IfNull(myReader1("TIPO_ALLOGGIO"), "0") = "0" Then
                        lblTipoAll.Text = "A"
                    Else
                        lblTipoAll.Text = "B"
                    End If

                    tipoAll.Value = par.IfNull(myReader1("TIPO_ALLOGGIO"), "0")

                    'lblPGDich.Attributes.Add("onclick", "javascript:window.open('../max.aspx?ID=" & ID_Dichiarazione & "&LE=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")
                    'lblPGDom.Attributes.Add("onclick", "javascript:window.open('../domanda.aspx?ID=" & idDomanda.Value & "&ID1=-1&PROGR=-1&LE=1&APP=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")
                    pgDom = lblPGDom.Text
                    lblPGDom.Text = "<a href=""javascript:window.open('../domanda.aspx?ID=" & idDomanda.Value & "&ID1=-1&PROGR=-1&LE=1&APP=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');void(0);"">" & lblPGDom.Text & "</a>"
                    lblPGDich.Text = "<a href=""javascript:window.open('../max.aspx?ID=" & idDichiarazione.Value & "&LE=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');void(0);"">" & lblPGDich.Text & "</a>"

                End If
                myReader1.Close()


                par.cmd.CommandText = "SELECT  * FROM EVENTI_BANDI WHERE ID_DOMANDA=" & idDomanda.Value & " AND COD_EVENTO='F136'"
                Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader11.Read() Then
                    If par.IfNull(myReader11("cod_evento"), "") <> "" Then

                        cmbDeroga.Visible = True

                    End If
                Else
                    cmbDeroga.Visible = False

                    'Label16.Text = ""
                End If
                myReader11.Close()

                par.cmd.CommandText = "SELECT * FROM DOMANDE_OFFERTE_SCAD WHERE ID_DOMANDA=" & idDomanda.Value & " AND ID=" & numOfferta.Value
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblScadOfferta.Text = par.FormattaData(par.IfNull(myReader1("DATA_SCADENZA"), ""))
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT count(COMP_NUCLEO.ID) FROM COMP_NUCLEO,dichiarazioni,domande_bando WHERE DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND DOMANDE_BANDO.ID=" & idDomanda.Value
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblNumComp.Text = par.IfNull(myReader1(0), "0")
                End If
                myReader1.Close()
            'Else
            '    myReader.Close()
            'End If


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Pratica aperta da un altro utente. Non è possibile procedere in questo momento!');history.go(-1);" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            Else
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('" & EX1.ToString & "');history.go(-1);" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "alert('" & ex.Message & "');history.go(-1);" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
            End If
        End Try

    End Sub


    Private Sub CaricaAlloggiProposti()
        Try
            Dim tabellaRiferimento As String = ""
            Dim proposteValida As Boolean = False
            Dim propNoValid As Boolean = False


            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Select Case tipoDomanda.Value
                Case "1"
                    tabellaRiferimento = "DOMANDE_BANDO"
                Case "2"
                    tabellaRiferimento = "DOMANDE_BANDO_CAMBI"
                Case "3"
                    tabellaRiferimento = "DOMANDE_BANDO_VSA"
            End Select

            par.cmd.CommandText = "SELECT alloggi.*,alloggi.id as id_alloggio,TO_CHAR(TO_DATE (REL_PRAT_ALL_CCAA_ERP.DATA_PROPOSTA, 'YYYYmmdd'),'DD/MM/YYYY') AS DATA_PROPOSTA,UNITA_IMMOBILIARI.ID, " _
                       & " TO_CHAR(TO_DATE (REL_PRAT_ALL_CCAA_ERP.DATA, 'YYYYmmdd'),'DD/MM/YYYY') as DATA, " _
                       & " REL_PRAT_ALL_CCAA_ERP.FL_DIFFIDA2 AS DIFFIDA2, " _
                       & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzoCompleto,REL_PRAT_ALL_CCAA_ERP.*, (CASE WHEN (motivazioni_ann_rif_all.motivazione || ' - ' || rel_prat_all_ccaa_erp.motivazione) = '' THEN '' WHEN motivazioni_ann_rif_all.motivazione IS NULL THEN rel_prat_all_ccaa_erp.motivazione WHEN rel_prat_all_ccaa_erp.motivazione IS NULL THEN motivazioni_ann_rif_all.motivazione WHEN (motivazioni_ann_rif_all.motivazione || ' - ' || rel_prat_all_ccaa_erp.motivazione) IS NOT NULL THEN motivazioni_ann_rif_all.motivazione || ' - ' || rel_prat_all_ccaa_erp.motivazione END) AS motivazione_all," _
                       & "('<a href=""javascript:window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID='||UNITA_IMMOBILIARI.ID||''',''Dettagli'',''height=580,top=0,left=0,width=780'');void(0);"">'||'<img alt=" & Chr(34) & "Dettagli Unità, Foto e Planimetrie" & Chr(34) & " title=" & Chr(34) & "Visualizza Alloggio" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/Abbina_Foto.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " />'||'</a>') as Visualizza," _
                       & "('<a href=""javascript:window.open(''CalcolaCanone.aspx?P='||PROPRIETA||'&IdDomanda=" & idDomanda.Value & "&Tipo=" & tipoDomanda.Value & "&ID='||UNITA_IMMOBILIARI.ID||''',''Dettagli'','''');void(0);"">'||'<img alt=" & Chr(34) & "Visualizza Canone" & Chr(34) & " title=" & Chr(34) & "Visualizza Schema Calcolo Canone" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/Abbina_Canone.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " />'||'</a>') as VisualizzaCanone " _
                       & " FROM REL_PRAT_ALL_CCAA_ERP,ALLOGGI,SISCOM_MI.UNITA_IMMOBILIARI, MOTIVAZIONI_ANN_RIF_ALL,t_tipo_indirizzo," & tabellaRiferimento & " " _
                       & " WHERE " & tabellaRiferimento & ".id = REL_PRAT_ALL_CCAA_ERP.id_pratica and alloggi.tipo_indirizzo=t_tipo_indirizzo.cod (+) and " _
                       & " ALLOGGI.COD_ALLOGGIO=UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE (+) AND ALLOGGI.ID = REL_PRAT_ALL_CCAA_ERP.ID_ALLOGGIO " _
                       & " AND MOTIVAZIONI_ANN_RIF_ALL.ID(+)= rel_prat_all_ccaa_erp.ID_MOTIVAZIONE" _
                       & " AND rel_prat_all_ccaa_erp.ID_PRATICA=" & idDomanda.Value & " ORDER BY REL_PRAT_ALL_CCAA_ERP.ULTIMO DESC"
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt As New Data.DataTable
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            da.Fill(dt)
            da.Dispose()
            Dim i As Integer = 0
            If dt.Rows.Count > 0 Then
                DataGridProposte.DataSource = dt
                DataGridProposte.DataBind()

                'Dim di1 As DataGridItem

                'For i = 0 To Me.DataGridProposte.Items.Count - 1
                '    di1 = Me.DataGridProposte.Items(i)
                '    If di1.Cells(11).Text = "1" Then

                '        CType(di1.Cells(12).FindControl("ChDiffida2"), CheckBox).Checked = True

                '    Else

                '        CType(di1.Cells(12).FindControl("ChDiffida2"), CheckBox).Checked = False

                '    End If

                'Next

                If DataGridProposte.Items.Count >= 2 Then

                    Dim di As DataGridItem
                    For i = 0 To Me.DataGridProposte.Items.Count - 1
                        di = Me.DataGridProposte.Items(i)
                        If i = 0 Then
                            idAlloggio.Value = di.Cells(0).Text
                            If di.Cells(10).Text = "0" Or di.Cells(10).Text = "3" Or di.Cells(10).Text = "4" Then

                                proposti.Value = "3"
                                'btnNuovaProp.Enabled = False
                                'btnAccetta.Enabled = False
                                cmbDeroga.Visible = False
                                proposteValida = False
                                propNoValid = True
                                ' diffida2 = 1

                            End If

                            If di.Cells(10).Text = "&nbsp;" Or di.Cells(10).Text = "1" Then

                                proposteValida = True
                                proposti.Value = "1"
                                btnNuovaProp.Enabled = False
                                btnAnnullaProp.Enabled = True
                                btnRifiuta.Enabled = True

                                '  btnAccetta.Enabled = False
                                di.Cells(8).Text = ""
                            End If

                        End If

                    Next
                Else


                    Dim di As DataGridItem
                    For i = 0 To Me.DataGridProposte.Items.Count - 1
                        di = Me.DataGridProposte.Items(i)

                        idAlloggio.Value = di.Cells(0).Text

                        If di.Cells(10).Text = "&nbsp;" Then

                            proposteValida = True
                            propNoValid = True
                            proposti.Value = "1"
                            btnNuovaProp.Enabled = False
                            btnAccetta.Enabled = True
                            btnRifiuta.Enabled = True
                            di.Cells(8).Text = ""
                        End If

                        If di.Cells(10).Text = "1" Then
                            proposteValida = True
                            propNoValid = True
                            proposti.Value = "1"
                            btnNuovaProp.Enabled = False
                            btnAccetta.Enabled = False
                            btnRifiuta.Enabled = False
                            di.Cells(8).Text = ""
                        End If

                        If di.Cells(10).Text = "0" Or di.Cells(10).Text = "3" Or di.Cells(10).Text = "4" Then

                            If di.Cells(11).Text = "0" Or di.Cells(11).Text = "&nbsp;" Then

                                proposti.Value = "0"
                                'proposteValida = False
                                btnNuovaProp.Enabled = True
                                btnAccetta.Enabled = False
                                cmbDeroga.Visible = False
                                btnAnnullaProp.Enabled = False
                                btnRifiuta.Enabled = False
                                btnAnnullaAss.Enabled = False
                                btnConfAss.Enabled = False

                            Else
                                proposti.Value = "3"
                                btnNuovaProp.Enabled = False
                                btnAccetta.Enabled = False
                                cmbDeroga.Visible = False
                                proposteValida = False
                                propNoValid = True

                            End If

                        End If

                    Next

                End If

            End If

            If DataGridProposte.Items.Count > 0 Then

                If DataGridProposte.Items.Count >= 2 Then
                    Dim di As DataGridItem

                    For i = 0 To Me.DataGridProposte.Items.Count - 1
                        di = Me.DataGridProposte.Items(i)

                        If di.Cells(11).Text = "0" Then
                            diffida.Value = "1"
                            diffida2 = 0
                        End If

                        If di.Cells(11).Text = "1" Then
                            diffida.Value = "0"
                            diffida2 = 1
                        End If
                    Next
                Else

                    Dim di As DataGridItem

                    For i = 0 To Me.DataGridProposte.Items.Count - 1
                        di = Me.DataGridProposte.Items(i)

                        If di.Cells(11).Text = "nbsp;" Then

                            diffida.Value = "0"
                            diffida2 = 0
                        End If

                        If di.Cells(11).Text = "0" Then

                            diffida.Value = "1"
                            diffida2 = 0
                        End If

                        If di.Cells(11).Text = "1" Then

                            diffida.Value = "0"
                            diffida2 = 1

                        End If
                    Next
                End If
            End If


            If DataGridProposte.Items.Count > 0 Then

                Dim di As DataGridItem

                For i = 0 To Me.DataGridProposte.Items.Count - 1
                    di = Me.DataGridProposte.Items(i)

                    If di.Cells(10).Text = "0" Then

                        di.Cells(12).Text = "Proposta rifiutata"
                    End If

                    If di.Cells(10).Text = "3" Then

                        di.Cells(12).Text = "Proposta annullata"

                    End If

                    If di.Cells(10).Text = "4" Then
                        di.Cells(12).Text = "Assegnazione annullata"

                    End If

                    If di.Cells(10).Text = "1" Then
                        di.Cells(12).Text = "Proposta accettata"

                    End If

                    If di.Cells(10).Text = "&nbsp;" Then

                        di.Cells(12).Text = "Proposta in corso"
                        idUnitaValida.Value = di.Cells(par.IndDGC(DataGridProposte, "id_unita")).Text
                        fl_proprieta.Value = di.Cells(par.IndDGC(DataGridProposte, "proprieta")).Text
                    End If
                Next
            End If

            CaricaDocumenti()

            If proposteValida = False Then
                btnAnnullaProp.Enabled = False
                btnAccetta.Enabled = False
                cmbDeroga.Visible = False
                btnRifiuta.Enabled = False
                btnAnnullaAss.Enabled = False
                btnConfAss.Enabled = False
                btnNuovaProp.Enabled = True
            Else
                btnAccetta.Enabled = True
            End If

            'If propNoValid = True Then
            '    btnNuovaProp.Enabled = False
            'End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Function ControllaSeEsisteDichAgg() As Long
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim nomeTbl As String = ""
            Dim tipoDimport As Integer = 0
            Select Case tipoDomanda.Value
                Case 1
                    nomeTbl = "Domande_bando"
                    tipoDimport = 3
                Case 2
                    nomeTbl = "Domande_bando_cambi"
                    tipoDimport = 3
                Case 3
                    nomeTbl = "Domande_bando_vsa"
                    tipoDimport = 1
            End Select

            par.cmd.CommandText = "select id from domande_bando_vsa domVSA where tipo_D_import=" & tipoDimport & " and id_motivo_domanda=11 and id_d_import=(select id_dichiarazione from " & nomeTbl & " where id=" & idDomanda.Value & " and domVSA.data_pg>=data_pg)"
            Dim idDomNew As Integer = par.IfEmpty(par.cmd.ExecuteScalar, 0)

            If idDomNew <> 0 Then
                idDomanda.Value = idDomNew
                tipoDomanda.Value = 3
                rigaNuova.Visible = True
                lblProtocolloDom2.Text = "Protocollo Dom. Nuova"
                lblProtocolloDich2.Text = "Protocollo Dich. Nuova"
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Return idDomNew
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Function

    Private Sub CaricaAlloggiAssegnati()
        Try
            Dim tabellaRiferimento As String = ""

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Select Case tipoDomanda.Value
                Case "1"
                    tabellaRiferimento = "DOMANDE_BANDO"
                Case "2"
                    tabellaRiferimento = "DOMANDE_BANDO_CAMBI"
                Case "3"
                    tabellaRiferimento = "DOMANDE_BANDO_VSA"
            End Select

            par.cmd.CommandText = "SELECT distinct alloggi.id as id_alloggio,alloggi.*,TO_CHAR(TO_DATE (REL_PRAT_ALL_CCAA_ERP.DATA_PROPOSTA, 'YYYYmmdd'),'DD/MM/YYYY') AS DATA_PROPOSTA,TO_CHAR(TO_DATE (REL_PRAT_ALL_CCAA_ERP.DATA, 'YYYYmmdd'),'DD/MM/YYYY') AS DATA_ACCETTAZIONE,(t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzoCompleto,REL_PRAT_ALL_CCAA_ERP.*,UNITA_IMMOBILIARI.ID AS ID_UNITA, " & tabellaRiferimento & ".id_stato, " _
                       & "('<a href=""javascript:window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID='||UNITA_IMMOBILIARI.ID||''',''Dettagli'',''height=580,top=0,left=0,width=780'');void(0);"">'||'<img alt=" & Chr(34) & "Dettagli Unità, Foto e Planimetrie" & Chr(34) & " title=" & Chr(34) & "Visualizza Alloggio" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/Abbina_Foto.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " />'||'</a>') as Visualizza," _
                       & "('<a href=""javascript:window.open(''CalcolaCanone.aspx?P='||PROPRIETA||'&IdDomanda=" & idDomanda.Value & "&Tipo=" & tipoDomanda.Value & "&ID='||UNITA_IMMOBILIARI.ID||''',''Dettagli'',''height=500,top=0,left=0,width=600'');void(0);"">'||'<img alt=" & Chr(34) & "Visualizza Canone" & Chr(34) & " title=" & Chr(34) & "Visualizza Schema Calcolo Canone" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/Abbina_Canone.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " />'||'</a>') as VisualizzaCanone " _
                       & " FROM REL_PRAT_ALL_CCAA_ERP,ALLOGGI,SISCOM_MI.UNITA_IMMOBILIARI,t_tipo_indirizzo,siscom_mi.unita_assegnate," & tabellaRiferimento & " " _
                       & " WHERE " & tabellaRiferimento & ".id = REL_PRAT_ALL_CCAA_ERP.id_pratica and alloggi.tipo_indirizzo=t_tipo_indirizzo.cod (+) and REL_PRAT_ALL_CCAA_ERP.esito = 1 and " _
                       & " ALLOGGI.COD_ALLOGGIO=UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE (+) AND unita_assegnate.id_domanda = " & tabellaRiferimento & ".ID and ASSEGNATO = 1 AND ALLOGGI.ID = REL_PRAT_ALL_CCAA_ERP.ID_ALLOGGIO AND rel_prat_all_ccaa_erp.ID_PRATICA=" & idDomanda.Value
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt As New Data.DataTable
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            da.Fill(dt)
            da.Dispose()
            'If dt.Rows.Count > 0 Then
            DataGridAssegnati.DataSource = dt
            DataGridAssegnati.DataBind()
            'End If

            If dt.Rows.Count > 0 Then
                btnAggiornaRedditi.Visible = False
                For Each row As Data.DataRow In dt.Rows
                    idUnita = par.IfNull(row.Item("ID_UNITA"), -1)
                Next
                btnAnnullaProp.Enabled = False
                btnNuovaProp.Enabled = False
                btnAccetta.Enabled = False
                cmbDeroga.Visible = False
                btnRifiuta.Enabled = False
                btnConfAss.Enabled = True
                btnAnnullaAss.Enabled = True
                assegnati.Value = "1"
            Else
                assegnati.Value = "0"
                btnConfAss.Enabled = False
                btnAnnullaAss.Enabled = False
            End If

            If dt.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt.Rows
                    If par.IfNull(row.Item("ID_STATO"), -1) = "10" Then
                        assegnati.Value = "0"
                        proposti.Value = "0"
                        btnConfAss.Enabled = False
                        btnAnnullaAss.Enabled = False
                        btnRevoca.Enabled = False
                    End If
                Next
            End If

            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.ID FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA WHERE ID_UNITA=" & idUnita & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SISCOM_MI.GETSTATOCONTRATTO(ID)='BOZZA' "
            Dim da1 As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt1 As New Data.DataTable
            da1 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da1.Fill(dt1)
            da1.Dispose()
            If dt1.Rows.Count > 0 Then
                assegnati.Value = "0"
                proposti.Value = "0"
                btnConfAss.Enabled = False
                btnAnnullaAss.Enabled = False
                btnRevoca.Enabled = False
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub caricaMotivi()
        Try
            Dim tabellaRiferimento As String = ""

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Dim sStringaSQL As String = ""
            ' Dim strRadioButton As String = ""

            sStringaSQL = " SELECT MOTIVAZIONI_ANN_RIF_ALL.ID, MOTIVAZIONI_ANN_RIF_ALL.motivazione FROM MOTIVAZIONI_ANN_RIF_ALL where id_tipo=1 order by id asc "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            Dim i As Integer = 1
            If dt.Rows.Count > 0 Then

                For Each row In dt.Rows
                    strRadioButton &= "<input id='Radio" & i & "' type='radio' name='rdbMotivi' value='" & par.IfNull(row.item("id"), -1) & "'/>" & par.IfNull(row.item("motivazione"), " ") & "<br />"
                    'rdbMotivi.Items.Add(New ListItem(par.IfNull(row.item("motivazione"), " "), par.IfNull(row.item("id"), -1)))
                Next
                i = i + 1
            End If


            strRadioButton = Replace(strRadioButton, "<", "@")
            strRadioButton = Replace(strRadioButton, ">", "#")
            strRadioButton1.Value = strRadioButton


            strRadioButton = ""
            sStringaSQL = ""

            sStringaSQL = " SELECT MOTIVAZIONI_ANN_RIF_ALL.ID, MOTIVAZIONI_ANN_RIF_ALL.motivazione FROM MOTIVAZIONI_ANN_RIF_ALL where id_tipo=2 order by id asc "

            da = New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL, par.OracleConn)
            dt = New Data.DataTable
            da.Fill(dt)

            i = 1
            If dt.Rows.Count > 0 Then

                For Each row In dt.Rows
                    strRadioButton &= "<input id='Radio" & i & "' type='radio' name='rdbMotivi' value='" & par.IfNull(row.item("id"), -1) & "'/>" & par.IfNull(row.item("motivazione"), " ") & "<br />"
                    'rdbMotivi.Items.Add(New ListItem(par.IfNull(row.item("motivazione"), " "), par.IfNull(row.item("id"), -1)))
                Next
                i = i + 1
            End If


            strRadioButton = Replace(strRadioButton, "<", "@")
            strRadioButton = Replace(strRadioButton, ">", "#")
            strRadioButton2.Value = strRadioButton








            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Private Sub VerificaInfoProvv()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_ASSEGNATE WHERE provvedimento IS NOT NULL AND DATA_PROVVEDIMENTO IS NOT NULL AND ID_DOMANDA= " & idDomanda.Value & " and n_offerta=" & numOfferta.Value
            Dim myReaderV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderV.Read Then
                numProvv.Value = par.IfNull(myReaderV("PROVVEDIMENTO"), "")
                dataProvv.Value = par.FormattaData(par.IfNull(myReaderV("DATA_PROVVEDIMENTO"), ""))
                lblAvviso.Text = "Assegnazione completata! Nel modulo Rapporti Utenza è possibile ora attivare il contratto."
            End If
            myReaderV.Close()

            par.cmd.CommandText = "SELECT * FROM PROPOSTE_REVOCHE WHERE ID_PRATICA=" & idDomanda.Value
            myReaderV = par.cmd.ExecuteReader()
            If myReaderV.Read Then
                lblRevoca.Visible = True
                btnAccetta.Enabled = False
                cmbDeroga.Visible = False
                btnAnnullaAss.Enabled = False
                btnConfAss.Enabled = False
                btnNuovaProp.Enabled = False
                btnRevoca.Enabled = False
                btnRifiuta.Enabled = False
                revocato.Value = "1"
                lblAvviso.Text = ""
                btnAggiornaRedditi.Visible = False
            Else
                lblRevoca.Visible = False
                revocato.Value = "0"
            End If
            myReaderV.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Private Sub CaricaDocumenti()
        Try
            'caricaMotivi()
            TStampe.Items(0).ChildItems.Clear()



            Dim item As MenuItem
            item = New MenuItem("Ristampa Offerta", "Offerta", "", "javascript:RistampaOfferta();")
            TStampe.Items(0).ChildItems.Add(item)

            item = New MenuItem("Telegramma Invito", "TestoTelegramma", "", "javascript:TestoInvito();")
            TStampe.Items(0).ChildItems.Add(item)

            item = New MenuItem("Modulo Offerta", "PermVisita", "", "javascript:PermessoVisita();")
            TStampe.Items(0).ChildItems.Add(item)


            controllaDocDiffide()


            If diffida.Value = 1 Then

                item = New MenuItem("Diffida Offerta Altro Alloggio", "DiffidaOfferta", "", "javascript:Diffida1Alloggio();")
                TStampe.Items(0).ChildItems.Add(item)

            End If


            If diffida2 = 1 Then

                item = New MenuItem("Diffida Offerta Stesso Alloggio", "Diffida2Offerta", "", "javascript:Diffida2Alloggio();")
                TStampe.Items(0).ChildItems.Add(item)


            End If

            item = New MenuItem("Rapporto sintetico Alloggio", "RappOffertaAlloggio", "", "javascript:RappSintetico();")
            TStampe.Items(0).ChildItems.Add(item)


            item = New MenuItem("Elenco Stampe", "ElStampe", "", "javascript:ElencoStampe();")
            TStampe.Items(0).ChildItems.Add(item)


        Catch ex As Exception

        End Try

    End Sub


    Protected Sub DataGridProposte_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridProposte.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridProposte.CurrentPageIndex = e.NewPageIndex
            CaricaAlloggiProposti()
        End If
    End Sub

    Protected Sub DataGridAssegnati_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridAssegnati.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridAssegnati.CurrentPageIndex = e.NewPageIndex
            CaricaAlloggiAssegnati()
        End If
    End Sub

    Protected Sub btnAnnullaProp_Click(sender As Object, e As System.EventArgs) Handles btnAnnullaProp.Click
        Try

            Dim RELAZIONE As String = "-1"
            Dim ID_ALLOGGIO As String = "-1"
            Dim stDiffida As String = "0"
            'Dim MOTIVO As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()

            'If motivazione.Value <> "-1" Then

            '    par.cmd.CommandText = " SELECT MOTIVAZIONI_ANN_RIF_ALL.* FROM MOTIVAZIONI_ANN_RIF_ALL where id=" & motivazione.Value
            '    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    If lettore.Read() Then
            '        If par.IfNull(lettore("fl_rilevante"), -1) = 1 Then


            '            stDiffida = "1"

            '        End If


            '    End If

            'End If





            'If DataGridProposte.Items.Count >= 2 Then

            '    Dim di As DataGridItem
            '    For i = 0 To Me.DataGridProposte.Items.Count - 1
            '        di = Me.DataGridProposte.Items(i)
            '        If i = 0 Then
            '            idAlloggio.Value = di.Cells(0).Text


            '            stDiffida = "1"


            '        End If
            '    Next
            'End If







            par.cmd.CommandText = "SELECT id,ID_ALLOGGIO from rel_prat_all_ccaa_erp where id_pratica=" & idDomanda.Value & " and ultimo='1'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                RELAZIONE = myReader("id")
                ID_ALLOGGIO = myReader("id_ALLOGGIO")
            Else
                RELAZIONE = "1"
            End If
            myReader.Close()

            par.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET DATA='" & Format(Now, "yyyyMMdd") & "',ESITO='3',ID_MOTIVAZIONE=" & motivazione.Value & ", FL_DIFFIDA2=" & stDiffida & " WHERE ID =" & RELAZIONE
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE ALLOGGI SET STATO=5,PRENOTATO='0',ID_PRATICA=null,ASSEGNATO='0' WHERE ID=" & ID_ALLOGGIO
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                & "3,5," _
                                & ID_ALLOGGIO & "," _
                                & idDomanda.Value & ",'" & "" & "')"
            par.cmd.ExecuteNonQuery()

            If idDomanda.Value > 8000000 Then
                par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET FL_PROPOSTA='0' WHERE ID=" & idDomanda.Value
            Else
                If idDomanda.Value < 500000 Then
                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET FL_PROPOSTA='0' WHERE ID=" & idDomanda.Value
                Else
                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET FL_PROPOSTA='0' WHERE ID=" & idDomanda.Value
                End If
            End If

            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                & "VALUES (" & idDomanda.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                & "','F64','','I')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE DOMANDE_OFFERTE_SCAD SET FL_VALIDA='0' WHERE ID_DOMANDA=" & idDomanda.Value & " AND ID=" & numOfferta.Value
            par.cmd.ExecuteNonQuery()

            par.myTrans.Commit()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write("<script>alert('Operazione effettuata con successo!')</script>")
            CaricaAlloggiProposti()
            ControllaDiffida2()
            proposti.Value = "0"
            assegnati.Value = "0"
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnNuovaProp_Click(sender As Object, e As System.EventArgs) Handles btnNuovaProp.Click
        'Response.Write("<script>window.showModalDialog('RicercaUIDaAbbinare.aspx?TIPO=' " & tipoDomanda.Value & " '&ID=" & idDomanda.Value & "', window, 'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');</script>")
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)





            If frmModify.Value = 1 Then

                par.cmd.Dispose()
                par.OracleConn.Close()
                Exit Sub

            End If




            Select Case tipoDomanda.Value
                Case "1"


                    par.cmd.CommandText = " SELECT TO_CHAR (TO_DATE (domande_offerte_scad.data_scadenza, 'YYYYmmdd'),'DD/MM/YYYY') AS SCADENZA, " _
                           & " domande_bando.ID,comp_nucleo.cognome, comp_nucleo.nome, domande_bando.pg AS PG, domande_offerte_scad.ID AS OFFERTA " _
                           & " FROM domande_offerte_scad, domande_bando, comp_nucleo, dichiarazioni " _
                           & " WHERE domande_bando.fl_ass_esterna = '1' " _
                           & " And domande_bando.progr_componente = comp_nucleo.progr " _
                           & " And domande_bando.id_dichiarazione = dichiarazioni.ID " _
                           & " And comp_nucleo.id_dichiarazione = dichiarazioni.ID " _
                           & " And domande_bando.ID = domande_offerte_scad.id_domanda " _
                           & " AND domande_bando.fl_invito = '1' " _
                           & " AND domande_bando.id_stato <> '10' " _
                           & " AND domande_bando.fl_pratica_chiusa <> '1' " _
                           & " AND (domande_bando.id_stato = '9') " _
                           & " AND domande_offerte_scad.ID IN (SELECT   MAX (ID) AS ID FROM domande_offerte_scad GROUP BY domande_offerte_scad.id_domanda) " _
                           & " And domande_bando.ID =" & idDomanda.Value & " " _
                           & " ORDER BY comp_nucleo.cognome ASC, comp_nucleo.nome ASC"

                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        numOfferta.Value = myReader("OFFERTA")
                    End If
                    myReader.Close()

                    VisualizzaDomanda()
                    CaricaAlloggiProposti()
                    CaricaAlloggiAssegnati()
                Case "2"


                    par.cmd.CommandText = " SELECT TO_CHAR (TO_DATE (domande_offerte_scad.data_scadenza, 'YYYYmmdd'),'DD/MM/YYYY') AS SCADENZA, " _
                                       & " domande_bando_cambi.ID,comp_nucleo_cambi.cognome, comp_nucleo_cambi.nome, domande_bando_cambi.pg AS PG, domande_offerte_scad.ID AS OFFERTA " _
                                       & " FROM domande_offerte_scad, domande_bando_cambi, comp_nucleo_cambi, dichiarazioni_cambi " _
                                       & " WHERE domande_bando_cambi.fl_ass_esterna = '1' " _
                                       & " And domande_bando_cambi.progr_componente = comp_nucleo_cambi.progr " _
                                       & " And domande_bando_cambi.id_dichiarazione = dichiarazioni_cambi.ID " _
                                       & " And comp_nucleo_cambi.id_dichiarazione = dichiarazioni_cambi.ID " _
                                       & " And domande_bando_cambi.ID = domande_offerte_scad.id_domanda " _
                                       & " AND domande_bando_cambi.fl_invito = '1' " _
                                       & " AND domande_bando_cambi.id_stato <> '10' " _
                                       & " AND domande_bando_cambi.fl_pratica_chiusa <> '1' " _
                                       & " AND (domande_bando_cambi.id_stato = '9') " _
                                       & " AND domande_offerte_scad.ID IN (SELECT   MAX (ID) AS ID FROM domande_offerte_scad GROUP BY domande_offerte_scad.id_domanda) " _
                                       & " And domande_bando_cambi.ID =" & idDomanda.Value & " " _
                                       & " ORDER BY comp_nucleo_cambi.cognome ASC, comp_nucleo_cambi.nome ASC"

                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        numOfferta.Value = myReader("OFFERTA")
                    End If
                    myReader.Close()




                    VisualizzaDomandaCambi()
                    CaricaAlloggiProposti()
                    CaricaAlloggiAssegnati()
                Case "3"


                    par.cmd.CommandText = " SELECT TO_CHAR (TO_DATE (domande_offerte_scad.data_scadenza, 'YYYYmmdd'),'DD/MM/YYYY') AS SCADENZA, " _
                                  & " domande_bando_vsa.ID,comp_nucleo_vsa.cognome, comp_nucleo_vsa.nome, domande_bando_vsa.pg AS PG, domande_offerte_scad.ID AS OFFERTA " _
                                  & " FROM domande_offerte_scad, domande_bando_vsa, comp_nucleo_vsa, dichiarazioni_vsa " _
                                  & " WHERE domande_bando_vsa.fl_ass_esterna = '1' " _
                                  & " And domande_bando_vsa.progr_componente = comp_nucleo_vsa.progr " _
                                  & " And domande_bando_vsa.id_dichiarazione = dichiarazioni_vsa.ID " _
                                  & " And comp_nucleo_vsa.id_dichiarazione = dichiarazioni_vsa.ID " _
                                  & " And domande_bando_vsa.ID = domande_offerte_scad.id_domanda " _
                                  & " AND domande_bando_vsa.fl_invito = '1' " _
                                  & " AND domande_bando_vsa.id_stato <> '10' " _
                                  & " AND domande_bando_vsa.fl_pratica_chiusa <> '1' " _
                                  & " AND (domande_bando_vsa.id_stato = '9') " _
                                  & " AND domande_offerte_scad.ID IN (SELECT   MAX (ID) AS ID FROM domande_offerte_scad GROUP BY domande_offerte_scad.id_domanda) " _
                                  & " And domande_bando_vsa.ID =" & idDomanda.Value & " " _
                                  & " ORDER BY comp_nucleo_vsa.cognome ASC, comp_nucleo_vsa.nome ASC"

                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        numOfferta.Value = myReader("OFFERTA")
                    End If
                    myReader.Close()



                    VisualizzaDomandaEmerg()
                    CaricaAlloggiProposti()
                    CaricaAlloggiAssegnati()
            End Select

            '  Response.Write("<script>opener.location.href('RisultatoRicOfferta.aspx?PR=1&T=" & tipoDomanda.Value & "');</script>")

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try


    End Sub

    Protected Sub btnAccetta_Click(sender As Object, e As System.EventArgs) Handles btnAccetta.Click
        Try
            Dim scriptblock As String
            Dim CODICE As String = ""
            Dim PROPRIETA As String = ""
            Dim RELAZIONE As String = ""
            Dim errore As Boolean = False

            If confermaAnnullo1.Value = "1" Then


                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans


                par.cmd.CommandText = "SELECT * FROM ALLOGGI WHERE ID=" & idAlloggio.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    CODICE = par.IfNull(myReader("COD_ALLOGGIO"), "")
                    PROPRIETA = par.IfNull(myReader("PROPRIETA"), "")
                    dataDisp = par.IfNull(myReader("DATA_DISPONIBILITA"), "")

                    par.cmd.CommandText = "SELECT id from rel_prat_all_ccaa_erp where id_pratica=" & idDomanda.Value & " and ultimo='1'"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        RELAZIONE = par.IfNull(myReader1("ID"), "-1")
                    End If
                    myReader1.Close()


                    If par.IfNull(myReader("STATO"), "") = "7" And par.IfNull(myReader("ID_PRATICA"), -1) = idDomanda.Value Then

                        par.cmd.CommandText = "UPDATE DOMANDE_OFFERTE_SCAD SET FL_VALIDA='0' WHERE ID=" & numOfferta.Value
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "UPDATE ALLOGGI SET STATO=8,PRENOTATO='1',ID_PRATICA=" & idDomanda.Value & ",ASSEGNATO='1',DATA_PRENOTATO='" & Format(Now, "yyyyMMdd") & "',DATA_RESO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & idAlloggio.Value
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET DATA='" & Format(Now, "yyyyMMdd") & "',ESITO='1' WHERE Id = " & RELAZIONE
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                   & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                   & "1,8," _
                                   & idAlloggio.Value & "," _
                                   & idDomanda.Value & ",'')"

                        par.cmd.ExecuteNonQuery()

                        Select Case tipoDomanda.Value
                            Case "1"
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET ID_STATO='9',NUM_ALLOGGIO='" & CODICE & "' WHERE ID=" & idDomanda.Value
                            Case "2"
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET ID_STATO='9',NUM_ALLOGGIO='" & CODICE & "' WHERE ID=" & idDomanda.Value
                            Case "3"
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET ID_STATO='9',NUM_ALLOGGIO='" & CODICE & "' WHERE ID=" & idDomanda.Value
                        End Select


                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "SELECT * FROM PRODUZIONE_ALLOGGI WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read = False Then
                            par.cmd.CommandText = "INSERT INTO PRODUZIONE_ALLOGGI (DATA,RESI,DISPONIBILI,PRENOTATI,ASSEGNATI,OCCUPATI,RISERVATI) VALUES ('" & Format(Now, "yyyyMMdd") & "',0,0,0,0,0,0)"
                        End If
                        myReader2.Close()

                        par.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI-1,ASSEGNATI=ASSEGNATI+1 WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                                            & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,20,'" & pgDom & "','" & dataPG & "',10," & idBando & ",10)"
                        par.cmd.ExecuteNonQuery()

                        If cmbDeroga.Visible = True Then

                            If cmbDeroga.SelectedValue = 0 Then
                                Select Case tipoDomanda.Value
                                    Case "1"
                                        par.cmd.CommandText = "update bandi_graduatoria set tipo=1 where id_domanda=" & idDomanda.Value
                                        par.cmd.ExecuteNonQuery()
                                    Case "2"
                                        par.cmd.CommandText = "update bandi_graduatoria_DEF set tipo=1 where id_domanda=" & idDomanda.Value
                                        par.cmd.ExecuteNonQuery()

                                End Select
                            Else
                                Select Case tipoDomanda.Value
                                    Case "1"
                                        par.cmd.CommandText = "update bandi_graduatoria set tipo=0 where id_domanda=" & idDomanda.Value
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "update bandi_graduatoria_def set tipo=0 where id_domanda=" & idDomanda.Value
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                        & "VALUES (" & idDomanda.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                        & "','F162','','I')"
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                            & "VALUES (" & idDomanda.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                                            & "','F13','','I')"
                                        par.cmd.ExecuteNonQuery()
                                    Case "2"
                                        par.cmd.CommandText = "update bandi_graduatoria_CAMBI set tipo=0 where id_domanda=" & idDomanda.Value
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                            & "VALUES (" & idDomanda.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                                            & "','F162','','I')"
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                            & "VALUES (" & idDomanda.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                                            & "','F13','','I')"
                                        par.cmd.ExecuteNonQuery()
                                    Case "3"

                                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                            & "VALUES (" & idDomanda.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                                            & "','F162','','I')"
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                            & "VALUES (" & idDomanda.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                                            & "','F13','','I')"
                                        par.cmd.ExecuteNonQuery()
                                End Select

                                If idDomanda.Value < 500000 Then

                                Else

                                End If

                            End If
                        End If

                        If PROPRIETA = "0" Then
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & CODICE & "'"
                            myReader1 = par.cmd.ExecuteReader()
                            If myReader1.Read = True Then
                                CODICE = par.IfNull(myReader1("ID"), "-1")
                            End If
                            myReader1.Close()

                            If CODICE <> "" Then
                                Dim TIPO As String = ""
                                Select Case tipoDomanda2.Value
                                    Case "1"
                                        TIPO = "E"
                                    Case "2"
                                        TIPO = "C"
                                    Case "3"
                                        TIPO = "Y"
                                End Select

                                par.cmd.CommandText = "Insert into SISCOM_MI.UNITA_ASSEGNATE " _
                                                    & "(ID_DOMANDA, ID_UNITA, DATA_ASSEGNAZIONE, GENERATO_CONTRATTO, ID_DICHIARAZIONE, " _
                                                    & "COGNOME_RS, NOME, CF_PIVA, PROVENIENZA, N_OFFERTA,DATA_DISPONIBILITA) " _
                                                    & "Values " _
                                                    & "(" & idDomanda.Value & ", " & CODICE & ", '" & Format(Now, "yyyyMMdd") & "',0, " & idDichiarazione.Value & " , " _
                                                    & "'" & par.PulisciStrSql(cognome) & "', '" _
                                                    & par.PulisciStrSql(nome) & "', '" _
                                                    & par.PulisciStrSql(codFisc) & "', '" & TIPO & "', " & numOfferta.Value & ",'" & dataDisp & "')"
                                par.cmd.ExecuteNonQuery()
                            End If
                        Else
                            par.cmd.CommandText = "Insert into SISCOM_MI.UNITA_ASSEGNATE " _
                                            & "(ID_DOMANDA, ID_UNITA, DATA_ASSEGNAZIONE, GENERATO_CONTRATTO, ID_DICHIARAZIONE, " _
                                            & "COGNOME_RS, NOME, CF_PIVA, PROVENIENZA, N_OFFERTA,DATA_DISPONIBILITA) " _
                                            & "Values " _
                                            & "(" & idDomanda.Value & ", " & idAlloggio.Value & ", '" & Format(Now, "yyyyMMdd") & "',0, " & idDichiarazione.Value & " , " _
                                            & "'" & par.PulisciStrSql(cognome) & "', '" _
                                            & par.PulisciStrSql(nome) & "', '" _
                                            & par.PulisciStrSql(codFisc) & "', 'G', " & numOfferta.Value & ",'" & dataDisp & "')"
                            par.cmd.ExecuteNonQuery()
                        End If
                    Else
                        errore = True
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('Alloggio non più disponibile!');" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
                        End If
                    End If
                End If

                par.myTrans.Commit()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                If errore = False Then
                    Response.Write("<script>alert('Operazione effettuata con successo!')</script>")
                    CaricaAlloggiProposti()
                    CaricaAlloggiAssegnati()
                    proposti.Value = "0"
                    assegnati.Value = "1"
                End If
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnRifiuta_Click(sender As Object, e As System.EventArgs) Handles btnRifiuta.Click
        Try
            Dim RELAZIONE As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans

            Dim stDiffida As String = "0"

            If motivazione.Value <> "-1" Then

                par.cmd.CommandText = " SELECT MOTIVAZIONI_ANN_RIF_ALL.* FROM MOTIVAZIONI_ANN_RIF_ALL where id=" & motivazione.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettore.Read() Then
                    If par.IfNull(lettore("fl_rilevante"), -1) = 1 Then


                        stDiffida = "1"

                    End If


                End If

            End If



            If DataGridProposte.Items.Count >= 2 Then

                Dim di As DataGridItem
                For i = 0 To Me.DataGridProposte.Items.Count - 1
                    di = Me.DataGridProposte.Items(i)
                    If i = 0 Then
                        idAlloggio.Value = di.Cells(0).Text


                        stDiffida = "1"

                    End If
                Next
            Else
            End If



            par.cmd.CommandText = "SELECT id from rel_prat_all_ccaa_erp where id_pratica=" & idDomanda.Value & " and ultimo='1'"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                RELAZIONE = par.IfNull(myReader1("ID"), "-1")
            End If
            myReader1.Close()

            par.cmd.CommandText = "UPDATE DOMANDE_OFFERTE_SCAD SET FL_VALIDA='0' WHERE ID=" & numOfferta.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE ALLOGGI SET STATO=5,PRENOTATO='0',ID_PRATICA=null,ASSEGNATO='0',DATA_PRENOTATO='' WHERE ID=" & idAlloggio.Value
            par.cmd.ExecuteNonQuery()

            If motivazione.Value = "undefined" Then
                motivazione.Value = ""
            End If
            If RifiutoNote.Value <> "" Then
                RifiutoNote.Value = " Note: " & RifiutoNote.Value
            End If
            par.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET DATA='" & Format(Now, "yyyyMMdd") & "',ESITO='0',ID_MOTIVAZIONE=" & motivazione.Value & ", MOTIVAZIONE='" & par.PulisciStrSql(RifiutoNote.Value) & "', FL_DIFFIDA2=" & stDiffida & " WHERE ID=" & RELAZIONE
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                   & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                   & "0,5," _
                                   & idAlloggio.Value & "," _
                                   & idDomanda.Value & ",'" & par.PulisciStrSql(motivazione.Value) & " " & par.PulisciStrSql(RifiutoNote.Value) & "')"
            par.cmd.ExecuteNonQuery()


            'SCRIVI_MOVIMENTO1:

            par.cmd.CommandText = "SELECT * FROM PRODUZIONE_ALLOGGI WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read = False Then
                par.cmd.CommandText = "INSERT INTO PRODUZIONE_ALLOGGI (DATA,RESI,DISPONIBILI,PRENOTATI,ASSEGNATI,OCCUPATI,RISERVATI) VALUES ('" & Format(Now, "yyyyMMdd") & "',0,0,0,0,0,0)"
                par.cmd.ExecuteNonQuery()
            End If
            myReader.Close()
            par.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI-1,DISPONIBILI=DISPONIBILI+1 WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
            par.cmd.ExecuteNonQuery()

            Select Case tipoDomanda.Value
                Case "1"
                    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                    & "VALUES (" & idDomanda.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                    & "','F16','','I')"
                    par.cmd.ExecuteNonQuery()

                    'If par.PulisciStrSql(cmbStato.SelectedItem.Text) <> "" Then
                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET FL_PROPOSTA='0' WHERE ID=" & idDomanda.Value
                    par.cmd.ExecuteNonQuery()
                    'End If
                Case "2"
                    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                    & "VALUES (" & idDomanda.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                    & "','F16','','I')"
                    par.cmd.ExecuteNonQuery()

                    'If par.PulisciStrSql(cmbStato.SelectedItem.Text) <> "" Then
                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET FL_PROPOSTA='0' WHERE ID=" & idDomanda.Value
                    par.cmd.ExecuteNonQuery()
                    'End If
                Case "3"
                    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                    & "VALUES (" & idDomanda.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                    & "','F16','','I')"
                    par.cmd.ExecuteNonQuery()

                    'If par.PulisciStrSql(cmbStato.SelectedItem.Text) <> "" Then
                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET FL_PROPOSTA='0' WHERE ID=" & idDomanda.Value
                    par.cmd.ExecuteNonQuery()
                    'End If
            End Select

            If idDomanda.Value < 500000 Then

            Else

            End If

            par.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                                & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,18,'" & pgDom & "','" & dataPG & "',10," & idBando & ",10)"
            par.cmd.ExecuteNonQuery()

            par.myTrans.Commit()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write("<script>alert('Operazione effettuata con successo!')</script>")
            CaricaAlloggiProposti()
            ControllaDiffida2()
            proposti.Value = "0"
            assegnati.Value = "0"

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnAnnullaAss_Click(sender As Object, e As System.EventArgs) Handles btnAnnullaAss.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans

            Dim UsiDiversi As Boolean
            Dim relazione As String = ""
            Dim stDiffida As String = "0"
            Dim tabellaRiferimento As String = ""

            If motivazione.Value <> "-1" Then

                par.cmd.CommandText = " SELECT MOTIVAZIONI_ANN_RIF_ALL.* FROM MOTIVAZIONI_ANN_RIF_ALL where id=" & motivazione.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettore.Read() Then
                    If par.IfNull(lettore("fl_rilevante"), -1) = 1 Then
                        stDiffida = "1"
                    End If
                End If
            End If

            If DataGridProposte.Items.Count >= 2 Then
                Dim di As DataGridItem
                For i = 0 To Me.DataGridProposte.Items.Count - 1
                    di = Me.DataGridProposte.Items(i)
                    If i = 0 Then
                        idAlloggio.Value = di.Cells(0).Text
                        stDiffida = "1"
                    End If
                Next
            Else
            End If

            par.cmd.CommandText = "SELECT id from alloggi where id=" & idAlloggio.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                UsiDiversi = False
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT id from siscom_mi.UI_usi_diversi where id=" & idAlloggio.Value
            Dim myReaderx As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderx.Read() Then
                UsiDiversi = True
            End If
            myReaderx.Close()

            par.cmd.CommandText = "SELECT id from rel_prat_all_ccaa_erp where id_pratica=" & idDomanda.Value & " and ultimo='1'"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                relazione = myReader("id")
            Else
                relazione = "1"
            End If
            myReader.Close()

            par.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET DATA='" & Format(Now, "yyyyMMdd") & "',ESITO='4', ID_MOTIVAZIONE=" & motivazione.Value & ",FL_DIFFIDA2=" & stDiffida & " WHERE ID=" & relazione
            par.cmd.ExecuteNonQuery()

            If idDomanda.Value < 500000 Then
                par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET id_stato='9',NUM_ALLOGGIO='',fl_proposta='0' WHERE ID=" & idDomanda.Value
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                    & "VALUES (" & idDomanda.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                    & "','F104','','I')"
                par.cmd.ExecuteNonQuery()
            Else
                If idDomanda.Value > 8000000 Then
                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET id_stato='9',NUM_ALLOGGIO='',fl_proposta='0' WHERE ID=" & idDomanda.Value
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & idDomanda.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                    & "','F104','','I')"
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO_cambi SET id_stato='9',NUM_ALLOGGIO='',fl_proposta='0' WHERE ID=" & idDomanda.Value
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_cambi (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & idDomanda.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                    & "','F104','','I')"
                    par.cmd.ExecuteNonQuery()
                End If
            End If

            If UsiDiversi = False Then
                par.cmd.CommandText = "UPDATE ALLOGGI SET STATO=5,PRENOTATO='0',ID_PRATICA=null,ASSEGNATO='0',data_prenotato='' WHERE ID=" & idAlloggio.Value
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyymmdd") & "'," _
                & "11,5," _
                & idAlloggio.Value & "," _
                & idDomanda.Value & ",'" & par.PulisciStrSql(motivazione.Value) & "')"
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "UPDATE siscom_mi.ui_usi_diversi SET STATO=5,PRENOTATO='0',ID_PRATICA=null,ASSEGNATO='0',data_prenotato='' WHERE ID=" & idAlloggio.Value
                par.cmd.ExecuteNonQuery()
            End If

            par.cmd.CommandText = "delete from siscom_mi.unita_assegnate where id_domanda=" & idDomanda.Value & " and generato_contratto=0 and cf_piva='" & codFisc & "' and id_unita=" & idUnita
            par.cmd.ExecuteNonQuery()


            par.myTrans.Commit()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write("<script>alert('Operazione effettuata con successo!')</script>")
            CaricaAlloggiAssegnati()
            CaricaAlloggiProposti()
            ControllaDiffida2()
            proposti.Value = "0"
            assegnati.Value = "0"

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnConfAss_Click(sender As Object, e As System.EventArgs) Handles btnConfAss.Click
        Try
            If par.AggiustaData(dataProvv.Value) <= Format(Now, "yyyyMMdd") Then

                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "update siscom_MI.unita_assegnate set DATA_INS_PROVV='" & Format(Now, "yyyyMMdd") & "',provvedimento='" & par.PulisciStrSql(numProvv.Value) & "',data_provvedimento='" & par.AggiustaData(dataProvv.Value) & "' where n_offerta=" & numOfferta.Value
                par.cmd.ExecuteNonQuery()

                lblAvviso.Text = "Assegnazione completata! Nel modulo Rapporti Utenza è possibile ora attivare il contratto."

                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Response.Write("<script>alert('Operazione Effettuata!');</script>")
            Else
                Response.Write("<script>alert('Attenzione, la data del provvedimento deve essere precedente o uguale alla data odierna. Operazione NON Effettuata!');</script>")
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub DataGridProposte_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGridProposte.EditCommand
        Try
            Dim tabellaRiferimento As String = ""
            Dim selezionato As Integer = 0
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt As New Data.DataTable
            Dim scriptblock As String = ""

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            frmModify.Value = 0

            Dim di1 As DataGridItem

            For i As Integer = 0 To Me.DataGridProposte.Items.Count - 1
                di1 = Me.DataGridProposte.Items(i)

                If CType(di1.Cells(12).FindControl("ChDiffida2"), CheckBox).Checked = True Then
                    selezionato = 1
                End If
            Next

            par.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET FL_DIFFIDA2=" & selezionato & " WHERE ID_PRATICA=" & idDomanda.Value
            par.cmd.ExecuteNonQuery()

            CaricaAlloggiProposti()
            ' CaricaDocumenti()

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Private Sub ControllaDiffida2()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT * from rel_prat_all_ccaa_erp where id_pratica=" & idDomanda.Value & " and ultimo='1'"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                ' RELAZIONE = par.IfNull(myReader1("ID"), "-1")

                If par.IfNull(myReader1("FL_DIFFIDA2"), "-1") = 1 Then

                    Select Case tipoDomanda.Value

                        Case "1"
                            par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET id_stato='4' WHERE ID=" & idDomanda.Value
                            par.cmd.ExecuteNonQuery()

                        Case "2"
                            par.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET id_stato='4' WHERE ID=" & idDomanda.Value
                            par.cmd.ExecuteNonQuery()

                        Case "3"
                            par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET id_stato='4' WHERE ID=" & idDomanda.Value
                            par.cmd.ExecuteNonQuery()

                    End Select

                    par.cmd.CommandText = "INSERT INTO PROPOSTE_REVOCHE (ID,ID_PRATICA,DATA_PROPOSTA,MOTIVAZIONE,TIPO_DOMANDA) " _
                                    & "VALUES (SEQ_PR_REVOCHE.NEXTVAL," & idDomanda.Value & ",'" & Format(Now, "yyyyMMdd") & "', '" & par.PulisciStrSql(motivazione.Value) & "', " & tipoDomanda.Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            End If
            myReader1.Close()

            par.myTrans.Commit()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            VerificaInfoProvv()

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Function ChkZerUno(ByVal chk As CheckBox) As Integer
        ChkZerUno = 0

        If chk.Checked = True Then
            ChkZerUno = 1
        End If

        Return ChkZerUno
    End Function

    Protected Sub btnRevoca_Click(sender As Object, e As System.EventArgs) Handles btnRevoca.Click
        Try
            Dim RELAZIONE As Long = 0
            Dim ID_ALLOGGIO As Long = 0

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT id,ID_ALLOGGIO from rel_prat_all_ccaa_erp where id_pratica=" & idDomanda.Value & " and ultimo='1'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                RELAZIONE = myReader("id")
                ID_ALLOGGIO = myReader("id_ALLOGGIO")
            Else
                RELAZIONE = "1"
            End If
            myReader.Close()

            par.cmd.CommandText = "UPDATE ALLOGGI SET STATO=5,PRENOTATO='0',ID_PRATICA=null,ASSEGNATO='0' WHERE ID=" & ID_ALLOGGIO
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET DATA='" & Format(Now, "yyyyMMdd") & "',ESITO='4',ID_MOTIVAZIONE=" & motivazione.Value & ", FL_DIFFIDA2=1 WHERE ID =" & RELAZIONE
            par.cmd.ExecuteNonQuery()

            Select Case tipoDomanda.Value
                Case "1"
                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET id_stato='4' WHERE ID=" & idDomanda.Value
                    par.cmd.ExecuteNonQuery()
                Case "2"
                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET id_stato='4' WHERE ID=" & idDomanda.Value
                    par.cmd.ExecuteNonQuery()
                Case "3"
                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET id_stato='4' WHERE ID=" & idDomanda.Value
                    par.cmd.ExecuteNonQuery()
            End Select

            par.cmd.CommandText = "INSERT INTO PROPOSTE_REVOCHE (ID,ID_PRATICA,DATA_PROPOSTA,MOTIVAZIONE,TIPO_DOMANDA) " _
                                    & "VALUES (SEQ_PR_REVOCHE.NEXTVAL," & idDomanda.Value & ",'" & par.AggiustaData(dataRevoca.Value) & "', '" & par.PulisciStrSql(motivazione.Value) & "', " & tipoDomanda.Value & ")"
            par.cmd.ExecuteNonQuery()

            Response.Write("<script>alert('Operazione effettuata con successo!')</script>")
            proposti.Value = "0"
            assegnati.Value = "0"


            par.myTrans.Commit()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            CaricaAlloggiProposti()
            VerificaInfoProvv()

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub controllaDocDiffide()

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            diffida2 = 0
            diffida.Value = 0

            par.cmd.CommandText = "SELECT esito, fl_diffida2 from rel_prat_all_ccaa_erp where id_pratica=" & idDomanda.Value & "order by ultimo desc"

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt As New Data.DataTable
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            da.Fill(dt)
            da.Dispose()
            Dim i As Integer = 0
            If dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    If par.IfNull(row.Item("fl_diffida2"), -1) = 1 Then

                        diffida2 = 1

                        Exit Sub
                    End If

                    If (par.IfNull(row.Item("esito"), -1) = 0 Or par.IfNull(row.Item("esito"), -1) = 3 Or par.IfNull(row.Item("esito"), -1) = 4) And diffida2 = 0 Then

                        diffida.Value = 1

                    End If
                Next
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub btnAggiornaPagina_Click(sender As Object, e As ImageClickEventArgs) Handles btnAggiornaPagina.Click
        ControllaSeEsisteDichAgg()

        Select Case tipoDomanda.Value
            Case "1"
                VisualizzaDomanda()
                CaricaAlloggiProposti()
                CaricaAlloggiAssegnati()
            Case "2"
                VisualizzaDomandaCambi()
                CaricaAlloggiProposti()
                CaricaAlloggiAssegnati()
            Case "3"
                VisualizzaDomandaEmerg()
                CaricaAlloggiProposti()
                CaricaAlloggiAssegnati()
        End Select
        VerificaInfoProvv()
    End Sub
End Class
