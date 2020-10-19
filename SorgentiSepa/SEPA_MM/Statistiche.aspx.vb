
Partial Class Statistiche
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            cmbAnnoa.SelectedIndex = -1
            cmbAnnoa.Items.FindByText(Year(Now)).Selected = True

            cmbMesea.SelectedIndex = -1
            cmbMesea.Items.FindByValue(Format(Month(Now), "00")).Selected = True

            cmbGa.SelectedIndex = -1
            cmbGa.Items.FindByText(Format(Day(Now), "00")).Selected = True

        End If
    End Sub

    Protected Sub btnChiudi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudi.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisualizza.Click
        Dim MiaStringa As String
        Dim TOT_DIC As Integer
        Dim TOT_DOM As Integer

        Dim INTERVALLO_DA As String
        Dim INTERVALLO_A As String
        Dim I As Integer

        Dim SOMMA As Integer
        Dim SOMMA1 As Integer

        Dim TESTO_DETT As String
        Dim TESTO_DETT1 As String = ""

        Dim MIAVAR As Date
        Dim MIAVAR1 As Date

        Dim miaSomma As String
        Dim miaSomma1 As String
        Dim operatore As String



        TESTO_DETT = ""


        INTERVALLO_DA = cmbAnnoDa.Text & cmbMDa.Text & cmbGDa.Text
        INTERVALLO_A = cmbAnnoa.Text & cmbMesea.Text & cmbGa.Text
        operatore = ""
        If txtOperatore.Text <> "" Then
            operatore = " and OPERATORE='" & par.PulisciStrSql(txtOperatore.Text) & "' "
        End If


        If Val(INTERVALLO_A) < Val(INTERVALLO_DA) Then
            Response.Write("<script>alert('Intervallo Date non valido!');</script>")
            Exit Sub
        End If

        If par.OracleConn.State = Data.ConnectionState.Open Then
            Response.Write("IMPOSSIBILE VISUALIZZARE LE STATISTICHE")
            Exit Sub
        Else
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        TOT_DIC = 0
        TOT_DOM = 0

        par.cmd.CommandText = "select * from OPERATORI where ID_CAF=" & Session.Item("ID_CAF") & operatore & " and fl_eliminato='0' ORDER BY ID ASC"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        TESTO_DETT = ""
        While myReader.Read
            par.cmd.CommandText = "SELECT COUNT(ID_PRATICA) FROM EVENTI_DICHIARAZIONI WHERE ID_OPERATORE=" & myReader("ID") & " AND COD_EVENTO='F130' AND TIPO_OPERATORE='I' AND SUBSTR(DATA_ORA,1,8)>=" & INTERVALLO_DA & " AND SUBSTR(DATA_ORA,1,8)<=" & INTERVALLO_A
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                TOT_DIC = TOT_DIC + myReader1(0)
            End If
            myReader1.Close()

            par.cmd.CommandText = "SELECT COUNT(ID_DOMANDA) FROM EVENTI_BANDI WHERE ID_OPERATORE=" & myReader("ID") & " AND COD_EVENTO='F01' AND TIPO_OPERATORE='I' AND SUBSTR(DATA_ORA,1,8)>=" & INTERVALLO_DA & " AND SUBSTR(DATA_ORA,1,8)<=" & INTERVALLO_A
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                TOT_DOM = TOT_DOM + myReader1(0)
            End If
            myReader1.Close()
            SOMMA = 0
            SOMMA1 = 0



            MIAVAR = DateSerial(Mid(INTERVALLO_DA, 1, 4), Mid(INTERVALLO_DA, 5, 2), Mid(INTERVALLO_DA, 7, 2))
            MIAVAR1 = DateSerial(Mid(INTERVALLO_A, 1, 4), Mid(INTERVALLO_A, 5, 2), Mid(INTERVALLO_A, 7, 2))

            While MIAVAR <= MIAVAR1

                SOMMA = 0
                SOMMA1 = 0
                I = Format(MIAVAR, "yyyyMMdd")

                par.cmd.CommandText = "SELECT COUNT(ID_PRATICA) FROM EVENTI_DICHIARAZIONI WHERE ID_OPERATORE=" & myReader("ID") & " AND COD_EVENTO='F130' AND TIPO_OPERATORE='I' AND SUBSTR(DATA_ORA,1,8)=" & I
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    SOMMA = SOMMA + myReader1(0)
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT COUNT(ID_DOMANDA) FROM EVENTI_BANDI WHERE ID_OPERATORE=" & myReader("ID") & " AND COD_EVENTO='F01' AND TIPO_OPERATORE='I' AND SUBSTR(DATA_ORA,1,8)=" & I
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    SOMMA1 = SOMMA1 + myReader1(0)
                End If
                myReader1.Close()

                If SOMMA = 0 And SOMMA1 = 0 Then
                    miaSomma = Chr(160)
                Else
                    miaSomma = SOMMA
                End If

                If SOMMA1 = 0 And SOMMA = 0 Then
                    miaSomma1 = Chr(160)
                Else
                    miaSomma1 = SOMMA1
                End If

                If CH1.Checked = False Then
                    TESTO_DETT1 = TESTO_DETT1 & "<td style='width: 117px; height: 21px;'>"
                    TESTO_DETT1 = TESTO_DETT1 & "</td>"
                    TESTO_DETT1 = TESTO_DETT1 & "<td style='width: 62px; text-align: left; height: 21px;border-bottom: black 1px dashed;'>"
                    TESTO_DETT1 = TESTO_DETT1 & "<span style='font-size: 10pt'>" & Format(MIAVAR, "dd/MM/yyyy") & "</span></td>"
                    TESTO_DETT1 = TESTO_DETT1 & "<td style='width: 155px; text-align: left; height: 21px;border-bottom: black 1px dashed;'>"
                    TESTO_DETT1 = TESTO_DETT1 & "<span style='font-size: 10pt'>" & miaSomma & "</span></td>"
                    TESTO_DETT1 = TESTO_DETT1 & "<td style='width: 99px; text-align: left; height: 21px;border-bottom: black 1px dashed;'>"
                    TESTO_DETT1 = TESTO_DETT1 & "<span style='font-size: 10pt'>" & miaSomma1 & "</span></td>"
                    TESTO_DETT1 = TESTO_DETT1 & "</tr>"
                Else
                    If SOMMA <> 0 Or SOMMA1 <> 0 Then
                        TESTO_DETT1 = TESTO_DETT1 & "<td style='width: 117px; height: 21px;'>"
                        TESTO_DETT1 = TESTO_DETT1 & "</td>"
                        TESTO_DETT1 = TESTO_DETT1 & "<td style='width: 62px; text-align: left; height: 21px;border-bottom: black 1px dashed;'>"
                        TESTO_DETT1 = TESTO_DETT1 & "<span style='font-size: 10pt'>" & Format(MIAVAR, "dd/MM/yyyy") & "</span></td>"
                        TESTO_DETT1 = TESTO_DETT1 & "<td style='width: 155px; text-align: left; height: 21px;border-bottom: black 1px dashed;'>"
                        TESTO_DETT1 = TESTO_DETT1 & "<span style='font-size: 10pt'>" & miaSomma & "</span></td>"
                        TESTO_DETT1 = TESTO_DETT1 & "<td style='width: 99px; text-align: left; height: 21px;border-bottom: black 1px dashed;'>"
                        TESTO_DETT1 = TESTO_DETT1 & "<span style='font-size: 10pt'>" & miaSomma1 & "</span></td>"
                        TESTO_DETT1 = TESTO_DETT1 & "</tr>"
                    End If
                End If
                MIAVAR = DateAdd(DateInterval.Day, 1, MIAVAR)

            End While
            TESTO_DETT = TESTO_DETT & "<table style='z-index: 100; left: 0px; width: 90%; position: static; top: 0px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid'>"
            TESTO_DETT = TESTO_DETT & "<tr>"
            TESTO_DETT = TESTO_DETT & "<td style='width: 117px'>"
            TESTO_DETT = TESTO_DETT & "<strong><span style='font-size: 10pt; font-family: Arial'>" & myReader("operatore") & "</span></strong></td>"
            TESTO_DETT = TESTO_DETT & "<td style='width: 62px'>"
            TESTO_DETT = TESTO_DETT & "</td>"
            TESTO_DETT = TESTO_DETT & "<td style='width: 155px'>"
            TESTO_DETT = TESTO_DETT & "</td>"
            TESTO_DETT = TESTO_DETT & "<td style='width: 99px'>"
            TESTO_DETT = TESTO_DETT & "</td>"
            TESTO_DETT = TESTO_DETT & "</tr>"
            TESTO_DETT = TESTO_DETT & "<tr>"
            TESTO_DETT = TESTO_DETT & "<td style='width: 117px'>"
            TESTO_DETT = TESTO_DETT & "</td>"
            TESTO_DETT = TESTO_DETT & "<td style='width: 62px; text-align: left'>"
            TESTO_DETT = TESTO_DETT & "<span style='font-size: 10pt; font-family: Arial'><strong>GIORNO</strong></span></td>"
            TESTO_DETT = TESTO_DETT & "<td style='width: 155px; text-align: left'>"
            TESTO_DETT = TESTO_DETT & "<strong><span style='font-size: 10pt; font-family: Arial'>DIC. INSERITE</span></strong></td>"
            TESTO_DETT = TESTO_DETT & "<td style='width: 99px; text-align: left'>"
            TESTO_DETT = TESTO_DETT & "<strong><span style='font-size: 10pt; font-family: Arial'>DOM. INSERITE</span></strong></td>"
            TESTO_DETT = TESTO_DETT & "</tr>"
            TESTO_DETT = TESTO_DETT & "<tr>"

            TESTO_DETT = TESTO_DETT & TESTO_DETT1
            TESTO_DETT = TESTO_DETT & "</table>"
            TESTO_DETT = TESTO_DETT & "<br /><br />"
            TESTO_DETT1 = ""
        End While
        myReader.Close()



        par.OracleConn.Close()
        par.OracleConn.Dispose()



        MiaStringa = "<html xmlns='http://www.w3.org/1999/xhtml'>"
        MiaStringa = MiaStringa & "<head>"
        MiaStringa = MiaStringa & "<meta http-equiv='Content-Style-Type' content='text/css'>"
        MiaStringa = MiaStringa & "<title>Statistiche</title>"
        MiaStringa = MiaStringa & "<title>SEPA@Web</title></head>"
        MiaStringa = MiaStringa & "<BODY>"
        MiaStringa = MiaStringa & "<form id='form1'>"
        MiaStringa = MiaStringa & "<div>"
        MiaStringa = MiaStringa & "<table align='center' cellpadding='0' cellspacing='0' width='90%' style='border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid'>"
        MiaStringa = MiaStringa & "<tr>"
        MiaStringa = MiaStringa & "<td style='width: 112px'>"
        MiaStringa = MiaStringa & "<span style='font-family: Arial'><strong>STATISTICHE</strong></span></td>"
        MiaStringa = MiaStringa & "<td style='width: 129px'>"
        MiaStringa = MiaStringa & "</td>"
        MiaStringa = MiaStringa & "<td style='width: 144px'>"
        MiaStringa = MiaStringa & "</td>"
        MiaStringa = MiaStringa & "<td style='width: 144px'>"
        MiaStringa = MiaStringa & "</td>"
        MiaStringa = MiaStringa & "<td style='width: 144px'>"
        MiaStringa = MiaStringa & "</td>"
        MiaStringa = MiaStringa & "</tr>"
        MiaStringa = MiaStringa & "<tr>"
        MiaStringa = MiaStringa & "<td style='width: 112px'>"
        MiaStringa = MiaStringa & "</td>"
        MiaStringa = MiaStringa & "<td style='width: 129px; text-align: center'>"
        MiaStringa = MiaStringa & "<strong><span style='font-size: 10pt; font-family: Arial'>DATA INIZIO</span></strong></td>"
        MiaStringa = MiaStringa & "<td style='width: 144px; text-align: center'>"
        MiaStringa = MiaStringa & "<strong><span style='font-size: 10pt; font-family: Arial'>DATA FINE</span></strong></td>"
        MiaStringa = MiaStringa & "<td style='width: 144px; text-align: center'>"
        MiaStringa = MiaStringa & "<strong><span style='font-size: 10pt; font-family: Arial'>TOT. DICHIARAZIONI</span></strong></td>"
        MiaStringa = MiaStringa & "<td style='width: 144px; text-align: center'>"
        MiaStringa = MiaStringa & "<strong><span style='font-size: 10pt; font-family: Arial'>TOT. DOMANDE</span></strong></td>"
        MiaStringa = MiaStringa & "</tr>"
        MiaStringa = MiaStringa & "<tr>"
        MiaStringa = MiaStringa & "<td style='width: 112px; height: 19px'>"
        MiaStringa = MiaStringa & "</td>"
        MiaStringa = MiaStringa & "<td style='width: 129px; height: 19px; text-align: center'>"
        MiaStringa = MiaStringa & "<span style='font-size: 10pt'>" & cmbGDa.Text & "/" & cmbMDa.Text & "/" & cmbAnnoDa.Text & "</span></td>"
        MiaStringa = MiaStringa & "<td style='width: 144px; height: 19px; text-align: center'>"
        MiaStringa = MiaStringa & "<span style='font-size: 10pt'>" & cmbGa.Text & "/" & cmbMesea.Text & "/" & cmbAnnoa.Text & "</span></td>"
        MiaStringa = MiaStringa & "<td style='width: 144px; height: 19px; text-align: center'>"
        MiaStringa = MiaStringa & "<span style='font-size: 10pt'>" & TOT_DIC & "</span></td>"
        MiaStringa = MiaStringa & "<td style='width: 144px; height: 19px; text-align: center'>"
        MiaStringa = MiaStringa & "<span style='font-size: 10pt'>" & TOT_DOM & "</span></td>"
        MiaStringa = MiaStringa & "</tr>"
        MiaStringa = MiaStringa & "</table><br /><br />"
        MiaStringa = MiaStringa & TESTO_DETT

        MiaStringa = MiaStringa & "</div>"
        MiaStringa = MiaStringa & "</body>"

        HttpContext.Current.Session.Add("STATISTICHE", MiaStringa)
        Response.Write("<script>window.open('StampaStatistiche.aspx','Statistiche','');</script>")

    End Sub
End Class
