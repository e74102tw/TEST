param($installPath, $toolsPath, $package, $project)

#function MarkDirectoryAsCopyToOutputRecursive($item)
#{
#    $item.ProjectItems | ForEach-Object { MarkFileASCopyToOutputDirectory($_) }
#}

#function MarkFileASCopyToOutputDirectory($item)
#{
#    Try
#    {
#        Write-Host Try set $item.Name
#        $item.Properties.Item("CopyToOutputDirectory").Value = 2
#    }
#    Catch
#    {
#        Write-Host RecurseOn $item.Name
#        MarkDirectoryAsCopyToOutputRecursive($item)
#    }
#}

#Now mark everything in the a directory as "Copy to newer"
#MarkDirectoryAsCopyToOutputRecursive($project.ProjectItems.Item("content"))
$project.ProjectItems.Item("content").ProjectItems.Item("Microsoft.VC90.DebugCRT").ProjectItems.Item("Microsoft.VC90.DebugCRT.manifest").Properties.Item("CopyToOutputDirectory").Value = 2
$project.ProjectItems.Item("content").ProjectItems.Item("Microsoft.VC90.DebugCRT").ProjectItems.Item("msvcm90d.dll").Properties.Item("CopyToOutputDirectory").Value = 2
$project.ProjectItems.Item("content").ProjectItems.Item("Microsoft.VC90.DebugCRT").ProjectItems.Item("msvcp90d.dll").Properties.Item("CopyToOutputDirectory").Value = 2
$project.ProjectItems.Item("content").ProjectItems.Item("Microsoft.VC90.DebugCRT").ProjectItems.Item("msvcr90d.dll").Properties.Item("CopyToOutputDirectory").Value = 2