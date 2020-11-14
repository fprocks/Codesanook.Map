using System.Linq;
using System.Threading.Tasks;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using Codesanook.Map.Models;
using Codesanook.Map.ViewModels;

namespace Codesanook.Map.Drivers
{
    public class MapPartDisplayDriver : ContentPartDisplayDriver<MapPart>
    {

        public override IDisplayResult Display(MapPart MapPart)
        {
            return Combine(
                Initialize<MapPartViewModel>(
                    "MapPart",
                    m => BuildViewModel(m, MapPart)
                ).Location("Detail", "Content:20"),

                Initialize<MapPartViewModel>(
                    "MapPart_Summary",
                    m => BuildViewModel(m, MapPart)
                ).Location("Summary", "Meta:5")
            );
        }

        public override IDisplayResult Edit(MapPart MapPart)
        {
            return Initialize<MapPartViewModel>(
                "MapPart_Edit",
                m => BuildViewModel(m, MapPart)
            );
        }

        public override async Task<IDisplayResult> UpdateAsync(MapPart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(
                model,
                Prefix,
                t => t.Latitude, t => t.Longitude
            );

            return Edit(model);
        }

        private void BuildViewModel(MapPartViewModel model, MapPart part)
        {
            model.Latitude = part.Latitude;
            model.Longitude = part.Longitude;
            model.MapPart = part;
            model.ContentItem = part.ContentItem;
        }
    }
}
