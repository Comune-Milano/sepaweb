
Partial Class Contratti_CanoneArt15
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim cognome As String = ""
    Dim nome As String = ""
    Dim cf As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            Indice = Request.QueryString("IDC")
            Unita = Request.QueryString("IDU")
            CODICEUnita = Request.QueryString("CODICE")

        End If
        txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Public Property Unita() As String
        Get
            If Not (ViewState("par_Unita") Is Nothing) Then
                Return CStr(ViewState("par_Unita"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Unita") = value
        End Set

    End Property

    Public Property Indice() As String
        Get
            If Not (ViewState("par_Indice") Is Nothing) Then
                Return CStr(ViewState("par_Indice"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Indice") = value
        End Set

    End Property

    Public Property CODICEUnita() As String
        Get
            If Not (ViewState("par_CODICEUnita") Is Nothing) Then
                Return CStr(ViewState("par_CODICEUnita"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_CODICEUnita") = value
        End Set

    End Property

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try
            If txtpg.Text <> "" And txtCanoneCorrente.Text <> "" And txtData.Text <> "" And par.AggiustaData(txtData.Text) <= Format(Now, "yyyyMMdd") Then

                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "select COMP_NUCLEO.* from COMP_NUCLEO,domande_bando where COMP_NUCLEO.PROGR=0 AND COMP_NUCLEO.id_DICHIARAZIONE=domande_bando.id_dichiarazione and domande_bando.id=" & Indice
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()


                If myReader.Read = True Then

                    cognome = par.IfNull(myReader("cognome"), "")
                    nome = par.IfNull(myReader("nome"), "")


                    cf = par.IfNull(myReader("cod_fiscale"), "")


                    par.cmd.CommandText = "Insert into siscom_mi.UNITA_ASSEGNATE (ID_DOMANDA, ID_UNITA, DATA_ASSEGNAZIONE, " _
                                        & "GENERATO_CONTRATTO, ID_DICHIARAZIONE, COGNOME_RS, NOME, CF_PIVA, PROVENIENZA, N_OFFERTA,CANONE,ID_ANAGRAFICA,PROVVEDIMENTO,DATA_PROVVEDIMENTO) " _
                                        & " Values " _
                                        & "(" & Indice & ", " & Unita & ", '" & par.AggiustaData(txtData.Text) & "', 0," & myReader("id_dichiarazione") & ", " _
                                        & "'" & par.PulisciStrSql(cognome) & "', '" & par.PulisciStrSql(nome) _
                                        & "', '" & par.PulisciStrSql(cf) & "', 'D', 0," & par.VirgoleInPunti(txtCanoneCorrente.Text) & "," & Indice & ",'" & par.PulisciStrSql(txtpg.Text) & "','" & par.AggiustaData(txtData.Text) & "')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "update alloggi set stato='8',assegnato='1',DATA_PRENOTATO='" & par.AggiustaData(txtData.Text) & "' where COD_ALLOGGIO='" & CODICEUnita & "'"
                    par.cmd.ExecuteNonQuery()


                    'par.cmd.CommandText = "select COMP_NUCLEO.* from COMP_NUCLEO,domande_bando where COMP_NUCLEO.id_DICHIARAZIONE=domande_bando.id_dichiarazione and domande_bando.id=" & Indice & " order by progr asc"
                    'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'Do While Not myReader1.Read
                    '    par.cmd.CommandText
                    'Loop
                    'myReader1.Close()

                    'Response.Write("<script>alert('Operazione Effettuata. Ora è possibile inserire il contratto!');</script>")
                    'Response.Write("<script>window.close();</script>")
                    Dim SriptJSContratto As String = "var chiediConferma;" _
                                              & "chiediConferma = window.confirm('Operazione Effettuata!\nSi vuole inserire immediatamente il nuovo contratto?');" _
                                              & "if (chiediConferma == true) { " _
                                              & "a = window.open('Contratto.aspx?ID=-1&IdDichiarazione=" & Indice & "&IdDomanda=" & Indice & "&IdUnita=" & Unita & "&TIPO=6&Lett=D','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" _
                                              & "window.close();}" _
                                              & "else {" _
                                              & "window.close();" _
                                              & "}"
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "conf", SriptJSContratto, True)
                End If
                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                Response.Write("<script>alert('Dati mancanti o errati! Si ricorda che la data di assegnazione deve essere inferiore la data odierna!');</script>")
            End If
        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label4.Visible = True
            Label4.Text = ex.Message
        End Try
    End Sub
End Class
