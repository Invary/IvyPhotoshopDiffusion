# IvyPhotoshopDiffusion for Windows


## Attention
Recently, Automatic1111 changes its API specifications every few days. <br />
As a result, there are times when an error occurs and the IvyPhotoshopDiffusion does not work. <br />
In this case, please use the version of Automatic1111 that is a few days old. <br />

Ver105 works on latest version of Automatic1111 (2022/11/02 11:00 UTC)<br />

<br />


##  About
IvyPhotoshopDiffusion is a tool for generating AI image on Photoshop for Windows.
The AI generating image process is done by [Stable Diffusion](https://github.com/CompVis/stable-diffusion) and [Automatic1111 Stable Diffusion web UI](https://github.com/AUTOMATIC1111/stable-diffusion-webui).

https://user-images.githubusercontent.com/99020933/198564805-4e96151b-df74-4a61-995b-9ffef95fa28c.mp4


<br />

## ⚡ Featurees

* Infinite outpainting on Photoshop
* Inpainting on Photoshop
* Inpainting/outpainting with mask


<br />

## Download

latest version of [IvyPhotoshopDiffusion](https://github.com/Invary/IvyPhotoshopDiffusion/releases)


<br />

## ️ Requirements

* Graphics board, NVIDIA GeForce RTX or higher is recomended.
* Windows 7 or later
* Adobe Photoshop
* [Automatic1111 stable-diffusion-webui](https://github.com/AUTOMATIC1111/stable-diffusion-webui)
* .NET Framework 4.8 runtime library

<br />

##  Install

1. [Download](https://github.com/AUTOMATIC1111/stable-diffusion-webui) latest version of **`Automatic1111 Stable Diffusion web UI (Automatic1111)`**
2. Install **`Automatic1111`** to the installation folder.
3. Check generate images on **`Automatic1111`**
4. Set command line arguments **`--api`** for **`Automatic1111`** <br />
     Edit **`webui-user.bat`**, **`set COMMANDLINE_ARGS=`** to **`set COMMANDLINE_ARGS=--api`**
###
5. [Download](https://github.com/Invary/IvyPhotoshopDiffusion/releases) latest version of IvyPhotoshopDiffusion
6. Extract it to installation folder
###
7. Start **`Automatic1111`**
8. Start **`Adobe Photoshop`**
9. Start **`IvyPhotoshopDiffusion.exe`** in the installation folder.

<br />

##  Documents

see [Documents](https://github.com/Invary/IvyPhotoshopDiffusion/tree/main/doc) page.

<br />


##  Privacy policy

- Do not send privacy information.
- Do not display online ads.
- Do not collect telemetry information.
- If online information is needed, get it from github repositories whenever possible.

*This policy only applies to IvyPhotoshopDiffusion, and not to other libraries or software such as **`Adobe Photoshop`** or **`Automatic1111`**

<br />

##  Changelog

- Ver105 <br />
Support restore faces, tiling, ENSD, clip skip<br />
Set/get photoshop forground/background color <br />
Bug fix json request/response <br />
Ver105 work for Automatic1111 2022/11/01 03:00 UTC version<br />
https://github.com/AUTOMATIC1111/stable-diffusion-webui/tree/5c9b3625fa03f18649e1843b5e9f2df2d4de94f9

- Ver104 <br />
Support latest version of Automatic1111's API <br />
Ver104 work for Automatic1111 2022/10/31 06:00 UTC version<br />
https://github.com/AUTOMATIC1111/stable-diffusion-webui/tree/17a2076f72562b428052ee3fc8c43d19c03ecd1e


- Ver103 <br />
Save last prompt/negative/layername setting for default value <br />
Add setting layer name function <br />
Bug fix image processing exception <br />
Ver103 work for Automatic1111 2022/10/28 version<br />
https://github.com/AUTOMATIC1111/stable-diffusion-webui/tree/9ceef81f77ecce89f0c8f412c4d849210d852e82


- Ver102 <br />
Support text2image. Use generate button with pressing [shift] key <br />
Change log output of generated images to InfoText only <br />


- Ver101 <br />
Bug fix log message out <br />
Bug fix seed input <br />
Bug fix json deserialize <br />


- Ver100 <br />
Initial release

<br />

##  Suggestions and feedback
If you have any idea or suggestion, please add a github issue.

<br />

## ⭐ Price

FREE of charge. <br /> 
If you would like to donate, please send me a ko-fi or crypto. It's a great encouragement!

[![ko-fi](https://raw.githubusercontent.com/Invary/IvyMediaDownloader/main/img/donation_kofi.png)](https://ko-fi.com/E1E7AC6QH)

- Address: 0xCbd4355d13CEA25D87F324E9f35A075adce6507c<br>
 -- Binance Smart Chain (BNB, USDT, BUSD etc.)<br>
 -- Polygon network (MATIC, USDC etc.)<br>
 -- Ethereum network (ETH)<br>

- Address: 1FvzxYriyNDdeA12eaUGXTGSJxkzpQdxPd<br>
 -- Bitcoin (BTC)<br>

<br />



##  Thanks
IvyPhotoshopDiffusion uses a some of tools and libraries. Thank you for these projects!

- [Stable Diffusion](https://github.com/CompVis/stable-diffusion)
- [Automatic1111 Stable Diffusion web UI](https://github.com/AUTOMATIC1111/stable-diffusion-webui)


<br />
<br />
<br />

#### Copyright (c) Invary




