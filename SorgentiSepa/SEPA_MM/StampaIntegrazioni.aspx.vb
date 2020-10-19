
Partial Class StampaIntegrazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        Dim KK As Integer
        Dim operatore As Long

        Dim POS As Integer
        Dim POS1 As Integer
        Dim OPERATORE_TR As String
        Dim MiaStringa As String
        Dim MioColore As String


        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If Not IsPostBack Then
            Try
                lN_Distinta = -1
                IOperatore = Request.QueryString("OP")
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                MiaStringa = "<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Language' content='it'>" & vbCrLf
                MiaStringa = MiaStringa & "<meta http-equiv='Content-Type' content='text/html; charset=windows-1252'>" & vbCrLf
                MiaStringa = MiaStringa & "<meta name='GENERATOR' content='Microsoft FrontPage 4.0'>" & vbCrLf
                MiaStringa = MiaStringa & "<meta name='ProgId' content='FrontPage.Editor.Document'>" & vbCrLf
                MiaStringa = MiaStringa & "<title>Elenco Integrazioni/Rinnovi</title>" & vbCrLf
                MiaStringa = MiaStringa & "</head>"
                MiaStringa = MiaStringa & "<body>"
                MiaStringa = MiaStringa & "<p><b><font face='Arial' size='4'>Elenco Integrazioni/Rinnovi effettuate dal " & par.FormattaData(Request.QueryString("PDA")) & " al " & par.FormattaData(Request.QueryString("PA")) & "</font></b></p>" & vbCrLf
                MiaStringa = MiaStringa & "<p>&nbsp;</p>"
                MiaStringa = MiaStringa & "<table border='1' cellpadding='0' cellspacing='0' width='100%' bordercolorlight='#FFFFFF' bordercolordark='#C0C0C0'>" & vbCrLf
                MiaStringa = MiaStringa & "<tr>"
                MiaStringa = MiaStringa & "<td width='16%'><font face='Arial' size='1'><b>PROTOCOLLO</b></font></td>" & vbCrLf
                MiaStringa = MiaStringa & "<td width='17%'><font face='Arial' size='1'><b>COD.FISCALE</b></font></td>" & vbCrLf
                MiaStringa = MiaStringa & "<td width='17%'><font face='Arial' size='1'><b>COGNOME E NOME</b></font></td>" & vbCrLf
                MiaStringa = MiaStringa & "<td width='17%'><font face='Arial' size='1'><b>ISBARC/R</b></font></td>" & vbCrLf
                MiaStringa = MiaStringa & "<td width='17%'><font face='Arial' size='1'><b>STATO</b></font></td>" & vbCrLf
                MiaStringa = MiaStringa & "<td width='17%'><font face='Arial' size='1'><b>DATA INT./RINN.</b></font></td>" & vbCrLf
                MiaStringa = MiaStringa & "<td width='17%'><font face='Arial' size='1'><b>OPERATORE</b></font></td>" & vbCrLf
                MiaStringa = MiaStringa & "</tr>" & vbCrLf

                par.cmd.CommandText = "select eventi_bandi.* from eventi_bandi,OPERATORI where eventi_bandi.cod_evento='F140' and eventi_bandi.tipo_operatore='I' and eventi_bandi.id_operatore=OPERATORI.id and OPERATORI.id_caf=" & Session.Item("ID_CAF") & " AND SUBSTR(EVENTI_BANDI.DATA_ORA,1,8)>=" & Request.QueryString("PDA") & " AND SUBSTR(EVENTI_BANDI.DATA_ORA,1,8)<=" & Request.QueryString("PA") & " order by eventi_bandi.id_domanda ASC"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                i = 0
                KK = 0
                MioColore = "bgcolor='D2D2D2'"
                'bgcolor="#FFFFFF
                While myReader.Read
                    operatore = par.IfNull(myReader("ID_operatore"), "")
                    POS = 1
                    While POS <= Len(IOperatore)
                        POS1 = InStr(POS, IOperatore, ",", CompareMethod.Text)
                        If POS1 = 0 Then POS1 = Len(IOperatore) + 1
                        OPERATORE_TR = Mid(IOperatore, POS, POS1 - POS)
                        POS = POS1 + 1
                        If OPERATORE_TR = operatore Then
                            par.cmd.CommandText = "SELECT domande_bando.pg,DOMANDE_BANDO.ISBARC_R,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,COMP_NUCLEO.COD_FISCALE,tab_stati.descrizione FROM DOMANDE_BANDO,COMP_NUCLEO,tab_stati WHERE domande_bando.id_stato=tab_stati.cod and DOMANDE_BANDO.ID=" & myReader("ID_DOMANDA") & "  AND DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR"
                            myReader1 = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                MiaStringa = MiaStringa & "<tr>"
                                MiaStringa = MiaStringa & "<td width='16%' " & MioColore & "><font face='Arial' size='1'>" & par.IfNull(myReader1("pg"), "") & "</font></td>" & vbCrLf
                                MiaStringa = MiaStringa & "<td width='17%' " & MioColore & "><font face='Arial' size='1'>" & par.IfNull(myReader1("cod_fiscale"), "") & "</font></td>" & vbCrLf
                                MiaStringa = MiaStringa & "<td width='17%' " & MioColore & "><font face='Arial' size='1'>" & par.IfNull(myReader1("cognome"), "") & " " & par.IfNull(myReader1("nome"), "") & "</font></td>" & vbCrLf
                                MiaStringa = MiaStringa & "<td width='17%' " & MioColore & "><font face='Arial' size='1'>" & par.Tronca(myReader1("isbarc_r")) & "</font></td>" & vbCrLf
                                MiaStringa = MiaStringa & "<td width='17%' " & MioColore & "><font face='Arial' size='1'>" & par.IfNull(myReader1("descrizione"), "") & "</font></td>" & vbCrLf
                                MiaStringa = MiaStringa & "<td width='17%' " & MioColore & "><font face='Arial' size='1'>" & par.FormattaData(Mid(myReader("data_ora"), 1, 8)) & "</font></td>" & vbCrLf

                                par.cmd.CommandText = "SELECT OPERATORI.OPERATORE AS ""DESCRIZIONE"" FROM OPERATORI " _
                                                    & "WHERE OPERATORI.ID=" & OPERATORE_TR
                                myReader2 = par.cmd.ExecuteReader()
                                If myReader2.Read Then
                                    MiaStringa = MiaStringa & "<td width='17%' " & MioColore & "><font face='Arial' size='1'>" & par.IfNull(myReader2("DESCRIZIONE"), "") & "</font></td>" & vbCrLf
                                Else
                                    MiaStringa = MiaStringa & "<td width='17%' " & MioColore & "><font face='Arial' size='1'>--</font></td>" & vbCrLf
                                End If
                                myReader2.Close()
                                MiaStringa = MiaStringa & "</tr>" & vbCrLf
                                If MioColore = "bgcolor='D2D2D2'" Then
                                    MioColore = "bgcolor='#FFFFFF'"
                                Else
                                    MioColore = "bgcolor='D2D2D2'"
                                End If
                                KK = KK + 1
                            End If
                            myReader1.Close()
                            Exit While
                        End If
                    End While
                    i = i + 1
                End While

                myReader.Close()
                par.OracleConn.Close()
                If i = 0 Then
                    Response.Write("<script>alert('Non ci sono domande integrate o rinnovate in questo intervallo di date!');</script>")
                    Exit Sub
                End If
                Response.Write(MiaStringa)
                Response.Write("<p><font face='Arial' size='3'>Totale: <b>" & KK & "</b></font></p>")
            Catch EX As Exception

                Response.Write("ERRORE: " + EX.Message)
                par.OracleConn.Close()
            End Try
        End If
    End Sub

    Public Property IOperatore() As String
        Get
            If Not (ViewState("par_IOperatore") Is Nothing) Then
                Return CStr(ViewState("par_IOperatore"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IOperatore") = value
        End Set
    End Property

    Public Property lN_Distinta() As Long
        Get
            If Not (ViewState("par_lN_Distinta") Is Nothing) Then
                Return CLng(ViewState("par_lN_Distinta"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lN_Distinta") = value
        End Set

    End Property

    Private Function Riempi()

    End Function

    '    Dim MiaStringa As String
    '    Dim TOT_DIC As Integer
    '    Dim TOT_DOM As Integer

    '    Dim INTERVALLO_DA As String
    '    Dim INTERVALLO_A As String








    '    INTERVALLO_DA = cmbAnnoDa.Text & cmbMDa.Text & cmbGDa.Text
    '    INTERVALLO_A = cmbAnnoa.Text & cmbMesea.Text & cmbGa.Text



    '    If Val(INTERVALLO_A) < Val(INTERVALLO_DA) Then
    '        Response.Write("<script>alert('Intervallo Date non valido!');</script>")
    '        Exit Sub
    '    End If

    '    If par.OracleConn.State = Data.ConnectionState.Open Then
    '        Response.Write("IMPOSSIBILE VISUALIZZARE LE INTEGRAZIONI")
    '        Exit Sub
    '    Else
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)
    '    End If





    '    par.cmd.CommandText = "select distinct(eventi_bandi.id_domanda) from eventi_bandi,OPERATORI where eventi_bandi.cod_evento='F140' and eventi_bandi.tipo_operatore='E' and eventi_bandi.id_operatore=OPERATORI.id and OPERATORI.id_caf=" & Session.Item("ID_CAF") & " AND SUBSTR(EVENTI_BANDI.DATA_ORA,1,8)>=" & INTERVALLO_DA & " AND SUBSTR(EVENTI_BANDI.DATA_ORA,1,8)<=" & INTERVALLO_A & " order by eventi_bandi.id_domanda desc"
    '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

    '    While myReader.Read
    '        par.cmd.CommandText = "SELECT domande_bando.pg,DOMANDE_BANDO.ISBARC_R,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,COMP_NUCLEO.COD_FISCALE,tab_stati.descrizione FROM DOMANDE_BANDO,COMP_NUCLEO,tab_stati WHERE domande_bando.id_stato=tab_stati.cod and DOMANDE_BANDO.ID=" & myReader("ID_DOMANDA") & "  AND DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR"
    '        myReader1 = par.cmd.ExecuteReader()
    '        If myReader1.Read Then
    '            MiaStringa = MiaStringa & "<tr>"
    '            MiaStringa = MiaStringa & "<td width='16%'><font face='Arial' size='2'>" & par.IfNull(myReader1("pg"), "") & "</font></td>" & vbCrLf
    '            MiaStringa = MiaStringa & "<td width='16%'><font face='Arial' size='2'>" & par.IfNull(myReader1("cod_fiscale"), "") & "</font></td>" & vbCrLf
    '            MiaStringa = MiaStringa & "<td width='17%'><font face='Arial' size='2'>" & par.IfNull(myReader1("cognome"), "") & " " & par.IfNull(myReader1("nome"), "") & "</font></td>" & vbCrLf
    '            MiaStringa = MiaStringa & "<td width='17%'><font face='Arial' size='2'>" & par.Tronca(myReader1("isbarc_r")) & "</font></td>" & vbCrLf
    '            MiaStringa = MiaStringa & "<td width='17%'><font face='Arial' size='2'>" & par.IfNull(myReader1("descrizione"), "") & "</font></td>" & vbCrLf
    '            MiaStringa = MiaStringa & "<td width='17%'><font face='Arial' size='2'>" & par.AggiustaData(myReader1("data_ora")) & "</font></td>" & vbCrLf
    '            MiaStringa = MiaStringa & "</tr>" & vbCrLf
    '        End If
    '        myReader1.Close()


    '    End While

    '    myReader.Close()



    '    par.OracleConn.Close()
    '    par.OracleConn.Dispose()

    '    MiaStringa = MiaStringa & "</table>"
    '    MiaStringa = MiaStringa & "</body>"
    '    MiaStringa = MiaStringa & "</html>"

    '    HttpContext.Current.Session.Add("INTEGRAZIONI", MiaStringa)
    '    Response.Write("<script>window.open('StampaIntegrazioni.aspx','Integrazioni','');</script>")

End Class
