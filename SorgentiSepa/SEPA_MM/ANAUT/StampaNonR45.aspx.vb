Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class ANAUT_StampaNonR
    Inherits PageSetIdMode
    Dim dt As New System.Data.DataTable
    Dim row As System.Data.DataRow
    Dim par As New CM.Global
    Dim i As Long = 0

    Dim sPosteAler As String = ""               'TUTTI i CAMPI
    Dim sPosteAlerNominativo As String = ""     '1)  Nominativo Postale
    Dim sPosteAlerInd As String = ""            '3)  Indirizzo
    Dim sPosteAlerScala As String = ""          '6)  Scala
    Dim sPosteAlerInterno As String = ""        '7)  Interno
    Dim sPosteAlerCAP As String = ""            '8)  CAP
    Dim sPosteAlerLocalita As String = ""       '9)  Località
    Dim sPosteAlerProv As String = ""           '10) Provincia
    Dim sPosteAlerCodUtente As String = ""      '11) Codice Utente (POSTALER.ID)
    Dim sPosteAlerAcronimo As String = ""       '12) Acronimo
    Dim sPosteDefault As String = ""            ' per i campi 2-4-5 (Presso, casella postale, indirizzo casella postale)
    Dim sPosteAlerIA As String = ""
    Dim AnnoBando As String = ""
    Dim AnnoRedditi As String = ""



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String
        Dim Cod_Contratto As String = ""

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='Elaborazione in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()
            Try
                Dim x As Integer = 0

                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans


                Dim dict As New System.Data.DataTable
                Dim ROW1 As System.Data.DataRow

                dict.Columns.Add("NOME")
                dict.Columns.Add("INDIRIZZO")
                dict.Columns.Add("CAP")
                dict.Columns.Add("LOCALITA")
                dict.Columns.Add("TELEFONI")
                dict.Columns.Add("REFERENTE")
                dict.Columns.Add("RESPONSABILE")
                dict.Columns.Add("NVERDE")
                dict.Columns.Add("ACRONIMO")


                Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader
                'SELECT * FROM utenza_bandi where id>1 ORDER BY id desc
                par.cmd.CommandText = "SELECT * FROM utenza_bandi where id>1 ORDER BY id desc"
                myReaderX = par.cmd.ExecuteReader()
                If myReaderX.Read Then
                    AnnoBando = Trim(Replace(par.IfNull(myReaderX("descrizione"), ""), "AU", ""))
                    AnnoRedditi = par.IfNull(myReaderX("anno_isee"), "")
                End If
                myReaderX.Close()


                par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali where indirizzi.id=tab_filiali.id_indirizzo AND id_tipo_st=0 and acronimo is not null"
                myReaderX = par.cmd.ExecuteReader()
                Do While myReaderX.Read
                    ROW1 = dict.NewRow()
                    ROW1.Item("NOME") = par.IfNull(myReaderX("nome"), "")
                    ROW1.Item("INDIRIZZO") = par.IfNull(myReaderX("descr"), "") & " " & par.IfNull(myReaderX("civico"), "")
                    ROW1.Item("CAP") = par.IfNull(myReaderX("cap"), "")
                    ROW1.Item("LOCALITA") = par.IfNull(myReaderX("LOCALITA"), "")
                    ROW1.Item("TELEFONI") = "Tel:" & par.IfNull(myReaderX("n_telefono"), "") & " - Fax:" & par.IfNull(myReaderX("n_fax"), "") & " - n.verde:" & par.IfNull(myReaderX("n_telefono_verde"), "")
                    ROW1.Item("REFERENTE") = Session.Item("NOME_OPERATORE")
                    ROW1.Item("RESPONSABILE") = par.IfNull(myReaderX("responsabile"), "")
                    ROW1.Item("NVERDE") = par.IfNull(myReaderX("n_telefono_verde"), "")
                    ROW1.Item("ACRONIMO") = par.IfNull(myReaderX("ACRONIMO"), "")
                    dict.Rows.Add(ROW1)
                    'ROW1 = dict.NewRow()
                    'ROW1.Item("NOME") = par.IfNull(myReaderX("nome"), "")
                    'ROW1.Item("INDIRIZZO") = par.IfNull(myReaderX("descr"), "") & " " & par.IfNull(myReaderX("civico"), "")
                    'ROW1.Item("CAP") = par.IfNull(myReaderX("cap"), "")
                    'ROW1.Item("LOCALITA") = par.IfNull(myReaderX("LOCALITA"), "")
                    'ROW1.Item("RESPONSABILE") = par.IfNull(myReaderX("responsabile"), "")
                    'ROW1.Item("ACRONIMO") = par.IfNull(myReaderX("ACRONIMO"), "")
                    'Select Case par.IfNull(myReaderX("id"), "")
                    '    Case "9" 'filiale 1
                    '        ROW1.Item("TELEFONI") = "Tel:" & par.IfNull(myReaderX("n_telefono"), "") & " - Fax:" & par.IfNull(myReaderX("n_fax"), "") & " - n.verde:" & "800201009"
                    '        ROW1.Item("REFERENTE") = "Antonella Viggiano"
                    '        ROW1.Item("NVERDE") = "800201009"
                    '    Case "6" 'filiale 2
                    '        ROW1.Item("TELEFONI") = "Tel:" & par.IfNull(myReaderX("n_telefono"), "") & " - Fax:" & par.IfNull(myReaderX("n_fax"), "") & " - n.verde:" & "800759999"
                    '        ROW1.Item("REFERENTE") = "Venera Logiudice"
                    '        ROW1.Item("NVERDE") = "800759999"
                    '    Case "1" 'filiale 3
                    '        ROW1.Item("TELEFONI") = "Tel:" & par.IfNull(myReaderX("n_telefono"), "") & " - Fax:" & par.IfNull(myReaderX("n_fax"), "") & " - n.verde:" & "800759645"
                    '        ROW1.Item("REFERENTE") = "Brenelli Stefania"
                    '        ROW1.Item("NVERDE") = "800759645"
                    '    Case "22" 'filiale 4
                    '        ROW1.Item("TELEFONI") = "Tel:" & par.IfNull(myReaderX("n_telefono"), "") & " - Fax:" & par.IfNull(myReaderX("n_fax"), "") & " - n.verde:" & "800112300"
                    '        ROW1.Item("REFERENTE") = "Alonzo Leonardo"
                    '        ROW1.Item("NVERDE") = "800112300"
                    '    Case "2" 'filiale 5
                    '        ROW1.Item("TELEFONI") = "Tel:" & par.IfNull(myReaderX("n_telefono"), "") & " - Fax:" & par.IfNull(myReaderX("n_fax"), "") & " - n.verde:" & "800400310"
                    '        ROW1.Item("REFERENTE") = "Bardo Rosamaria"
                    '        ROW1.Item("NVERDE") = "800400310"
                    '    Case "8" 'filiale 6
                    '        ROW1.Item("TELEFONI") = "Tel:" & par.IfNull(myReaderX("n_telefono"), "") & " - Fax:" & par.IfNull(myReaderX("n_fax"), "") & " - n.verde:" & "800400310"
                    '        ROW1.Item("REFERENTE") = "Cinquanta Laura"
                    '        ROW1.Item("NVERDE") = "800400310"
                    '    Case "3" 'filiale legnano
                    '        ROW1.Item("TELEFONI") = "Tel:" & par.IfNull(myReaderX("n_telefono"), "") & " - Fax:" & par.IfNull(myReaderX("n_fax"), "") & " - n.verde:" & "800212300"
                    '        ROW1.Item("REFERENTE") = "Simone Cerini"
                    '        ROW1.Item("NVERDE") = "800212300"
                    '    Case "5" 'filiale sesto
                    '        ROW1.Item("TELEFONI") = "Tel:" & par.IfNull(myReaderX("n_telefono"), "") & " - Fax:" & par.IfNull(myReaderX("n_fax"), "") & " - n.verde:" & "800019229"
                    '        ROW1.Item("REFERENTE") = "Piersante Dibiase"
                    '        ROW1.Item("NVERDE") = "800019229"
                    '    Case "4" 'filiale rozzano
                    '        ROW1.Item("TELEFONI") = "Tel:" & par.IfNull(myReaderX("n_telefono"), "") & " - Fax:" & par.IfNull(myReaderX("n_fax"), "") & " - n.verde:" & "02 57562322"
                    '        ROW1.Item("REFERENTE") = "Filippo Santoro"
                    '        ROW1.Item("NVERDE") = "02 57562322"

                    'End Select



                    'dict.Rows.Add(ROW1)
                Loop
                myReaderX.Close()



                Dim TestoLettera() As String

                Dim PG As String = Request.QueryString("PG")

                Dim FILIALE As String = ""
                Dim ESTREMI As String = ""
                Dim ESTREMI0 As String = ""
                Dim ESTREMI1 As String = ""
                Dim ESTREMI2 As String = ""
                Dim ESTREMI3 As String = ""
                Dim ESTREMI4 As String = ""
                Dim ESTREMI6 As String = ""
                Dim ESTREMI5 As String = ""
                Dim INTESTATARIO As String = ""
                Dim INDIRIZZO_POSTALE As String = ""
                Dim INDIRIZZO_POSTALE0 As String = ""
                Dim INDIRIZZO_POSTALE1 As String = ""
                Dim INDIRIZZO_POSTALE2 As String = ""
                Dim NOMEFILIALE As String = ""
                Dim INDIRIZZO As String = ""
                Dim LOCALITA As String = ""
                Dim sBANDO As String = "0"

                Dim INDICELETTERA As String = "0"


                dt = CType(HttpContext.Current.Session.Item("ElencoDT"), Data.DataTable)
                For Each row In dt.Rows
                    FILIALE = ""
                    ESTREMI = ""
                    ESTREMI0 = ""
                    ESTREMI1 = ""
                    ESTREMI2 = ""
                    ESTREMI3 = ""
                    ESTREMI4 = ""
                    ESTREMI6 = ""
                    ESTREMI5 = ""
                    INTESTATARIO = ""
                    INDIRIZZO_POSTALE = ""
                    INDIRIZZO_POSTALE0 = ""
                    INDIRIZZO_POSTALE1 = ""
                    INDIRIZZO_POSTALE2 = ""
                    INDIRIZZO = ""
                    LOCALITA = ""

                    sPosteAlerScala = ""
                    sPosteAlerInterno = ""
                    sPosteAlerCAP = ""
                    sPosteAlerLocalita = ""
                    sPosteAlerProv = ""
                    sPosteAlerCodUtente = ""
                    sPosteAlerAcronimo = ""

                    ReDim Preserve TestoLettera(i)

                    Cod_Contratto = par.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), 0)
                    NOMEFILIALE = par.IfNull(dt.Rows(i).Item("FILIALE"), 0)
                    sBANDO = par.IfNull(dt.Rows(i).Item("BANDO"), 0)

                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                    par.cmd.CommandText = "select anagrafica.id as ida,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,tipo_cor,presso_cor,via_cor,civico_cor,cap_cor,luogo_cor,sigla_cor from siscom_mi.SOGGETTI_CONTRATTUALI,siscom_mi.ANAGRAFICA,siscom_mi.rapporti_utenza where SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND cod_contratto='" & Cod_Contratto & "'"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        INTESTATARIO = Mid(Trim(par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "")), 1, 29)
                        INDIRIZZO_POSTALE = Mid(Trim(Replace(UCase(par.IfNull(myReader("presso_cor"), "")), "C/O", "")), 1, 29)

                        INDIRIZZO_POSTALE0 = par.IfNull(myReader("tipo_cor"), "") & " " & par.IfNull(myReader("via_cor"), "") & " " & par.IfNull(myReader("civico_cor"), "")
                        INDIRIZZO_POSTALE1 = par.IfNull(myReader("cap_cor"), "") & " " & par.IfNull(myReader("luogo_cor"), "") & " " & par.IfNull(myReader("sigla_cor"), "")
                        INDIRIZZO_POSTALE2 = ""

                        sPosteAlerCAP = par.IfNull(myReader("cap_cor"), "")
                        sPosteAlerLocalita = par.IfNull(myReader("luogo_cor"), "")
                        sPosteAlerProv = par.IfNull(myReader("sigla_cor"), "")
                        sPosteAlerCodUtente = Format(par.IfNull(myReader("ida"), ""), "000000000000")

                        Dim FF As String = ""
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                        par.cmd.CommandText = "select scale_edifici.descrizione as SCA,unita_immobiliari.*,TIPO_LIVELLO_PIANO.LIVELLO from siscom_mi.scale_edifici,siscom_mi.unita_immobiliari,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE where SISCOM_MI.UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND RAPPORTI_UTENZA.COD_CONTRATTO='" & Cod_Contratto & "' AND unita_immobiliari.id_scala=scale_edifici.id (+) and TIPO_LIVELLO_PIANO.COD=UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO and unita_immobiliari.id=unita_contrattuale.id_unita"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            If par.IfNull(myReader1("INTERNO"), "") <> "" Then
                                FF = FF & "INTERNO " & par.IfNull(myReader1("INTERNO"), "") & " "
                                sPosteAlerInterno = par.IfNull(myReader1("INTERNO"), "")
                            End If

                            Dim ll As Integer = 0
                            If par.IfNull(myReader1("SCA"), "") <> "" Then
                                FF = FF & "SCALA " & par.IfNull(myReader1("SCA"), "") & " "
                                sPosteAlerScala = par.IfNull(myReader1("SCA"), "")

                                For ll = 1 To Strings.Len(par.IfNull(myReader1("SCA"), ""))
                                    If Char.IsDigit(Strings.Mid(par.IfNull(myReader1("SCA"), ""), ll, 1)) = False Then
                                        sPosteAlerScala = Strings.Mid(par.IfNull(myReader1("SCA"), ""), ll, Strings.Len(par.IfNull(myReader1("SCA"), "")))  'POSTE 
                                        Exit For
                                    End If
                                Next ll

                            End If
                            If par.IfNull(myReader1("LIVELLO"), -100) <> -100 Then
                                If CInt(myReader1("LIVELLO")) - myReader1("LIVELLO") = 0 Then
                                    If myReader1("LIVELLO") - 0.5 = 0 Then
                                        FF = FF & "PIANO T" & " "
                                    Else
                                        FF = FF & "PIANO " & myReader1("LIVELLO") & " "
                                    End If
                                Else
                                    If myReader1("LIVELLO") - 0.5 = 0 Then
                                        FF = FF & "PIANO T" & " "
                                    Else
                                        FF = FF & "PIANO " & myReader1("LIVELLO") - 0.5 & " "
                                    End If
                                End If
                            End If
                            INDIRIZZO_POSTALE2 = Mid(FF, 1, 30)
                        End If
                        myReader1.Close()
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "select indirizzi.*,comuni_nazioni.nome,comuni_nazioni.sigla from comuni_nazioni,siscom_mi.indirizzi where comuni_nazioni.cod=indirizzi.cod_comune and indirizzi.id in (select id_indirizzo from siscom_mi.unita_immobiliari where cod_unita_immobiliare='" & Mid(Cod_Contratto, 1, 17) & "')"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        INDIRIZZO = par.IfNull(myReader("descrizione"), "") & " " & par.IfNull(myReader("civico"), "")
                        LOCALITA = par.IfNull(myReader("cap"), "") & " " & par.IfNull(myReader("nome"), "") & " " & par.IfNull(myReader("sigla"), "")
                    End If
                    myReader.Close()

                    If NOMEFILIALE <> "&nbsp;" Then

                        For Each ROW1 In dict.Rows
                            If NOMEFILIALE = par.IfNull(dict.Rows(x).Item("NOME"), "") Then

                                FILIALE = par.IfNull(dict.Rows(x).Item("NOME"), "")
                                ESTREMI = par.IfNull(dict.Rows(x).Item("INDIRIZZO"), "")
                                ESTREMI0 = par.IfNull(dict.Rows(x).Item("CAP"), "")
                                ESTREMI1 = par.IfNull(dict.Rows(x).Item("LOCALITA"), "")
                                ESTREMI2 = par.IfNull(dict.Rows(x).Item("TELEFONI"), "")
                                ESTREMI3 = UCase(par.IfNull(dict.Rows(x).Item("REFERENTE"), ""))
                                ESTREMI4 = par.IfNull(dict.Rows(x).Item("RESPONSABILE"), "")
                                ESTREMI6 = par.IfNull(dict.Rows(x).Item("NVERDE"), "")
                                ESTREMI5 = "GL0000/" & par.IfNull(dict.Rows(x).Item("ACRONIMO"), "")
                                sPosteAlerAcronimo = par.IfNull(dict.Rows(x).Item("ACRONIMO"), "")
                                Exit For
                            End If
                            x = x + 1
                        Next

                    Else


                    End If


                    'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & Mid(Cod_Contratto, 1, 17) & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
                    'myReader = par.cmd.ExecuteReader()
                    'If myReader.Read Then
                    '    FILIALE = par.IfNull(myReader("nome"), "")
                    '    ESTREMI = par.IfNull(myReader("descr"), "") & " " & par.IfNull(myReader("civico"), "")
                    '    ESTREMI0 = par.IfNull(myReader("cap"), "")
                    '    ESTREMI1 = par.IfNull(myReader("localita"), "")
                    '    ESTREMI2 = "Tel:" & par.IfNull(myReader("n_telefono"), "") & " - Fax:" & par.IfNull(myReader("n_fax"), "") & " - n.verde:" & par.IfNull(myReader("n_telefono_verde"), "")
                    '    ESTREMI3 = Session.Item("NOME_OPERATORE")
                    '    ESTREMI4 = par.IfNull(myReader("responsabile"), "")
                    '    ESTREMI6 = par.IfNull(myReader("n_telefono_verde"), "")
                    '    ESTREMI5 = "GL0000/" & par.IfNull(myReader("ACRONIMO"), "")

                    'End If
                    'myReader.Close()



                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("MODELLI\NonRispondente.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    Dim contenuto As String = sr1.ReadToEnd()
                    sr1.Close()

                    contenuto = Replace(contenuto, "$codcontratto$", Cod_Contratto)
                    contenuto = Replace(contenuto, "$protocollo$", PG)
                    contenuto = Replace(contenuto, "$intestatario$", INTESTATARIO)

                    contenuto = Replace(contenuto, "$indirizzo$", INDIRIZZO)
                    contenuto = Replace(contenuto, "$localita$", LOCALITA)

                    contenuto = Replace(contenuto, "$nominativo$", INDIRIZZO_POSTALE)
                    contenuto = Replace(contenuto, "$indirizzo0$", INDIRIZZO_POSTALE0)
                    contenuto = Replace(contenuto, "$indirizzo1$", INDIRIZZO_POSTALE1)

                    contenuto = Replace(contenuto, "$nomefiliale$", FILIALE)
                    contenuto = Replace(contenuto, "$indirizzofiliale$", ESTREMI)
                    contenuto = Replace(contenuto, "$capfiliale$", ESTREMI0)
                    contenuto = Replace(contenuto, "$cittafiliale$", ESTREMI1)
                    contenuto = Replace(contenuto, "$telfax$", ESTREMI2)
                    contenuto = Replace(contenuto, "$referente$", ESTREMI3)
                    contenuto = Replace(contenuto, "$numeroverde$", ESTREMI6)
                    contenuto = Replace(contenuto, "$responsabile$", ESTREMI4)
                    contenuto = Replace(contenuto, "$centrodicosto$", ESTREMI5 & "/" & PG)

                    contenuto = Replace(contenuto, "$indirizzo2$", INDIRIZZO_POSTALE2)
                    contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))
                    contenuto = Replace(contenuto, "$spazi$", "")
                    contenuto = Replace(contenuto, "$spazi1$", "")
                    contenuto = Replace(contenuto, "$acapo$", "")

                    contenuto = Replace(contenuto, "$bando$", AnnoBando)
                    contenuto = Replace(contenuto, "$annoredditi$", AnnoRedditi)

                    If Request.QueryString("FI") <> "58" Then
                        contenuto = Replace(contenuto, "$testoresponabile$", "Il responsabile di Filiale")
                    Else
                        contenuto = Replace(contenuto, "$testoresponabile$", "Il responsabile")
                    End If

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                       & "VALUES (" & par.IfNull(dt.Rows(i).Item("IDC"), 0) & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                       & "'F170','')"
                    par.cmd.ExecuteNonQuery()




                    ' '' Ricavo ID di POSTALER per PostAler.txt

                    par.cmd.CommandText = " select SISCOM_MI.SEQ_POSTALER.NEXTVAL FROM dual "
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        sPosteAlerIA = myReader(0)
                    End If
                    myReader.Close()

                    If sPosteAler <> "" Then
                        sPosteAler = sPosteAler & vbCrLf
                    End If
                    sPosteAler = sPosteAler _
                           & INDIRIZZO_POSTALE.PadRight(50).Substring(0, 50) & ";" _
                           & sPosteDefault.PadRight(50).Substring(0, 50) & ";" _
                           & INDIRIZZO_POSTALE0.PadRight(50).Substring(0, 50) & ";" _
                           & sPosteDefault.PadRight(50).Substring(0, 50) & ";" _
                           & sPosteDefault.PadRight(50).Substring(0, 50) & ";" _
                           & sPosteAlerScala.PadRight(2).Substring(0, 2) & ";" _
                           & sPosteAlerInterno.PadRight(3).Substring(0, 3) & ";" _
                           & sPosteAlerCAP.PadRight(5).Substring(0, 5) & ";" _
                           & sPosteAlerLocalita.PadRight(50).Substring(0, 50) & ";" _
                           & sPosteAlerProv.PadRight(2).Substring(0, 2) & ";" _
                           & sPosteAlerCodUtente.PadRight(12).Substring(0, 12) & ";" _
                           & sPosteAlerAcronimo.PadRight(4).Substring(0, 4) & ";" _
                           & sPosteAlerIA.PadRight(16).Substring(0, 16) & ";"


                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.DIFFIDE_LETTERE (ID,ID_CONTRATTO,ID_AU,DATA_GENERAZIONE) " _
                                        & "VALUES (SISCOM_MI.SEQ_DIFFIDE_LETTERE.NEXTVAL," & par.IfNull(dt.Rows(i).Item("IDC"), 0) & "," & sBANDO & ",'" & Format(Now, "yyyyMMdd") & "')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = " select SISCOM_MI.SEQ_DIFFIDE_LETTERE.CURRVAL FROM dual "
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        INDICELETTERA = myReader(0)
                    End If
                    myReader.Close()


                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.POSTALER (ID,ID_LETTERA,ID_TIPO_LETTERA) " _
                                       & "VALUES (" & sPosteAlerIA & "," & INDICELETTERA & ",2)"
                    par.cmd.ExecuteNonQuery()


                    TestoLettera(i) = contenuto
                    i = i + 1

                Next



                Dim KK As Integer = 0

                Dim NomeFile1 As String = "AU2009_DiffideNonRispondenti-" & Format(Now, "yyyyMMddHHmmss")
                'scrivo il nuovo modulo compilato
                Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\ANAGRAFE_UTENZA\DIFFIDE\") & NomeFile1 & ".htm", False, System.Text.Encoding.Default)
                For KK = 0 To i - 1
                    sr.WriteLine(TestoLettera(KK))
                    If KK <> i - 1 Then
                        sr.WriteLine("<p style='page-break-before: always'>&nbsp;</p>")
                    End If
                Next
                sr.Close()

                Dim sr2 As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\ANAGRAFE_UTENZA\DIFFIDE\") & "POSTALER_" & NomeFile1 & ".txt", False, System.Text.Encoding.Default)
                sr2.WriteLine(sPosteAler)
                sr2.Close()

                Dim url As String = Server.MapPath("..\ALLEGATI\ANAGRAFE_UTENZA\DIFFIDE\") & NomeFile1
                Dim urlPostAler As String = Server.MapPath("..\ALLEGATI\ANAGRAFE_UTENZA\DIFFIDE\") & "POSTALER_" & NomeFile1
                Dim pdfConverter1 As PdfConverter = New PdfConverter
                Dim Licenza As String = ""

                Licenza = Session.Item("LicenzaHtmlToPdf")
                If Licenza <> "" Then
                    pdfConverter1.LicenseKey = Licenza
                End If

                pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
                pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
                pdfConverter1.PdfDocumentOptions.ShowHeader = False

                pdfConverter1.PdfDocumentOptions.LeftMargin = 20
                pdfConverter1.PdfDocumentOptions.RightMargin = 20
                pdfConverter1.PdfDocumentOptions.TopMargin = 10
                pdfConverter1.PdfDocumentOptions.BottomMargin = 1
                pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True


                pdfConverter1.PdfDocumentOptions.ShowFooter = False
                pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Black
                pdfConverter1.PdfFooterOptions.DrawFooterLine = False
                pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
                pdfConverter1.PdfFooterOptions.ShowPageNumber = True
                pdfConverter1.PdfDocumentOptions.ShowHeader = False


                pdfConverter1.PdfFooterOptions.DrawFooterLine = False
                pdfConverter1.SavePdfFromUrlToFile(url & ".htm", url & ".pdf")
                IO.File.Delete(url & ".htm")

                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream
                Dim zipfic As String


                zipfic = Server.MapPath("..\ALLEGATI\ANAGRAFE_UTENZA\DIFFIDE\" & NomeFile1 & ".zip")

                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)

                Dim strFile As String
                strFile = url & ".pdf"
                Dim strmFile As FileStream = File.OpenRead(strFile)
                Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                '
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


                strFile = urlPostAler & ".txt"
                strmFile = File.OpenRead(strFile)
                Dim abyBuffer1(Convert.ToInt32(strmFile.Length - 1)) As Byte
                strmFile.Read(abyBuffer1, 0, abyBuffer1.Length)
                Dim sFile1 As String = Path.GetFileName(strFile)
                theEntry = New ZipEntry(sFile1)
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

                IO.File.Delete(url & ".pdf")
                IO.File.Delete(urlPostAler & ".txt")

                Response.Write("<script>location.href='ElencoFileDiffide.aspx';</script>")
                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()




            Catch ex As Exception
                lblErrore.Visible = True
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Text = ex.Message
            End Try


        End If


    End Sub
End Class
