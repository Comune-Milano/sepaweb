Imports System.IO
Imports SubSystems.RP
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class ANAUT_ElencoModelliStampa
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then
                idd.Value = Request.QueryString("ID")
                idc.Value = Request.QueryString("COD")
                Cerca()
            End If
            txtProtocollo0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub Cerca()
        Try
            Dim ID_BANDO As Integer = 0
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI where ID =" & Request.QueryString("ID")

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                ID_BANDO = myReader("ID_BANDO")
                If myReader("fl_ausi") = "1" Then
                    imgAlert.Visible = True
                    lblsindacati.Visible = True
                Else
                    imgAlert.Visible = False
                    lblsindacati.Visible = False
                End If
                If par.IfNull(myReader("FL_IN_SERVIZIO"), "0") = "1" Then
                    imgAlert0.Visible = True
                    lblsindacati0.Visible = True
                Else
                    imgAlert0.Visible = False
                    lblsindacati0.Visible = False
                End If
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            sStringaSQL1 = "select utenza_bandi.descrizione as DESCR_BANDO,UTENZA_TIPO_DOC.*,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''VisModello.aspx?ID='||UTENZA_TIPO_DOC.ID||''','''',''height=580,top=0,left=0,width=780'');£>'||'Visualizza'||'</a>','$','&'),'£','" & Chr(34) & "') as MODELLO1 FROM UTENZA_TIPO_DOC,UTENZA_BANDI WHERE UTENZA_BANDI.ID=UTENZA_TIPO_DOC.ID_BANDO AND UTENZA_TIPO_DOC.ID_BANDO=" & ID_BANDO & " order by UTENZA_TIPO_DOC.DESCRIZIONE ASC,ID_BANDO desc"
            BindGrid()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Private Sub BindGrid()
        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "UTENZA_BANDI")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
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

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Silver'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")


        End If
    End Sub

    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try
            Dim indicecontratto As Long = 0
            Dim Ausi As String = ""
            Dim INDICEBANDO As Long = 0


            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim BaseFile As String = "MODELLO_" & Format(Now, "yyyyMMddHHmmss")
            Dim file1 As String = BaseFile & ".RTF"
            Dim fileName As String = Server.MapPath("..\FileTemp\") & file1
            Dim fileNamePDF As String = Server.MapPath("..\FileTemp\") & BaseFile & ".pdf"
            Dim NomeModello As String = ""

            Dim trovato As Boolean = False

            If LBLID.Value <> "" Then
                par.cmd.CommandText = "SELECT * FROM UTENZA_TIPO_DOC WHERE id=" & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    NomeModello = myReader("descrizione")
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

                If trovato = True Then
                    Dim sr1 As StreamReader = New StreamReader(fileName, System.Text.Encoding.GetEncoding("iso-8859-1"))
                    Dim contenuto As String = sr1.ReadToEnd()
                    sr1.Close()

                    Dim TempisticaNonrispondenti As String = ""
                    Dim TempisticaIncompleti As String = ""
                    Dim documentazionemancante As String = ""

                    par.cmd.CommandText = "select valore from parameter where id=119"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        TempisticaNonrispondenti = par.IfNull(myReader("valore"), "0")
                    End If
                    myReader.Close()


                    par.cmd.CommandText = "select valore from parameter where id=118"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        TempisticaIncompleti = par.IfNull(myReader("valore"), "0")
                    End If
                    myReader.Close()

                    contenuto = Replace(contenuto, "$tempisticaincomplete$", TempisticaIncompleti)
                    contenuto = Replace(contenuto, "$tempisticanonrispondenti$", TempisticaNonrispondenti)

                    contenuto = Replace(contenuto, "$protocollo$", txtProtocollo.Text)

                    contenuto = Replace(contenuto, "$testoresponsabile$", "Il Responsabile")


                    contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))
                    contenuto = Replace(contenuto, "$datastampa$", txtProtocollo0.Text)

                    contenuto = Replace(contenuto, "$dataappuntamento$", "")
                    contenuto = Replace(contenuto, "$oreappuntamento$", "")

                    par.cmd.CommandText = "select fl_ausi,anno_au,anno_isee,ID_BANDO from utenza_dichiarazioni,utenza_bandi where utenza_bandi.id=utenza_dichiarazioni.id_bando and utenza_dichiarazioni.id=" & idd.Value
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        If myReader("fl_ausi") = "1" Then
                            Ausi = "1"
                        Else
                            Ausi = "0"
                        End If
                        contenuto = Replace(contenuto, "$annoau$", par.IfNull(myReader("anno_au"), ""))
                        contenuto = Replace(contenuto, "$annoredditi$", par.IfNull(myReader("anno_isee"), ""))
                        INDICEBANDO = par.IfNull(myReader("ID_BANDO"), 0)
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "select * from utenza_doc_mancante where id_dichiarazione=" & idd.Value & " order by descrizione asc"
                    myReader = par.cmd.ExecuteReader()
                    Do While myReader.Read
                        documentazionemancante = documentazionemancante & "-" & par.IfNull(myReader("descrizione"), "") & "\par "
                    Loop
                    myReader.Close()
                    contenuto = Replace(contenuto, "$documentimancanti$", documentazionemancante)


                    par.cmd.CommandText = "select UTENZA_COMP_NUCLEO.*,(select nome from comuni_nazioni where cod=substr(utenza_comp_nucleo.cod_fiscale,12,4)) as luogo,(select sigla from comuni_nazioni where cod=substr(utenza_comp_nucleo.cod_fiscale,12,4)) as provincia from utenza_dichiarazioni,UTENZA_COMP_NUCLEO where UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID AND UTENZA_COMP_NUCLEO.PROGR=0 and utenza_dichiarazioni.id=" & idd.Value
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then

                        contenuto = Replace(contenuto, "$dichiarante$", par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), ""))
                        contenuto = Replace(contenuto, "$datanascitadichiarante$", par.FormattaData(par.IfNull(myReader("data_nascita"), "")))
                        contenuto = Replace(contenuto, "$luogonascitadichiarante$", par.IfNull(myReader("luogo"), ""))
                        contenuto = Replace(contenuto, "$provincianascitadichiarante$", par.IfNull(myReader("provincia"), ""))
                    End If
                    myReader.Close()

                    If idc.Value <> "" And Len(idc.Value) = 19 Then

                        par.cmd.CommandText = "select ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,tipo_cor,presso_cor,via_cor,civico_cor,cap_cor,luogo_cor,sigla_cor,rapporti_utenza.id,rapporti_utenza.cod_contratto from siscom_mi.SOGGETTI_CONTRATTUALI,siscom_mi.ANAGRAFICA,siscom_mi.rapporti_utenza where SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND cod_contratto='" & idc.Value & "'"
                        myReader = par.cmd.ExecuteReader
                        If myReader.Read Then

                            indicecontratto = par.IfNull(myReader("id"), "-1")
                            contenuto = Replace(contenuto, "$codcontratto$", par.IfNull(myReader("cod_contratto"), ""))

                            contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))

                            If Trim(UCase(par.IfNull(myReader("presso_cor"), ""))) = Trim(UCase(par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))) Then
                                contenuto = Replace(contenuto, "$nominativocorr$", par.IfNull(myReader("presso_cor"), ""))
                            Else
                                contenuto = Replace(contenuto, "$nominativocorr$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & " c/o " & par.IfNull(myReader("presso_cor"), ""))
                            End If

                            contenuto = Replace(contenuto, "$indirizzocorr$", Mid(par.IfNull(myReader("tipo_cor"), "") & " " & par.IfNull(myReader("via_cor"), "") & " " & par.IfNull(myReader("civico_cor"), ""), 1, 30))
                            contenuto = Replace(contenuto, "$localitacorr$", par.IfNull(myReader("cap_cor"), "") & " " & par.IfNull(myReader("luogo_cor"), "") & " " & par.IfNull(myReader("sigla_cor"), ""))

                            Dim FF As String = ""
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                            par.cmd.CommandText = "select scale_edifici.descrizione as SCA,unita_immobiliari.*,TIPO_LIVELLO_PIANO.LIVELLO from siscom_mi.UNITA_CONTRATTUALE,siscom_mi.scale_edifici,siscom_mi.unita_immobiliari,SISCOM_MI.TIPO_LIVELLO_PIANO where UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & indicecontratto & " AND unita_immobiliari.id_scala=scale_edifici.id (+) and TIPO_LIVELLO_PIANO.COD=UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO "
                            myReader1 = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                If par.IfNull(myReader1("INTERNO"), "") <> "" Then
                                    FF = FF & "INTERNO " & par.IfNull(myReader1("INTERNO"), "") & " "
                                End If
                                If par.IfNull(myReader1("SCA"), "") <> "" Then
                                    FF = FF & "SCALA " & par.IfNull(myReader1("SCA"), "") & " "
                                End If
                                If par.IfNull(myReader1("LIVELLO"), -100) <> -100 Then
                                    If myReader1("LIVELLO") = CInt(myReader1("LIVELLO")) Then
                                        FF = FF & "PIANO " & Replace(myReader1("LIVELLO"), "0", "T") & " "
                                    Else
                                        FF = FF & "PIANO " & myReader1("LIVELLO") - 0.5 & " "
                                    End If
                                End If
                                contenuto = Replace(contenuto, "$internoscalapiano$", Mid(FF, 1, 30))
                                contenuto = Replace(contenuto, "$internoscalapianocorr$", Mid(FF, 1, 30))
                                contenuto = Replace(contenuto, "$interno$", par.IfNull(myReader1("INTERNO"), ""))
                                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReader1("SCA"), ""))
                                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReader1("livello"), ""))
                            End If
                            myReader1.Close()

                            par.cmd.CommandText = "SELECT INDIRIZZI.*,comuni_nazioni.nome,comuni_nazioni.sigla FROM siscom_mi.UNITA_CONTRATTUALE,siscom_mi.UNITA_IMMOBILIARI,comuni_nazioni,siscom_mi.INDIRIZZI WHERE comuni_nazioni.cod=INDIRIZZI.cod_comune AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & indicecontratto
                            myReader1 = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                contenuto = Replace(contenuto, "$indirizzounita$", par.IfNull(myReader1("descrizione"), "") & " " & par.IfNull(myReader1("civico"), ""))
                                contenuto = Replace(contenuto, "$localitaunita$", par.IfNull(myReader1("cap"), "") & " " & par.IfNull(myReader1("nome"), "") & " " & par.IfNull(myReader1("sigla"), ""))
                            End If
                            myReader1.Close()


                            If Ausi = "0" Then
                                'par.cmd.CommandText = "SELECT TAB_FILIALI.*,INDIRIZZI.descrizione AS descr,INDIRIZZI.civico,INDIRIZZI.cap,INDIRIZZI.localita FROM siscom_mi.INDIRIZZI,siscom_mi.TAB_FILIALI,siscom_mi.COMPLESSI_IMMOBILIARI,siscom_mi.EDIFICI,siscom_mi.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE WHERE INDIRIZZI.ID=TAB_FILIALI.id_indirizzo AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & indicecontratto & " AND EDIFICI.ID=UNITA_IMMOBILIARI.id_edificio AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.id_complesso AND TAB_FILIALI.ID=COMPLESSI_IMMOBILIARI.id_filiale"

                                par.cmd.CommandText = "SELECT ACRONIMO,REF_AMMINISTRATIVO,RESPONSABILE,UTENZA_SPORTELLI.*,(SELECT NOME FROM COMUNI_NAZIONI WHERE ID=UTENZA_SPORTELLI.ID_COMUNE) AS LOCALITA FROM SISCOM_MI.TAB_FILIALI,UTENZA_FILIALI,UTENZA_SPORTELLI WHERE UTENZA_FILIALI.ID=UTENZA_SPORTELLI.ID_FILIALE AND TAB_FILIALI.ID=UTENZA_FILIALI.ID_STRUTTURA AND UTENZA_SPORTELLI.ID IN (SELECT ID_SPORTELLO FROM UTENZA_SPORTELLI_PATRIMONIO WHERE ID_AU=" & INDICEBANDO & " AND ID_UNITA=(SELECT ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_UNITA_PRINCIPALE IS NULL AND ID_CONTRATTO=" & indicecontratto & "))"

                                myReader1 = par.cmd.ExecuteReader()
                                If myReader1.Read Then
                                    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader1("DESCRIZIONE"), ""))
                                    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader1("INDIRIZZO"), "") & " " & par.IfNull(myReader1("civico"), ""))
                                    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader1("cap"), ""))
                                    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader1("localita"), ""))
                                    contenuto = Replace(contenuto, "$telfax$", "Tel:" & par.IfNull(myReader1("n_telefono"), "") & " - Fax:" & par.IfNull(myReader1("n_fax"), ""))
                                    contenuto = Replace(contenuto, "$referente$", par.IfNull(myReader1("ref_amministrativo"), ""))
                                    contenuto = Replace(contenuto, "$operatore$", Session.Item("NOME_OPERATORE"))
                                    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader1("responsabile"), ""))
                                    contenuto = Replace(contenuto, "$numverdefiliale$", par.IfNull(myReader1("n_verde"), ""))
                                    contenuto = Replace(contenuto, "$filialeappartenenza$", UCase(par.IfNull(myReader1("DESCRIZIONE"), "") & "-" & par.IfNull(myReader1("INDIRIZZO"), "") & " " & par.IfNull(myReader1("civico"), "") & " " & par.IfNull(myReader1("cap"), "") & " " & par.IfNull(myReader1("localita"), "")))
                                    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader1("ACRONIMO"), ""))
                                    contenuto = Replace(contenuto, "$cds$", "GL0000/" & par.PulisciStrSql(par.IfNull(myReader1("ACRONIMO"), "")) & "/" & par.PulisciStrSql(txtProtocollo.Text))
                                Else
                                    contenuto = Replace(contenuto, "$nomefiliale$", "__________")
                                    contenuto = Replace(contenuto, "$indirizzofiliale$", "__________")
                                    contenuto = Replace(contenuto, "$capfiliale$", "__________")
                                    contenuto = Replace(contenuto, "$cittafiliale$", "__________")
                                    contenuto = Replace(contenuto, "$telfax$", "Tel:" & "__________ - Fax: __________")
                                    contenuto = Replace(contenuto, "$referente$", "__________")
                                    contenuto = Replace(contenuto, "$operatore$", Session.Item("NOME_OPERATORE"))
                                    contenuto = Replace(contenuto, "$responsabile$", "__________")
                                    contenuto = Replace(contenuto, "$numverdefiliale$", "__________")
                                    contenuto = Replace(contenuto, "$filialeappartenenza$", "__________")
                                    contenuto = Replace(contenuto, "$acronimo$", "____")
                                    contenuto = Replace(contenuto, "$cds$", "GL0000/______/" & par.PulisciStrSql(txtProtocollo.Text))
                                End If
                                myReader1.Close()

                            Else
                                par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali where indirizzi.id=tab_filiali.id_indirizzo and tab_filiali.id= 58"
                                myReader1 = par.cmd.ExecuteReader()
                                If myReader1.Read Then
                                    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader1("nome"), ""))
                                    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader1("descr"), "") & " " & par.IfNull(myReader1("civico"), ""))
                                    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader1("cap"), ""))
                                    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader1("localita"), ""))
                                    contenuto = Replace(contenuto, "$telfax$", "Tel:" & par.IfNull(myReader1("n_telefono"), "") & " - Fax:" & par.IfNull(myReader1("n_fax"), ""))
                                    contenuto = Replace(contenuto, "$referente$", par.IfNull(myReader1("ref_amministrativo"), ""))
                                    contenuto = Replace(contenuto, "$operatore$", Session.Item("NOME_OPERATORE"))
                                    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader1("responsabile"), ""))
                                    contenuto = Replace(contenuto, "$numverdefiliale$", par.IfNull(myReader1("n_telefono_verde"), ""))
                                    contenuto = Replace(contenuto, "$filialeappartenenza$", UCase(par.IfNull(myReader1("nome"), "") & "-" & par.IfNull(myReader1("descr"), "") & " " & par.IfNull(myReader1("civico"), "") & " " & par.IfNull(myReader1("cap"), "") & " " & par.IfNull(myReader1("localita"), "")))
                                    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader1("ACRONIMO"), ""))
                                    contenuto = Replace(contenuto, "$cds$", "GL0000/" & par.PulisciStrSql(par.IfNull(myReader1("ACRONIMO"), "")) & "/" & par.PulisciStrSql(txtProtocollo.Text))
                                Else
                                    contenuto = Replace(contenuto, "$nomefiliale$", "__________")
                                    contenuto = Replace(contenuto, "$indirizzofiliale$", "__________")
                                    contenuto = Replace(contenuto, "$capfiliale$", "__________")
                                    contenuto = Replace(contenuto, "$cittafiliale$", "__________")
                                    contenuto = Replace(contenuto, "$telfax$", "Tel:" & "__________ - Fax: __________")
                                    contenuto = Replace(contenuto, "$referente$", "__________")
                                    contenuto = Replace(contenuto, "$operatore$", Session.Item("NOME_OPERATORE"))
                                    contenuto = Replace(contenuto, "$responsabile$", "__________")
                                    contenuto = Replace(contenuto, "$numverdefiliale$", "__________")
                                    contenuto = Replace(contenuto, "$filialeappartenenza$", "__________")
                                    contenuto = Replace(contenuto, "$acronimo$", "____")
                                    contenuto = Replace(contenuto, "$cds$", "GL0000/______/" & par.PulisciStrSql(txtProtocollo.Text))
                                End If
                                myReader1.Close()
                            End If

                        End If
                        myReader.Close()

                    Else
                        If Request.QueryString("45") = "1" Then
                            par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.PG,T_TIPO_INDIRIZZO.descrizione,ind_res_dnte,civico_res_dnte,COMUNI_NAZIONI.nome AS comune,COMUNI_NAZIONI.sigla,cap_res_dnte,scala,alloggio,piano,UTENZA_COMP_NUCLEO.nome,UTENZA_COMP_NUCLEO.cognome " _
                                            & "FROM UTENZA_COMP_NUCLEO, COMUNI_NAZIONI, T_TIPO_INDIRIZZO, UTENZA_DICHIARAZIONI " _
                                            & "WHERE UTENZA_COMP_NUCLEO.id_dichiarazione=UTENZA_DICHIARAZIONI.ID AND UTENZA_COMP_NUCLEO.progr=0 AND " _
                                            & "COMUNI_NAZIONI.ID = UTENZA_DICHIARAZIONI.id_luogo_res_dnte And T_TIPO_INDIRIZZO.cod = UTENZA_DICHIARAZIONI.id_tipo_ind_res_dnte " _
                                            & "AND UTENZA_DICHIARAZIONI.ID=" & idc.Value
                            myReader = par.cmd.ExecuteReader()
                            If myReader.Read Then
                                contenuto = Replace(contenuto, "$codcontratto$", par.IfNull(myReader("PG"), ""))
                                contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                                contenuto = Replace(contenuto, "$nominativocorr$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                                contenuto = Replace(contenuto, "$indirizzocorr$", Mid(par.IfNull(myReader("descrizione"), "") & " " & par.IfNull(myReader("ind_res_dnte"), "") & " " & par.IfNull(myReader("civico_res_dnte"), ""), 1, 30))
                                contenuto = Replace(contenuto, "$localitacorr$", par.IfNull(myReader("cap_res_dnte"), "") & " " & par.IfNull(myReader("comune"), "") & " " & par.IfNull(myReader("sigla"), ""))

                                Dim FF As String = ""

                                If par.IfNull(myReader("alloggio"), "") <> "" Then
                                    FF = FF & "INTERNO " & par.IfNull(myReader("alloggio"), "") & " "
                                End If
                                If par.IfNull(myReader("SCAla"), "") <> "" Then
                                    FF = FF & "SCALA " & par.IfNull(myReader("SCALA"), "") & " "
                                End If
                                If par.IfNull(myReader("piano"), "") <> "" Then
                                    FF = FF & "PIANO " & myReader("piano") & " "
                                End If
                                contenuto = Replace(contenuto, "$internoscalapiano$", Mid(FF, 1, 30))
                                contenuto = Replace(contenuto, "$internoscalapianocorr$", Mid(FF, 1, 30))
                                contenuto = Replace(contenuto, "$interno$", par.IfNull(myReader("alloggio"), ""))
                                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReader("SCALA"), ""))
                                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReader("piano"), ""))
                                contenuto = Replace(contenuto, "$indirizzounita$", Mid(par.IfNull(myReader("descrizione"), "") & " " & par.IfNull(myReader("ind_res_dnte"), "") & " " & par.IfNull(myReader("civico_res_dnte"), ""), 1, 30))
                                contenuto = Replace(contenuto, "$localitaunita$", par.IfNull(myReader("cap_res_dnte"), "") & " " & par.IfNull(myReader("comune"), "") & " " & par.IfNull(myReader("sigla"), ""))

                            End If
                            myReader.Close()

                            If Ausi = "0" Then
                                'par.cmd.CommandText = "SELECT tab_filiali.*,indirizzi.descrizione AS descr,indirizzi.civico,indirizzi.cap,indirizzi.localita FROM siscom_mi.indirizzi,siscom_mi.tab_filiali WHERE indirizzi.ID=tab_filiali.id_indirizzo AND tab_filiali.ID=" & Session.Item("ID_STRUTTURA")
                                par.cmd.CommandText = "SELECT ACRONIMO,REF_AMMINISTRATIVO,RESPONSABILE,UTENZA_SPORTELLI.*,(SELECT NOME FROM COMUNI_NAZIONI WHERE ID=UTENZA_SPORTELLI.ID_COMUNE) AS LOCALITA FROM SISCOM_MI.TAB_FILIALI,UTENZA_FILIALI,UTENZA_SPORTELLI WHERE UTENZA_FILIALI.ID=UTENZA_SPORTELLI.ID_FILIALE AND TAB_FILIALI.ID=UTENZA_FILIALI.ID_STRUTTURA AND UTENZA_SPORTELLI.ID IN (SELECT ID_SPORTELLO FROM UTENZA_SPORTELLI_PATRIMONIO WHERE ID_AU=" & INDICEBANDO & " AND ID_UNITA=(SELECT ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_UNITA_PRINCIPALE IS NULL AND ID_CONTRATTO=" & indicecontratto & "))"
                                myReader = par.cmd.ExecuteReader()
                                If myReader.Read Then
                                    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("nome"), ""))
                                    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("descr"), "") & " " & par.IfNull(myReader("civico"), ""))
                                    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("cap"), ""))
                                    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("localita"), ""))
                                    contenuto = Replace(contenuto, "$telfax$", "Tel:" & par.IfNull(myReader("n_telefono"), "") & " - Fax:" & par.IfNull(myReader("n_fax"), ""))
                                    contenuto = Replace(contenuto, "$referente$", par.IfNull(myReader("ref_amministrativo"), ""))
                                    contenuto = Replace(contenuto, "$operatore$", Session.Item("NOME_OPERATORE"))
                                    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("responsabile"), ""))
                                    contenuto = Replace(contenuto, "$numverdefiliale$", par.IfNull(myReader("n_verde"), ""))
                                    contenuto = Replace(contenuto, "$filialeappartenenza$", UCase(par.IfNull(myReader("nome"), "") & "-" & par.IfNull(myReader("descr"), "") & " " & par.IfNull(myReader("civico"), "") & " " & par.IfNull(myReader("cap"), "") & " " & par.IfNull(myReader("localita"), "")))
                                    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
                                    contenuto = Replace(contenuto, "$protocollo$", txtProtocollo.Text)
                                    contenuto = Replace(contenuto, "$cds$", "GL0000/" & par.PulisciStrSql(par.IfNull(myReader("ACRONIMO"), "")) & "/" & par.PulisciStrSql(txtProtocollo.Text))
                                Else
                                    contenuto = Replace(contenuto, "$nomefiliale$", "__________")
                                    contenuto = Replace(contenuto, "$indirizzofiliale$", "__________")
                                    contenuto = Replace(contenuto, "$capfiliale$", "__________")
                                    contenuto = Replace(contenuto, "$cittafiliale$", "__________")
                                    contenuto = Replace(contenuto, "$telfax$", "Tel:" & "__________ - Fax: __________")
                                    contenuto = Replace(contenuto, "$referente$", "__________")
                                    contenuto = Replace(contenuto, "$operatore$", Session.Item("NOME_OPERATORE"))
                                    contenuto = Replace(contenuto, "$responsabile$", "__________")
                                    contenuto = Replace(contenuto, "$numverdefiliale$", "__________")
                                    contenuto = Replace(contenuto, "$filialeappartenenza$", "__________")
                                    contenuto = Replace(contenuto, "$acronimo$", "____")
                                    contenuto = Replace(contenuto, "$cds$", "GL0000/______/" & par.PulisciStrSql(txtProtocollo.Text))
                                End If
                                myReader.Close()
                            Else
                                par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali where indirizzi.id=tab_filiali.id_indirizzo and tab_filiali.id= 58"
                                myReader = par.cmd.ExecuteReader()
                                If myReader.Read Then
                                    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("nome"), ""))
                                    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("descr"), "") & " " & par.IfNull(myReader("civico"), ""))
                                    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("cap"), ""))
                                    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("localita"), ""))
                                    contenuto = Replace(contenuto, "$telfax$", "Tel:" & par.IfNull(myReader("n_telefono"), "") & " - Fax:" & par.IfNull(myReader("n_fax"), ""))
                                    contenuto = Replace(contenuto, "$referente$", par.IfNull(myReader("ref_amministrativo"), ""))
                                    contenuto = Replace(contenuto, "$operatore$", Session.Item("NOME_OPERATORE"))
                                    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("responsabile"), ""))
                                    contenuto = Replace(contenuto, "$numverdefiliale$", par.IfNull(myReader("n_telefono_verde"), ""))
                                    contenuto = Replace(contenuto, "$filialeappartenenza$", UCase(par.IfNull(myReader("nome"), "") & "-" & par.IfNull(myReader("descr"), "") & " " & par.IfNull(myReader("civico"), "") & " " & par.IfNull(myReader("cap"), "") & " " & par.IfNull(myReader("localita"), "")))
                                    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
                                    contenuto = Replace(contenuto, "$protocollo$", txtProtocollo.Text)
                                    contenuto = Replace(contenuto, "$cds$", "GL0000/" & par.PulisciStrSql(par.IfNull(myReader("ACRONIMO"), "")) & "/" & par.PulisciStrSql(txtProtocollo.Text))
                                Else
                                    contenuto = Replace(contenuto, "$nomefiliale$", "__________")
                                    contenuto = Replace(contenuto, "$indirizzofiliale$", "__________")
                                    contenuto = Replace(contenuto, "$capfiliale$", "__________")
                                    contenuto = Replace(contenuto, "$cittafiliale$", "__________")
                                    contenuto = Replace(contenuto, "$telfax$", "Tel:" & "__________ - Fax: __________")
                                    contenuto = Replace(contenuto, "$referente$", "__________")
                                    contenuto = Replace(contenuto, "$operatore$", Session.Item("NOME_OPERATORE"))
                                    contenuto = Replace(contenuto, "$responsabile$", "__________")
                                    contenuto = Replace(contenuto, "$numverdefiliale$", "__________")
                                    contenuto = Replace(contenuto, "$filialeappartenenza$", "__________")
                                    contenuto = Replace(contenuto, "$acronimo$", "____")
                                    contenuto = Replace(contenuto, "$cds$", "GL0000/______/" & par.PulisciStrSql(txtProtocollo.Text))
                                End If
                                myReader.Close()

                            End If
                        End If
                    End If

                    'svuoto eventuali variabili non compilate
                    contenuto = Replace(contenuto, "$dichiarante$", "")
                    contenuto = Replace(contenuto, "$datanascitadichiarante$", "")
                    contenuto = Replace(contenuto, "$luogonascitadichiarante$", "")
                    contenuto = Replace(contenuto, "$provincianascitadichiarante$", "")
                    contenuto = Replace(contenuto, "$cds$", "GL0000/---/" & par.PulisciStrSql(txtProtocollo.Text))

                    Dim SchedaIsee As Boolean = False
                    If InStr(contenuto, "$schedaisee$") > 0 Then
                        SchedaIsee = True
                        contenuto = Replace(contenuto, "$schedaisee$", "")
                    End If


                    'MAX 30/09/2015 frontespizio
                    Dim ValoreProcesso As String = ""
                    par.cmd.CommandText = "select * from PROCESSI_BARCODE where id=1"
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        ValoreProcesso = par.IfNull(myReader("valore"), "")
                        contenuto = Replace(contenuto, "$nomeprocesso$", par.IfNull(myReader("descrizione"), ""))
                    End If
                    myReader.Close()
                    contenuto = Replace(contenuto, "$barcode$", "")
                    par.cmd.CommandText = "select * from utenza_doc_mancante where id_dichiarazione=" & idd.Value & " order by descrizione asc"
                    myReader = par.cmd.ExecuteReader()
                    Do While myReader.Read
                        documentazionemancante = documentazionemancante & "-" & par.IfNull(myReader("descrizione"), "") & "\par "
                    Loop
                    myReader.Close()
                    contenuto = Replace(contenuto, "$documentimancanti$", documentazionemancante)


                    File.Delete(fileName)

                    BaseFile = "MODELLO_" & Format(Now, "yyyyMMddHHmmss")
                    file1 = BaseFile & ".RTF"
                    fileName = Server.MapPath("..\FileTemp\") & file1
                    Dim basefilePDF As String = Format(Now, "yyyyMMddHHmmss") & "_" & idd.Value & "$" & Replace(NomeModello, " ", "_") & ".pdf"
                    fileNamePDF = Server.MapPath("..\ALLEGATI\ANAGRAFE_UTENZA\") & basefilePDF

                    Dim sr As StreamWriter = New StreamWriter(fileName, False, System.Text.Encoding.Default)
                    sr.WriteLine(contenuto)
                    sr.Close()

                    Dim rp As New Rpn
                    Dim i As Boolean
                    Dim K As Integer = 0

                    'Dim result As Integer = Rpn.RpsSetLicenseInfo("G927S-F6R7A-7VH31", "srab35887-1", "S&S SISTEMI E SOLUZIONI S.R.L.")
                    Dim result As Int64 = Rpn.RpsSetLicenseInfo("8RWQS-6Y9UC-HA2L1-91017", "srab35887-1", "S&S SISTEMI E SOLUZIONI S.R.L.")
                    rp.InWebServer = True
                    rp.EmbedFonts = True
                    rp.ExactTextPlacement = True

                    i = rp.RpsConvertFile(fileName, fileNamePDF)
                    For K = 0 To 100

                    Next
                    File.Delete(fileName)

                    If SchedaIsee = True Then
                        Dim ElencoFile()
                        Dim Conta As Integer = 0
                        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/ANAGRAFE_UTENZA/"), FileIO.SearchOption.SearchTopLevelOnly, "02_" & Request.QueryString("COD") & "_" & Request.QueryString("ID") & "*.pdf")
                            If InStr(foundFile, "02_") > 0 Then
                                ReDim Preserve ElencoFile(Conta)
                                ElencoFile(Conta) = foundFile
                                Conta = Conta + 1
                            End If
                        Next
                        Dim kk As Long
                        Dim jj As Long
                        Dim scambia

                        For kk = 0 To Conta - 2
                            For jj = kk + 1 To Conta - 1
                                If CLng(Mid(RicavaFile(ElencoFile(kk)), InStr(RicavaFile(ElencoFile(kk)), "-") + 1, 14)) < CLng(Mid(RicavaFile(ElencoFile(jj)), InStr(RicavaFile(ElencoFile(jj)), "-") + 1, 14)) Then
                                    scambia = ElencoFile(kk)
                                    ElencoFile(kk) = ElencoFile(jj)
                                    ElencoFile(jj) = scambia
                                End If
                            Next
                        Next
                        Dim NomeFileDichiarazione As String = ""
                        If Conta > 0 Then
                            For j = 0 To Conta - 1
                                NomeFileDichiarazione = RicavaFile(ElencoFile(j))
                            Next
                        End If

                        If NomeFileDichiarazione = "" Then
                            NomeFileDichiarazione = "02_" & Request.QueryString("COD") & "_" & idd.Value & ".pdf"
                        End If

                        Dim url1 As String = Server.MapPath("..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFileDichiarazione

                        Dim pdfDocumentOptions As New ExpertPdf.MergePdf.PdfDocumentOptions()
                        pdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
                        pdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
                        Dim pdfMerge As New ExpertPdf.MergePdf.PDFMerge(pdfDocumentOptions)

                        Dim Licenza As String = ""
                        Licenza = Session.Item("LicenzaPdfMerge")
                        If Licenza <> "" Then
                            pdfMerge.LicenseKey = Licenza
                        End If

                        pdfMerge.AppendPDFFile(fileNamePDF)

                        If IO.File.Exists(url1) = True Then
                            pdfMerge.AppendPDFFile(url1)
                        End If
                        pdfMerge.SaveMergedPDFToFile(Server.MapPath("..\ALLEGATI\ANAGRAFE_UTENZA\") & Replace(basefilePDF, " ", "_"))


                    End If

                    If i = True Then
                        par.cmd.CommandText = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                        & "VALUES (" & idd.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                        & "'F211','" & par.PulisciStrSql(NomeModello) & "','I')"
                        par.cmd.ExecuteNonQuery()

                        'Response.Redirect("..\FileTemp\" & BaseFile & ".pdf", False)
                        Response.Write("<script>window.location.href='../ALLEGATI/ANAGRAFE_UTENZA/" & Replace(basefilePDF, " ", "_") & "';</script>")
                    Else
                        Response.Write("<script>alert('Attenzione...errore durante la generazione del file pdf. Impossibile procedere!');</script>")
                    End If
                End If
            Else
                Response.Write("<script>alert('Attenzione...selezionare un modello dalla lista prima di procedere!');</script>")
            End If

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
            'Response.Write("<script>window.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

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
End Class
