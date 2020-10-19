Imports System.IO
Imports SubSystems.RP
Imports System.Drawing
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports ExpertPdf.HtmlToPdf

Partial Class ANAUT_EmissioneConvocazioni1
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public percentuale As Long = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then
                Cerca()
                CaricaSportelli()
            End If
            txtDataStampa.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    'Private Sub MessJQuery(ByVal Messaggio As String, ByVal Tipo As Integer, Optional ByVal Titolo As String = "Messaggio")
    '    Try
    '        Dim sc As String = ""
    '        If Tipo = 0 Then
    '            sc = ScriptErrori(Messaggio, Titolo)
    '        Else
    '            sc = ScriptChiudi(Messaggio, Titolo)
    '        End If
    '        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "ScriptMsg", sc, True)
    '    Catch ex As Exception
    '        lblErrore.Text = ex.Message
    '        lblErrore.Visible = True
    '    End Try
    'End Sub
    'Private Function ScriptErrori(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
    '    Try
    '        Dim retvalue As String = ""
    '        Dim sb As New StringBuilder
    '        sb.Append("$(document).ready(function(){")
    '        sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
    '        sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');}}});")
    '        sb.Append("});")
    '        retvalue = sb.ToString()
    '        Return retvalue
    '    Catch ex As Exception
    '        Return ""
    '    End Try
    'End Function
    'Private Function ScriptChiudi(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
    '    Try
    '        Dim retvalue As String = ""
    '        Dim sb As New StringBuilder
    '        sb.Append("$(document).ready(function(){")
    '        sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
    '        sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');self.close();}}});")
    '        sb.Append("});")
    '        retvalue = sb.ToString()
    '        Return retvalue
    '    Catch ex As Exception
    '        Return ""
    '    End Try
    'End Function

    'Public Sub SceltaJQuery(ByVal Messaggio As String, ByVal Funzione As String, Optional ByVal Titolo As String = "Messaggio", Optional ByVal Funzione2 As String = "")
    '    Try
    '        Dim sc As String = ScriptScelta(Messaggio, Funzione, Titolo, Funzione2)
    '        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "ScriptScelta", sc, True)
    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub

    'Private Function ScriptScelta(ByVal Messaggio As String, ByVal Funzione As String, Optional ByVal Titolo As String = "Messaggio", Optional ByVal Funzione2 As String = "") As String
    '    Try
    '        Dim retvalue As String = ""
    '        Dim sb As New StringBuilder
    '        sb.Append("$(document).ready(function(){")
    '        sb.Append("$('#ScriptScelta').text('" & Messaggio & "');")
    '        sb.Append("$('#ScriptScelta').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "', buttons: {'Si': function() { __doPostBack('" & Funzione & "', '');{$(this).dialog('close');} },'No': function() {$(this).dialog('close');" & Funzione2 & "}}});")
    '        sb.Append("});")
    '        retvalue = sb.ToString()
    '        Return retvalue
    '    Catch ex As Exception
    '        Return ""
    '    End Try
    'End Function

    Private Sub Cerca()
        sStringaSQL1 = "select utenza_bandi.descrizione as DESCR_BANDO,UTENZA_TIPO_DOC.*,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''VisModello.aspx?ID='||UTENZA_TIPO_DOC.ID||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||'Visualizza'||'</a>','$','&'),'£','" & Chr(34) & "') as MODELLO1,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''TestModello.aspx?ID='||UTENZA_TIPO_DOC.ID||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||'TEST'||'</a>','$','&'),'£','" & Chr(34) & "') as TEST FROM UTENZA_TIPO_DOC,UTENZA_BANDI WHERE UTENZA_BANDI.ID=UTENZA_TIPO_DOC.ID_BANDO order by UTENZA_TIPO_DOC.DESCRIZIONE ASC,ID_BANDO desc"
        BindGrid()
    End Sub

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Private Sub BindGrid()
        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "UTENZA_BANDI")
            Label4.Text = DataGrid1.Items.Count
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            Label4.Text = " - " & DataGrid1.Items.Count & " nella lista"

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Silver'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato: " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")


        End If
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click

        Session.Timeout = 120
        lblErrore.Visible = False
        Dim j As Integer = 0
        Dim Trovato1 As Boolean = False
        Dim sSportelli As String = "("
        Dim zipfic As String = ""
        Dim ElencoFile() As String
        Dim ElencoPostaler() As String
        Dim NumeroLettere As Long = 1000000
        Dim ContatorePagine As Long = 0

        Dim Contatore As Long = 0
        Dim NUMERORIGHE As Long = 0

        Dim Str As String = ""
        Dim ZIPFile As String = ""

        Dim numeroconvocazioni As Long = 0

        nlettere.Value = "0"

        'postaler
        Dim sPosteAler As String = ""
        Dim sPosteAlerNominativo As String = ""
        Dim sPosteAlerInd As String = ""
        Dim sPosteAlerScala As String = ""
        Dim sPosteAlerInterno As String = ""
        Dim sPosteAlerCAP As String = ""
        Dim sPosteAlerLocalita As String = ""
        Dim sPosteAlerProv As String = ""
        Dim sPosteAlerCodUtente As String = ""
        Dim sPosteAlerAcronimo As String = ""
        Dim sPosteDefault As String = ""
        Dim sPosteAlerIA As String = ""
        Dim NomeFilePosteAler As String = ""
        Dim sPosteIndirizzoPostale As String = ""
        Dim TestoPostAler As String = ""
        Dim bloccoStampa As Integer = 0
        Dim Identificativo As String = ""
        Dim BASE_FILE As String = ""

        For j = 0 To ListaVoci.Items.Count - 1
            If ListaVoci.Items(j).Selected = True Then
                Trovato1 = True
                sSportelli = sSportelli & ListaVoci.Items(j).Value & ","
            End If
        Next
        sSportelli = Mid(sSportelli, 1, Len(sSportelli) - 1) & ")"

        If Val(txtNumPagine.Text) > 0 Then
            NumeroLettere = txtNumPagine.Text
        End If

        If H1.Value = "1" And txtDataStampa.Text <> "" And txtNumPG.Text <> "" And Trovato1 <> False Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                Dim MIADATA As String = Format(Now, "yyyyMMddHHmm")
                Dim BaseFile As String = Format(CLng(Request.QueryString("ID")), "000000") & "_" & Format(Now, "yyyyMMddHHmmss")
                Dim file1 As String = BaseFile & ".RTF"
                Dim fileName As String = Server.MapPath("..\FileTemp\") & file1
                Dim contenuto As String = ""
                Dim rp As New Rpn
                Dim i As Boolean
                Dim K As Integer = 0

                'Dim result As Integer = Rpn.RpsSetLicenseInfo("G927S-F6R7A-7VH31", "srab35887-1", "S&S SISTEMI E SOLUZIONI S.R.L.")
                Dim result As Int64 = Rpn.RpsSetLicenseInfo("8RWQS-6Y9UC-HA2L1-91017", "srab35887-1", "S&S SISTEMI E SOLUZIONI S.R.L.")
                rp.InWebServer = True
                rp.EmbedFonts = True
                rp.ExactTextPlacement = True


                Dim trovato As Boolean = False

                Dim pdfDocumentOptions As New ExpertPdf.MergePdf.PdfDocumentOptions()
                pdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
                pdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
                Dim pdfMerge As New ExpertPdf.MergePdf.PDFMerge(pdfDocumentOptions)
                Dim Licenza As String = Session.Item("LicenzaPdfMerge")
                If Licenza <> "" Then
                    pdfMerge.LicenseKey = Licenza
                End If

                par.cmd.CommandText = "SELECT * FROM UTENZA_TIPO_DOC WHERE id=" & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Dim bw As BinaryWriter
                    If par.IfNull(myReader("MODELLO"), "").LENGTH > 0 Then
                        Dim fs As New FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write)
                        bw = New BinaryWriter(fs)
                        bw.Write(myReader("MODELLO"))
                        bw.Flush()
                        bw.Close()
                        trovato = True
                    End If
                End If
                myReader.Close()

                Dim contenutoOriginale As String = ""
                If trovato = True Then
                    Dim sr1 As StreamReader = New StreamReader(fileName, System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenutoOriginale = sr1.ReadToEnd()
                    sr1.Close()
                End If

                Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
                Str = Str & "font:verdana; font-size:10px;'><br><img src='../Contratti/Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso...Attendere il messaggio di fine operazione</br>Lettere create: <input id=" & Chr(34) & "Text1" & Chr(34) & " readonly=" & Chr(34) & "readonly" & Chr(34) & " type=" & Chr(34) & "text" & Chr(34) & " style=" & Chr(34) & "border: 1px solid #FFFFFF; font-family: arial, Helvetica, sans-serif; font-size: 10pt; font-weight: bold; text-align: center; width: 50px;" & Chr(34) & "/></div><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo='';function Mostra() {document.getElementById('Text1').value = tempo;}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"

                Response.Write(Str)
                Response.Flush()

NUOVO_GIRO:
                Identificativo = Format(Now, "yyyyMMddHHmmss")


                bloccoStampa = bloccoStampa + 1
                ContatorePagine = 0
                If trovato = True Then

                    par.cmd.CommandText = "SELECT count(*) FROM UTENZA_FILIALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.TAB_FILIALI,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.CONVOCAZIONI_AU,UTENZA_SPORTELLI,SISCOM_MI.INDIRIZZI WHERE siscom_mi.convocazioni_au.DATA_APP IS NOT NULL AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=CONVOCAZIONI_AU.ID_CONTRATTO AND TAB_FILIALI.ID = UTENZA_FILIALI.ID_STRUTTURA AND UTENZA_FILIALI.ID=UTENZA_SPORTELLI.id_filiale AND UTENZA_SPORTELLI.ID=CONVOCAZIONI_AU.ID_SPORTELLO AND RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO AND CONVOCAZIONI_AU.ID_CONTRATTO IS NOT NULL AND ID_GRUPPO = " & Request.QueryString("ID") & " and ID_SPORTELLO IN " & sSportelli & " and convocazioni_au.id not in (select id_convocazione from siscom_mi.convocazioni_au_lettere where ID_GRUPPO = " & Request.QueryString("ID") & " and ID_SPORTELLO IN " & sSportelli & ")"
                    Dim myReaderY As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderY.Read Then
                        NUMERORIGHE = par.IfNull(myReaderY(0), 0)
                    End If
                    myReaderY.Close()

                    If NUMERORIGHE = 0 Then
                        'bisogna trovare una soluzione, in caso di stampa per singoli sportelli, la simulazione non sarà più visibile e non si potrà procedere
                        'par.cmd.CommandText = "UPDATE SISCOM_MI.CONVOCAZIONI_AU_GRUPPI SET FL_STAMPATA=1 WHERE ID=" & Request.QueryString("ID")
                        'par.cmd.ExecuteNonQuery()
                        Response.Write("<script>alert('Operazione effettuata!');</script>")
                        par.myTrans.Commit()
                        par.OracleConn.Close()
                        par.cmd.Dispose()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Str = "<script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">document.getElementById('dvvvPre').style.visibility = 'hidden';document.location.href = 'pagina_home.aspx';</script>"
                        Response.Write(Str)
                        Response.Flush()
                        Exit Sub
                    End If

                    par.cmd.CommandText = "SELECT ANAGRAFICA.ID AS IDA,(SELECT REPLACE(UPPER(DESCRIZIONE),'FUORI TERRA','') FROM SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD=UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO) AS PIANO,(SELECT DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID=UNITA_IMMOBILIARI.ID_SCALA) AS SCALA, UNITA_IMMOBILIARI.INTERNO,INDIRIZZI.DESCRIZIONE AS UN_INDIRIZZO,INDIRIZZI.CIVICO AS UN_CIVICO,INDIRIZZI.CAP AS UN_CAP,INDIRIZZI.LOCALITA AS UN_LOCALITA,UTENZA_SPORTELLI.DESCRIZIONE AS DESCR_SPORTELLO,UTENZA_SPORTELLI.INDIRIZZO AS INDIRIZZO_SPORTELLO,UTENZA_SPORTELLI.CIVICO AS CIVICO_SPORTELLO,UTENZA_SPORTELLI.CAP AS CAP_SPORTELLO,(SELECT NOME FROM COMUNI_NAZIONI WHERE ID=UTENZA_SPORTELLI.ID_COMUNE) AS CITTA_SPORTELLO,TAB_FILIALI.RESPONSABILE,tab_filiali.acronimo,UTENZA_SPORTELLI.N_VERDE,UTENZA_SPORTELLI.N_TELEFONO,CONVOCAZIONI_AU.*,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.PRESSO_COR,RAPPORTI_UTENZA.VIA_COR,RAPPORTI_UTENZA.CIVICO_COR,RAPPORTI_UTENZA.LUOGO_COR,RAPPORTI_UTENZA.CAP_COR,RAPPORTI_UTENZA.SIGLA_COR,rapporti_utenza.tipo_cor FROM UTENZA_FILIALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.TAB_FILIALI,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.CONVOCAZIONI_AU,UTENZA_SPORTELLI,SISCOM_MI.INDIRIZZI WHERE siscom_mi.convocazioni_au.DATA_APP IS NOT NULL AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=CONVOCAZIONI_AU.ID_CONTRATTO AND TAB_FILIALI.ID = UTENZA_FILIALI.ID_STRUTTURA AND UTENZA_FILIALI.ID=UTENZA_SPORTELLI.id_filiale AND UTENZA_SPORTELLI.ID=CONVOCAZIONI_AU.ID_SPORTELLO AND RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO AND CONVOCAZIONI_AU.ID_CONTRATTO IS NOT NULL AND ID_GRUPPO = " & Request.QueryString("ID") & " and ID_SPORTELLO IN " & sSportelli & " and convocazioni_au.id not in (select id_convocazione from siscom_mi.convocazioni_au_lettere where ID_GRUPPO = " & Request.QueryString("ID") & " and ID_SPORTELLO IN " & sSportelli & ")"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader1.Read
                        If ContatorePagine < NumeroLettere Then
                            'postaler
                            sPosteAler = ""
                            sPosteAlerNominativo = ""
                            sPosteAlerInd = ""
                            sPosteAlerScala = ""
                            sPosteAlerInterno = ""
                            sPosteAlerCAP = ""
                            sPosteAlerLocalita = ""
                            sPosteAlerProv = ""
                            sPosteAlerCodUtente = ""
                            sPosteAlerAcronimo = ""
                            sPosteDefault = ""
                            sPosteAlerIA = ""
                            sPosteIndirizzoPostale = ""

                            par.cmd.CommandText = "select SISCOM_MI.SEQ_POSTALER.NEXTVAL FROM dual"
                            Dim myReaderAler As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderAler.Read Then
                                sPosteAlerIA = CStr(par.IfNull(myReaderAler(0), "-1")).PadRight(16)
                            End If
                            myReaderAler.Close()

                            sPosteIndirizzoPostale = Trim(Mid(Replace(UCase(par.IfNull(myReader1("PRESSO_COR"), "")), "C/O", ""), 1, 50)).PadRight(50)
                            sPosteDefault = "                                                  "
                            sPosteAlerInd = CStr(par.IfNull(myReader1("TIPO_COR"), "") & " " & par.IfNull(myReader1("VIA_COR"), "") & " " & par.IfNull(myReader1("CIVICO_COR"), "")).PadRight(50).Substring(0, 50)
                            sPosteAlerCodUtente = Format(par.IfNull(myReader1("ida"), ""), "000000000000").PadRight(12)
                            sPosteAlerInterno = CStr(Mid(par.IfNull(myReader1("INTERNO"), ""), 1, 3)).PadRight(3)
                            sPosteAlerScala = Replace(Mid(par.IfNull(myReader1("SCALA"), "00"), 1, 2), "00", "", 1, 2).PadRight(2)
                            sPosteAlerAcronimo = CStr(par.IfNull(myReader1("ACRONIMO"), "")).PadRight(4)
                            sPosteAlerCAP = CStr(par.IfNull(myReader1("CAP_COR"), "")).PadRight(5)
                            sPosteAlerLocalita = CStr(Mid(par.IfNull(myReader1("LUOGO_COR"), ""), 1, 50)).PadRight(50)
                            sPosteAlerProv = CStr(par.IfNull(myReader1("SIGLA_COR"), "")).PadRight(2)

                            sPosteAler = sPosteIndirizzoPostale & ";" _
                                       & sPosteDefault & ";" _
                                       & sPosteAlerInd & ";" _
                                       & sPosteDefault & ";" _
                                       & sPosteDefault & ";" _
                                       & sPosteAlerScala & ";" _
                                       & sPosteAlerInterno & ";" _
                                       & sPosteAlerCAP & ";" _
                                       & sPosteAlerLocalita & ";" _
                                       & sPosteAlerProv & ";" _
                                       & sPosteAlerCodUtente & ";" _
                                       & sPosteAlerAcronimo & ";" _
                                       & sPosteAlerIA & ";"

                            par.cmd.CommandText = "Insert into SISCOM_MI.CONVOCAZIONI_AU_LETTERE (ID_CONVOCAZIONE, COD_CONTRATTO, DATA_GENERAZIONE, INDIRIZZO_1, INDIRIZZO_2, INDIRIZZO_3, INDIRIZZO_4, " _
                                                & "DATI_FILIALE, RESPONSABILE, DATA_APP, ORE_APP, ID, CDS, N_VERDE_FILIALE, INDIRIZZO_UN_1, INDIRIZZO_UN_2, INDIRIZZO_UN_3, ID_GRUPPO, ID_FILIALE,ID_SPORTELLO,DATA_STAMPA,POSTALER,IDENTIFICATIVO) " _
                                                & "Values (" _
                                                & myReader1("ID") & ", '" & myReader1("COD_CONTRATTO") & "', '" & Format(Now, "yyyyMMdd") & "', '" & par.PulisciStrSql(par.IfNull(myReader1("PRESSO_COR"), "")) _
                                                & "','" & par.PulisciStrSql(par.IfNull(myReader1("TIPO_COR"), "")) & " " & par.PulisciStrSql(par.IfNull(myReader1("VIA_COR"), "")) & " " _
                                                & par.PulisciStrSql(par.IfNull(myReader1("CIVICO_COR"), "")) & "', '" & par.PulisciStrSql(par.IfNull(myReader1("CAP_COR"), "")) & " " _
                                                & par.PulisciStrSql(par.IfNull(myReader1("LUOGO_COR"), "")) & " " & par.PulisciStrSql(par.IfNull(myReader1("SIGLA_COR"), "")) _
                                                & "', '', '" & par.PulisciStrSql(par.IfNull(myReader1("DESCR_SPORTELLO"), "") & " - " & par.IfNull(myReader1("INDIRIZZO_SPORTELLO"), "") & " " & par.IfNull(myReader1("CIVICO_SPORTELLO"), "") & " - " & par.IfNull(myReader1("CAP_SPORTELLO"), "") & " " & par.IfNull(myReader1("CITTA_SPORTELLO"), "")) _
                                                & "', '" & par.PulisciStrSql(par.IfNull(myReader1("RESPONSABILE"), "")) & "','" & par.FormattaData(par.IfNull(myReader1("data_app"), "")) _
                                                & "', '" & par.IfNull(myReader1("ORE_APP"), "") & "', SISCOM_MI.SEQ_CONVOCAZIONI_LETTERE.NEXTVAL, 'GL0000/" & par.PulisciStrSql(par.IfNull(myReader1("ACRONIMO"), "")) & "/" & par.PulisciStrSql(txtNumPG.Text) & "', '" & par.PulisciStrSql(par.IfNull(myReader1("N_VERDE"), "")) _
                                                & "','" & par.PulisciStrSql(par.IfNull(myReader1("UN_INDIRIZZO"), "") & " " & par.IfNull(myReader1("UN_CIVICO"), "")) & "', 'INTERNO " & par.PulisciStrSql(par.IfNull(myReader1("INTERNO"), "")) & " SCALA " & par.PulisciStrSql(par.IfNull(myReader1("SCALA"), "")) & " PIANO " _
                                                & par.PulisciStrSql(par.IfNull(myReader1("PIANO"), "")) & "', '" & par.PulisciStrSql(par.IfNull(myReader1("UN_CAP"), "") & " " & par.IfNull(myReader1("UN_LOCALITA"), "")) & "', " & par.IfNull(myReader1("ID_GRUPPO"), "") & ", " & par.IfNull(myReader1("ID_FILIALE"), "") & "," & par.IfNull(myReader1("ID_SPORTELLO"), "") & ",'" & txtDataStampa.Text & "','" & par.PulisciStrSql(sPosteAler) & "','" & Identificativo & "')"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "select SISCOM_MI.SEQ_CONVOCAZIONI_LETTERE.CURRVAL FROM dual"
                            myReaderAler = par.cmd.ExecuteReader()
                            If myReaderAler.Read Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.POSTALER (ID,ID_LETTERA,ID_TIPO_LETTERA) VALUES (" & sPosteAlerIA & "," & myReaderAler(0) & ",4)"
                                par.cmd.ExecuteNonQuery()
                            End If
                            myReaderAler.Close()
                        Else
                            Exit Do
                        End If
                        ContatorePagine = ContatorePagine + 1
                        Contatore = Contatore + 1
                    Loop
                    myReader1.Close()
                    Str = ""
                    Contatore = 0
                    NUMERORIGHE = 0
                    Response.Flush()
                    par.cmd.CommandText = "SELECT DISTINCT ID_SPORTELLO FROM SISCOM_MI.CONVOCAZIONI_AU_LETTERE WHERE IDENTIFICATIVO='" & Identificativo & "'"
                    myReader1 = par.cmd.ExecuteReader()
                    Do While myReader1.Read
                        Contatore = 0
                        NUMERORIGHE = 0
                        Response.Write("<script>tempo=0;</script>")
                        Response.Flush()
                        K = 0
                        TestoPostAler = ""
                        ReDim ElencoPostaler(0)
                        ElencoPostaler(0) = ""

                        BASE_FILE = Format(Now, "yyyyMMddHHmmss")
                        par.cmd.CommandText = "SELECT UTENZA_BANDI.DESCRIZIONE AS NOME_BANDO,CONVOCAZIONI_AU.ID_CONTRATTO,UTENZA_BANDI.ID AS ID_BANDO,UTENZA_BANDI.ANNO_ISEE,UTENZA_BANDI.ANNO_AU,CONVOCAZIONI_AU_LETTERE.*,UTENZA_SPORTELLI.DESCRIZIONE AS NOME_SPORTELLO,UTENZA_SPORTELLI.N_TELEFONO,UTENZA_SPORTELLI.N_FAX,UTENZA_SPORTELLI.CAP AS CAP_SPORTELLO,COMUNI_NAZIONI.NOME AS CITTA_SPORTELLO,UTENZA_SPORTELLI.INDIRIZZO AS IND_SPORTELLO,UTENZA_SPORTELLI.CIVICO AS CIVICO_SPORTELLO FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI, UTENZA_BANDI,COMUNI_NAZIONI,UTENZA_SPORTELLI,SISCOM_MI.CONVOCAZIONI_AU_LETTERE,SISCOM_MI.CONVOCAZIONI_AU WHERE convocazioni_au_lettere.IDENTIFICATIVO='" & Identificativo & "' and CONVOCAZIONI_AU.ID=CONVOCAZIONI_AU_LETTERE.ID_CONVOCAZIONE AND CONVOCAZIONI_AU_GRUPPI.ID=CONVOCAZIONI_AU_LETTERE.ID_GRUPPO AND UTENZA_BANDI.ID=CONVOCAZIONI_AU_GRUPPI.ID_AU AND COMUNI_NAZIONI.ID=UTENZA_SPORTELLI.ID_COMUNE AND UTENZA_SPORTELLI.ID=CONVOCAZIONI_AU_LETTERE.ID_SPORTELLO AND CONVOCAZIONI_AU_LETTERE.ID_SPORTELLO=" & myReader1("ID_SPORTELLO") & " AND CONVOCAZIONI_AU_LETTERE.ID_GRUPPO=" & Request.QueryString("ID") & " ORDER BY CONVOCAZIONI_AU_LETTERE.ID ASC"
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader2.Read
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & myReader2("ID_convocazione") & ",'" & MIADATA & "'," & Session.Item("ID_OPERATORE") & ",'STAMPATA CONVOCAZIONE AU')"
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) VALUES (" & myReader2("ID_CONTRATTO") & "," & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'F221', '" & par.PulisciStrSql(myReader2("NOME_BANDO")) & "')"
                            par.cmd.ExecuteNonQuery()
                            contenuto = contenutoOriginale
                            contenuto = Replace(contenuto, "$testoresponsabile$", "Il Responsabile")
                            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))
                            contenuto = Replace(contenuto, "$datastampa$", txtDataStampa.Text)
                            contenuto = Replace(contenuto, "$dataappuntamento$", par.IfNull(myReader2("DATA_APP"), ""))
                            contenuto = Replace(contenuto, "$oreappuntamento$", par.IfNull(myReader2("ORE_APP"), ""))
                            contenuto = Replace(contenuto, "$annoau$", par.IfNull(myReader2("ANNO_AU"), ""))
                            contenuto = Replace(contenuto, "$annoredditi$", par.IfNull(myReader2("ANNO_ISEE"), ""))
                            contenuto = Replace(contenuto, "$documentimancanti$", "")
                            contenuto = Replace(contenuto, "$dichiarante$", "")
                            contenuto = Replace(contenuto, "$datanascitadichiarante$", "")
                            contenuto = Replace(contenuto, "$luogonascitadichiarante$", "")
                            contenuto = Replace(contenuto, "$provincianascitadichiarante$", "")
                            contenuto = Replace(contenuto, "$codcontratto$", par.IfNull(myReader2("COD_CONTRATTO"), ""))
                            contenuto = Replace(contenuto, "$intestatario$", "")
                            contenuto = Replace(contenuto, "$nominativocorr$", Mid(par.IfNull(myReader2("INDIRIZZO_1"), ""), 1, 40))
                            contenuto = Replace(contenuto, "$indirizzocorr$", Mid(par.IfNull(myReader2("INDIRIZZO_2"), ""), 1, 40))
                            contenuto = Replace(contenuto, "$localitacorr$", par.IfNull(myReader2("INDIRIZZO_3"), ""))
                            contenuto = Replace(contenuto, "$internoscalapiano$", Replace(par.IfNull(myReader2("INDIRIZZO_UN_2"), ""), "MEZZANINO", "MEZ."))
                            contenuto = Replace(contenuto, "$internoscalapianocorr$", "")
                            contenuto = Replace(contenuto, "$interno$", "")
                            contenuto = Replace(contenuto, "$scala$", "")
                            contenuto = Replace(contenuto, "$piano$", "")
                            contenuto = Replace(contenuto, "$indirizzounita$", Mid(par.IfNull(myReader2("INDIRIZZO_UN_1"), ""), 1, 23))
                            contenuto = Replace(contenuto, "$localitaunita$", par.IfNull(myReader2("INDIRIZZO_UN_3"), ""))
                            contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader2("NOME_SPORTELLO"), ""))
                            contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader2("IND_SPORTELLO"), "") & ", " & par.IfNull(myReader2("CIVICO_SPORTELLO"), ""))
                            contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader2("CAP_SPORTELLO"), ""))
                            contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader2("CITTA_SPORTELLO"), ""))
                            contenuto = Replace(contenuto, "$telfax$", "Tel:" & par.IfNull(myReader2("N_TELEFONO"), "") & " - Fax:" & par.IfNull(myReader2("N_FAX"), ""))
                            contenuto = Replace(contenuto, "$referente$", "")
                            contenuto = Replace(contenuto, "$operatore$", Session.Item("NOME_OPERATORE"))
                            contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader2("RESPONSABILE"), ""))
                            contenuto = Replace(contenuto, "$numverdefiliale$", par.IfNull(myReader2("N_VERDE_FILIALE"), ""))
                            contenuto = Replace(contenuto, "$filialeappartenenza$", par.IfNull(myReader2("DATI_FILIALE"), ""))
                            contenuto = Replace(contenuto, "$acronimo$", "")
                            contenuto = Replace(contenuto, "$protocollo$", txtNumPG.Text)
                            contenuto = Replace(contenuto, "$cds$", par.IfNull(myReader2("cds"), ""))
                            'TestoPostAler = TestoPostAler & Replace(par.IfNull(myReader2("postaler"), ""), "''", "'") & vbCrLf
                            ReDim Preserve ElencoPostaler(K)
                            ElencoPostaler(K) = Replace(par.IfNull(myReader2("postaler"), ""), "''", "'")

                            ReDim Preserve ElencoFile(K)

                            BaseFile = par.IfNull(myReader2("ID_BANDO"), "") & "_" & par.IfNull(myReader2("ID_CONTRATTO"), "") & "_" & Format(Now, "yyyyMMddHHmmss")
                            file1 = BaseFile & ".RTF"
                            fileName = Server.MapPath("..\FileTemp\") & file1
                            Dim basefilePDF As String = BaseFile & ".pdf"
                            Dim fileNamePDF As String = Server.MapPath("..\FileTemp\") & basefilePDF

                            Dim sr As StreamWriter = New StreamWriter(fileName, False, System.Text.Encoding.Default)
                            sr.WriteLine(contenuto)
                            sr.Close()


                            i = rp.RpsConvertFile(fileName, fileNamePDF)
                            ElencoFile(K) = fileNamePDF
                            K = K + 1

                            zipfic = Server.MapPath("..\ALLEGATI\ANAGRAFE_UTENZA\CONVOCAZIONI\") & Format(myReader1("ID_SPORTELLO"), "000") & "_" & myReader2("NOME_BANDO") & "_" & myReader2("NOME_SPORTELLO") & "_Blocco_" & bloccoStampa & "_" & BASE_FILE & ".pdf"
                            NomeFilePosteAler = Server.MapPath("..\ALLEGATI\ANAGRAFE_UTENZA\CONVOCAZIONI\") & Format(myReader1("ID_SPORTELLO"), "000") & "_" & myReader2("NOME_BANDO") & "_" & myReader2("NOME_SPORTELLO") & "_Blocco_" & bloccoStampa & "_" & Format(Now, "yyyyMMddHHmmss") & ".txt"
                            par.cmd.CommandText = "UPDATE SISCOM_MI.CONVOCAZIONI_AU SET NOME_FILE='" & par.PulisciStrSql(Mid(RicavaFile(zipfic), 1, Len(RicavaFile(zipfic)) - 4) & ".ZIP") & "' WHERE ID=" & myReader2("ID_convocazione")
                            par.cmd.ExecuteNonQuery()
                            Contatore = Contatore + 1
                            numeroconvocazioni = numeroconvocazioni + 1
                            nlettere.Value = numeroconvocazioni
                            Response.Write("<script>tempo=" & numeroconvocazioni & ";</script>")
                            Response.Flush()
                        Loop
                        myReader2.Close()

                        Dim sr2 As StreamWriter = New StreamWriter(NomeFilePosteAler, False, System.Text.Encoding.Default)
                        For j = 0 To K - 1
                            sr2.WriteLine(ElencoPostaler(j))
                        Next
                        sr2.Close()

                        For j = 0 To K - 1
                            pdfMerge.AppendPDFFile(ElencoFile(j))
                        Next
                        pdfMerge.SaveMergedPDFToFile(zipfic)

                        ZIPFile = Mid(zipfic, 1, Len(zipfic) - 4) & ".ZIP"

                        Dim objCrc32 As New Crc32()
                        Dim strmZipOutputStream As ZipOutputStream
                        strmZipOutputStream = New ZipOutputStream(File.Create(ZIPFile))
                        strmZipOutputStream.SetLevel(6)
                        Dim strFile As String


                        strFile = zipfic
                        Dim strmFile As FileStream = File.OpenRead(strFile)
                        Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                        strmFile.Read(abyBuffer, 0, abyBuffer.Length)
                        Dim sFile As String = Path.GetFileName(strFile)
                        Dim theEntry As ZipEntry = New ZipEntry(sFile)
                        Dim fi As New FileInfo(strFile)
                        theEntry.DateTime = fi.LastWriteTime
                        theEntry.Size = strmFile.Length
                        strmFile.Close()
                        objCrc32.Reset()
                        objCrc32.Update(abyBuffer)
                        theEntry.Crc = objCrc32.Value
                        strmZipOutputStream.PutNextEntry(theEntry)
                        strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)

                        strFile = NomeFilePosteAler
                        strmFile = File.OpenRead(strFile)
                        Dim abyBuffer1(Convert.ToInt32(strmFile.Length - 1)) As Byte
                        strmFile.Read(abyBuffer1, 0, abyBuffer1.Length)
                        sFile = Path.GetFileName(strFile)
                        theEntry = New ZipEntry(sFile)
                        fi = New FileInfo(strFile)
                        theEntry.DateTime = fi.LastWriteTime
                        theEntry.Size = strmFile.Length
                        strmFile.Close()
                        objCrc32.Reset()
                        objCrc32.Update(abyBuffer1)
                        theEntry.Crc = objCrc32.Value
                        strmZipOutputStream.PutNextEntry(theEntry)
                        strmZipOutputStream.Write(abyBuffer1, 0, abyBuffer1.Length)

                        strmZipOutputStream.Finish()
                        strmZipOutputStream.Close()
                        File.Delete(zipfic)
                        File.Delete(NomeFilePosteAler)
                        'raggruppare in un unico file per sportello

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_STAMPE (ID_GRUPPO,DESCRIZIONE,DATA_STAMPA,IDENTIFICATIVO) VALUES (" & Request.QueryString("ID") & ",'" & par.PulisciStrSql(RicavaFile(ZIPFile)) & "','','" & Identificativo & "')"
                        par.cmd.ExecuteNonQuery()

                        par.myTrans.Commit()
                        par.myTrans = par.OracleConn.BeginTransaction()
                        ‘‘par.cmd.Transaction = par.myTrans
                    Loop
                    myReader1.Close()
                    GoTo NUOVO_GIRO
                End If
                par.myTrans.Commit()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Str = "<script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">document.getElementById('dvvvPre').style.visibility = 'hidden';document.location.href = 'pagina_home.aspx';</script>"
                Response.Write(Str)
                Response.Flush()

            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Str = "<script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">document.getElementById('dvvvPre').style.visibility = 'hidden';</script>"
                Response.Write(Str)
                Response.Flush()
                Label6.Text = ex.Message
            End Try
        Else
            ' Response.Write("<script>alert('Attenzione...Specificare il modello da utilizzare, il numero di protocollo, la data da apporre sulle lettere e selezionare almeno uno sportello!');</script>")
            lblErrore.Text = "Attenzione...Specificare il modello da utilizzare, il numero di protocollo, la data da apporre sulle lettere e selezionare almeno uno sportello!"
            lblErrore.Visible = True
        End If
    End Sub

    Private Function RicavaFile(ByVal sFile) As String
        Dim N

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function

    Private Function CaricaSportelli()
        Try
            Dim SS As String = ""
            Dim ds As New Data.DataSet()
            Dim dlist As CheckBoxList
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter

            ds.Clear()
            ds.Dispose()
            ListaVoci.Items.Clear()

            SS = "SELECT id,descrizione FROM UTENZA_SPORTELLI where id in (select distinct id_sportello from siscom_mi.convocazioni_au where id_gruppo=" & Request.QueryString("ID") & ")"
            ListaVoci.Items.Clear()
            dlist = ListaVoci
            dlist = ListaVoci

            da = New Oracle.DataAccess.Client.OracleDataAdapter(SS, par.OracleConn)
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



            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(SS, par.OracleConn)
            'Dim ds As New Data.DataSet()
            'da.Fill(ds, "UTENZA_SPORTELLI_PATRIMONIO")
            'DataGrid1.DataSource = ds
            'DataGrid1.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Function
End Class
