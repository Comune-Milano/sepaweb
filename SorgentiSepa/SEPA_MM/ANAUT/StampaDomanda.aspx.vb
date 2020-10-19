Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class ANAUT_StampaDomanda
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session.Item("OPERATORE") = "" Then
        '    Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        '    Exit Sub
        'End If

        Try

            Session.Add("LicenzaHtmlToPdf", par.LicenzaHtmlToPdf)
            Session.Add("LicenzaPdfMerge", par.LicenzaPdfMerge)

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim IdDomanda As Long = 973 ' Request.QueryString("ID")

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("DomandaBando.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = "select (SELECT t_tipo_indirizzo.descrizione FROM t_tipo_indirizzo WHERE t_tipo_indirizzo.cod=NVL(DOMANDE_BANDO.TIND_COM,-1)) AS TIND_COM,(SELECT nome FROM COMUNI_NAZIONI WHERE ID=NVL(domande_bando.id_comu_com,-2)) AS luogo_com,(select nome from comuni_nazioni where id=nvl(comp_nucleo.id_comu_res,-2)) as luogo_residenza,t_tipo_indirizzo.descrizione as tipo_indirizzo,(select nome from comuni_nazioni where id=nvl(comp_nucleo.id_comu_nas,-2)) as luogo_nascita,comp_nucleo.id_nazione_nas,comp_nucleo.prov_nas,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,COMP_NUCLEO.COD_FISCALE,COMP_NUCLEO.DATA_NASCITA,COMP_NUCLEO.INDIRIZZO_RES,COMP_NUCLEO.CIVICO_RES,COMP_NUCLEO.INTERNO_RES,COMP_NUCLEO.PIANO_RES,comp_nucleo.scala_res,DOMANDE_BANDO.*,BANDI.DESCRIZIONE as NOME_BANDO,bandi.anno_isee from t_tipo_indirizzo,COMP_NUCLEO,DOMANDE_BANDO,BANDI where t_tipo_indirizzo.cod=comp_nucleo.cod_tipo_ind_res (+) and COMP_NUCLEO.PROGR=0 AND COMP_NUCLEO.ID_DOMANDA=DOMANDE_BANDO.ID AND BANDI.ID=DOMANDE_BANDO.ID_BANDO AND DOMANDE_BANDO.ID=" & IdDomanda
            myReader = par.cmd.ExecuteReader()

            Dim Nazionalita As String = ""

            If myReader.Read Then

                contenuto = Replace(contenuto, "$datapresentazione$", Format(Now, "dd/MM/yyyy"))

                contenuto = Replace(contenuto, "$numerobando$", par.IfNull(myReader("NOME_BANDO"), ""))
                contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), ""))
                contenuto = Replace(contenuto, "$codicefiscale$", par.IfNull(myReader("cod_fiscale"), ""))
                contenuto = Replace(contenuto, "$annoreddito$", par.IfNull(myReader("anno_isee"), ""))


                If par.IfNull(myReader("id_nazione_nas"), "0") = "3670" Then 'italia
                    contenuto = Replace(contenuto, "$luogonascita$", par.IfNull(myReader("luogo_nascita"), ""))
                    contenuto = Replace(contenuto, "$provincianascita$", par.IfNull(myReader("prov_nas"), ""))
                    Nazionalita = "ITALIA"
                Else
                    contenuto = Replace(contenuto, "$provincianascita$", "--")
                    Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader
                    par.cmd.CommandText = "select * FROM COMUNI_NAZIONI WHERE ID=" & par.IfNull(myReader("id_nazione_nas"), "0")
                    myReader123 = par.cmd.ExecuteReader()
                    If myReader123.Read Then
                        contenuto = Replace(contenuto, "$luogonascitaextra$", par.IfNull(myReader123("NOME"), ""))
                        If par.IfNull(myReader123("SIGLA"), "") = "E" Then
                            Nazionalita = "EXTRA"
                        End If
                    End If
                    myReader123.Close()

                End If

                contenuto = Replace(contenuto, "$comuneresidenza$", par.IfNull(myReader("luogo_residenza"), ""))
                contenuto = Replace(contenuto, "$indirizzoresidenza$", par.IfNull(myReader("tipo_indirizzo"), "") & " " & par.IfNull(myReader("indirizzo_res"), ""))

                contenuto = Replace(contenuto, "$datanascita$", par.FormattaData(par.IfNull(myReader("data_nascita"), "")))

                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReader("civico_res"), "___"))
                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReader("piano_res"), "___"))
                contenuto = Replace(contenuto, "$interno$", par.IfNull(myReader("interno_res"), "___"))
                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReader("scala_res"), "_____"))
                contenuto = Replace(contenuto, "$telefono$", par.IfNull(myReader("dnte_telefono"), "---"))
                contenuto = Replace(contenuto, "$mail$", par.IfNull(myReader("dnte_mail"), "---"))
                Dim ScalaCom As String = par.IfNull(myReader("scala_COM"), "")
                Dim InternoCom As String = par.IfNull(myReader("INTERNO_COM"), "")

                If ScalaCom <> "" Then
                    ScalaCom = " Scala " & ScalaCom
                End If
                If InternoCom <> "" Then
                    InternoCom = " Interno " & InternoCom
                End If
                If par.IfNull(myReader("IND_COM"), "") <> "" Then
                    contenuto = Replace(contenuto, "$indirizzocomunicazioni$", par.IfNull(myReader("TIND_COM"), "") & " " & par.IfNull(myReader("IND_COM"), "") & " " & par.IfNull(myReader("CIV_COM"), "") & " " & ScalaCom & " " & InternoCom & " - " & par.IfNull(myReader("LUOGO_COM"), ""))
                Else
                    contenuto = Replace(contenuto, "$indirizzocomunicazioni$", "")
                End If


                Dim ComponentiNucleo As String = "" '= "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 14pt'><td style='border-color: #000000; border-width: 1px;  border-bottom-style: solid; border-left-style: solid;'></td><td style='border-color: #000000; border-width: 1px;  border-bottom-style: solid; border-left-style: solid;'></td><td style=' border-bottom-style: solid; border-left-style: solid; border-width: 1px; border-color: #000000'></td><td style=' border-bottom-style: solid; border-left-style: solid; border-width: 1px; border-color: #000000'></td><td style='border-style: solid; border-width: 1px; border-color: #000000'></td></tr>"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "SELECT (SELECT nome FROM COMUNI_NAZIONI WHERE ID=NVL(COMP_NUCLEO.id_comu_res,0)) AS luogo_residenza,T_TIPO_PARENTELA.descrizione AS tipo_parentela,(SELECT nome FROM COMUNI_NAZIONI WHERE COD=SUBSTR(NVL(COMP_NUCLEO.COD_FISCALE,'0000'),12,4)) AS luogo_nascita,COMP_NUCLEO.* FROM COMP_NUCLEO,T_TIPO_PARENTELA WHERE T_TIPO_PARENTELA.cod=grado_parentela AND progr<>0 AND ID_domanda=" & IdDomanda & " ORDER BY progr ASC"
                myReader1 = par.cmd.ExecuteReader()
                Do While myReader1.Read

                    ComponentiNucleo = ComponentiNucleo & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 14pt'><td style='border-color: #000000; border-width: 1px;  border-bottom-style: solid; border-left-style: solid;'>" & par.IfNull(myReader1("cognome"), "") & " " & par.IfNull(myReader1("nome"), "") & "</td><td style='border-color: #000000; border-width: 1px;  border-bottom-style: solid; border-left-style: solid;'>" & par.IfNull(myReader1("cod_fiscale"), "") & "</td><td style=' border-bottom-style: solid; border-left-style: solid; border-width: 1px; border-color: #000000'>" & par.IfNull(myReader1("luogo_nascita"), "") & " " & par.FormattaData(par.IfNull(myReader1("data_nascita"), "")) & "</td><td style=' border-bottom-style: solid; border-left-style: solid; border-color: #000000; border-right-style: solid; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px;'>" & par.IfNull(myReader1("tipo_parentela"), "") & "</td><td style='border-color: #000000; border-right-style: solid; border-bottom-style: solid; border-right-width: 1px; border-bottom-width: 1px;'>" & par.IfNull(myReader1("luogo_residenza"), "") & "</td></tr>"
                Loop
                myReader1.Close()
                contenuto = Replace(contenuto, "$componentinucleo$", ComponentiNucleo)


                Select Case par.IfNull(myReader("FL_CITTADINANZA"), "")
                    Case 1 'CITTADINO ITALIANO
                        contenuto = Replace(contenuto, "$punto2$", "<img alt='' src='block_checked.png' />")
                        contenuto = Replace(contenuto, "$stato$", "----")
                        contenuto = Replace(contenuto, "$stato1$", "----")
                    Case 2 ' COMUNTA EUROPEA
                        contenuto = Replace(contenuto, "$punto2$", "<img alt='' src='block.png' />")
                        contenuto = Replace(contenuto, "$stato1$", "----")

                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                        par.cmd.CommandText = "select nome from comuni_nazioni where id=" & par.IfNull(myReader("id_stato_estero"), -1)
                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            contenuto = Replace(contenuto, "$stato$", par.IfNull(myReader2("nome"), ""))
                        Else
                            contenuto = Replace(contenuto, "$stato$", "---")
                        End If
                        myReader2.Close()


                    Case 3 ' EXTRACOMUNITARIO
                        contenuto = Replace(contenuto, "$punto2$", "<img alt='' src='block.png' />")
                        contenuto = Replace(contenuto, "$stato$", "----")
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                        par.cmd.CommandText = "select nome from comuni_nazioni where id=" & par.IfNull(myReader("id_stato_estero"), -1)
                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            contenuto = Replace(contenuto, "$stato1$", par.IfNull(myReader2("nome"), ""))
                        Else
                            contenuto = Replace(contenuto, "$stato1$", "---")
                        End If
                        myReader2.Close()
                End Select

                contenuto = Replace(contenuto, "$numeropermesso$", par.IfNull(myReader("permesso_sogg_n"), "---"))

                Select Case par.IfNull(myReader("fl_coniugato"), 0) '??? possibili valori. come mai fl_coniugato=0 e ci sono i dati del coniuge?
                    Case 0
                        contenuto = Replace(contenuto, "$coniuge$", "---")
                        contenuto = Replace(contenuto, "$dataconiuge$", "---")

                        contenuto = Replace(contenuto, "$fconiuge$", par.IfNull(myReader("cognome_coniuge"), "---") & " " & par.IfNull(myReader("nome_coniuge"), "---"))

                        If par.IfNull(myReader("data_nas_coniuge"), "") <> "" Then
                            contenuto = Replace(contenuto, "$datanascitaconiuge$", par.FormattaData(par.IfNull(myReader("data_nas_coniuge"), "---")))
                        Else
                            contenuto = Replace(contenuto, "$datanascitaconiuge$", "---")
                        End If

                        'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                        'par.cmd.CommandText = "select nome from comuni_nazioni where id=" & par.IfNull(myReader("id_comu_nas_coniuge"), -1)
                        'myReader2 = par.cmd.ExecuteReader()
                        'If myReader2.Read Then
                        '    contenuto = Replace(contenuto, "$luogonascitaconiuge$", par.IfNull(myReader2("nome"), ""))
                        'Else
                        '    contenuto = Replace(contenuto, "$luogonascitaconiuge$", "---")
                        'End If

                        contenuto = Replace(contenuto, "$luogonascitaconiuge$", "---")

                        ' myReader2.Close()


                    Case 1
                        contenuto = Replace(contenuto, "$coniuge$", UCase(par.IfNull(myReader("cognome_coniuge"), "---") & " " & par.IfNull(myReader("nome_coniuge"), "---")))
                        contenuto = Replace(contenuto, "$dataconiuge$", par.FormattaData(par.IfNull(myReader("data_matrimonio"), "---")))

                        contenuto = Replace(contenuto, "$luogonascitaconiuge$", "---")
                        contenuto = Replace(contenuto, "$datanascitaconiuge$", "---")
                        contenuto = Replace(contenuto, "$fconiuge$", "---")

                End Select

                Select Case par.IfNull(myReader("fl_lavoro"), 0) '??? possibili valori. 
                    Case 0
                        contenuto = Replace(contenuto, "$comuneservizio$", "---")
                        contenuto = Replace(contenuto, "$datainsediamento$", "---")
                        contenuto = Replace(contenuto, "$insediamento$", "---")
                        contenuto = Replace(contenuto, "$pressolavoro$", UCase(par.IfNull(myReader("societa"), "")))
                        contenuto = Replace(contenuto, "$qualitalavoro$", UCase(par.IfNull(myReader("mansione"), "")))
                        contenuto = Replace(contenuto, "$comunelavoro$", "---")

                    Case 1
                        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader
                        par.cmd.CommandText = "select nome from comuni_nazioni where id=" & par.IfNull(myReader("id_comu_lavoro"), -1)
                        myReader3 = par.cmd.ExecuteReader()
                        If myReader3.Read Then
                            contenuto = Replace(contenuto, "$comunelavoro$", UCase(par.IfNull(myReader3("nome"), "")))
                        Else
                            contenuto = Replace(contenuto, "$comunelavoro$", "---")
                        End If
                        myReader3.Close()

                        contenuto = Replace(contenuto, "$comuneservizio$", "---")
                        contenuto = Replace(contenuto, "$datainsediamento$", "---")
                        contenuto = Replace(contenuto, "$insediamento$", "---")

                        contenuto = Replace(contenuto, "$pressolavoro$", UCase(par.IfNull(myReader("societa"), "")))
                        contenuto = Replace(contenuto, "$qualitalavoro$", UCase(par.IfNull(myReader("mansione"), "")))

                    Case 2 '???
                        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader
                        par.cmd.CommandText = "select nome from comuni_nazioni where id=" & par.IfNull(myReader("id_comu_lavoro"), -1)
                        myReader3 = par.cmd.ExecuteReader()
                        If myReader3.Read Then
                            contenuto = Replace(contenuto, "$comuneservizio$", par.IfNull(myReader3("nome"), ""))
                        Else
                            contenuto = Replace(contenuto, "$comuneservizio$", "---")
                        End If
                        myReader3.Close()

                        contenuto = Replace(contenuto, "$comunelavoro$", "---")
                        contenuto = Replace(contenuto, "$pressolavoro$", "---")
                        contenuto = Replace(contenuto, "$qualitalavoro$", "---")

                        contenuto = Replace(contenuto, "$insediamento$", par.IfNull(myReader("societa"), ""))
                        contenuto = Replace(contenuto, "$datainsediamento$", par.FormattaData(par.IfNull(myReader("data_lav_fut"), "")))


                End Select

                Select Case par.IfNull(myReader("fl_lav_emigrato"), 0) '??? possibili valori. 
                    Case 0
                        contenuto = Replace(contenuto, "$img1$", "<img alt='' src='block.png' />")
                        contenuto = Replace(contenuto, "$img2$", "<img alt='' src='block.png' />")
                        contenuto = Replace(contenuto, "$comunelavoroestero$", "---")
                        contenuto = Replace(contenuto, "$datarientro$", "---")

                    Case 1
                        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader
                        par.cmd.CommandText = "select nome from comuni_nazioni where id=" & par.IfNull(myReader("id_stato_l_emigrato"), -1)
                        myReader3 = par.cmd.ExecuteReader()
                        If myReader3.Read Then
                            contenuto = Replace(contenuto, "$comunelavoroestero$", par.IfNull(myReader3("nome"), ""))
                        Else
                            contenuto = Replace(contenuto, "$comunelavoroestero$", "---")
                        End If
                        myReader3.Close()

                        contenuto = Replace(contenuto, "$img1$", "<img alt='' src='block_checked.png' />")
                        contenuto = Replace(contenuto, "$img2$", "<img alt='' src='block.png' />")
                        contenuto = Replace(contenuto, "$datarientro$", "---")

                    Case 2
                        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader
                        par.cmd.CommandText = "select nome from comuni_nazioni where id=" & par.IfNull(myReader("id_stato_l_emigrato"), -1)
                        myReader3 = par.cmd.ExecuteReader()
                        If myReader3.Read Then
                            contenuto = Replace(contenuto, "$comunelavoroestero$", par.IfNull(myReader3("nome"), ""))
                        Else
                            contenuto = Replace(contenuto, "$comunelavoroestero$", "---")
                        End If
                        myReader3.Close()

                        contenuto = Replace(contenuto, "$img1$", "<img alt='' src='block.png' />")
                        contenuto = Replace(contenuto, "$img2$", "<img alt='' src='block_checked.png' />")
                        contenuto = Replace(contenuto, "$datarientro$", par.FormattaData(par.IfNull(myReader("data_rientro"), "")))

                End Select


                Select Case par.IfNull(myReader("fl_coab"), 0) '??? possibili valori. 
                    Case 0
                        contenuto = Replace(contenuto, "$datacoabitazione$", "---")
                        contenuto = Replace(contenuto, "$nomecoab$", "---")
                        contenuto = Replace(contenuto, "$componentinucleoCoab$", "")
                    Case 1
                        contenuto = Replace(contenuto, "$datacoabitazione$", par.FormattaData(par.IfNull(myReader("data_coabitazione"), "")))


                        Dim myReaderC As Oracle.DataAccess.Client.OracleDataReader
                        par.cmd.CommandText = "select * from COMP_NUCLEO_COABITAZIONE where progr=0 and id_domanda=" & IdDomanda


                        Dim ComponentiNucleoCoab As String = "" '= "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 14pt'><td style='border-color: #000000; border-width: 1px;  border-bottom-style: solid; border-left-style: solid;'></td><td style='border-color: #000000; border-width: 1px;  border-bottom-style: solid; border-left-style: solid;'></td><td style=' border-bottom-style: solid; border-left-style: solid; border-width: 1px; border-color: #000000'></td><td style=' border-bottom-style: solid; border-left-style: solid; border-width: 1px; border-color: #000000'></td><td style='border-style: solid; border-width: 1px; border-color: #000000'></td></tr>"
                        myReaderC = par.cmd.ExecuteReader()
                        If myReaderC.Read Then
                            contenuto = Replace(contenuto, "$nomecoab$", par.IfNull(myReaderC("cognome"), "---") & " " & par.IfNull(myReaderC("nome"), "---"))
                        End If
                        myReaderC.Close()

                        par.cmd.CommandText = "SELECT (SELECT nome FROM COMUNI_NAZIONI WHERE ID=NVL(COMP_NUCLEO_COABITAZIONE.id_comu_res,0)) AS luogo_residenza,T_TIPO_PARENTELA.descrizione AS tipo_parentela,COMP_NUCLEO_COABITAZIONE.* FROM COMP_NUCLEO_COABITAZIONE,T_TIPO_PARENTELA WHERE T_TIPO_PARENTELA.cod=grado_parentela AND ID_domanda=" & IdDomanda & " ORDER BY progr ASC"
                        myReaderC = par.cmd.ExecuteReader()
                        Do While myReaderC.Read

                            ComponentiNucleoCoab = ComponentiNucleoCoab & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 14pt'><td style='border-color: #000000; border-width: 1px;  border-bottom-style: solid; border-left-style: solid;'>" & par.IfNull(myReaderC("cognome"), "") & " " & par.IfNull(myReaderC("nome"), "") & "</td><td style='border-color: #000000; border-width: 1px;  border-bottom-style: solid; border-left-style: solid;'>" & par.IfNull(myReaderC("cod_fiscale"), "") & "</td><td style=' border-bottom-style: solid; border-left-style: solid; border-color: #000000; border-right-style: solid; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px;'>" & par.IfNull(myReaderC("tipo_parentela"), "") & "</td><td style='border-color: #000000; border-right-style: solid; border-bottom-style: solid; border-right-width: 1px; border-bottom-width: 1px;'>" & par.IfNull(myReaderC("luogo_residenza"), "") & "</td></tr>"
                        Loop
                        myReaderC.Close()
                        contenuto = Replace(contenuto, "$componentinucleoCoab$", ComponentiNucleoCoab)

                End Select

                Select Case par.IfNull(myReader("fl_domicilio"), 0) '??? possibili valori. 
                    Case 0
                        contenuto = Replace(contenuto, "$comunedom$", "---")
                        contenuto = Replace(contenuto, "$indirizzodom$", "---")
                        contenuto = Replace(contenuto, "$civicodom$", "---")
                        contenuto = Replace(contenuto, "$annodom$", "---")
                        contenuto = Replace(contenuto, "$vanidom$", "---")
                        contenuto = Replace(contenuto, "$mqdom$", "---")
                        contenuto = Replace(contenuto, "$img1dom$", "<img alt='' src='block.png' />")
                        contenuto = Replace(contenuto, "$img2dom$", "<img alt='' src='block.png' />")

                    Case 1

                        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader
                        par.cmd.CommandText = "select nome from comuni_nazioni where id=" & par.IfNull(myReader("id_comu_dom"), -1)
                        myReader3 = par.cmd.ExecuteReader()
                        If myReader3.Read Then
                            contenuto = Replace(contenuto, "$comunedom$", UCase(par.IfNull(myReader3("nome"), "")))
                        Else
                            contenuto = Replace(contenuto, "$comunedom$", "---")
                        End If
                        myReader3.Close()
                        contenuto = Replace(contenuto, "$indirizzodom$", UCase(par.IfNull(myReader("ind_dom"), "---")))
                        contenuto = Replace(contenuto, "$civicodom$", par.IfNull(myReader("civ_dom"), "---"))
                        contenuto = Replace(contenuto, "$annodom$", par.FormattaData(par.IfNull(myReader("data_dom"), "")))
                        contenuto = Replace(contenuto, "$vanidom$", par.IfNull(myReader("vani_dom"), "---"))
                        contenuto = Replace(contenuto, "$mqdom$", par.IfNull(myReader("mq_dom"), "---"))

                        If par.IfNull(myReader("fl_tipo_alloggio_dom"), "0") = "0" Then
                            contenuto = Replace(contenuto, "$img1dom$", "<img alt='' src='block_checked.png' />")
                            contenuto = Replace(contenuto, "$img2dom$", "<img alt='' src='block.png' />")
                        Else
                            contenuto = Replace(contenuto, "$img2dom$", "<img alt='' src='block_checked.png' />")
                            contenuto = Replace(contenuto, "$img1dom$", "<img alt='' src='block.png' />")
                        End If
                End Select


                Select Case par.IfNull(myReader("FL_NON_TIT"), 0)
                    Case 1 'NON TITOLARE
                        contenuto = Replace(contenuto, "$punto13$", "<img alt='' src='block_checked.png' />")

                    Case 0 ' TITOLARE, QUINDI ESCLUSO
                        contenuto = Replace(contenuto, "$punto13$", "<img alt='' src='block.png' />")
                End Select

                Select Case par.IfNull(myReader("FL_NON_LOC"), 0)
                    Case 1 'NON TITOLARE
                        contenuto = Replace(contenuto, "$punto14$", "<img alt='' src='block_checked.png' />")

                    Case 0 ' TITOLARE, QUINDI ESCLUSO
                        contenuto = Replace(contenuto, "$punto14$", "<img alt='' src='block.png' />")
                End Select



                Dim myReaderSIT As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "SELECT * from SIT_BANDI WHERE ID_domanda=" & IdDomanda
                myReaderSIT = par.cmd.ExecuteReader()
                If myReaderSIT.Read Then
                    Select Case par.IfNull(myReaderSIT("S1_1"), "")
                        Case "A"
                            contenuto = Replace(contenuto, "$a11$", "<img alt='' src='block_checked.png' />")
                            contenuto = Replace(contenuto, "$a12$", "<img alt='' src='block.png' />")
                            contenuto = Replace(contenuto, "$a13$", "<img alt='' src='block.png' />")
                        Case "B"
                            contenuto = Replace(contenuto, "$a12$", "<img alt='' src='block_checked.png' />")
                            contenuto = Replace(contenuto, "$a11$", "<img alt='' src='block.png' />")
                            contenuto = Replace(contenuto, "$a13$", "<img alt='' src='block.png' />")
                        Case "C"
                            contenuto = Replace(contenuto, "$a13$", "<img alt='' src='block_checked.png' />")
                            contenuto = Replace(contenuto, "$a12$", "<img alt='' src='block.png' />")
                            contenuto = Replace(contenuto, "$a11$", "<img alt='' src='block.png' />")
                        Case Else
                            contenuto = Replace(contenuto, "$a11$", "<img alt='' src='block.png' />")
                            contenuto = Replace(contenuto, "$a12$", "<img alt='' src='block.png' />")
                            contenuto = Replace(contenuto, "$a13$", "<img alt='' src='block.png' />")
                    End Select

                    Select Case par.IfNull(myReaderSIT("S2_1"), "")
                        Case "A"
                            contenuto = Replace(contenuto, "$a21$", "<img alt='' src='block_checked.png' />")
                            contenuto = Replace(contenuto, "$a22$", "<img alt='' src='block.png' />")
                            contenuto = Replace(contenuto, "$a23$", "<img alt='' src='block.png' />")
                        Case "B"
                            contenuto = Replace(contenuto, "$a22$", "<img alt='' src='block_checked.png' />")
                            contenuto = Replace(contenuto, "$a21$", "<img alt='' src='block.png' />")
                            contenuto = Replace(contenuto, "$a23$", "<img alt='' src='block.png' />")
                        Case "C"
                            contenuto = Replace(contenuto, "$a23$", "<img alt='' src='block_checked.png' />")
                            contenuto = Replace(contenuto, "$a22$", "<img alt='' src='block.png' />")
                            contenuto = Replace(contenuto, "$a21$", "<img alt='' src='block.png' />")
                        Case Else
                            contenuto = Replace(contenuto, "$a21$", "<img alt='' src='block.png' />")
                            contenuto = Replace(contenuto, "$a22$", "<img alt='' src='block.png' />")
                            contenuto = Replace(contenuto, "$a23$", "<img alt='' src='block.png' />")
                    End Select

                    Select Case par.IfNull(myReaderSIT("S3_1"), "")
                        Case "S"
                            contenuto = Replace(contenuto, "$a3$", "<img alt='' src='block_checked.png' />")
                        Case Else
                            contenuto = Replace(contenuto, "$a3$", "<img alt='' src='block.png' />")
                    End Select

                    Select Case par.IfNull(myReaderSIT("S4_1"), "")
                        Case "A"
                            contenuto = Replace(contenuto, "$a41$", "<img alt='' src='block_checked.png' />")
                            contenuto = Replace(contenuto, "$a42$", "<img alt='' src='block.png' />")
                        Case "B"
                            contenuto = Replace(contenuto, "$a42$", "<img alt='' src='block_checked.png' />")
                            contenuto = Replace(contenuto, "$a41$", "<img alt='' src='block.png' />")
                        Case Else
                            contenuto = Replace(contenuto, "$a41$", "<img alt='' src='block.png' />")
                            contenuto = Replace(contenuto, "$a42$", "<img alt='' src='block.png' />")
                    End Select




                    Select Case par.IfNull(myReaderSIT("S5_1"), "")
                        Case "S"
                            contenuto = Replace(contenuto, "$a5$", "<img alt='' src='block_checked.png' />")
                        Case Else
                            contenuto = Replace(contenuto, "$a5$", "<img alt='' src='block.png' />")
                    End Select

                    Select Case par.IfNull(myReaderSIT("S6_1"), "")
                        Case "S"
                            contenuto = Replace(contenuto, "$a6$", "<img alt='' src='block_checked.png' />")
                        Case Else
                            contenuto = Replace(contenuto, "$a6$", "<img alt='' src='block.png' />")
                    End Select

                    Select Case par.IfNull(myReaderSIT("S7_1"), "")
                        Case "S"
                            contenuto = Replace(contenuto, "$b1$", "<img alt='' src='block_checked.png' />")
                        Case Else
                            contenuto = Replace(contenuto, "$b1$", "<img alt='' src='block.png' />")
                    End Select

                    Select Case par.IfNull(myReaderSIT("S8_1"), "")
                        Case "S"
                            contenuto = Replace(contenuto, "$b2$", "<img alt='' src='block_checked.png' />")
                        Case Else
                            contenuto = Replace(contenuto, "$b2$", "<img alt='' src='block.png' />")
                    End Select

                    Select Case par.IfNull(myReaderSIT("S9_1"), "")
                        Case "S"
                            contenuto = Replace(contenuto, "$b3$", "<img alt='' src='block_checked.png' />")
                        Case Else
                            contenuto = Replace(contenuto, "$b3$", "<img alt='' src='block.png' />")
                    End Select

                    Select Case par.IfNull(myReaderSIT("S10_1"), "")
                        Case "A"
                            contenuto = Replace(contenuto, "$b41$", "<img alt='' src='block_checked.png' />")
                            contenuto = Replace(contenuto, "$b42$", "<img alt='' src='block.png' />")
                        Case "B"
                            contenuto = Replace(contenuto, "$b42$", "<img alt='' src='block_checked.png' />")
                            contenuto = Replace(contenuto, "$b41$", "<img alt='' src='block.png' />")
                        Case Else
                            contenuto = Replace(contenuto, "$b41$", "<img alt='' src='block.png' />")
                            contenuto = Replace(contenuto, "$b42$", "<img alt='' src='block.png' />")
                    End Select

                    Select Case par.IfNull(myReaderSIT("S11_1"), "")
                        Case "S"
                            contenuto = Replace(contenuto, "$b5$", "<img alt='' src='block_checked.png' />")
                        Case Else
                            contenuto = Replace(contenuto, "$b5$", "<img alt='' src='block.png' />")
                    End Select

                    Select Case par.IfNull(myReaderSIT("S12_1"), "")
                        Case "S"
                            contenuto = Replace(contenuto, "$b6$", "<img alt='' src='block_checked.png' />")
                        Case Else
                            contenuto = Replace(contenuto, "$b6$", "<img alt='' src='block.png' />")
                    End Select

                    Select Case par.IfNull(myReaderSIT("S13_1"), "")
                        Case "S"
                            contenuto = Replace(contenuto, "$b7$", "<img alt='' src='block_checked.png' />")
                        Case Else
                            contenuto = Replace(contenuto, "$b7$", "<img alt='' src='block.png' />")
                    End Select

                    Select Case par.IfNull(myReaderSIT("S14_1"), "")
                        Case "S"
                            contenuto = Replace(contenuto, "$c1$", "<img alt='' src='block_checked.png' />")
                        Case Else
                            contenuto = Replace(contenuto, "$c1$", "<img alt='' src='block.png' />")
                    End Select

                    Select Case par.IfNull(myReaderSIT("S15_1"), "")
                        Case "S"
                            contenuto = Replace(contenuto, "$c2$", "<img alt='' src='block_checked.png' />")
                        Case Else
                            contenuto = Replace(contenuto, "$c2$", "<img alt='' src='block.png' />")
                    End Select

                    If Nazionalita = "EXTRA" Then

                        Select Case par.IfNull(myReader("fl_tipolavoratore"), "")
                            Case "0"
                                contenuto = Replace(contenuto, "$rd1$", "<img alt='' src='block_checked.png' />")
                                contenuto = Replace(contenuto, "$rd2$", "<img alt='' src='block.png' />")
                                contenuto = Replace(contenuto, "$rd3$", "<img alt='' src='block.png' />")
                                contenuto = Replace(contenuto, "$tipolavoro$", par.IfNull(myReader("mansione_extrace"), ""))
                                contenuto = Replace(contenuto, "$tipolavoroaut$", "")
                                contenuto = Replace(contenuto, "$datacciaa$", "")
                                contenuto = Replace(contenuto, "$matricola$", "")
                                contenuto = Replace(contenuto, "$piva$", "")
                                contenuto = Replace(contenuto, "$cc1$", "<img alt='' src='block_NO.gif' />")
                                contenuto = Replace(contenuto, "$cc2$", "<img alt='' src='block_SI.gif' />")
                            Case "1"
                                contenuto = Replace(contenuto, "$rd1$", "<img alt='' src='block.png' />")
                                contenuto = Replace(contenuto, "$rd2$", "<img alt='' src='block_checked.png' />")
                                contenuto = Replace(contenuto, "$rd3$", "<img alt='' src='block.png' />")
                                contenuto = Replace(contenuto, "$tipolavoro$", "")
                                contenuto = Replace(contenuto, "$tipolavoroaut$", par.IfNull(myReader("mansione_extrace"), ""))
                                contenuto = Replace(contenuto, "$datacciaa$", par.FormattaData(par.IfNull(myReader("data_cciaa"), "")))
                                contenuto = Replace(contenuto, "$matricola$", par.IfNull(myReader("matricola"), ""))
                                contenuto = Replace(contenuto, "$piva$", par.IfNull(myReader("partita_iva"), ""))

                                If par.IfNull(myReader("cl_cciaa"), "") = "0" Then
                                    contenuto = Replace(contenuto, "$cc1$", "<img alt='' src='block_NO_Checked.gif' />")
                                    contenuto = Replace(contenuto, "$cc2$", "<img alt='' src='block_SI.gif' />")
                                Else
                                    contenuto = Replace(contenuto, "$cc2$", "<img alt='' src='block_NO_Checked.gif' />")
                                    contenuto = Replace(contenuto, "$cc1$", "<img alt='' src='block_SI.gif' />")
                                End If

                                contenuto = Replace(contenuto, "$cc1$", "<img alt='' src='block_NO.gif' />")
                                contenuto = Replace(contenuto, "$cc2$", "<img alt='' src='block_SI.gif' />")

                            Case "2"
                                contenuto = Replace(contenuto, "$rd1$", "<img alt='' src='block.png' />")
                                contenuto = Replace(contenuto, "$rd2$", "<img alt='' src='block.png' />")
                                contenuto = Replace(contenuto, "$rd3$", "<img alt='' src='block_checked.png' />")
                                contenuto = Replace(contenuto, "$tipolavoro$", "")
                                contenuto = Replace(contenuto, "$tipolavoroaut$", "")
                                contenuto = Replace(contenuto, "$datacciaa$", "")
                                contenuto = Replace(contenuto, "$matricola$", "")
                                contenuto = Replace(contenuto, "$piva$", "")
                                contenuto = Replace(contenuto, "$cc1$", "<img alt='' src='block_NO.gif' />")
                                contenuto = Replace(contenuto, "$cc2$", "<img alt='' src='block_SI.gif' />")
                            Case Else
                                contenuto = Replace(contenuto, "$rd1$", "<img alt='' src='block.png' />")
                                contenuto = Replace(contenuto, "$rd2$", "<img alt='' src='block.png' />")
                                contenuto = Replace(contenuto, "$rd3$", "<img alt='' src='block.png' />")
                                contenuto = Replace(contenuto, "$tipolavoro$", "")
                                contenuto = Replace(contenuto, "$tipolavoroaut$", "")
                                contenuto = Replace(contenuto, "$datacciaa$", "")
                                contenuto = Replace(contenuto, "$matricola$", "")
                                contenuto = Replace(contenuto, "$piva$", "")
                                contenuto = Replace(contenuto, "$cc1$", "<img alt='' src='block_NO.gif' />")
                                contenuto = Replace(contenuto, "$cc2$", "<img alt='' src='block_SI.gif' />")
                        End Select
                    Else
                        contenuto = Replace(contenuto, "$rd1$", "<img alt='' src='block.png' />")
                        contenuto = Replace(contenuto, "$rd2$", "<img alt='' src='block.png' />")
                        contenuto = Replace(contenuto, "$rd3$", "<img alt='' src='block.png' />")
                        contenuto = Replace(contenuto, "$tipolavoro$", "")
                        contenuto = Replace(contenuto, "$tipolavoroaut$", "")
                        contenuto = Replace(contenuto, "$datacciaa$", "")
                        contenuto = Replace(contenuto, "$matricola$", "")
                        contenuto = Replace(contenuto, "$piva$", "")
                        contenuto = Replace(contenuto, "$luogonascitaextra$", "")

                        contenuto = Replace(contenuto, "$cc1$", "<img alt='' src='block_NO.gif' />")
                        contenuto = Replace(contenuto, "$cc2$", "<img alt='' src='block_SI.gif' />")
                    End If







                Else
                    contenuto = Replace(contenuto, "$a11$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$a12$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$a13$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$a21$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$a22$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$a23$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$a3$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$a41$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$a42$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$a5$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$a6$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$a7$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$b1$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$b2$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$b3$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$b41$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$b42$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$b5$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$b6$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$b7$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$c1$", "<img alt='' src='block.png' />")
                    contenuto = Replace(contenuto, "$c2$", "<img alt='' src='block.png' />")

                End If
                myReaderSIT.Close()


                par.cmd.CommandText = "SELECT * from DOM_DOC_ALL_BANDO where ID_domanda=" & IdDomanda
                myReaderSIT = par.cmd.ExecuteReader()
                Do While myReaderSIT.Read
                    Select Case par.IfNull(myReaderSIT("id_doc_bando"), "")
                        Case "1"
                            contenuto = Replace(contenuto, "$da1$", "<img alt='' src='block_checked.png' />")
                        Case "2"
                            contenuto = Replace(contenuto, "$da2$", "<img alt='' src='block_checked.png' />")
                        Case "3"
                            contenuto = Replace(contenuto, "$da3$", "<img alt='' src='block_checked.png' />")
                        Case "4"
                            contenuto = Replace(contenuto, "$da4$", "<img alt='' src='block_checked.png' />")
                        Case "5"
                            contenuto = Replace(contenuto, "$da5$", "<img alt='' src='block_checked.png' />")
                        Case "6"
                            contenuto = Replace(contenuto, "$da61$", "<img alt='' src='block_checked.png' />")
                        Case "7"
                            contenuto = Replace(contenuto, "$da62$", "<img alt='' src='block_checked.png' />")
                    End Select
                Loop
                myReaderSIT.Close()
                contenuto = Replace(contenuto, "$da1$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$da2$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$da3$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$da4$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$da5$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$da61$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$da62$", "<img alt='' src='block.png' />")

                par.cmd.CommandText = "SELECT * from DOM_DOC_ALL_PUNTEGGIO where ID_domanda=" & IdDomanda
                myReaderSIT = par.cmd.ExecuteReader()
                Do While myReaderSIT.Read
                    Select Case par.IfNull(myReaderSIT("id_doc_punteggio"), "")
                        Case "1"
                            contenuto = Replace(contenuto, "$db1$", "<img alt='' src='block_checked.png' />")
                        Case "2"
                            contenuto = Replace(contenuto, "$db21$", "<img alt='' src='block_checked.png' />")
                        Case "3"
                            contenuto = Replace(contenuto, "$db22$", "<img alt='' src='block_checked.png' />")
                        Case "4"
                            contenuto = Replace(contenuto, "$db3$", "<img alt='' src='block_checked.png' />")
                        Case "5"
                            contenuto = Replace(contenuto, "$db4$", "<img alt='' src='block_checked.png' />")
                        Case "6"
                            contenuto = Replace(contenuto, "$db5$", "<img alt='' src='block_checked.png' />")
                        Case "7"
                            contenuto = Replace(contenuto, "$db6$", "<img alt='' src='block_checked.png' />")
                        Case "8"
                            contenuto = Replace(contenuto, "$db7$", "<img alt='' src='block_checked.png' />")
                        Case "9"
                            contenuto = Replace(contenuto, "$db8$", "<img alt='' src='block_checked.png' />")
                        Case "10"
                            contenuto = Replace(contenuto, "$db9$", "<img alt='' src='block_checked.png' />")
                        Case "11"
                            contenuto = Replace(contenuto, "$db10$", "<img alt='' src='block_checked.png' />")
                        Case "12"
                            contenuto = Replace(contenuto, "$db11$", "<img alt='' src='block_checked.png' />")
                        Case "13"
                            contenuto = Replace(contenuto, "$db12$", "<img alt='' src='block_checked.png' />")
                        Case "14"
                            contenuto = Replace(contenuto, "$db13$", "<img alt='' src='block_checked.png' />")
                        Case "15"
                            contenuto = Replace(contenuto, "$db14$", "<img alt='' src='block_checked.png' />")
                        Case "16"
                            contenuto = Replace(contenuto, "$db15$", "<img alt='' src='block_checked.png' />")
                    End Select
                Loop
                myReaderSIT.Close()

                contenuto = Replace(contenuto, "$db1$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db21$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db22$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db3$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db4$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db5$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db6$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db7$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db8$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db9$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db10$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db11$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db12$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db13$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db14$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db15$", "<img alt='' src='block.png' />")


                'ALLEGATO - REDDITI

                Dim CompRedditi As String = ""
                Dim occupazione As String = ""
                Dim tot_riga As Double = 0
                Dim tot_redditi As Double = 0

                Dim myReaderR As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "SELECT DOMANDE_REDDITI.*,T_TIPO_PARENTELA.descrizione AS tipo_parentela,COMP_NUCLEO.* FROM DOMANDE_REDDITI,COMP_NUCLEO,T_TIPO_PARENTELA WHERE COMP_NUCLEO.ID=DOMANDE_REDDITI.ID_COMPONENTE (+) AND T_TIPO_PARENTELA.cod=grado_parentela AND comp_nucleo.ID_domanda=" & IdDomanda & " ORDER BY comp_nucleo.progr ASC"
                myReaderR = par.cmd.ExecuteReader()
                Do While myReaderR.Read
                    tot_riga = par.IfNull(myReaderR("dip_pens"), 0) + par.IfNull(myReaderR("autonomo_ass"), 0) + par.IfNull(myReaderR("emolumenti"), 0) + par.IfNull(myReaderR("dom_ag_fab"), 0)
                    tot_redditi = tot_redditi + tot_riga
                    If par.IfNull(myReaderR("fl_occupazione"), "0") = "0" Then
                        occupazione = "NO"
                    Else
                        occupazione = "SI"
                    End If
                    CompRedditi = CompRedditi & "<tr><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>" & par.IfNull(myReaderR("COGNOME"), "&nbsp;") & " " & par.IfNull(myReaderR("NOME"), "&nbsp;") & "</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>" & par.FormattaData(par.IfNull(myReaderR("data_nascita"), "")) & "</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>" & par.IfNull(myReaderR("tipo_parentela"), "&nbsp;") & "</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>" & par.IfNull(myReaderR("cod_fiscale"), "&nbsp;") & "</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>" & par.IfNull(myReaderR("dip_pens"), "&nbsp;") & "</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>" & par.IfNull(myReaderR("autonomo_ass"), "&nbsp;") & "</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>" & par.IfNull(myReaderR("dom_ag_fab"), "&nbsp;") & "</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>" & par.IfNull(myReaderR("emolumenti"), "&nbsp;") & "</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>" & occupazione & "</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-right-style: Solid; border-bottom-style: Solid; border-left-style: Solid;'>" & tot_riga & "</td></tr>"
                Loop
                myReaderR.Close()
                CompRedditi = CompRedditi & "<tr><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>&nbsp;</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>&nbsp;</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>&nbsp;</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>&nbsp;</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>&nbsp;</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>&nbsp;</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>&nbsp;</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>&nbsp;</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-bottom-style: Solid; border-left-style: Solid;'>TOTALE</td><td style='border-width: 1px; border-color: #000000; text-align: left; border-right-style: Solid; border-bottom-style: Solid; border-left-style: Solid;'>" & tot_redditi & "</td></tr>"
                contenuto = Replace(contenuto, "$compredditi$", CompRedditi)
                contenuto = Replace(contenuto, "$totalereddito$", tot_redditi)


            Else
                contenuto = Replace(contenuto, "$a11$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$a12$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$a13$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$a21$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$a22$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$a23$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$a3$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$a41$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$a42$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$a5$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$a6$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$a7$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$b1$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$b2$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$b3$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$b41$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$b42$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$b5$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$b6$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$b7$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$c1$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$c2$", "<img alt='' src='block.png' />")

                contenuto = Replace(contenuto, "$datapresentazione$", "")







                contenuto = Replace(contenuto, "$totalereddito$", "")

                contenuto = Replace(contenuto, "$compredditi$", "")
                contenuto = Replace(contenuto, "$annoreddito$", "")
                contenuto = Replace(contenuto, "$scala$", "")

                contenuto = Replace(contenuto, "$componentinucleo$", "")
                contenuto = Replace(contenuto, "$datarientro$", "")
                contenuto = Replace(contenuto, "$componentinucleoCoab$", "")

                contenuto = Replace(contenuto, "$punto13$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$punto14$", "<img alt='' src='block.png' />")

                contenuto = Replace(contenuto, "$comunedom$", "")
                contenuto = Replace(contenuto, "$indirizzodom$", "")
                contenuto = Replace(contenuto, "$annodom$", "")
                contenuto = Replace(contenuto, "$vanidom$", "")
                contenuto = Replace(contenuto, "$mqdom$", "")
                contenuto = Replace(contenuto, "$datacoabitazione$", "")
                contenuto = Replace(contenuto, "$nomecoab$", "")
                contenuto = Replace(contenuto, "$img1dom$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$img2dom$", "<img alt='' src='block.png' />")

                contenuto = Replace(contenuto, "$img1$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$img2$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$codicefiscale$", "")
                contenuto = Replace(contenuto, "$intestatario$", "")
                contenuto = Replace(contenuto, "$luogonascita$", "")
                contenuto = Replace(contenuto, "$provincianascita$", "")
                contenuto = Replace(contenuto, "$datanascita$", "")
                contenuto = Replace(contenuto, "$comuneresidenza$", "")
                contenuto = Replace(contenuto, "$indirizzoresidenza$", "")
                contenuto = Replace(contenuto, "$civico$", "")
                contenuto = Replace(contenuto, "$piano$", "")
                contenuto = Replace(contenuto, "$interno$", "")
                contenuto = Replace(contenuto, "$telefono$", "")
                contenuto = Replace(contenuto, "$mail$", "")
                contenuto = Replace(contenuto, "$numerobando$", "")
                contenuto = Replace(contenuto, "$stato$", "")
                contenuto = Replace(contenuto, "$stato1$", "")
                contenuto = Replace(contenuto, "$numeropermesso$", "")
                contenuto = Replace(contenuto, "$coniuge$", "")
                contenuto = Replace(contenuto, "$dataconiuge$", "")
                contenuto = Replace(contenuto, "$fconiuge$", "")
                contenuto = Replace(contenuto, "$luogonascitaconiuge$", "")
                contenuto = Replace(contenuto, "$datanascitaconiuge$", "")
                contenuto = Replace(contenuto, "$fconiuge$", "")
                contenuto = Replace(contenuto, "$fconiuge$", "")


                contenuto = Replace(contenuto, "$comunelavoro$", "")
                contenuto = Replace(contenuto, "$pressolavoro$", "")
                contenuto = Replace(contenuto, "$qualitalavoro$", "")
                contenuto = Replace(contenuto, "$comuneservizio$", "")
                contenuto = Replace(contenuto, "$insediamento$", "")
                contenuto = Replace(contenuto, "$datainsediamento$", "")
                contenuto = Replace(contenuto, "$comunelavoroestero$", "")

                contenuto = Replace(contenuto, "$da1$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$da2$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$da3$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$da4$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$da5$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$da61$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$da62$", "<img alt='' src='block.png' />")

                contenuto = Replace(contenuto, "$db1$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db21$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db22$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db3$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db4$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db5$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db6$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db7$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db8$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db9$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db10$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db11$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db12$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db13$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db14$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$db15$", "<img alt='' src='block.png' />")


                contenuto = Replace(contenuto, "$indirizzocomunicazioni$", "")

                contenuto = Replace(contenuto, "$nazionalita$", "")
                contenuto = Replace(contenuto, "$rd1$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$rd2$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$rd3$", "<img alt='' src='block.png' />")
                contenuto = Replace(contenuto, "$datacciaa$", "")
                contenuto = Replace(contenuto, "$matricola$", "")
                contenuto = Replace(contenuto, "$piva$", "")
                contenuto = Replace(contenuto, "$tipolavoroaut$", "")
                contenuto = Replace(contenuto, "$tipolavoro$", "")

                contenuto = Replace(contenuto, "$cc1$", "<img alt='' src='block_NO.gif' />")
                contenuto = Replace(contenuto, "$cc2$", "<img alt='' src='block_SI.gif' />")




                End If
            myReader.Close()



            Dim NomeFile1 As String = Format(Now, "yyyyMMdd") & "_000_" & IdDomanda


            'scrivo il nuovo modulo compilato
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\BANDI_ERP\STAMPE\") & NomeFile1 & ".htm", False, System.Text.Encoding.Default)
            sr.WriteLine(contenuto)
            sr.Close()

            Dim url As String = Server.MapPath("..\ALLEGATI\BANDI_ERP\STAMPE\") & NomeFile1
            Dim url1 As String = Server.MapPath("..\ALLEGATI\BANDI_ERP\") & NomeFile1

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

            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 10
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 1
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False

            pdfConverter1.SavePdfFromUrlToFile(url & ".htm", url & ".pdf")
            pdfConverter1.SavePdfFromUrlToFile(url & ".htm", url1 & ".pdf")
            IO.File.Delete(url & ".htm")

            Dim fileDescrizione As String = url1 & "_DESCRIZIONE.txt"
            Dim zipfic As String = url1 & ".zip"

            sr = New StreamWriter(fileDescrizione, False, System.Text.Encoding.Default)
            sr.WriteLine("Data del documento:" & Format(Now, "dd/MM/yyyy") & vbCrLf & "DOMANDA DI BANDO")
            sr.Close()

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            Dim strFile As String
            strFile = url1 & ".pdf"

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
            File.Delete(strFile)



            strFile = fileDescrizione
            strmFile = File.OpenRead(strFile)
            Dim abyBuffer12(Convert.ToInt32(strmFile.Length - 1)) As Byte
            strmFile.Read(abyBuffer12, 0, abyBuffer12.Length)
            Dim sFile12 As String = Path.GetFileName(strFile)
            theEntry = New ZipEntry(sFile12)
            fi = New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer12)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer12, 0, abyBuffer12.Length)
            File.Delete(strFile)


            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()

            Response.Redirect("..\ALLEGATI\BANDI_ERP\STAMPE\" & NomeFile1 & ".pdf")



            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:Stampa Domanda Bando" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub
End Class
