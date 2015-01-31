//
//  IOSInstaPlugin.h
//  Unity-iPhone
//
//  Created by Osipov Stanislav on 3/8/14.
//
//

#import <Foundation/Foundation.h>
#include "iPhone_View.h"
#include "MGInstagram.h"
#import "SPDataUtility.h"

@interface IOSInstaPlugin : NSObject<UIDocumentInteractionControllerDelegate>


+ (id) sharedInstance;

- (void) share:(NSString*)status media: (NSString*) media;


@end
