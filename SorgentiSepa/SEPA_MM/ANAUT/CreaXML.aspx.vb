Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums


Partial Class ANAUT_CreaXML
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public XMLError As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim smNomeFile As String
        Dim MiaSHTML As String
        Dim ElencoFile() As String
        Dim I As Long
        Dim J As Long
        Dim POS As Integer
        Dim MIOCOLORE As String

        Try

            If Session.Item("OPERATORE") = "" Then
                Response.Redirect("~/AccessoNegato.htm", True)
                Exit Sub
            End If


            smNomeFile = Session.Item("ID_CAF") & "_"
            POS = Len(smNomeFile) + 1

            MiaSHTML = "</br></br></br></br></br></br><p><b><font face='Arial' size='2'>Elenco file già creati</font></b></p><table border='0' cellpadding='1' cellspacing='1' width='600px'>" & vbCrLf
            MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='250px'><font face='Arial' size='2'>Nome del File</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='250px'><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='100px'><font face='Arial' size='2'><p align='center'>Download</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

            I = 0
            MIOCOLORE = "#CCFFFF"
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("EXPORT/"), FileIO.SearchOption.SearchTopLevelOnly, smNomeFile & "*.zip")
                ReDim Preserve ElencoFile(I)
                ElencoFile(I) = foundFile
                I = I + 1

            Next

            Dim kk As Long
            Dim jj As Long
            Dim scambia

            For kk = 0 To I - 2
                For jj = kk + 1 To I - 1
                    If CLng(Mid(RicavaFile(ElencoFile(kk)), POS, 14)) < CLng(Mid(RicavaFile(ElencoFile(jj)), POS, 14)) Then
                        scambia = ElencoFile(kk)
                        ElencoFile(kk) = ElencoFile(jj)
                        ElencoFile(jj) = scambia
                    End If
                Next
            Next


            If I > 0 Then
                For J = 0 To I - 1
                    MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='250px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'>" & RicavaFile(ElencoFile(J)) & "</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='250px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & Mid(RicavaFile(ElencoFile(J)), POS + 6, 2) & "/" & Mid(RicavaFile(ElencoFile(J)), POS + 4, 2) & "/" & Mid(RicavaFile(ElencoFile(J)), POS, 4) & "</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><p align='center'><a href='Allegati_1.aspx?NOME=" & RicavaFile(ElencoFile(J)) & "&EXT=ZIP' target='_blank'><img border='0' src='../ImmMaschere/MenuTopDownload.gif'></a></font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                    If MIOCOLORE = "#CCFFFF" Then
                        MIOCOLORE = "#FFFFCC"
                    Else
                        MIOCOLORE = "#CCFFFF"
                    End If
                    If J = 10 Then Exit For
                Next J
            End If
            MiaSHTML = MiaSHTML & "</table>" & vbCrLf
            Response.Write(MiaSHTML)
            ImageButton1.Attributes.Add("OnClick", "javascript:Attendi();")

        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write(ex.ToString)
        End Try
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

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Dim StringaDati As String
        Dim StringaDati1 As String
        Dim StringaDati2 As String
        Dim StringaDati3 As String

        Dim lmNumDichiarazioni As Long
        Dim smNomeFile As String
        Dim smAppoggio As String
        Dim smAppoggio1 As String
        Dim PROV_NAS As String
        Dim PROV_RES As String
        Dim TIPO_PARENTE As String

        Dim QUADRO_C2 As String
        Dim QUADRO_C1 As String
        Dim QUADRO_D As String
        Dim TotalePatrimonioMobiliare As Long
        Dim TIP_DETR As String = ""
        Dim COD_INTERMEDIARIO As String
        Dim TIPO_IMM As String = ""
        Dim COMP As Integer
        Dim tot_spese As Long

        Dim k As Long
        Dim kk As Long

        Dim CodiceASL As String
        Dim NomeGestore As String

        Dim veetDich(1000) As String
        Dim CategoriaCatastale As String


        Try
            par.OracleConn.Open()

            smAppoggio = Format(Now, "yyyyMMddHHmmss")
            smAppoggio1 = Format(Now, "HHmmss")
            smNomeFile = Session.Item("ID_CAF") & "_" & smAppoggio '& smAppoggio1
            Select Case Session.Item("ID_CAF")
                Case "10"
                    NomeGestore = "GEFI"
                Case "11"
                    NomeGestore = "PIRELLI"
                Case "12"
                    NomeGestore = "ROMEO"
                Case Else
                    NomeGestore = "COMUNE DI MILANO"
            End Select

            Dim ID_BANDO As Long

            Dim j As Long
            j = 1




            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT ID FROM UTENZA_BANDI WHERE STATO=1"
            Dim myRec55 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myRec55.Read Then
                ID_BANDO = par.IfNull(myRec55(0), 0)
                myRec55.Close()
            Else
                myRec55.Close()
                Response.Write("<script>alert('Attenzione...Nessun bando aperto!')</script>")
                Exit Sub
            End If


            par.cmd.CommandText = "SELECT COUNT(ID) FROM UTENZA_DICHIARAZIONI WHERE  ID_STATO=1 AND ID_BANDO=" & ID_BANDO & " AND id_caf=" & Session.Item("ID_CAF")

            myRec55 = par.cmd.ExecuteReader()

            If myRec55.Read Then
                lmNumDichiarazioni = par.IfNull(myRec55(0), 0)
            End If
            myRec55.Close()


            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("EXPORT\" & smNomeFile & ".xml"), False, System.Text.Encoding.Default)
            k = 0
            kk = 0
            StringaDati = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "iso-8859-1" & Chr(34) & "?>" & vbCrLf
            StringaDati = StringaDati & "<!-- created with SEPA@Web (www.sistemiesoluzionisrl.it) -->" & vbCrLf
            StringaDati = StringaDati & "<RichiestaTrasmissione_DichiarazioniERP " _
                        & " " _
                        & "" _
                        & "CodiceEnte=" & Chr(34) & Session.Item("ID_CAF") & Chr(34) & " " _
                        & "DescrizioneEnte=" & Chr(34) & NomeGestore & Chr(34) & " " _
                        & "IdentificatoreRichiesta=" & Chr(34) & "" & smNomeFile & "" & Chr(34) & " " _
                        & "DataCreazione=" & Chr(34) & "" & Mid(smAppoggio, 1, 8) & "" & Chr(34) & " " _
                        & "OraCreazione=" & Chr(34) & "" & smAppoggio1 & "" & Chr(34) & ">" & vbCrLf
            StringaDati = StringaDati & "<ListaDichiarazioni NumeroDichiarazioni=" & Chr(34) & "" & lmNumDichiarazioni & "" & Chr(34) & ">"

            sr.WriteLine(StringaDati)
            StringaDati = ""



            par.cmd.CommandText = "select utenza_dichiarazioni.*,utenza_COMP_NUCLEO.COD_FISCALE from utenza_dichiarazioni,utenza_COMP_NUCLEO where utenza_DICHIARAZIONI.ID_BANDO=" & ID_BANDO & " AND utenza_dichiarazioni.id_caf=" & Session.Item("ID_CAF") & " and utenza_DICHIARAZIONI.ID_STATO=1 AND utenza_DICHIARAZIONI.PROGR_DNTE=utenza_COMP_NUCLEO.PROGR AND utenza_COMP_NUCLEO.ID_DICHIARAZIONE=utenza_DICHIARAZIONI.ID AND utenza_DICHIARAZIONI.PG IS NOT NULL ORDER BY utenza_DICHIARAZIONI.ID ASC"
            Dim myRec As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myRec.Read
                j = j + 1
                'par.cmd.CommandText = "select id from domande_bando where id_dichiarazione=" & par.IfNull(myRec("ID"), -1)
                'Dim myRec6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myRec6.Read Then
                PROV_NAS = ""
                PROV_RES = ""


                'par.cmd.CommandText = "SELECT DESCRIZIONE FROM T_TIPO_CATEGORIE_IMMOBILE WHERE COD=" & par.IfNull(myRec("ID_TIPO_CAT_AB"), -1)
                'Dim myRec1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                'If myRec1.Read Then
                '    CategoriaCatastale = par.IfNull(myRec1("DESCRIZIONE"), "BBBB")
                'Else
                '    CategoriaCatastale = ""
                'End If

                CategoriaCatastale = CStr(par.IfNull(myRec("ID_TIPO_CAT_AB"), -1))

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & par.IfNull(myRec("ID_LUOGO_NAS_DNTE"), -1)
                Dim myRec2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & par.IfNull(myRec("ID_LUOGO_RES_DNTE"), -1)
                Dim myRec3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myRec2.Read Then
                    PROV_NAS = par.IfNull(myRec2("SIGLA"), "")
                End If
                If myRec3.Read Then
                    PROV_RES = par.IfNull(myRec3("SIGLA"), "")
                End If
                If PROV_NAS = "C" Or PROV_NAS = "E" Then PROV_NAS = "EE"
                If PROV_RES = "C" Or PROV_RES = "E" Then PROV_RES = "EE"

                StringaDati1 = "<Dichiarazione ProtocolloMittente=" & Chr(34) & "06" & par.IfNull(myRec("PG"), "") & "F205" & Chr(34) & " " _
                           & "" _
                           & "ChiaveEnteEsterno=" & Chr(34) & "" & par.IfNull(myRec("chiave_ente_esterno"), "") & "" & Chr(34) & " " _
                           & "StatoDichiarazione=" & Chr(34) & "" & par.IfNull(myRec("ID_STATO"), "") & "" & Chr(34) & " " _
                           & "CodiceFiscaleDichiarante=" & Chr(34) & "" & par.IfNull(myRec("COD_FISCALE"), "") & "" & Chr(34) & " " _
                           & "ProvinciaNascitaDichiarante=" & Chr(34) & "" & PROV_NAS & "" & Chr(34) & " " _
                           & "CodiceComuneNascitaDichiarante=" & Chr(34) & "" & par.IfNull(myRec2("COD"), "") & "" & Chr(34) & " " _
                           & "TipoIndirizzoResidenzaDichiarante=" & Chr(34) & "" & par.IfNull(myRec("id_TIPO_IND_RES_DNTE"), "") & "" & Chr(34) & " " _
                           & "IndirizzoResidenzaDichiarante=" & Chr(34) & "" & par.IfNull(myRec("IND_RES_DNTE"), "") & "" & Chr(34) & " " _
                           & "CivicoResidenzaDichiarante=" & Chr(34) & "" & par.IfNull(myRec("CIVICO_RES_DNTE"), "") & "" & Chr(34) & " " _
                           & "ProvinciaResidenzaDichiarante=" & Chr(34) & "" & PROV_RES & "" & Chr(34) & " " _
                           & "CodiceComuneResidenzaDichiarante=" & Chr(34) & "" & par.IfNull(myRec3("COD"), "") & "" & Chr(34) & " " _
                           & "CapResidenzaDichiarante=" & Chr(34) & "" & Format(par.IfNull(myRec("CAP_RES_DNTE"), ""), "00000") & "" & Chr(34) & " " _
                           & "TelefonoDichiarante=" & Chr(34) & "" & par.IfNull(myRec("TELEFONO_DNTE"), "") & "" & Chr(34) & " " _
                           & "AlloggioScala=" & Chr(34) & "" & par.IfNull(myRec("scala"), "") & "" & Chr(34) & " " _
                           & "AlloggioPiano=" & Chr(34) & "" & par.IfNull(myRec("piano"), "") & "" & Chr(34) & " " _
                           & "AlloggioNumero=" & Chr(34) & "" & par.IfNull(myRec("alloggio"), "") & "" & Chr(34) & " " _
                           & "AlloggioFoglio=" & Chr(34) & "" & par.IfNull(myRec("foglio"), "") & "" & Chr(34) & " " _
                           & "AlloggioMappale=" & Chr(34) & "" & par.IfNull(myRec("mappale"), "") & "" & Chr(34) & " " _
                           & "AlloggioSub=" & Chr(34) & "" & par.IfNull(myRec("sub"), "") & "" & Chr(34) & " " _
                           & "Ic=" & Chr(34) & "" & par.IfNull(myRec("int_c"), "0") & "" & Chr(34) & " " _
                           & "Uv=" & Chr(34) & "" & par.IfNull(myRec("int_v"), "0") & "" & Chr(34) & " " _
                           & "Oa=" & Chr(34) & "" & par.IfNull(myRec("int_a"), "0") & "" & Chr(34) & " " _
                           & "Am=" & Chr(34) & "" & par.IfNull(myRec("int_m"), "0") & "" & Chr(34) & " " _
                           & "MinoriCarico=" & Chr(34) & "" & par.IfNull(myRec("minori_carico"), "0") & "" & Chr(34) & " " _
                           & "Posizione=" & Chr(34) & "" & par.IfNull(myRec("posizione"), "") & "" & Chr(34) & " " _
                           & "Rapporto=" & Chr(34) & "" & par.IfNull(myRec("rapporto"), "") & "" & Chr(34) & " " _
                           & "CodiceUtente=" & Chr(34) & "" & par.IfNull(myRec("cod_alloggio"), "") & "" & Chr(34) & " " _
                           & "TipoCanone=" & Chr(34) & "" & par.IfNull(myRec("tipo_ass"), "1") & "" & Chr(34) & " " _
                           & "ISEE=" & Chr(34) & "" & par.IfNull(myRec("isee"), "0") & "" & Chr(34) & " " _
                           & "ISE=" & Chr(34) & "" & Format(par.IfNull(myRec("ise_erp"), "0"), "##,##0.00000") & "" & Chr(34) & " " _
                           & "ISR=" & Chr(34) & "" & Format(par.IfNull(myRec("isr_erp"), "0"), "##,##0.00000") & "" & Chr(34) & " " _
                           & "ISP=" & Chr(34) & "" & Format(par.IfNull(myRec("isp_erp"), "0"), "##,##0.00000") & "" & Chr(34) & " " _
                           & "PSE=" & Chr(34) & "" & Format(CDbl(par.IfNull(myRec("pse"), "0")), "##,##0.00") & "" & Chr(34) & " " _
                           & "VSE=" & Chr(34) & "" & Format(CDbl(par.IfNull(myRec("vse"), "0")), "##,##0.00") & "" & Chr(34) & " " _
                           & "Data_Cessazione=" & Chr(34) & "" & par.IfNull(myRec("DATA_CESSAZIONE"), "") & "" & Chr(34) & " " _
                           & "Data_Decorrenza=" & Chr(34) & "" & par.IfNull(myRec("DATA_DECORRENZA"), "") & "" & Chr(34) & " " _
                           & "LuogoPresentazione=" & Chr(34) & "" & par.IfNull(myRec("LUOGO"), "") & "" & Chr(34) & " " _
                           & "DataPresentazione=" & Chr(34) & "" & par.IfNull(myRec("DATA"), "") & "" & Chr(34) & " " _
                           & "NumInvalidiConIndennita=" & Chr(34) & "" & par.IfNull(myRec("N_INV_100_CON"), "0") & "" & Chr(34) & " " _
                           & "NumInvalidiSenzaIndennita=" & Chr(34) & "" & par.IfNull(myRec("N_INV_100_SENZA"), "0") & "" & Chr(34) & " " _
                           & "NumInvalidi10066=" & Chr(34) & "" & par.IfNull(myRec("N_INV_100_66"), "") & "" & Chr(34) & " " _
                           & "LimitePatrimonioSuperato=" & Chr(34) & "" & par.IfNull(myRec("PATR_SUPERATO"), "") & "" & Chr(34) & " "

                QUADRO_C2 = "0"
                QUADRO_C1 = "0"
                QUADRO_D = "0"
                COMP = 0



                TotalePatrimonioMobiliare = 0
                StringaDati2 = ""
                tot_spese = 0

                par.cmd.CommandText = "SELECT * FROM utenza_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & par.IfNull(myRec("ID"), -1) & " ORDER BY PROGR ASC"
                Dim myRec4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()


                While myRec4.Read
                    COMP = COMP + 1
                    TIPO_PARENTE = ""
                    If par.IfNull(myRec4("PROGR"), "-1") = "0" Then
                        TIPO_PARENTE = "1"
                    Else
                        Select Case par.IfNull(myRec4("GRADO_PARENTELA"), "")
                            Case 1
                                TIPO_PARENTE = "1"
                            Case 2
                                TIPO_PARENTE = "2"
                            Case 3
                                TIPO_PARENTE = "3"
                            Case 4
                                TIPO_PARENTE = "4"
                            Case 5
                                TIPO_PARENTE = "5"
                            Case 6
                                TIPO_PARENTE = "6"
                            Case 7
                                TIPO_PARENTE = "7"
                            Case 8
                                TIPO_PARENTE = "8"
                            Case 9
                                TIPO_PARENTE = "9"
                            Case 10
                                TIPO_PARENTE = "10"
                            Case 11
                                TIPO_PARENTE = "11"
                            Case 12
                                TIPO_PARENTE = "12"
                            Case 13
                                TIPO_PARENTE = "13"
                            Case 14
                                TIPO_PARENTE = "14"
                            Case 15
                                TIPO_PARENTE = "15"
                            Case 16
                                TIPO_PARENTE = "16"
                            Case 17
                                TIPO_PARENTE = "17"
                            Case 18
                                TIPO_PARENTE = "18"
                            Case 20
                                TIPO_PARENTE = "20"
                            Case 22
                                TIPO_PARENTE = "22"
                            Case 24
                                TIPO_PARENTE = "24"
                            Case 26
                                TIPO_PARENTE = "26"
                            Case 28
                                TIPO_PARENTE = "28"
                            Case 30
                                TIPO_PARENTE = "30"
                            Case 32
                                TIPO_PARENTE = "32"
                        End Select
                    End If
                    CodiceASL = ""
                    If par.IfNull(myRec4("USL"), "") = "-----" Then
                        CodiceASL = ""
                    Else
                        CodiceASL = par.IfNull(myRec4("USL"), "")
                    End If
                    StringaDati2 = StringaDati2 & "<Persona CodiceFiscale=" & Chr(34) & "" & par.IfNull(myRec4("COD_FISCALE"), "") & "" & Chr(34) & " " _
                               & "Cognome=" & Chr(34) & "" & par.IfNull(myRec4("COGNOME"), "") & "" & Chr(34) & " " _
                               & "Nome=" & Chr(34) & "" & par.IfNull(myRec4("NOME"), "") & "" & Chr(34) & " " _
                               & "DataNascita=" & Chr(34) & "" & par.IfNull(myRec4("DATA_NASCITA"), "") & "" & Chr(34) & " " _
                               & "CodiceASL=" & Chr(34) & "" & CodiceASL & "" & Chr(34) & " " _
                               & "Sesso=" & Chr(34) & "" & par.IfNull(myRec4("SESSO"), "") & "" & Chr(34) & " " _
                               & "PercentualeInv=" & Chr(34) & "" & par.IfNull(myRec4("perc_inval"), "0") & "" & Chr(34) & " " _
                               & "IndennitaAcc=" & Chr(34) & "" & par.IfNull(myRec4("indennita_acc"), "0") & "" & Chr(34) & " " _
                               & "TipoComponente=" & Chr(34) & "" & TIPO_PARENTE & "" & Chr(34) & ">" & vbCrLf

                    par.cmd.CommandText = "SELECT * FROM utenza_COMP_REDDITO WHERE ID_COMPONENTE=" & par.IfNull(myRec4("ID"), -1)
                    Dim myRec5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myRec5.Read
                        QUADRO_D = "1"
                        StringaDati2 = StringaDati2 & "<Reddito RedditoIRPEF=" & Chr(34) & "" & par.IfNull(myRec5("REDDITO_IRPEF"), "-1") & "" & Chr(34) & " ProventiAgrari=" & Chr(34) & "" & par.IfNull(myRec5("PROV_AGRARI"), "-1") & "" & Chr(34) & "/>" & vbCrLf
                    End While
                    myRec5.Close()

                    par.cmd.CommandText = "SELECT * FROM utenza_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & par.IfNull(myRec4("ID"), -1)
                    myRec5 = par.cmd.ExecuteReader()
                    While myRec5.Read
                        StringaDati2 = StringaDati2 & "<AltriRedditi AltroReddito=" & Chr(34) & "" & par.IfNull(myRec5("IMPORTO"), "-1") & "" & Chr(34) & "/>" & vbCrLf
                    End While
                    myRec5.Close()

                    par.cmd.CommandText = "SELECT * FROM utenza_COMP_DETRAZIONI WHERE ID_COMPONENTE=" & par.IfNull(myRec4("ID"), -1)
                    myRec5 = par.cmd.ExecuteReader()
                    While myRec5.Read
                        Select Case par.IfNull(myRec5("ID_TIPO"), "-1")
                            Case -1
                                TIP_DETR = ""
                            Case 0
                                TIP_DETR = "0"
                            Case 1
                                TIP_DETR = "1"
                            Case 2
                                TIP_DETR = "2"
                        End Select
                        StringaDati2 = StringaDati2 & "<Detrazioni TipoDetrazione=" & Chr(34) & "" & TIP_DETR & "" & Chr(34) & " Detrazione=" & Chr(34) & "" & par.IfNull(myRec5("IMPORTO"), "-1") & "" & Chr(34) & "/>" & vbCrLf

                    End While
                    myRec5.Close()

                    par.cmd.CommandText = "SELECT * FROM utenza_COMP_PATR_MOB WHERE ID_COMPONENTE=" & par.IfNull(myRec4("ID"), -1)
                    myRec5 = par.cmd.ExecuteReader()
                    While myRec5.Read
                        QUADRO_C1 = "1"
                        If par.IfNull(myRec5("COD_INTERMEDIARIO"), "") <> "" Then
                            COD_INTERMEDIARIO = par.IfNull(myRec5("COD_INTERMEDIARIO"), "") 'Mid(par.IfNull(myRec5("COD_INTERMEDIARIO"), ""), 1, 5) & Mid(par.IfNull(myRec5("COD_INTERMEDIARIO"), ""), 7, 5) & Mid(par.IfNull(myRec5("COD_INTERMEDIARIO"), ""), 13, 1)
                        Else
                            COD_INTERMEDIARIO = ""
                        End If
                        StringaDati2 = StringaDati2 & "<Mobiliare CodiceIntermediario=" & Chr(34) & "" & COD_INTERMEDIARIO & "" & Chr(34) & " DescrizioneIntermediario=" & Chr(34) & "" & par.IfNull(myRec5("INTERMEDIARIO"), "") & "" & Chr(34) & " ImportoInvestimento=" & Chr(34) & "" & par.IfNull(myRec5("IMPORTO"), "") & "" & Chr(34) & "/>" & vbCrLf
                        TotalePatrimonioMobiliare = TotalePatrimonioMobiliare + par.IfNull(myRec5("IMPORTO"), 0)
                    End While
                    myRec5.Close()

                    par.cmd.CommandText = "SELECT * FROM utenza_COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & par.IfNull(myRec4("ID"), -1)
                    myRec5 = par.cmd.ExecuteReader()
                    While myRec5.Read
                        QUADRO_C2 = "1"
                        Select Case par.IfNull(myRec5("ID_TIPO"), -1)
                            Case -1
                                TIPO_IMM = "0"
                            Case 0
                                TIPO_IMM = "0"
                            Case 1
                                TIPO_IMM = "1"
                            Case 2
                                TIPO_IMM = "2"
                        End Select
                        If par.IfNull(myRec5("VALORE"), "0") <> "0" Or par.IfNull(myRec5("MUTUO"), "0") <> "0" Then
                            StringaDati2 = StringaDati2 & "<Immobiliare TipoImmobile=" & Chr(34) & "" & TIPO_IMM & "" & Chr(34) & " PercentualeProprieta=" & Chr(34) & "" & par.IfNull(myRec5("PERC_PATR_IMMOBILIARE"), "0") & "" & Chr(34) & " ValoreIci=" & Chr(34) & "" & par.IfNull(myRec5("VALORE"), "-1") & "" & Chr(34) & " MutuoResiduo=" & Chr(34) & "" & par.IfNull(myRec5("MUTUO"), "-1") & "" & Chr(34) & " FlagResidenza=" & Chr(34) & "" & par.IfNull(myRec5("F_RESIDENZA"), "") & "" & Chr(34) & "/>" & vbCrLf
                        End If

                    End While
                    myRec5.Close()

                    par.cmd.CommandText = "SELECT * FROM utenza_COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & par.IfNull(myRec4("ID"), -1)
                    myRec5 = par.cmd.ExecuteReader()
                    While myRec5.Read
                        tot_spese = tot_spese + par.IfNull(myRec5("IMPORTO"), 0)
                        StringaDati2 = StringaDati2 & "<SpeseInvalido Importo=" & Chr(34) & "" & par.IfNull(myRec5("IMPORTO"), "-1") & "" & Chr(34) & " SpeseInvalidoDescrizione=" & Chr(34) & "" & par.IfNull(myRec5("descrizione"), "") & "" & Chr(34) & "/>" & vbCrLf
                    End While
                    myRec5.Close()

                    par.cmd.CommandText = "SELECT * FROM utenza_REDDITI WHERE ID_COMPONENTE=" & par.IfNull(myRec4("ID"), -1)
                    myRec5 = par.cmd.ExecuteReader()
                    If myRec5.Read Then
                        StringaDati2 = StringaDati2 & "<Convenzionale Condizione=" & Chr(34) & "" & par.IfNull(myRec5("CONDIZIONE"), "-1") & "" & Chr(34) & " Professione=" & Chr(34) & "" & par.IfNull(myRec5("PROFESSIONE"), "-1") & "" & Chr(34) & " Dipendente=" & Chr(34) & "" & par.IfNull(myRec5("DIPENDENTE"), "-1") & "" & Chr(34) & " Pensione=" & Chr(34) & "" & par.IfNull(myRec5("PENSIONE"), "-1") & "" & Chr(34) & " Autonomo=" & Chr(34) & "" & par.IfNull(myRec5("AUTONOMO"), "-1") & "" & Chr(34) & " Non_Imponibili=" & Chr(34) & "" & par.IfNull(myRec5("NON_IMPONIBILI"), "-1") & "" & Chr(34) & " Dom_Agr_Fab=" & Chr(34) & "" & par.IfNull(myRec5("DOM_AG_FAB"), "-1") & "" & Chr(34) & " Occasionali=" & Chr(34) & "" & par.IfNull(myRec5("OCCASIONALI"), "-1") & "" & Chr(34) & " Oneri=" & Chr(34) & "" & par.IfNull(myRec5("ONERI"), "-1") & "" & Chr(34) & "/>" & vbCrLf
                    End If
                    myRec5.Close()

                    StringaDati2 = StringaDati2 & "</Persona>" & vbCrLf
                End While
                myRec4.Close()

                StringaDati1 = StringaDati1 _
                           & "TotalePatrimonioMobiliare=" & Chr(34) & "" & TotalePatrimonioMobiliare & "" & Chr(34) & " " _
                           & "AnnoRiferimento=" & Chr(34) & "" & par.IfNull(myRec("ANNO_SIT_ECONOMICA"), "") & "" & Chr(34) & " " _
                           & "Note=" & Chr(34) & "" & Chr(34) & " " _
                           & "NumeroComponenti=" & Chr(34) & "" & COMP & "" & Chr(34) & " "
                StringaDati1 = StringaDati1 _
                           & "CategoriaCatastale=" & Chr(34) & "" & CategoriaCatastale & "" & Chr(34) & ">" & vbCrLf

                'myRec1.Close()
                myRec2.Close()
                myRec3.Close()

                PROV_RES = ""
                PROV_NAS = ""


                StringaDati3 = "" '<SpeseInvalido Importo=" & Chr(34) & "" & tot_spese & "" & Chr(34) & "/>" & vbCrLf

                'par.cmd.CommandText = "select * from SOTTOSCRITTORI WHERE ID_DICHIARAZIONE=" & par.IfNull(myRec("ID"), -1)
                'myRec4 = par.cmd.ExecuteReader()
                'If myRec4.Read Then
                '    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & par.IfNull(myRec4("ID_LUOGO_NAS"), -1)
                '    myRec2 = par.cmd.ExecuteReader()

                '    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & par.IfNull(myRec4("ID_LUOGO_RES"), -1)
                '    myRec3 = par.cmd.ExecuteReader()

                '    If myRec3.Read Then
                '        PROV_RES = par.IfNull(myRec3("SIGLA"), "")
                '    End If

                '    If myRec2.Read Then
                '        PROV_NAS = par.IfNull(myRec2("SIGLA"), "")
                '    End If



                '    If PROV_NAS = "C" Or PROV_NAS = "E" Then PROV_NAS = "EE"
                '    If PROV_RES = "C" Or PROV_RES = "E" Then PROV_RES = "EE"

                '    If Len(par.IfNull(myRec3("CAP"), "")) < 5 Then
                '        SLOG = SLOG & "PG " & par.IfNull(myRec("PG"), "") & "CAP ERRATO" & vbCrLf
                '    End If

                '    StringaDati3 = StringaDati3 & "<Sottoscrittore " _
                '                 & "Cognome=" & Chr(34) & "" & par.IfNull(myRec4("COGNOME"), "") & "" & Chr(34) & " " _
                '                 & "Nome=" & Chr(34) & "" & par.IfNull(myRec4("NOME"), "") & "" & Chr(34) & " " _
                '                 & "DataNascita=" & Chr(34) & "" & par.IfNull(myRec4("DATA_NAS"), "") & "" & Chr(34) & " " _
                '                 & "ProvinciaNascita=" & Chr(34) & "" & PROV_NAS & "" & Chr(34) & " " _
                '                 & "CodiceComuneNascita=" & Chr(34) & "" & par.IfNull(myRec2("COD"), "") & "" & Chr(34) & " " _
                '                 & "IndirizzoResidenza=" & Chr(34) & "" & par.IfNull(myRec4("IND"), "") & " " & par.IfNull(myRec4("CIVICO"), "") & "" & Chr(34) & " " _
                '                 & "ProvinciaResidenza=" & Chr(34) & "" & PROV_RES & "" & Chr(34) & " " _
                '                 & "CodiceComuneResidenza=" & Chr(34) & "" & par.IfNull(myRec3("COD"), "") & "" & Chr(34) & " " _
                '                 & "CapResidenza=" & Chr(34) & "" & par.IfNull(myRec3("CAP"), "") & "" & Chr(34) & " " _
                '                 & "Telefono=" & Chr(34) & "" & par.IfNull(myRec4("TELEFONO"), "") & "" & Chr(34) & "/>" & vbCrLf
                '    myRec3.Close()
                '    myRec2.Close()
                'End If
                'myRec4.Close()



                sr.WriteLine(StringaDati1 & StringaDati2 & StringaDati3 & "</Dichiarazione>")

                StringaDati1 = ""
                StringaDati2 = ""
                StringaDati3 = ""
                k = k + 1
                'End If
                Response.Write("<script>parent.funzioni.bb=" & j & ";</script>")
            End While

            myRec.Close()
            sr.WriteLine("</ListaDichiarazioni>")
            sr.WriteLine("</RichiestaTrasmissione_DichiarazioniERP>")
            StringaDati = ""
            sr.Close()
            par.OracleConn.Close()

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("EXPORT\" & smNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("EXPORT\" & smNomeFile & ".xml")
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
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()

            Response.Write("<script>window.open('Allegati_1.aspx?NOME=" & smNomeFile & ".zip" & "&EXT=ZIP','Export','');location.replace('CreaXML.aspx');</script>")

        Catch ex As Exception
            'Response.Write("<script>parent.funzioni.aa.close();</script>")
            par.OracleConn.Close()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub



End Class
