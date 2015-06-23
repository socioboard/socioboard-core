function install_dialog([System.String]$title, [System.String]$msg){
	[void] [System.Reflection.Assembly]::LoadWithPartialName("System.Drawing") 
	[void] [System.Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms") 

	$objForm = New-Object System.Windows.Forms.Form 
	$objForm.Text = $title
	$objForm.Size = New-Object System.Drawing.Size(300,200) 
	$objForm.StartPosition = "CenterScreen"
	$objForm.FormBorderStyle = "FixedDialog"

	$objForm.KeyPreview = $True
	$objForm.Add_KeyDown({if ($_.KeyCode -eq "Enter") 
		{$script:x=$objTextBox.Text;$objForm.Close()}})
	$objForm.Add_KeyDown({if ($_.KeyCode -eq "Escape") 
		{$script:x=$null;$objForm.Close()}})

	$OKButton = New-Object System.Windows.Forms.Button
	$OKButton.Location = New-Object System.Drawing.Size(75,120)
	$OKButton.Size = New-Object System.Drawing.Size(75,23)
	$OKButton.Text = "OK"
	$OKButton.Add_Click({$script:x=$objTextBox.Text;$objForm.Close()})
	$objForm.Controls.Add($OKButton)

	$CancelButton = New-Object System.Windows.Forms.Button
	$CancelButton.Location = New-Object System.Drawing.Size(150,120)
	$CancelButton.Size = New-Object System.Drawing.Size(75,23)
	$CancelButton.Text = "Cancel"
	$CancelButton.Add_Click({$script:x=$null;$objForm.Close()})
	$objForm.Controls.Add($CancelButton)

	$objLabel = New-Object System.Windows.Forms.Label
	$objLabel.Location = New-Object System.Drawing.Size(10,20) 
	$objLabel.Size = New-Object System.Drawing.Size(280,60) 
	$objLabel.Text = $msg
	$objForm.Controls.Add($objLabel) 

	$objTextBox = New-Object System.Windows.Forms.TextBox 
	$objTextBox.Location = New-Object System.Drawing.Size(10,80) 
	$objTextBox.Size = New-Object System.Drawing.Size(260,20) 
	$objForm.Controls.Add($objTextBox) 

	$objForm.Topmost = $True

	$objForm.Add_Shown({$objForm.Activate()})
	[void] $objForm.ShowDialog()
	return $x
	
	}


	
function get_activation_key([System.__ComObject] $project){
    $actKey = install_dialog "Activation Key" "Please enter your Stackify Activation Key, found in your account settings."
    replace_activation_key $project $actKey
}


function replace_activation_key([System.__ComObject] $project, [System.String]$actKey){
    $webConfigItem = $project.ProjectItems | Where-Object{$_.Name -eq 'Web.config'}
    $appConfigItem = $project.ProjectItems | Where-Object{$_.Name -eq 'App.config'}
    if($webConfigItem -ne $null)
    {
        $webConfigFile = $project.ProjectItems.Item("web.config")
        $path = $webConfigFile.Properties.Item("FullPath").Value

        [xml] $xml = gc $path



        $key = $xml.SelectSingleNode('//appSettings/add[@key="Stackify.ApiKey"]/@value').'#text' 
        if($key -ne $null)
        {
            $xml.SelectSingleNode('//appSettings/add[@key="Stackify.ApiKey"]/@value').'#text' = $actKey
        }
        else
        {
            $setting = $xml.configuration.appSettings
            if($setting -ne $null)
            {
        
                $key = $xml.CreateElement("add")
                $key.SetAttribute("key","Stackify.ApiKey")
                $key.SetAttribute("value",$actKey)
                $xml.configuration.appSettings.AppendChild($key)
            }
            else
            {
                $setting = $xml.CreateElement("appSettings")
                $xml.configuration.AppendChild($setting)
                $key = $xml.CreateElement("add")
                $key.SetAttribute("key","Stackify.ApiKey")
                $key.SetAttribute("value",$actKey)
                $xml.configuration.LastChild.AppendChild($key)
            }
    
        }

        $xml.Save($path)

    }
    if($appConfigItem -ne $null)
    {
        $appConfigFile = $project.ProjectItems.Item("app.config")
        $path = $appConfigFile.Properties.Item("FullPath").Value

        [xml] $xml = gc $path

        

        $key = $xml.SelectSingleNode('//appSettings/add[@key="Stackify.ApiKey"]/@value').'#text' 
        if($key -ne $null)
        {
            $xml.SelectSingleNode('//appSettings/add[@key="Stackify.ApiKey"]/@value').'#text' = $actKey
        }
        else
        {
            $setting = $xml.configuration.appSettings
            if($setting -ne $null)
            {
        
                $key = $xml.CreateElement("add")
                $key.SetAttribute("key","Stackify.ApiKey")
                $key.SetAttribute("value",$actKey)
                $xml.configuration.appSettings.AppendChild($key)
            }
            else
            {
                $setting = $xml.CreateElement("appSettings")
                $xml.configuration.AppendChild($setting)
                $key = $xml.CreateElement("add")
                $key.SetAttribute("key","Stackify.ApiKey")
                $key.SetAttribute("value",$actKey)
                $xml.configuration.LastChild.AppendChild($key)
            }
    
        }

        $xml.Save($path)

    }
    
}