Azure Files and File Sync 
Files vs Blobs 
File storage5 offers shared storage for applications using the industry standard SMB protocol6. Microsoft Azure virtual machines and cloud services can share file data across application components via mounted shares, and on-premises applications can also access file data in the share. 
Applications running in Azure virtual machines or cloud services can mount a file storage share to access file data, just as a desktop application would mount a typical SMB share. Any number of Azure virtual machines or roles can mount and access the File storage share simultaneously. 
Common uses of file storage 
    1 Replace and supplement. Azure Files can be used to completely replace or supplement traditional on-premises file servers or NAS devices. 
    2 Access anywhere. Popular operating systems such as Windows, macOS, and Linux can directly mount Azure File shares wherever they are in the world. 
    3 Lift and shift. Azure Files makes it easy to “lift and shift” applications to the cloud that expect a file share to store file application or user data. 
    4 Azure File Sync. Azure File shares can also be replicated with Azure File Sync to Windows Servers, either on-premises or in the cloud, for performance and distributed caching of the data where it's being used. 
    5 Shared applications. Storing shared application settings, for example in configuration files. 
    6 Diagnostic data. Storing diagnostic data such as logs, metrics, and crash dumps in a shared location. 
    7 Tools and utilities. Storing tools and utilities needed for developing or administering Azure virtual machines or cloud services. 
    8 
    9 Comparing Files and Blobs 
Sometimes it is difficult to decide when to use file shares instead of blobs or disk shares. Take a minute to review this table that compares the different features.







Feature 
Description 
When to use 
Azure Files 
Provides an SMB interface, client libraries, and a REST interface that allows access from any­where to stored files. 
You want to “lift and shift” an application to the cloud which already uses the native file system APIs to share data between it and other applica­tions running in Azure. You want to store development and debugging tools that need to be accessed from many virtual 
Feature 
Description 
When to use 
Azure Blobs 
Provides client libraries and a REST interface that allows unstructured data to be stored and accessed at a massive scale in block blobs. 
You want your application to support streaming and ran­dom-access scenarios.You want to be able to access application data from anywhere. 














aws ecr get-login-password --region sa-east-1 | docker login --username AWS --password-stdin 123721783999.dkr.ecr.sa-east-1.amazonaws.com
docker build --file Dockerfile --pull -t localhost:tst-nr-667 --no-cache .




$vm = Get-AzVM -ResourceGroupName westus `
    -Name vhd1-image-20200829162832

$image = New-AzImageConfig -SourceVirtualMachineId `
    $vm.ID -Location westus

New-AzImage -Image $image `
    -ImageName vhd1-image-20200829162832 `
    -ResourceGroupName westus










New-AzVm `
    -ResourceGroupName westus `
    -Name fromvhd `
    -ImageName vhd1-image-20200829162832`
    -Location westus `