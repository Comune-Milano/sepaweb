
Partial Class CICLO_PASSIVO_CicloPassivo_Plan_ConvalidaAlerSceltaF
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Property Tabella() As String
        Get
            If Not (ViewState("par_Tabella") Is Nothing) Then
                Return CStr(ViewState("par_Tabella"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Tabella") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0

        If Not IsPostBack Then
            idPianoF.Value = Request.QueryString("IDP")
            idvoce.Value = Request.QueryString("IDV")
            per.Value = ""
            CaricaStato()
            CaricaTabella()

        End If
    End Sub

    Private Function CaricaTabella()
        Try
            Dim TestoPagina As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            TestoPagina = TestoPagina & "<table style='width: 95%;' cellpadding=0 cellspacing = 0'>"

            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'><td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>STRUTTURA</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>RICHIESTO</td></tr>"

            par.cmd.CommandText = "select tab_filiali.id,tab_filiali.nome from siscom_mi.pf_voci,siscom_mi.pf_voci_struttura,siscom_mi.tab_filiali where pf_voci_struttura.id_voce=pf_voci.id and tab_filiali.id=pf_voci_struttura.id_struttura and pf_voci.id=" & idvoce.Value & " order by tab_filiali.nome asc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                par.cmd.CommandText = "select sum(valore_lordo) from siscom_mi.pf_voci_struttura where pf_voci_struttura.id_voce=" & idvoce.Value & " and id_struttura=" & myReader("id")
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    If par.IfNull(myReader1(0), 0) <> 0 Then
                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'><td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.showModalDialog('ConvalidaFilialeAler.aspx?IDV1=" & idvoce.Value & "&IDP=" & idPianoF.Value & "&IDF=" & par.IfNull(myReader("id"), "-1") & "',window,'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');" & Chr(34) & ">" & par.IfNull(myReader("nome"), "") & "</a></td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.showModalDialog('ConvalidaFilialeAler.aspx?IDP=" & idPianoF.Value & "&IDV=" & idvoce.Value & "&IDF=" & par.IfNull(myReader("id"), "-1") & "',window,'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');" & Chr(34) & ">" & Format(par.IfNull(myReader1(0), 0), "##,##0.00") & "</a></td></tr>"
                    Else
                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'><td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & par.IfNull(myReader("nome"), "") & "</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(par.IfNull(myReader1(0), 0), "##,##0.00") & "</td></tr>"
                    End If
                End If
                myReader1.Close()
            Loop
            myReader.Close()
            TestoPagina = TestoPagina & "</table>"
            Tabella = TestoPagina
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
        


    End Function


    Function CaricaStato()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID=" & idPianoF.Value & " and PF_STATI.ID=PF_MAIN.ID_STATO and t_esercizio_finanziario.id=pf_main.id_esercizio_finanziario"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Label1.Text = myReader5("inizio") & "-" & myReader5("fine")
                per.Value = Label1.Text
                lblStato.Text = "STATO:" & par.IfNull(myReader5("stato"), "")
                stato.Value = par.IfNull(myReader5("id_stato"), "")
            End If
            myReader5.Close()

            par.cmd.CommandText = "select pf_voci.descrizione,pf_voci.codice FROM SISCOM_MI.PF_VOCI WHERE ID=" & idvoce.Value
            myReader5 = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblVoce.Text = myReader5("CODICE") & "-" & myReader5("DESCRIZIONE")
            End If
            myReader5.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function
End Class
