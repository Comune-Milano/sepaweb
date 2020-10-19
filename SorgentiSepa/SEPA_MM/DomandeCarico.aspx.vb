
Partial Class DomandeCarico
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        

        If Not IsPostBack Then

            Dim i As Integer = 2006
            For i = 2006 To Year(Now)
                cmbAnnoDa.Items.Add(New ListItem(i, i))
                cmbAnnoa.Items.Add(New ListItem(i, i))
            Next

            cmbAnnoa.SelectedIndex = -1
            cmbAnnoa.Items.FindByText(Year(Now)).Selected = True

            cmbMesea.SelectedIndex = -1
            cmbMesea.Items.FindByValue(Format(Month(Now), "00")).Selected = True

            cmbGa.SelectedIndex = -1
            cmbGa.Items.FindByText(Format(Day(Now), "00")).Selected = True
            RiempiOperatori()
        End If
    End Sub

    Private Function RiempiOperatori()
        Dim ds As New Data.DataSet()
        Dim dlist As CheckBoxList
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter


        If par.OracleConn.State = Data.ConnectionState.Open Then
            Response.Write("IMPOSSIBILE VISUALIZZARE")
            Exit Function
        Else
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If


        dlist = CheckOperatori

        da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID,OPERATORE AS ""DESCRIZIONE"" FROM OPERATORI WHERE ID_CAF=" & Session.Item("ID_CAF") & " ORDER BY ID ASC", par.OracleConn)
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

        ds.Clear()
        ds.Dispose()
        ds = Nothing
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Function

    Protected Sub btnChiudi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudi.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisualizza.Click
        Dim operatori As String
        Dim INTERVALLO_DA As String
        Dim INTERVALLO_A As String
        Dim i As Integer
        Dim sPagina As String
        Dim operatore As String
        Dim POS As Integer
        Dim POS1 As Integer
        Dim OPERATORE_TR As String


        Try
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            INTERVALLO_DA = cmbAnnoDa.Text & cmbMDa.Text & cmbGDa.Text
            INTERVALLO_A = cmbAnnoa.Text & cmbMesea.Text & cmbGa.Text

            operatori = ""

            For i = 0 To CheckOperatori.Items.Count - 1
                If CheckOperatori.Items(i).Selected Then
                    operatori = operatori & CheckOperatori.Items(i).Value & ","
                End If
            Next
            If operatori = "" Then
                Response.Write("<script>alert('Selezionare almeno un operatore');</script>")
                Exit Sub
            Else
                operatori = Mid(operatori, 1, Len(operatori) - 1)
            End If
            If Val(INTERVALLO_A) < Val(INTERVALLO_DA) Then
                Response.Write("<script>alert('Intervallo Date non valido!');</script>")
                Exit Sub
            End If


            If cmbTipo.SelectedValue = "0" Then
                par.cmd.CommandText = "select DOMANDE_BANDO.ID,DOMANDE_BANDO.PG,TO_CHAR(TO_DATE(DOMANDE_BANDO.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG1"",DOMANDE_BANDO.isbarc_r,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,DOMANDE_BANDO.N_DISTINTA from DOMANDE_BANDO,DICHIARAZIONI,COMP_NUCLEO where DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND DICHIARAZIONI.ID_CAF=" & Session.Item("ID_CAF") & " AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID and DOMANDE_BANDO.N_DISTINTA=0 AND (DOMANDE_BANDO.ID_STATO='4' OR DOMANDE_BANDO.ID_STATO='7a') AND DICHIARAZIONI.ID_STATO<>2 ORDER BY DOMANDE_BANDO.ID ASC"
            Else
                par.cmd.CommandText = "select DOMANDE_BANDO.ID,DOMANDE_BANDO.PG,TO_CHAR(TO_DATE(DOMANDE_BANDO.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG1"",DOMANDE_BANDO.isbarc_r,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,DOMANDE_BANDO.N_DISTINTA from DOMANDE_BANDO,DICHIARAZIONI,COMP_NUCLEO where DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND DICHIARAZIONI.ID_CAF=" & Session.Item("ID_CAF") & " AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID and DOMANDE_BANDO.N_DISTINTA<>0 AND DICHIARAZIONI.ID_STATO<>2 ORDER BY DOMANDE_BANDO.ID ASC"
            End If
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()


            sPagina = "<table border='0' cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf
            sPagina = sPagina & "<tr>" & vbCrLf
            sPagina = sPagina & "<td width='100%' colspan='6'>" & vbCrLf
            If cmbTipo.SelectedValue = "0" Then
                sPagina = sPagina & "<p align='center'><b><font face='Arial' size='3'>Elenco delle Domande in carico dal " & par.FormattaData(INTERVALLO_DA) & " al " & par.FormattaData(INTERVALLO_A) & "</font></b><p>&nbsp;</td>" & vbCrLf
            Else
                sPagina = sPagina & "<p align='center'><b><font face='Arial' size='3'>Elenco delle Domande scaricate dal " & par.FormattaData(INTERVALLO_DA) & " al " & par.FormattaData(INTERVALLO_A) & "</font></b><p>&nbsp;</td>" & vbCrLf
            End If
            sPagina = sPagina & "</tr>" & vbCrLf
            sPagina = sPagina & "<tr>" & vbCrLf
            sPagina = sPagina & "<td width='100%' colspan='6'><p>&nbsp;</td>" & vbCrLf
            sPagina = sPagina & "</tr>" & vbCrLf
            sPagina = sPagina & "<tr>" & vbCrLf
            sPagina = sPagina & "<td width='6%'><b><font face='Arial' color='#000080' size='2'> </font></b>" & vbCrLf
            sPagina = sPagina & "<p align='left'><b><font face='Arial' size='2'>PROTOCOLLO</font></b></td>" & vbCrLf
            sPagina = sPagina & "<td width='7%'>" & vbCrLf
            sPagina = sPagina & "<p align='left'><font size='2' face='Arial'><b>DATA</b></font></td>" & vbCrLf
            sPagina = sPagina & "<td width='17%'>" & vbCrLf
            sPagina = sPagina & "<p align='left'><b><font size='2' face='Arial'>NOMINATIVO</font></b></td>" & vbCrLf
            sPagina = sPagina & "<td width='6%'>" & vbCrLf
            sPagina = sPagina & "<p align='left'><b><font size='2' face='Arial'>ISBARC/R</font></b></td>" & vbCrLf
            sPagina = sPagina & "<td width='10%'>" & vbCrLf
            sPagina = sPagina & "<p align='left'><b><font size='2' face='Arial'>UTENTE</font></b></td>" & vbCrLf

            If cmbTipo.SelectedValue = "1" Then
                sPagina = sPagina & "<td width='10%'>" & vbCrLf
                sPagina = sPagina & "<p align='left'><b><font size='2' face='Arial'>DISTINTA</font></b></td>" & vbCrLf
            End If
            sPagina = sPagina & "<td width='66%'></td>" & vbCrLf
            sPagina = sPagina & "</tr>" & vbCrLf
            i = 0
            While myReader.Read
                par.cmd.CommandText = "SELECT OPERATORI.ID,OPERATORI.OPERATORE AS ""DESCRIZIONE"" FROM OPERATORI,EVENTI_BANDI " _
                                    & "WHERE EVENTI_BANDI.TIPO_OPERATORE='I' AND " _
                                    & "EVENTI_BANDI.COD_EVENTO='F133' AND " _
                                    & "EVENTI_BANDI.ID_DOMANDA=" & myReader("ID") _
                                    & " AND OPERATORI.ID=EVENTI_BANDI.ID_OPERATORE  ORDER BY EVENTI_BANDI.DATA_ORA DESC"
                'AND EVENTI_BANDI.ID_OPERATORE IN (" & operatori & ")
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    operatore = myReader1("ID")

                    POS = 1
                    While POS <= Len(operatori)
                        POS1 = InStr(POS, operatori, ",", CompareMethod.Text)
                        'If POS1 = 0 Then POS1 = Len(operatori) + 1
                        'OPERATORE_TR = Mid(operatori, POS, POS1 - 1)

                        If POS1 = 0 Then POS1 = Len(operatori) + 1
                        OPERATORE_TR = Mid(operatori, POS, POS1 - POS)

                        POS = POS1 + 1
                        If OPERATORE_TR = operatore Then

                            sPagina = sPagina & "<tr>" & vbCrLf
                            sPagina = sPagina & "<td width='21%'><p align='left'><font face='Arial' size='2'>" & myReader("PG") & "</font></td>" & vbCrLf
                            sPagina = sPagina & "<td width='12%'><p align='left'><font face='Arial' size='2'>" & myReader("DATA_PG1") & "</font></td>" & vbCrLf
                            sPagina = sPagina & "<td width='35%'><p align='left'><font face='Arial' size='2'>" & myReader("COGNOME") & " " & myReader("NOME") & "</font></td>" & vbCrLf
                            sPagina = sPagina & "<td width='15%'><p align='left'><font face='Arial' size='2'>" & par.Tronca(myReader("ISBARC_R")) & "</font></td>" & vbCrLf
                            sPagina = sPagina & "<td width='24%'><p align='left'><font face='Arial' size='2'>" & par.IfNull(myReader1("DESCRIZIONE"), "") & "</font></td>" & vbCrLf
                            If cmbTipo.SelectedValue = "1" Then
                                sPagina = sPagina & "<td width='10%'><p align='left'><font face='Arial' size='2'>" & par.IfNull(myReader("N_DISTINTA"), "") & "</font></td>" & vbCrLf
                            End If
                            sPagina = sPagina & "<td width='5%'><p align='left'><font face='Arial' size='2'></font></td>" & vbCrLf
                            sPagina = sPagina & "</tr>" & vbCrLf
                            Exit While
                        End If
                    End While

                End If
                myReader1.Close()
                i = i + 1
            End While

            sPagina = sPagina & "</table>" & vbCrLf

            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If i = 0 Then
                Response.Write("<script>alert('Non sono state trovate domande!');</script>")
                Exit Sub
            End If
            HttpContext.Current.Session.Add("CARICO", sPagina)
            Response.Write("<script>window.open('ListaCarico.aspx','Domande','');</script>")

            'Response.ClearContent()


            'Response.Write("<html xmlns='http://www.w3.org/1999/xhtml'>")
            'Response.Write("<head runat='server'>")
            'Response.Write("<title>Lista Domande</title>")
            'Response.Write("</head>")
            'Response.Write("<body bgcolor='#ffffcc'>")
            'Response.Write("<form id='form1' runat='server'>")
            'Response.Write("<div>")
            'Response.Write(sPagina)
            'Response.Write("</div>")
            'Response.Write("</form>")
            'Response.Write("</body>")
            'Response.Write("</html>")
        Catch EX As Exception
            Label4.Text = "ERRORE: " + EX.Message
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub cmdTutti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTutti.Click
        Dim I As Integer
        For I = 0 To CheckOperatori.Items.Count - 1
            CheckOperatori.Items(I).Selected = True
        Next
    End Sub

    Protected Sub btnDeseleziona_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeseleziona.Click
        Dim I As Integer
        For I = 0 To CheckOperatori.Items.Count - 1
            CheckOperatori.Items(I).Selected = False
        Next
    End Sub
End Class
