
Partial Class ANAUT_ElencoSportelli
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Property ID_AU() As String
        Get
            If Not (ViewState("par_ID_AU") Is Nothing) Then
                Return CStr(ViewState("par_ID_AU"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_ID_AU") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT MAX(ID) FROM UTENZA_BANDI"
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                ID_AU = myReader2(0)
            End If
            myReader2.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            If Session.Item("MOD_AU_CONV_VIS_TUTTO") = "1" Then
                par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "SELECT * FROM siscom_mi.tab_filiali where id=9 or id=6 or id=1 or id=2 or id=3 or id=22 or id=4 or id=5 or id=8 order by NOME asc", "NOME", "ID")
                cmbFiliale.Items.Add("TUTTE LE SEDI")
                cmbFiliale.Items.FindByText("TUTTE LE SEDI").Selected = True
            Else
                par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "SELECT * FROM siscom_mi.tab_filiali where id in (select id_UFFICIO from operatori where id=" & Session.Item("ID_OPERATORE") & ") order by NOME asc", "NOME", "ID")
                cmbFiliale.Enabled = False
            End If
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            CaricaLista()
        End If
    End Sub

    Private Function CaricaLista()

        Try

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim INIZIO_F_M As String = ""
            Dim FINE_F_M As String = ""

            Dim INIZIO_F_P As String = ""
            Dim FINE_F_P As String = ""

            Dim MIOCOLORE As String = ""
            Dim MiaSHTML As String = ""

            Dim i As Integer

            Dim orarioAgg As String = Format(Now, "yyyyMMddHHmm")

            Label4.Text = "Aggiornato al " & par.FormattaData(Mid(orarioAgg, 1, 8)) & " " & Mid(orarioAgg, 9, 2) & "." & Mid(orarioAgg, 11, 2)
            Dim TotRec As Long = 0
            Dim TotLibe As Long = 0
            Dim TotOcc As Long = 0
            Dim TotLibeFO As Long = 0


            Dim Struttura As String = ""
            If cmbFiliale.SelectedItem.Text = "TUTTE LE SEDI" Then
                Struttura = ""
            Else
                Struttura = " ID_FILIALE=" & cmbFiliale.SelectedItem.Value & " AND "
            End If

            MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='100%'>" & vbCrLf
            MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='20%' bgcolor='#507cd1'><font face='Arial' size='1' color='White'>SEDE T.</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='25%' bgcolor='#507cd1'><font face='Arial' size='1' color='White'>OPERATORE/SPORTELLO</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='15%' bgcolor='#507cd1'><font size='1' face='Arial' color='White'>SLOT LIBERI</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='15%' bgcolor='#507cd1'><font size='1' face='Arial' color='White'>SLOT LIBERI FUORI ORARIO*</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='15%' bgcolor='#507cd1'><font size='1' face='Arial' color='White'>SLOT OCCUPATI</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='15%' bgcolor='#507cd1'><font size='1' face='Arial' color='White'>TOTALE</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='10%' bgcolor='#507cd1'><font size='1' face='Arial' color='White'>% OCC.</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

            i = 0
            MIOCOLORE = "#dcdcdc"

            par.cmd.CommandText = "SELECT DISTINCT ID_FILIALE,TAB_FILIALI.NOME FROM SISCOM_MI.AGENDA_APPUNTAMENTI,SISCOM_MI.TAB_FILIALI WHERE " & Struttura & " ID_AU=" & ID_AU & " AND TAB_FILIALI.ID=AGENDA_APPUNTAMENTI.ID_FILIALE ORDER BY TAB_FILIALI.NOME ASC"
            Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader4.Read


                par.cmd.CommandText = "SELECT DISTINCT N_OPERATORE FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID_FILIALE=" & myReader4(0) & " AND ID_AU=" & ID_AU & " ORDER BY N_OPERATORE ASC"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader2.Read

                    MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='20%' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'>" & myReader4(1) & "</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='25%' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('CalendarioSportello.aspx?A=" & ID_AU & "&F=" & myReader4(0) & "&S=" & myReader2(0) & "','Calendario" & Format(Now, "hhss") & "','');" & Chr(34) & " >SPORTELLO " & myReader2(0) & "</font></a></td>" & vbCrLf

                    par.cmd.CommandText = "select count(id) FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE (TIPO_F_ORARIA=0 or TIPO_F_ORARIA=4) and  inizio>'" & orarioAgg & "' and inizio<='20120831" & FINE_F_P & "' and ID_FILIALE=" & myReader4(0) & " AND ID_AU=" & ID_AU & " and n_operatore=" & myReader2(0)
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader3.Read Then
                        TotRec = myReader3(0)
                    End If
                    myReader3.Close()

                    par.cmd.CommandText = "select count(id) FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE (TIPO_F_ORARIA=0) and  inizio>'" & orarioAgg & "' and id_stato=0 and inizio<='20120831" & FINE_F_P & "' and ID_FILIALE=" & myReader4(0) & " AND ID_AU=" & ID_AU & " and n_operatore=" & myReader2(0)
                    myReader3 = par.cmd.ExecuteReader()
                    If myReader3.Read Then
                        TotLibe = myReader3(0)
                    End If
                    myReader3.Close()

                    par.cmd.CommandText = "select count(id) FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE (TIPO_F_ORARIA=4) and  inizio>'" & orarioAgg & "' and id_stato=0 and inizio<='20120831" & FINE_F_P & "' and ID_FILIALE=" & myReader4(0) & " AND ID_AU=" & ID_AU & " and n_operatore=" & myReader2(0)
                    myReader3 = par.cmd.ExecuteReader()
                    If myReader3.Read Then
                        TotLibeFO = myReader3(0)
                    End If
                    myReader3.Close()


                    par.cmd.CommandText = "select count(id) FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE (TIPO_F_ORARIA=0 or TIPO_F_ORARIA=4) and inizio>'" & orarioAgg & "' and id_stato=1 and inizio<='20120831" & FINE_F_P & "' and ID_FILIALE=" & myReader4(0) & " AND ID_AU=" & ID_AU & " and n_operatore=" & myReader2(0)
                    myReader3 = par.cmd.ExecuteReader()
                    If myReader3.Read Then
                        TotOcc = myReader3(0)
                    End If
                    myReader3.Close()


                    MiaSHTML = MiaSHTML & "<td width='15%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & TotLibe & "</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='15%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & TotLibeFO & "</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='15%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & TotOcc & "</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='15%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & TotRec & "</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='10%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & Format((TotOcc * 100) / TotRec, "00") & "</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "</tr>" & vbCrLf



                Loop
                myReader2.Close()
                If MIOCOLORE = "#dcdcdc" Then
                    MIOCOLORE = "#ffffc0"
                Else
                    MIOCOLORE = "#dcdcdc"
                End If
            Loop
            myReader4.Close()


            MiaSHTML = MiaSHTML & "</table>" & vbCrLf
            Label3.Text = MiaSHTML

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
        
    End Function


    Private Function RicavaFiliale(ByVal INDICE As String) As String
        Try
            RicavaFiliale = ""

            'par.OracleConn.Open()
            'par.SettaCommand(par)

            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID=" & INDICE

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                RicavaFiliale = myReader2("NOME")
            End If
            myReader2.Close()

            'par.cmd.Dispose()
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Function


    Protected Sub cmbFiliale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiliale.SelectedIndexChanged
        CaricaLista()
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
